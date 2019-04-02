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
            Server.UrlCreator urlCreator = new Server.UrlCreator($"https://bitskins.com/api/v1/get_account_balance/");

            string result = Server.ServerRequest.RequestServer(urlCreator.ReadUrl());
            AccountBalance accountBalance = ReadAccountBalance(result);
            return accountBalance;
        }

        static AccountBalance ReadAccountBalance(string result)
        {
            dynamic responseServerD = JsonConvert.DeserializeObject(result);
            dynamic dataD = responseServerD.data;

            AccountBalance accountBalance = null;
            if (dataD != null)
            {
                double available_balance = dataD.available_balance;
                double pending_withdrawals = dataD.pending_withdrawals;
                double withdrawable_balance = dataD.withdrawable_balance;
                double couponable_balance = dataD.couponable_balance;

                accountBalance = new AccountBalance(available_balance, pending_withdrawals, withdrawable_balance, couponable_balance);
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
