/*
 * BitSkinsApi
 * Copyright (C) 2019 Dmitry
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Security;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

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
            if (apiKey == null || apiKey.Length == 0)
            {
                throw new SetupAccountException("First you must setup AccountData: BitSkinsApi.Account.AccountData.SetupAccount().");
            }
            return SecureStringToString(apiKey);
        }

        internal static string GetSecret()
        {
            if (secret == null || secret.Length == 0)
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

        /// <summary>
        /// Get two factor code for test is correct.
        /// </summary>
        /// <returns>Two factor code.</returns>
        public static string GetTwoFactorCode()
        {
            string code = Secret.GetTwoFactorCode();
            return code;
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

    [Serializable]
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

        protected SetupAccountException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
