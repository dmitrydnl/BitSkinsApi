/*
 * BitSkinsApi
 * Copyright (C) 2019 Captious99
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
using Newtonsoft.Json;
using BitSkinsApi.Extensions;

namespace BitSkinsApi.Crypto
{
    /// <summary>
    /// Work with Bitcoin deposit rate.
    /// </summary>
    public static class BitcoinConversionRate
    {
        /// <summary>
        /// Allows you to retrieve the current conversion rate per Bitcoin (in USD), and the time this conversion rate will expire.
        /// </summary>
        /// <returns>Bitcoin deposit rate and the time this rate will expire.</returns>
        public static BitcoinDepositRate GetBitcoinDepositRate()
        {
            string urlRequest = GetUrlRequest();
            string result = Server.ServerRequest.RequestServer(urlRequest);
            BitcoinDepositRate bitcoinDepositRate = ReadBitcoinDepositRate(result);
            return bitcoinDepositRate;
        }

        private static string GetUrlRequest()
        {
            Server.UrlCreator urlCreator = new Server.UrlCreator($"https://bitskins.com/api/v1/get_current_deposit_conversion_rate/");
            return urlCreator.ReadUrl();
        }

        private static BitcoinDepositRate ReadBitcoinDepositRate(string result)
        {
            dynamic responseServerD = JsonConvert.DeserializeObject(result);
            dynamic dataD = responseServerD.data;

            BitcoinDepositRate bitcoinDepositRate = null;
            if (dataD != null)
            {
                double pricePerBitcoinInUsd = dataD.price_per_bitcoin_in_usd;
                DateTime expiresAt = DateTimeExtension.FromUnixTime((long)dataD.expires_at);
                int expiresIn = dataD.expires_in;

                bitcoinDepositRate = new BitcoinDepositRate(pricePerBitcoinInUsd, expiresAt, expiresIn);
            }

            return bitcoinDepositRate;
        }
    }

    /// <summary>
    /// Current conversion rate per Bitcoin (in USD), and the time this conversion rate will expire.
    /// </summary>
    public class BitcoinDepositRate
    {
        public double PricePerBitcoinInUsd { get; private set; }
        public DateTime ExpiresAt { get; private set; }
        public int ExpiresIn { get; private set; }

        internal BitcoinDepositRate(double pricePerBitcoinInUsd, DateTime expiresAt, int expiresIn)
        {
            PricePerBitcoinInUsd = pricePerBitcoinInUsd;
            ExpiresAt = expiresAt;
            ExpiresIn = expiresIn;
        }
    }
}
