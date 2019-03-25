using OtpSharp;
using Albireo.Base32;

namespace BitSkinsApi.Account
{
    static class Secret
    {
        internal static string GetCode()
        {
            string secret = AccountData.secret;
            if (string.IsNullOrEmpty(secret))
                throw new System.Exception("First you must initialize AccountData: BitSkinsApi.Account.AccountData.InitializeAccount().");

            Totp totpgen = new Totp(Base32.Decode(secret));
            return totpgen.ComputeTotp();
        }
    }
}
