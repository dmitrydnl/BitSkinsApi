using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using BitSkinsApi.Extensions;

namespace BitSkinsApi.Market
{
    /// <summary>
    /// Work with price database.
    /// </summary>
    public static class PriceDatabase
    {
        /// <summary>
        /// Allows you to retrieve the entire price database used at BitSkins.
        /// </summary>
        /// <param name="app">For the inventory's game.</param>
        /// <returns>List of price database's items.</returns>
        public static List<PriceDatabaseItem> GetAllItemPrices(AppId.AppName app)
        {
            string url = $"https://bitskins.com/api/v1/get_all_item_prices/?api_key={Account.AccountData.ApiKey}&code={Account.Secret.Code}&app_id={(int)app}";
            if (!Server.ServerRequest.RequestServer(url, out string result))
                throw new Exception(result);

            dynamic responseServer = JsonConvert.DeserializeObject(result);

            List<PriceDatabaseItem> marketDataItems = new List<PriceDatabaseItem>();
            foreach (dynamic item in responseServer.prices)
            {
                string name = item.market_hash_name;
                double price = item.price;
                string pricingMode = item.pricing_mode;
                double skewness = item.skewness;
                DateTime createdAt = DateTimeExtension.FromUnixTime((long)item.created_at);
                double instantSalePrice = (item.instant_sale_price != null) ? (double)item.instant_sale_price : 0;

                PriceDatabaseItem databaseItem = new PriceDatabaseItem(name, price, pricingMode, skewness, createdAt, instantSalePrice);
                marketDataItems.Add(databaseItem);
            }

            return marketDataItems;
        }
    }

    /// <summary>
    /// BitSkins price database's item.
    /// </summary>
    public class PriceDatabaseItem
    {
        public string Name { get; private set; }
        public double Price { get; private set; }
        public string PricingMode { get; private set; }
        public double Skewness { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public double InstantSalePrice { get; private set; }

        internal PriceDatabaseItem(string name, double price, string pricingMode, double skewness, DateTime createdAt, double instantSalePrice)
        {
            Name = name;
            Price = price;
            PricingMode = pricingMode;
            Skewness = skewness;
            CreatedAt = createdAt;
            InstantSalePrice = instantSalePrice;
        }
    }
}
