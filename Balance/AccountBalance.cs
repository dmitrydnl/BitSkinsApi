using Newtonsoft.Json;

namespace BitSkinsApi.Balance
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
            string url = $"https://bitskins.com/api/v1/get_account_balance/?api_key={Account.AccountData.GetApiKey()}&code={Account.Secret.GetTwoFactorCode()}";
            string result = Server.ServerRequest.RequestServer(url);
            Balance balance = ReadBalance(result);
            return balance;
        }

        static Balance ReadBalance(string result)
        {
            dynamic responseServer = JsonConvert.DeserializeObject(result);
            dynamic data = responseServer.data;
            if (data == null)
            {
                return new Balance(0, 0, 0, 0);
            }

            double available_balance = data.available_balance;
            double pending_withdrawals = data.pending_withdrawals;
            double withdrawable_balance = data.withdrawable_balance;
            double couponable_balance = data.couponable_balance;
            Balance balance = new Balance(available_balance, pending_withdrawals, withdrawable_balance, couponable_balance);

            return balance;
        }
    }

    /// <summary>
    /// BitSkins account balance.
    /// </summary>
    public class Balance
    {
        public double AvailableBalance { get; private set; }
        public double PendingWithdrawals { get; private set; }
        public double WithdrawableBalance { get; private set; }
        public double CouponableBalance { get; private set; }

        internal Balance(double availableBalance, double pendingWithdrawals, double withdrawableBalance, double couponableBalance)
        {
            AvailableBalance = availableBalance;
            PendingWithdrawals = pendingWithdrawals;
            WithdrawableBalance = withdrawableBalance;
            CouponableBalance = couponableBalance;
        }
    }
}
