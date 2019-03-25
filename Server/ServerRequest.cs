using System;
using System.Net;
using System.IO;
using System.Threading;

namespace BitSkinsApi.Server
{
    static class ServerRequest
    {
        internal static bool RequestServer(string url, out string result)
        {
            int maxRequestsPerSecond = Account.AccountData.MaxRequestsPerSecond;
            Thread.Sleep(1000 / maxRequestsPerSecond);

            try
            {
                string res = Request(url);
                result = res;
            }
            catch (Exception ex)
            {
                result = ex.Message;
                return false;
            }
            return true;
        }

        private static string Request(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }
    }
}