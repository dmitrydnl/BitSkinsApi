using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using BitSkinsApi.Extensions;

namespace BitSkinsApi.Market
{
    /// <summary>
    /// Work with raw Steam Market price data.
    /// </summary>
    public static class SteamRawPriceData
    {
        /// <summary>
        /// Allows you to retrieve raw Steam Community Market price data for a given item. 
        /// You can use this data to create your own pricing algorithm if you need it.
        /// </summary>
        /// <param name="app">Inventory's game id.</param>
        /// <param name="marketHashName">The item's name.</param>
        /// <returns>Raw Steam Market price data for a given item.</returns>
        public static SteamItemRawPriceData GetRawPriceData(AppId.AppName app, string marketHashName)
        {
            string urlRequest = GetUrlRequest(app, marketHashName);
            string result = Server.ServerRequest.RequestServer(urlRequest);
            SteamItemRawPriceData steamItemRawPriceData = ReadSteamItemRawPrice(result);
            return steamItemRawPriceData;
        }

        private static string GetUrlRequest(AppId.AppName app, string marketHashName)
        {
            Server.UrlCreator urlCreator = new Server.UrlCreator($"https://bitskins.com/api/v1/get_steam_price_data/");
            urlCreator.AppendUrl($"&app_id={(int)app}");
            urlCreator.AppendUrl($"&market_hash_name={marketHashName}");

            return urlCreator.ReadUrl();
        }

        private static SteamItemRawPriceData ReadSteamItemRawPrice(string result)
        {
            dynamic responseServerD = JsonConvert.DeserializeObject(result);
            dynamic updatedAtD = responseServerD.data.updated_at;
            dynamic rawDataD = responseServerD.data.raw_data;

            DateTime? updatedAt = ReadUpdatedAt(updatedAtD);
            List<ItemRawPrice> itemRawPrices = ReadItemRawPrices(rawDataD);

            SteamItemRawPriceData steamItemRawPriceData = new SteamItemRawPriceData(itemRawPrices, updatedAt);
            return steamItemRawPriceData;
        }

        private static DateTime? ReadUpdatedAt(dynamic updatedAtD)
        {
            DateTime? updatedAt = null;
            if (updatedAtD != null)
            {
                updatedAt = DateTimeExtension.FromUnixTime((long)updatedAtD);
            }

            return updatedAt;
        }

        private static List<ItemRawPrice> ReadItemRawPrices(dynamic rawDataD)
        {
            List<ItemRawPrice> itemRawPrices = new List<ItemRawPrice>();
            if (rawDataD != null)
            {
                foreach (dynamic item in rawDataD)
                {
                    ItemRawPrice itemRawPrice = ReadItemRawPrice(item);
                    itemRawPrices.Add(itemRawPrice);
                }
            }

            return itemRawPrices;
        }

        private static ItemRawPrice ReadItemRawPrice(dynamic item)
        {
            DateTime time = DateTimeExtension.FromUnixTime((long)item.time);
            double price = item.price;
            int volume = item.volume;

            ItemRawPrice itemRawPrice = new ItemRawPrice(time, price, volume);
            return itemRawPrice;
        }
    }

    /// <summary>
    /// Item's sales data in Steam.
    /// </summary>
    public class SteamItemRawPriceData
    {
        public List<ItemRawPrice> ItemRawPrices { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        internal SteamItemRawPriceData(List<ItemRawPrice> rawPriceItems, DateTime? updatedAt)
        {
            ItemRawPrices = rawPriceItems;
            UpdatedAt = updatedAt;
        }
    }

    /// <summary>
    /// Sales data about Steam item at a certain time.
    /// </summary>
    public class ItemRawPrice
    {
        public DateTime Time { get; private set; }
        public double Price { get; private set; }
        public int Volume { get; private set; }

        internal ItemRawPrice(DateTime time, double price, int volume)
        {
            Time = time;
            Price = price;
            Volume = volume;
        }
    }
}
