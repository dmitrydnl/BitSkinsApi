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
using System.Net;
using System.IO;
using System.Threading;
using System.Runtime.Serialization;

namespace BitSkinsApi.Server
{
    static class ServerRequest
    {
        private static DateTime lastRequestTime = DateTime.Now;

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

    [Serializable]
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

        protected RequestServerException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}