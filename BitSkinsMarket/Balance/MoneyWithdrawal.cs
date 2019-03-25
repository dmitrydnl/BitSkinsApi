using System;

namespace BitSkinsApi.BitSkinsMarket.Balance
{
    /// <summary>
    /// Work with BitSkins withdrawal money.
    /// </summary>
    public class MoneyWithdrawal
    {
        /// <summary>
        /// All BitSkins money withdrawal methods.
        /// </summary>
        public enum WithdrawalMethod { PayPal, Bitcoin, Ethereum, Litecoin, Skrill, BankWire };

        /// <summary>
        /// Allows you to request withdrawal of available balance on your BitSkins account. All withdrawals are finalized 15 days after this request on a rolling basis.
        /// </summary>
        /// <param name="amount">Amount in USD to withdraw. Must be at most equal to available balance, and over $5.00 USD.</param>
        /// <param name="withdrawalMethod">Can be bitcoin, paypal, and so on.</param>
        public static void WithdrawMoney(double amount, WithdrawalMethod withdrawalMethod)
        {
            string method = WithdrawalMethodToString(withdrawalMethod);
            string url = $"https://bitskins.com/api/v1/request_withdrawal/?api_key={Account.AccountData.ApiKey}&amount={amount}&withdrawal_method={method}&code={Account.Secret.Code}";
            if (!Server.ServerRequest.RequestServer(url, out string result))
                throw new Exception(result);
        }

        private static string WithdrawalMethodToString(WithdrawalMethod withdrawalMethod)
        {
            switch (withdrawalMethod)
            {
                case WithdrawalMethod.PayPal:
                    return "paypal";
                case WithdrawalMethod.Bitcoin:
                    return "bitcoin";
                case WithdrawalMethod.Ethereum:
                    return "ethereum";
                case WithdrawalMethod.Litecoin:
                    return "litecoin";
                case WithdrawalMethod.Skrill:
                    return "skrill";
                case WithdrawalMethod.BankWire:
                    return "bank%20wire";
                default:
                    return "";
            }
        }
    }
}
