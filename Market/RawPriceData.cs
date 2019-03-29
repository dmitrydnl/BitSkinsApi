using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using BitSkinsApi.Extensions;

namespace BitSkinsApi.Market
{
    /// <summary>
    /// Work with Steam Market data.
    /// </summary>
    public static class RawPriceData
    {
        /// <summary>
        /// Allows you to retrieve raw Steam Community Market price data for a given item. You can use this data to create your own pricing algorithm if you need it.
        /// </summary>
        /// <param name="marketHashName">The item's name.</param>
        /// <param name="app">For the inventory's game.</param>
        /// <returns>Sales data for this item.</returns>
        public static RawPrice GetRawPriceData(string marketHashName, AppId.AppName app)
        {
            string url = $"https://bitskins.com/api/v1/get_steam_price_data/?api_key={Account.AccountData.GetApiKey()}&market_hash_name={marketHashName}&app_id={(int)app}&code={Account.Secret.GetTwoFactorCode()}";
            string result = Server.ServerRequest.RequestServer(url);
            RawPrice steamMarketItems = ReadRawPrice(result);
            return steamMarketItems;
        }

        static RawPrice ReadRawPrice(string result)
        {
            dynamic responseServer = JsonConvert.DeserializeObject(result);
            dynamic rawData = responseServer.data.raw_data;

            DateTime? updatedAt = null;
            if (responseServer.data.updated_at != null)
            {
                updatedAt = DateTimeExtension.FromUnixTime((long)responseServer.data.updated_at);
            }

            if (rawData == null)
            {
                return new RawPrice(new List<RawPriceItem>(), updatedAt);
            }

            RawPrice rawPrice = new RawPrice(new List<RawPriceItem>(), updatedAt);
            foreach (dynamic item in rawData)
            {
                DateTime time = DateTimeExtension.FromUnixTime((long)item.time);
                double price = item.price;
                int volume = item.volume;

                RawPriceItem steamMarketItem = new RawPriceItem(time, price, volume);
                rawPrice.RawPriceItems.Add(steamMarketItem);
            }

            return rawPrice;
        }
    }

    /// <summary>
    /// Sales data about Steam iteam.
    /// </summary>
    public class RawPrice
    {
        public List<RawPriceItem> RawPriceItems { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        internal RawPrice(List<RawPriceItem> rawPriceItems, DateTime? updatedAt)
        {
            RawPriceItems = rawPriceItems;
            UpdatedAt = updatedAt;
        }
    }

    /// <summary>
    /// Sales data about Steam iteam at a certain time.
    /// </summary>
    public class RawPriceItem
    {
        public DateTime Time { get; private set; }
        public double Price { get; private set; }
        public int Volume { get; private set; }

        internal RawPriceItem(DateTime time, double price, int volume)
        {
            Time = time;
            Price = price;
            Volume = volume;
        }
    }
}
