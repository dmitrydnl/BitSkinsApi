/*
 * BitSkinsApi
 * Copyright (C) 2019 Dmitry
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
    /// Work with withdrawal money.
    /// </summary>
    public static class MoneyWithdraw
    {
        /// <summary>
        /// All BitSkins money withdrawal methods.
        /// </summary>
        public enum WithdrawalMoneyMethod { PayPal, Bitcoin, Ethereum, Litecoin, Skrill, BankWire };

        /// <summary>
        /// Allows you to request withdrawal of available balance on your BitSkins account. 
        /// All withdrawals are finalized 15 days after this request on a rolling basis.
        /// </summary>
        /// <param name="amount">Amount in USD to withdraw. Must be at most equal to available balance, and over $5.00 USD.</param>
        /// <param name="withdrawalMoneyMethod">Can be bitcoin, paypal, and so on.</param>
        /// <returns>Whether the withdrawal was successful.</returns>
        public static bool WithdrawMoney(double amount, WithdrawalMoneyMethod withdrawalMoneyMethod)
        {
            string urlRequest = GetUrlRequest(amount, withdrawalMoneyMethod);
            string result = Server.ServerRequest.RequestServer(urlRequest);
            bool success = ReadStatus(result) == "success";
            return success;
        }

        private static string GetUrlRequest(double amount, WithdrawalMoneyMethod withdrawalMoneyMethod)
        {
            string method = WithdrawalMoneyMethodToString(withdrawalMoneyMethod);

            Server.UrlCreator urlCreator = new Server.UrlCreator($"https://bitskins.com/api/v1/request_withdrawal/");
            urlCreator.AppendUrl($"&amount={amount}");
            urlCreator.AppendUrl($"&withdrawal_method={method}");

            return urlCreator.ReadUrl();
        }

        private static string ReadStatus(string result)
        {
            dynamic responseServer = JsonConvert.DeserializeObject(result);
            string status = responseServer.status;
            return status;
        }

        private static string WithdrawalMoneyMethodToString(WithdrawalMoneyMethod withdrawalMoneyMethod)
        {
            switch (withdrawalMoneyMethod)
            {
                case WithdrawalMoneyMethod.PayPal:
                    return "paypal";
                case WithdrawalMoneyMethod.Bitcoin:
                    return "bitcoin";
                case WithdrawalMoneyMethod.Ethereum:
                    return "ethereum";
                case WithdrawalMoneyMethod.Litecoin:
                    return "litecoin";
                case WithdrawalMoneyMethod.Skrill:
                    return "skrill";
                case WithdrawalMoneyMethod.BankWire:
                    return "bank%20wire";
                default:
                    return "";
            }
        }
    }
}
