using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using BitSkinsApi.Extensions;

namespace BitSkinsApi.Market
{
    /// <summary>
    /// Work with BitSkins market data.
    /// </summary>
    public static class MarketData
    {
        /// <summary>
        /// Allows you to retrieve basic price data for items currently on sale at BitSkins.
        /// </summary>
        /// <param name="app">Inventory's game id.</param>
        /// <returns>List of items currently on sale at BitSkins.</returns>
        public static List<MarketItem> GetMarketData(AppId.AppName app)
        {
            string urlRequest = GetUrlRequest(app);
            string result = Server.ServerRequest.RequestServer(urlRequest);
            List<MarketItem> marketItems = ReadMarketItems(result);
            return marketItems;
        }

        private static string GetUrlRequest(AppId.AppName app)
        {
            Server.UrlCreator urlCreator = new Server.UrlCreator($"https://bitskins.com/api/v1/get_price_data_for_items_on_sale/");
            urlCreator.AppendUrl($"&app_id={(int)app}");

            return urlCreator.ReadUrl();
        }

        private static List<MarketItem> ReadMarketItems(string result)
        {
            dynamic responseServerD = JsonConvert.DeserializeObject(result);
            dynamic itemsD = responseServerD.data.items;

            List<MarketItem> marketItems = new List<MarketItem>();
            if (itemsD != null)
            {
                foreach (dynamic item in itemsD)
                {
                    MarketItem marketItem = ReadMarketItem(item);
                    marketItems.Add(marketItem);
                }
            }

            return marketItems;
        }

        private static MarketItem ReadMarketItem(dynamic item)
        {
            string marketHashName = item.market_hash_name;
            int totalItems = item.total_items;
            double lowestPrice = item.lowest_price;
            double highestPrice = item.highest_price;
            double cumulativePrice = item.cumulative_price;
            double recentAveragePrice = (item.recent_sales_info != null) ? (double)item.recent_sales_info.average_price : 0;
            DateTime? updatedAt = null;
            if (item.updated_at != null)
            {
                updatedAt = DateTimeExtension.FromUnixTime((long)item.updated_at);
            }

            MarketItem marketItem = new MarketItem(marketHashName, totalItems, lowestPrice,
                highestPrice, cumulativePrice, recentAveragePrice, updatedAt);
            return marketItem;
        }
    }

    /// <summary>
    /// BitSkins market item.
    /// </summary>
    public class MarketItem
    {
        public string MarketHashName { get; private set; }
        public int TotalItems { get; private set; }
        public double LowestPrice { get; private set; }
        public double HighestPrice { get; private set; }
        public double CumulativePrice { get; private set; }
        public double RecentAveragePrice { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        internal MarketItem(string marketHashName, int totalItems, double lowestPrice, double highestPrice, 
            double cumulativePrice, double recentAveragePrice, DateTime? updatedAt)
        {
            MarketHashName = marketHashName;
            TotalItems = totalItems;
            LowestPrice = lowestPrice;
            HighestPrice = highestPrice;
            CumulativePrice = cumulativePrice;
            RecentAveragePrice = recentAveragePrice;
            UpdatedAt = updatedAt;
        }
    }
}
