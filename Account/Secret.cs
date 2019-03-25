using OtpSharp;
using Albireo.Base32;

namespace BitSkinsApi.Account
{
    static class Secret
    {
        internal static string Code
        {
            get
            {
                string secret = AccountData.Secret;
                Totp totpgen = new Totp(Base32.Decode(secret));
                return totpgen.ComputeTotp();
            }
        }
    }
}
