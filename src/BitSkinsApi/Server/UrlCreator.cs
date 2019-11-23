using System.Text;

namespace BitSkinsApi.Server
{
    internal class UrlCreator
    {
        private readonly StringBuilder Url;

        public UrlCreator(string urlStart)
        {
            Url = new StringBuilder(urlStart);
            Url.Append($"?api_key={Account.AccountData.GetApiKey()}");
            Url.Append($"&code={Account.Secret.GetTwoFactorCode()}");
        }

        internal void AppendUrl(string url)
        {
            Url.Append(url);
        }

        internal string ReadUrl()
        {
            return Url.ToString();
        }
    }
}
