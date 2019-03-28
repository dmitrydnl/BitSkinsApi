using OtpSharp;
using Albireo.Base32;

namespace BitSkinsApi.Account
{
    static class Secret
    {
        internal static string GetTwoFactorCode()
        {
            Totp totpgen = new Totp(Base32.Decode(AccountData.GetSecret()));
            return totpgen.ComputeTotp();
        }
    }
}
