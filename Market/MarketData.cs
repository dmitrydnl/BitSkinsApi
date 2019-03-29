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
        /// <param name="app">For the inventory's game.</param>
        /// <returns>List of items currently on sale at BitSkins.</returns>
        public static List<MarketDataItem> GetMarketData(AppId.AppName app)
        {
            string url = $"https://bitskins.com/api/v1/get_price_data_for_items_on_sale/?api_key={Account.AccountData.GetApiKey()}&app_id={(int)app}&code={Account.Secret.GetTwoFactorCode()}";
            string result = Server.ServerRequest.RequestServer(url);
            List<MarketDataItem> marketDataItems = ReadMarketDataItems(result);
            return marketDataItems;
        }

        static List<MarketDataItem> ReadMarketDataItems(string result)
        {
            dynamic responseServer = JsonConvert.DeserializeObject(result);
            dynamic items = responseServer.data.items;
            if (items == null)
            {
                return new List<MarketDataItem>();
            }

            List<MarketDataItem> marketDataItems = new List<MarketDataItem>();
            foreach (dynamic item in items)
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

                MarketDataItem marketItem = new MarketDataItem(marketHashName, totalItems, lowestPrice,
                    highestPrice, cumulativePrice, recentAveragePrice, updatedAt);
                marketDataItems.Add(marketItem);
            }

            return marketDataItems;
        }
    }

    /// <summary>
    /// BitSkins item currently on sale.
    /// </summary>
    public class MarketDataItem
    {
        public string MarketHashName { get; private set; }
        public int TotalItems { get; private set; }
        public double LowestPrice { get; private set; }
        public double HighestPrice { get; private set; }
        public double CumulativePrice { get; private set; }
        public double RecentAveragePrice { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        internal MarketDataItem(string marketHashName, int totalItems, double lowestPrice, double highestPrice, 
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
