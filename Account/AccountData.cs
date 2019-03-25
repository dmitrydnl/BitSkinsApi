namespace BitSkinsApi.Account
{
    public static class AccountData
    {
        internal static string apiKey { get; private set; }
        internal static string secret { get; private set; }
        internal static int maxRequestsPerSecond { get; private set; }

        public static void InitializeAccount(string apiKey, string secret, int maxRequestsPerSecond = 8)
        {
            AccountData.apiKey = apiKey;
            AccountData.secret = secret;
            AccountData.maxRequestsPerSecond = maxRequestsPerSecond;
        }
    }
}
