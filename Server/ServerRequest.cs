using System;
using System.Net;
using System.IO;
using System.Threading;

namespace BitSkinsApi.Server
{
    static class ServerRequest
    {
        private static DateTime lastRequestTime;

        static ServerRequest()
        {
            lastRequestTime = DateTime.Now;
        }

        internal static bool RequestServer(string url, out string result)
        {
            TimeSpan timeSinceLastRequest = DateTime.Now - lastRequestTime;
            TimeSpan minTime = new TimeSpan(0, 0, 0, 0, 1000 / Account.AccountData.MaxRequestsPerSecond);

            if (timeSinceLastRequest < minTime)
                Thread.Sleep(minTime - timeSinceLastRequest);

            bool success = true;
            try
            {
                result = Request(url);
            }
            catch (Exception ex)
            {
                result = ex.Message;
                success = false;
            }
            finally
            {
                lastRequestTime = DateTime.Now;
            }

            return success;
        }

        private static string Request(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                    using (StreamReader reader = new StreamReader(stream))
                        return reader.ReadToEnd();
        }
    }
}