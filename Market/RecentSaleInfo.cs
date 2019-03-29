using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using BitSkinsApi.Extensions;

namespace BitSkinsApi.Market
{
    /// <summary>
    /// Work with BitSkins recent sales.
    /// </summary>
    public static class RecentSaleInfo
    {
        /// <summary>
        /// Allows you to retrieve upto 5 pages worth of recent sale data for a given item name. These are the recent sales for the given item at BitSkins, in descending order.
        /// </summary>
        /// <param name="marketHashName">The item's name.</param>
        /// <param name="page">The page number. From 1 to 5.</param>
        /// <param name="app">For the inventory's game.</param>
        /// <returns>List of recent sales info.</returns>
        public static List<RecentSale> GetRecentSaleInfo(string marketHashName, int page, AppId.AppName app)
        {
            string url = $"https://bitskins.com/api/v1/get_sales_info/" +
                $"?api_key={Account.AccountData.GetApiKey()}" +
                $"&market_hash_name={marketHashName}" +
                $"&page={page}" +
                $"&app_id={(int)app}" +
                $"&code={Account.Secret.GetTwoFactorCode()}";

            string result = Server.ServerRequest.RequestServer(url);
            List<RecentSale> recentSaleItems = ReadRecentSales(result);
            return recentSaleItems;
        }

        static List<RecentSale> ReadRecentSales(string result)
        {
            dynamic responseServer = JsonConvert.DeserializeObject(result);
            dynamic sales = responseServer.data.sales;

            if (sales == null)
            {
                return new List<RecentSale>();
            }

            List<RecentSale> recentSaleItems = new List<RecentSale>();
            foreach (dynamic item in sales)
            {
                double price = item.price;
                double wearValue = item.wear_value;
                DateTime soldAt = DateTimeExtension.FromUnixTime((long)item.sold_at);

                RecentSale recentSaleItem = new RecentSale(price, wearValue, soldAt);
                recentSaleItems.Add(recentSaleItem);
            }

            return recentSaleItems;
        }
    }

    /// <summary>
    /// Info about item's recent sales. 
    /// </summary>
    public class RecentSale
    {
        public double Price { get; private set; }
        public double WearValue { get; private set; }
        public DateTime SoldAt { get; private set; }

        internal RecentSale(double price, double wearValue, DateTime soldAt)
        {
            Price = price;
            WearValue = wearValue;
            SoldAt = soldAt;
        }
    }
}
