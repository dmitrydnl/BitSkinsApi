using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using BitSkinsApi.Extensions;

namespace BitSkinsApi.Market
{
    /// <summary>
    /// Work with price database.
    /// </summary>
    public static class AllItemPrices
    {
        /// <summary>
        /// Allows you to retrieve the entire price database used at BitSkins.
        /// </summary>
        /// <param name="app">For the inventory's game.</param>
        /// <returns>List of price database's items.</returns>
        public static List<ItemPrice> GetAllItemPrices(AppId.AppName app)
        {
            string url = $"https://bitskins.com/api/v1/get_all_item_prices/" +
                $"?api_key={Account.AccountData.GetApiKey()}" +
                $"&app_id={(int)app}" +
                $"&code={Account.Secret.GetTwoFactorCode()}";

            string result = Server.ServerRequest.RequestServer(url);
            List<ItemPrice> priceDatabaseItems = ReadItemPrices(result);
            return priceDatabaseItems;
        }

        static List<ItemPrice> ReadItemPrices(string result)
        {
            dynamic responseServer = JsonConvert.DeserializeObject(result);
            dynamic prices = responseServer.prices;

            if (prices == null)
            {
                return new List<ItemPrice>();
            }

            List<ItemPrice> priceDatabaseItems = new List<ItemPrice>();
            foreach (dynamic item in prices)
            {
                string marketHashName = item.market_hash_name;
                double price = item.price;
                string pricingMode = item.pricing_mode;
                DateTime createdAt = DateTimeExtension.FromUnixTime((long)item.created_at);
                string iconUrl = item.icon_url;
                double? instantSalePrice = item.instant_sale_price;

                ItemPrice databaseItem = new ItemPrice(marketHashName, price, pricingMode, createdAt, iconUrl, instantSalePrice);
                priceDatabaseItems.Add(databaseItem);
            }

            return priceDatabaseItems;
        }
    }

    /// <summary>
    /// BitSkins price database's item.
    /// </summary>
    public class ItemPrice
    {
        public string MarketHashName { get; private set; }
        public double Price { get; private set; }
        public string PricingMode { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string IconUrl { get; private set; }
        public double? InstantSalePrice { get; private set; }

        internal ItemPrice(string marketHashName, double price, string pricingMode, 
            DateTime createdAt, string iconUrl, double? instantSalePrice)
        {
            MarketHashName = marketHashName;
            Price = price;
            PricingMode = pricingMode;
            CreatedAt = createdAt;
            IconUrl = iconUrl;
            InstantSalePrice = instantSalePrice;
        }
    }
}
