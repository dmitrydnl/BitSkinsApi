namespace BitSkinsApi.Account
{
    /// <summary>
    /// Work with BitSkins account data.
    /// </summary>
    public static class AccountData
    {
        private static string apiKey;
        internal static string ApiKey
        {
            get
            {
                if (string.IsNullOrEmpty(apiKey))
                    throw new System.Exception("First you must initialize AccountData: BitSkinsApi.Account.AccountData.InitializeAccount().");

                return apiKey;
            }
            private set
            {
                apiKey = value;
            }
        }
        private static string secret;
        internal static string Secret
        {
            get
            {
                if (string.IsNullOrEmpty(secret))
                    throw new System.Exception("First you must initialize AccountData: BitSkinsApi.Account.AccountData.InitializeAccount().");

                return secret;
            }
            private set
            {
                secret = value;
            }
        }
        private static int maxRequestsPerSecond;
        internal static int MaxRequestsPerSecond
        {
            get
            {
                if (maxRequestsPerSecond == 0)
                    throw new System.Exception("First you must initialize AccountData: BitSkinsApi.Account.AccountData.InitializeAccount().");

                return maxRequestsPerSecond;
            }
            private set
            {
                maxRequestsPerSecond = value;
            }
        }

        /// <summary>
        /// Initialization of all required BitSkins account data.
        /// </summary>
        /// <param name="apiKey">API Key you can retrieve through the BitSkins settings page after you enable API access for your BitSkins account.</param>
        /// <param name="secret">Your two-factor secret shown when you enable Secure Access to your BitSkins account.</param>
        /// <param name="maxRequestsPerSecond">Default API throttle limits are 8 requests per second.</param>
        public static void InitializeAccount(string apiKey, string secret, int maxRequestsPerSecond = 8)
        {
            AccountData.ApiKey = apiKey;
            AccountData.Secret = secret;
            AccountData.MaxRequestsPerSecond = maxRequestsPerSecond;
        }
    }
}
