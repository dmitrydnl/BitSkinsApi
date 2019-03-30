using System;
using System.Security;
using System.Runtime.InteropServices;

namespace BitSkinsApi.Account
{
    /// <summary>
    /// Work with BitSkins account data.
    /// </summary>
    public static class AccountData
    {
        const int maxRequestsPerSecondDefault = 8;

        static SecureString apiKey;
        static SecureString secret;
        static int maxRequestsPerSecond;

        internal static string GetApiKey()
        {
            if (apiKey.Length == 0)
            {
                throw new SetupAccountException("First you must setup AccountData: BitSkinsApi.Account.AccountData.SetupAccount().");
            }
            return SecureStringToString(apiKey);
        }

        internal static string GetSecret()
        {
            if (secret.Length == 0)
            {
                throw new SetupAccountException("First you must setup AccountData: BitSkinsApi.Account.AccountData.SetupAccount().");
            }
            return SecureStringToString(secret);
        }

        internal static int GetMaxRequestsPerSecond()
        {
            if (maxRequestsPerSecond == 0)
            {
                throw new SetupAccountException("First you must setup AccountData: BitSkinsApi.Account.AccountData.SetupAccount().");
            }
            return maxRequestsPerSecond;
        }

        /// <summary>
        /// Setup of all required BitSkins account data.
        /// </summary>
        /// <param name="apiKey">API Key you can retrieve through the BitSkins settings page after you enable API access for your BitSkins account.</param>
        /// <param name="secret">Your two-factor secret shown when you enable Secure Access to your BitSkins account.</param>
        /// <param name="maxRequestsPerSecond">API throttle limits per second.</param>
        public static void SetupAccount(string apiKey, string secret, int maxRequestsPerSecond)
        {
            AccountData.apiKey = StringToSecureString(apiKey);
            AccountData.apiKey.MakeReadOnly();
            AccountData.secret = StringToSecureString(secret);
            AccountData.secret.MakeReadOnly();
            AccountData.maxRequestsPerSecond = maxRequestsPerSecond;
        }

        /// <summary>
        /// Setup of all required BitSkins account data. Default requests per second is 8.
        /// </summary>
        /// <param name="apiKey">API Key you can retrieve through the BitSkins settings page after you enable API access for your BitSkins account.</param>
        /// <param name="secret">Your two-factor secret shown when you enable Secure Access to your BitSkins account.</param>
        public static void SetupAccount(string apiKey, string secret)
        {
            AccountData.apiKey = StringToSecureString(apiKey);
            AccountData.apiKey.MakeReadOnly();
            AccountData.secret = StringToSecureString(secret);
            AccountData.secret.MakeReadOnly();
            AccountData.maxRequestsPerSecond = maxRequestsPerSecondDefault;
        }

        static SecureString StringToSecureString(string str)
        {
            SecureString secureString = new SecureString();
            foreach (char ch in str)
            {
                secureString.AppendChar(ch);
            }
            return secureString;
        }

        static string SecureStringToString(SecureString secureString)
        {
            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                return Marshal.PtrToStringUni(valuePtr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }
    }

    public class SetupAccountException : Exception
    {
        internal SetupAccountException()
        {
        }

        internal SetupAccountException(string message)
            : base(message)
        {
        }

        internal SetupAccountException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
