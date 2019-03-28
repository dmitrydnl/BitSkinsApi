using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using BitSkinsApi.Extensions;

namespace BitSkinsApi.Market
{
    /// <summary>
    /// Work with Steam Market data.
    /// </summary>
    public static class SteamMarket
    {
        /// <summary>
        /// Allows you to retrieve raw Steam Community Market price data for a given item. You can use this data to create your own pricing algorithm if you need it.
        /// </summary>
        /// <param name="marketHashName">The item's name.</param>
        /// <param name="app">For the inventory's game.</param>
        /// <returns>Sales data for this item.</returns>
        public static List<SteamMarketItem> GetRawPriceData(string marketHashName, AppId.AppName app)
        {
            string url = $"https://bitskins.com/api/v1/get_steam_price_data/?api_key={Account.AccountData.GetApiKey()}&market_hash_name={marketHashName}&app_id={(int)app}&code={Account.Secret.GetTwoFactorCode()}";
            string result = Server.ServerRequest.RequestServer(url);

            dynamic responseServer = JsonConvert.DeserializeObject(result);

            if (responseServer.data.raw_data == null)
            {
                return new List<SteamMarketItem>();
            }

            List<SteamMarketItem> steamMarketItems = new List<SteamMarketItem>();
            foreach (dynamic item in responseServer.data.raw_data)
            {
                DateTime time = DateTimeExtension.FromUnixTime((long)item.time);
                double price = item.price;
                int volume = item.volume;

                SteamMarketItem steamMarketItem = new SteamMarketItem(time, price, volume);
                steamMarketItems.Add(steamMarketItem);
            }

            return steamMarketItems;
        }
    }

    /// <summary>
    /// Sales data about Steam iteam.
    /// </summary>
    public class SteamMarketItem
    {
        public DateTime Time { get; private set; }
        public double Price { get; private set; }
        public int Volume { get; private set; }

        internal SteamMarketItem(DateTime time, double price, int volume)
        {
            Time = time;
            Price = price;
            Volume = volume;
        }
    }
}
