namespace BitSkinsApi.Account
{
    /// <summary>
    /// Work with BitSkins account data.
    /// </summary>
    public static class AccountData
    {
        internal static string apiKey { get; private set; }
        internal static string secret { get; private set; }
        internal static int maxRequestsPerSecond { get; private set; }

        /// <summary>
        /// Initialization of all required BitSkins account data.
        /// </summary>
        /// <param name="apiKey">API Key you can retrieve through the BitSkins settings page after you enable API access for your BitSkins account.</param>
        /// <param name="secret">Your two-factor secret shown when you enable Secure Access to your BitSkins account.</param>
        /// <param name="maxRequestsPerSecond">Default API throttle limits are 8 requests per second.</param>
        public static void InitializeAccount(string apiKey, string secret, int maxRequestsPerSecond = 8)
        {
            AccountData.apiKey = apiKey;
            AccountData.secret = secret;
            AccountData.maxRequestsPerSecond = maxRequestsPerSecond;
        }
    }
}
