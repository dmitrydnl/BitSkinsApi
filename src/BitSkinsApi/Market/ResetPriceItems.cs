/*
 * BitSkinsApi
 * Copyright (C) 2019 Captious99
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

namespace BitSkinsApi.Market
{
    /// <summary>
    /// Work with reset price items.
    /// </summary>
    public static class ResetPriceItems
    {
        /// <summary>
        /// Returns a paginated list of items that need their prices reset. 
        /// Items need prices reset when Steam changes tracker so BitSkins are unable to match specified prices to the received items
        /// when you list them for sale. 
        /// Upto 30 items per page. 
        /// Items that need price resets always have the reserved price of 4985.11.
        /// </summary>
        /// <param name="app">Inventory's game id.</param>
        /// <param name="page">Page number.</param>
        /// <returns>List of reset price items.</returns>
        public static List<ResetPriceItem> GetResetPriceItems(AppId.AppName app, int page)
        {
            CheckParameters(page);
            string urlRequest = GetUrlRequest(app, page);
            string result = Server.ServerRequest.RequestServer(urlRequest);
            List<ResetPriceItem> resetPriceItems = ReadResetPriceItems(result);
            return resetPriceItems;
        }

        private static void CheckParameters(int page)
        {
            if (page < 1)
            {
                throw new ArgumentException("\"page\" must be positive number.");
            }
        }

        private static string GetUrlRequest(AppId.AppName app, int page)
        {
            Server.UrlCreator urlCreator = new Server.UrlCreator($"https://bitskins.com/api/v1/get_reset_price_items/");
            urlCreator.AppendUrl($"&app_id={(int)app}");
            urlCreator.AppendUrl($"&page={page}");

            return urlCreator.ReadUrl();
        }

        private static List<ResetPriceItem> ReadResetPriceItems(string result)
        {
            dynamic responseServerD = JsonConvert.DeserializeObject(result);
            dynamic itemsD = responseServerD.data.items;

            List<ResetPriceItem> resetPriceItems = new List<ResetPriceItem>();
            if (itemsD != null)
            {
                foreach (dynamic item in itemsD)
                {
                    ResetPriceItem resetPriceItem = ReadResetPriceItem(item);
                    resetPriceItems.Add(resetPriceItem);
                }
            }

            return resetPriceItems;
        }

        private static ResetPriceItem ReadResetPriceItem(dynamic item)
        {
            string marketHashName = item.market_hash_name;
            double? price = null;
            if (item.price != null)
            {
                price = (double)item.price;
            }

            ResetPriceItem resetPriceItem = new ResetPriceItem(marketHashName, price);
            return resetPriceItem;
        }
    }

    /// <summary>
    /// BitSkins reset price item.
    /// </summary>
    public class ResetPriceItem
    {
        public string MarketHashName { get; private set; }
        public double? Price { get; private set; }

        internal ResetPriceItem(string marketHashName, double? price)
        {
            MarketHashName = marketHashName;
            Price = price;
        }
    }
}
