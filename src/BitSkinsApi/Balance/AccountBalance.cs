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
                double? availableBalance = dataD.available_balance ?? null;
                double? pendingWithdrawals = dataD.pending_withdrawals ?? null;
                double? withdrawableBalance = dataD.withdrawable_balance ?? null;
                double? couponableBalance = dataD.couponable_balance ?? null;

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
        public double? AvailableBalance { get; private set; }
        public double? PendingWithdrawals { get; private set; }
        public double? WithdrawableBalance { get; private set; }
        public double? CouponableBalance { get; private set; }

        internal AccountBalance(double? availableBalance, double? pendingWithdrawals, double? withdrawableBalance, double? couponableBalance)
        {
            AvailableBalance = availableBalance;
            PendingWithdrawals = pendingWithdrawals;
            WithdrawableBalance = withdrawableBalance;
            CouponableBalance = couponableBalance;
        }
    }
}
