using System;
using System.Net;
using System.IO;
using System.Threading;

namespace BitSkinsApi.Server
{
    static class ServerRequest
    {
        static DateTime lastRequestTime;

        static ServerRequest()
        {
            lastRequestTime = DateTime.Now;
        }

        internal static string RequestServer(string url)
        {
            TimeSpan timeSinceLastRequest = DateTime.Now - lastRequestTime;
            TimeSpan minTime = new TimeSpan(0, 0, 0, 0, 1000 / Account.AccountData.GetMaxRequestsPerSecond());

            if (timeSinceLastRequest < minTime)
            {
                Thread.Sleep(minTime - timeSinceLastRequest);
            }
            
            try
            {
                string result = Request(url);
                lastRequestTime = DateTime.Now;
                return result;
            }
            catch (Exception ex)
            {
                lastRequestTime = DateTime.Now;
                throw new RequestServerException(ex.Message);
            }
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

    public class RequestServerException : Exception
    {
        internal RequestServerException()
        {
        }

        internal RequestServerException(string message)
            : base(message)
        {
        }

        internal RequestServerException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}