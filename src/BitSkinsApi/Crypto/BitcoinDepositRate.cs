using System;
using Newtonsoft.Json;
using BitSkinsApi.Extensions;

namespace BitSkinsApi.Crypto
{
    /// <summary>
    /// Work with Bitcoin deposit rate.
    /// </summary>
    public static class BitcoinExchangeRate
    {
        /// <summary>
        /// WARNING!!! BitSkins stop support CryptoAPI! This api call can work not correct!
        /// Allows you to retrieve the current exchange rate per Bitcoin (in USD), and the time this conversion rate will expire.
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
                double? pricePerBitcoinInUsd = dataD.price_per_bitcoin_in_usd ?? null;
                DateTime? expiresAt = null;
                if (dataD.expires_at != null)
                {
                    expiresAt = DateTimeExtension.FromUnixTime((long)dataD.expires_at);
                }
                int? expiresIn = dataD.expires_in ?? null;

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
        public double? PricePerBitcoinInUsd { get; private set; }
        public DateTime? ExpiresAt { get; private set; }
        public int? ExpiresIn { get; private set; }

        internal BitcoinDepositRate(double? pricePerBitcoinInUsd, DateTime? expiresAt, int? expiresIn)
        {
            PricePerBitcoinInUsd = pricePerBitcoinInUsd;
            ExpiresAt = expiresAt;
            ExpiresIn = expiresIn;
        }
    }
}
