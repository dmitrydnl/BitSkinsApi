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
        /// Allows you to retrieve upto 5 pages worth of recent sale data for a given item name. 
        /// These are the recent sales for the given item at BitSkins, in descending order.
        /// </summary>
        /// <param name="app">Inventory's game id.</param>
        /// <param name="marketHashName">The item's name.</param>
        /// <param name="page">The page number. From 1 to 5.</param>
        /// <returns>List of item's recent sales.</returns>
        public static List<ItemRecentSale> GetRecentSaleInfo(AppId.AppName app, string marketHashName, int page)
        {
            Server.UrlCreator urlCreator = new Server.UrlCreator($"https://bitskins.com/api/v1/get_sales_info/");
            urlCreator.AppendUrl($"&app_id={(int)app}");
            urlCreator.AppendUrl($"&market_hash_name={marketHashName}");
            urlCreator.AppendUrl($"&page={page}");

            string result = Server.ServerRequest.RequestServer(urlCreator.ReadUrl());
            List<ItemRecentSale> itemRecentSales = ReadItemRecentSales(result);
            return itemRecentSales;
        }

        static List<ItemRecentSale> ReadItemRecentSales(string result)
        {
            dynamic responseServerD = JsonConvert.DeserializeObject(result);
            dynamic salesD = responseServerD.data.sales;

            List<ItemRecentSale> itemRecentSales = new List<ItemRecentSale>();
            if (salesD != null)
            {
                foreach (dynamic item in salesD)
                {
                    double price = item.price;
                    double wearValue = item.wear_value;
                    DateTime soldAt = DateTimeExtension.FromUnixTime((long)item.sold_at);

                    ItemRecentSale recentSaleItem = new ItemRecentSale(price, wearValue, soldAt);
                    itemRecentSales.Add(recentSaleItem);
                }
            }

            return itemRecentSales;
        }
    }

    /// <summary>
    /// Information about item's recent sale. 
    /// </summary>
    public class ItemRecentSale
    {
        public double Price { get; private set; }
        public double WearValue { get; private set; }
        public DateTime SoldAt { get; private set; }

        internal ItemRecentSale(double price, double wearValue, DateTime soldAt)
        {
            Price = price;
            WearValue = wearValue;
            SoldAt = soldAt;
        }
    }
}
