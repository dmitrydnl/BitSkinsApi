/*
 * BitSkinsApi
 * Copyright (C) 2019 dmitrydnl
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using Newtonsoft.Json;

namespace BitSkinsApi.Balance
{
    /// <summary>
    /// Work with current account balance.
    /// </summary>
    public static class CurrentBalance
    {
        /// <summary>
        /// Allows you to retrieve your available balance.
        /// </summary>
        /// <returns>Account balance.</returns>
        public static AccountBalance GetAccountBalance()
        {
            string urlRequest = GetUrlRequest();
            string result = Server.ServerRequest.RequestServer(urlRequest);
            AccountBalance accountBalance = ReadAccountBalance(result);
            return accountBalance;
        }

        private static string GetUrlRequest()
        {
            Server.UrlCreator urlCreator = new Server.UrlCreator($"https://bitskins.com/api/v1/get_account_balance/");
            return urlCreator.ReadUrl();
        }

        private static AccountBalance ReadAccountBalance(string result)
        {
            dynamic responseServerD = JsonConvert.DeserializeObject(result);
            dynamic dataD = responseServerD.data;

            AccountBalance accountBalance = null;
            if (dataD != null)
            {
                double availableBalance = dataD.available_balance;
                double pendingWithdrawals = dataD.pending_withdrawals;
                double withdrawableBalance = dataD.withdrawable_balance;
                double couponableBalance = dataD.couponable_balance;

                accountBalance = new AccountBalance(availableBalance, pendingWithdrawals, withdrawableBalance, couponableBalance);
            }

            return accountBalance;
        }
    }

    /// <summary>
    /// Account's balance.
    /// </summary>
    public class AccountBalance
    {
        public double AvailableBalance { get; private set; }
        public double PendingWithdrawals { get; private set; }
        public double WithdrawableBalance { get; private set; }
        public double CouponableBalance { get; private set; }

        internal AccountBalance(double availableBalance, double pendingWithdrawals, double withdrawableBalance, double couponableBalance)
        {
            AvailableBalance = availableBalance;
            PendingWithdrawals = pendingWithdrawals;
            WithdrawableBalance = withdrawableBalance;
            CouponableBalance = couponableBalance;
        }
    }
}
