using System;
using Newtonsoft.Json;

namespace BitSkinsApi.BitSkinsMarket.Balance
{
    /// <summary>
    /// Work with BitSkins account balance.
    /// </summary>
    public static class AccountBalance
    {
        /// <summary>
        /// Allows you to retrieve your available and pending balance in all currencies supported by BitSkins.
        /// </summary>
        /// <returns>BitSkins account balance.</returns>
        public static Balance GetAccountBalance()
        {
            string url = $"https://bitskins.com/api/v1/get_account_balance/?api_key={Account.AccountData.ApiKey}&code={Account.Secret.Code}";
            if (!Server.ServerRequest.RequestServer(url, out string result))
                throw new Exception(result);

            dynamic responseServer = JsonConvert.DeserializeObject(result);

            double available_balance = responseServer.data.available_balance;
            double pending_withdrawals = responseServer.data.pending_withdrawals;
            double withdrawable_balance = responseServer.data.withdrawable_balance;
            double couponable_balance = responseServer.data.couponable_balance;

            Balance balance = new Balance(available_balance, pending_withdrawals, withdrawable_balance, couponable_balance);

            return balance;
        }
    }

    /// <summary>
    /// BitSkins account balance.
    /// </summary>
    public class Balance
    {
        public double availableBalance { get; private set; }
        public double pendingWithdrawals { get; private set; }
        public double withdrawableBalance { get; private set; }
        public double couponableBalance { get; private set; }

        public Balance(double availableBalance, double pendingWithdrawals, double withdrawableBalance, double couponableBalance)
        {
            this.availableBalance = availableBalance;
            this.pendingWithdrawals = pendingWithdrawals;
            this.withdrawableBalance = withdrawableBalance;
            this.couponableBalance = couponableBalance;
        }
    }
}
