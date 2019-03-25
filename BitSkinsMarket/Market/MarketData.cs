using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using BitSkinsApi.Extensions;

namespace BitSkinsApi.BitSkinsMarket.Market
{
    public static class MarketData
    {
        public static List<MarketDataItem> GetMarketData(AppId.AppName app)
        {
            string url = $"https://bitskins.com/api/v1/get_price_data_for_items_on_sale/?api_key={Account.AccountData.ApiKey}&code={Account.Secret.Code}&app_id={(int)app}";
            if (!Server.ServerRequest.RequestServer(url, out string result))
                throw new Exception(result);

            dynamic responseServer = JsonConvert.DeserializeObject(result);

            List<MarketDataItem> marketDataItems = new List<MarketDataItem>();
            foreach (dynamic item in responseServer.data.items)
            {
                if (item.updated_at == null)
                    continue;

                string name = item.market_hash_name;
                int totalItems = item.total_items;
                double lowestPrice = item.lowest_price;
                double highestPrice = item.highest_price;
                double cumulativePrice = item.cumulative_price;
                RecentSalesInfo recentSalesInfo = (item.recent_sales_info != null) ? 
                    new RecentSalesInfo((double)item.recent_sales_info.hours, (double)item.recent_sales_info.average_price) : null;
                DateTime updatedAt = DateTimeExtension.FromUnixTime((long)item.updated_at);

                MarketDataItem marketItem = new MarketDataItem(name, totalItems, lowestPrice, highestPrice, cumulativePrice, recentSalesInfo, updatedAt);
                marketDataItems.Add(marketItem);
            }

            return marketDataItems;
        }
    }

    public class RecentSalesInfo
    {
        public double hours { get; private set; }
        public double averagePrice { get; private set; }

        public RecentSalesInfo(double hours, double averagePrice)
        {
            this.hours = hours;
            this.averagePrice = averagePrice;
        }
    }

    public class MarketDataItem
    {
        public string name { get; private set; }
        public int totalItems { get; private set; }
        public double lowestPrice { get; private set; }
        public double highestPrice { get; private set; }
        public double cumulativePrice { get; private set; }
        public RecentSalesInfo recentSalesInfo { get; private set; }
        public DateTime updatedAt { get; private set; }

        public MarketDataItem(string name, int totalItems, double lowestPrice, double highestPrice, double cumulativePrice, 
            RecentSalesInfo recentSalesInfo, DateTime updatedAt)
        {
            this.name = name;
            this.totalItems = totalItems;
            this.lowestPrice = lowestPrice;
            this.highestPrice = highestPrice;
            this.cumulativePrice = cumulativePrice;
            this.recentSalesInfo = recentSalesInfo;
            this.updatedAt = updatedAt;
        }
    }
}
