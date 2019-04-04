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
            string method = WithdrawalMoneyMethodToString(withdrawalMoneyMethod);

            Server.UrlCreator urlCreator = new Server.UrlCreator($"https://bitskins.com/api/v1/request_withdrawal/");
            urlCreator.AppendUrl($"&amount={amount}");
            urlCreator.AppendUrl($"&withdrawal_method={method}");

            string result = Server.ServerRequest.RequestServer(urlCreator.ReadUrl());
            bool success = ReadStatus(result) == "success";
            return success;
        }

        static string ReadStatus(string result)
        {
            dynamic responseServer = JsonConvert.DeserializeObject(result);
            string status = responseServer.status;
            return status;
        }

        static string WithdrawalMoneyMethodToString(WithdrawalMoneyMethod withdrawalMoneyMethod)
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
