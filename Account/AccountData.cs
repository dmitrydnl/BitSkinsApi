using System;

namespace BitSkinsApi.Account
{
    /// <summary>
    /// Work with BitSkins account data.
    /// </summary>
    public static class AccountData
    {
        const int maxRequestsPerSecondDefault = 8;

        static string apiKey;
        static string secret;
        static int maxRequestsPerSecond;

        internal static string GetApiKey()
        {
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new InitializeAccountException("First you must initialize AccountData: BitSkinsApi.Account.AccountData.InitializeAccount().");
            }
            return apiKey;
        }

        internal static string GetSecret()
        {
            if (string.IsNullOrEmpty(secret))
            {
                throw new InitializeAccountException("First you must initialize AccountData: BitSkinsApi.Account.AccountData.InitializeAccount().");
            }
            return secret;
        }

        internal static int GetMaxRequestsPerSecond()
        {
            if (maxRequestsPerSecond == 0)
            {
                throw new InitializeAccountException("First you must initialize AccountData: BitSkinsApi.Account.AccountData.InitializeAccount().");
            }
            return maxRequestsPerSecond;
        }

        /// <summary>
        /// Initialization of all required BitSkins account data.
        /// </summary>
        /// <param name="apiKey">API Key you can retrieve through the BitSkins settings page after you enable API access for your BitSkins account.</param>
        /// <param name="secret">Your two-factor secret shown when you enable Secure Access to your BitSkins account.</param>
        /// <param name="maxRequestsPerSecond">API throttle limits per second.</param>
        public static void InitializeAccount(string apiKey, string secret, int maxRequestsPerSecond)
        {
            AccountData.apiKey = apiKey;
            AccountData.secret = secret;
            AccountData.maxRequestsPerSecond = maxRequestsPerSecond;
        }

        /// <summary>
        /// Initialization of all required BitSkins account data. Default requests per second is 8.
        /// </summary>
        /// <param name="apiKey">API Key you can retrieve through the BitSkins settings page after you enable API access for your BitSkins account.</param>
        /// <param name="secret">Your two-factor secret shown when you enable Secure Access to your BitSkins account.</param>
        public static void InitializeAccount(string apiKey, string secret)
        {
            AccountData.apiKey = apiKey;
            AccountData.secret = secret;
            AccountData.maxRequestsPerSecond = maxRequestsPerSecondDefault;
        }
    }

    class InitializeAccountException : Exception
    {
        internal InitializeAccountException()
        {
        }

        internal InitializeAccountException(string message)
            : base(message)
        {
        }

        internal InitializeAccountException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
