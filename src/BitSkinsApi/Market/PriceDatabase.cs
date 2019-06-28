/*
 * BitSkinsApi
 * Copyright (C) 2019 dmitrydnl
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

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
        /// <param name="app">Inventory's game id.</param>
        /// <returns>List of price database's items.</returns>
        public static List<ItemPrice> GetAllItemPrices(AppId.AppName app)
        {
            string urlRequest = GetUrlRequest(app);
            string result = Server.ServerRequest.RequestServer(urlRequest);
            List<ItemPrice> itemsPrices = ReadItemsPrices(result);
            return itemsPrices;
        }

        private static string GetUrlRequest(AppId.AppName app)
        {
            Server.UrlCreator urlCreator = new Server.UrlCreator($"https://bitskins.com/api/v1/get_all_item_prices/");
            urlCreator.AppendUrl($"&app_id={(int)app}");

            return urlCreator.ReadUrl();
        }

        private static List<ItemPrice> ReadItemsPrices(string result)
        {
            dynamic responseServerD = JsonConvert.DeserializeObject(result);
            dynamic pricesD = responseServerD.prices;

            List<ItemPrice> itemsPrices = new List<ItemPrice>();
            if (pricesD != null)
            {
                foreach (dynamic item in pricesD)
                {
                    ItemPrice databaseItem = ReadItemPrice(item);
                    itemsPrices.Add(databaseItem);
                }
            }

            return itemsPrices;
        }

        private static ItemPrice ReadItemPrice(dynamic item)
        {
            string marketHashName = item.market_hash_name;
            double price = item.price;
            string pricingMode = item.pricing_mode;
            DateTime createdAt = DateTimeExtension.FromUnixTime((long)item.created_at);
            string iconUrl = item.icon_url;
            double? instantSalePrice = item.instant_sale_price;

            ItemPrice databaseItem = new ItemPrice(marketHashName, price, pricingMode, createdAt, iconUrl, instantSalePrice);
            return databaseItem;
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
