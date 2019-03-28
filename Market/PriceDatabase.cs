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
            string url = $"https://bitskins.com/api/v1/get_all_item_prices/?api_key={Account.AccountData.GetApiKey()}&app_id={(int)app}&code={Account.Secret.GetTwoFactorCode()}";
            string result = Server.ServerRequest.RequestServer(url);

            dynamic responseServer = JsonConvert.DeserializeObject(result);

            if (responseServer.prices == null)
            {
                return new List<PriceDatabaseItem>();
            }

            List<PriceDatabaseItem> priceDatabaseItems = new List<PriceDatabaseItem>();
            foreach (dynamic item in responseServer.prices)
            {
                AppId.AppName appId = (AppId.AppName)((int)item.app_id);
                string marketHashName = item.market_hash_name;
                double price = item.price;
                string pricingMode = item.pricing_mode;
                double skewness = item.skewness;
                DateTime createdAt = DateTimeExtension.FromUnixTime((long)item.created_at);
                string iconUrl = item.icon_url;
                string nameColor = item.name_color;
                string qualityColor = item.quality_color;
                string rarityColor = item.rarity_color;
                double? instantSalePrice = item.instant_sale_price;

                PriceDatabaseItem databaseItem = new PriceDatabaseItem(appId, marketHashName, price, pricingMode, 
                    skewness, createdAt, iconUrl, nameColor, qualityColor, rarityColor, instantSalePrice);
                priceDatabaseItems.Add(databaseItem);
            }

            return priceDatabaseItems;
        }
    }

    /// <summary>
    /// BitSkins price database's item.
    /// </summary>
    public class PriceDatabaseItem
    {
        public AppId.AppName AppId { get; private set; }
        public string MarketHashName { get; private set; }
        public double Price { get; private set; }
        public string PricingMode { get; private set; }
        public double Skewness { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string IconUrl { get; private set; }
        public string NameColor { get; private set; }
        public string QualityColor { get; private set; }
        public string RarityColor { get; private set; }
        public double? InstantSalePrice { get; private set; }

        internal PriceDatabaseItem(AppId.AppName appId, string marketHashName, double price, string pricingMode, double skewness, 
            DateTime createdAt, string iconUrl, string nameColor, string qualityColor, string rarityColor, double? instantSalePrice)
        {
            AppId = appId;
            MarketHashName = marketHashName;
            Price = price;
            PricingMode = pricingMode;
            Skewness = skewness;
            CreatedAt = createdAt;
            IconUrl = iconUrl;
            NameColor = nameColor;
            QualityColor = qualityColor;
            RarityColor = rarityColor;
            InstantSalePrice = instantSalePrice;
        }
    }
}
