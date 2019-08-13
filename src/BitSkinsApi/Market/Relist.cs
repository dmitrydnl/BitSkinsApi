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
using BitSkinsApi.CheckParameters;

namespace BitSkinsApi.Market
{
    /// <summary>
    /// Work with relist for sale.
    /// </summary>
    public static class RelistForSale
    {
        private static DateTime lastRelistItemTime = DateTime.Now;

        /// <summary>
        /// Allows you to re-list a delisted/purchased item for sale. 
        /// Re-listed items can be sold instantly, where applicable.
        /// </summary>
        /// <param name="app">Inventory's game id.</param>
        /// <param name="itemIds">List of item IDs.</param>
        /// <param name="itemPrices">Prices for the item Ids.</param>
        /// <returns>List of relisted items.</returns>
        public static List<RelistedItem> RelistItem(AppId.AppName app, List<string> itemIds, List<double> itemPrices)
        {
            CheckParameters(itemIds, itemPrices);
            string urlRequest = GetUrlRequest(app, itemIds, itemPrices);
            string result = Server.ServerRequest.RequestServer(urlRequest);
            List<RelistedItem> relistedItems = ReadRelistedItems(result);
            return relistedItems;
        }

        /// <summary>
        /// New version of method RelistItem(). 
        /// This method considering that now items cannot be relisted more than once an hour.
        /// Allows you to re-list a delisted/purchased item for sale. 
        /// Re-listed items can be sold instantly, where applicable.
        /// </summary>
        /// <param name="app">Inventory's game id.</param>
        /// <param name="itemIds">List of item IDs.</param>
        /// <param name="itemPrices">Prices for the item Ids.</param>
        /// <returns>List of relisted items.</returns>
        public static List<RelistedItem> RelistItemDelayHour(AppId.AppName app, List<string> itemIds, List<double> itemPrices)
        {
            TimeSpan timeSinceLastRelistItem = DateTime.Now - lastRelistItemTime;
            TimeSpan minTime = new TimeSpan(0, 1, 0, 0);

            if (timeSinceLastRelistItem <= minTime)
            {
                return new List<RelistedItem>();
            }

            List<RelistedItem> relistedItems = RelistItem(app, itemIds, itemPrices);
            lastRelistItemTime = DateTime.Now;
            return relistedItems;
        }

        private static void CheckParameters(List<string> itemIds, List<double> itemPrices)
        {
            Checking.NotEmptyList(itemIds, "itemIds");
            Checking.NotEmptyList(itemPrices, "itemPrices");
        }

        private static string GetUrlRequest(AppId.AppName app, List<string> itemIds, List<double> itemPrices)
        {
            const string delimiter = ",";

            Server.UrlCreator urlCreator = new Server.UrlCreator($"https://bitskins.com/api/v1/relist_item/");
            urlCreator.AppendUrl($"&app_id={(int)app}");
            urlCreator.AppendUrl($"&item_ids={itemIds.ToStringWithDelimiter(delimiter)}");
            urlCreator.AppendUrl($"&prices={itemPrices.ToStringWithDelimiter(delimiter)}");

            return urlCreator.ReadUrl();
        }

        private static List<RelistedItem> ReadRelistedItems(string result)
        {
            dynamic responseServerD = JsonConvert.DeserializeObject(result);
            dynamic itemsD = responseServerD.data.items;

            List<RelistedItem> relistedItems = new List<RelistedItem>();
            if (itemsD != null)
            {
                foreach (dynamic item in itemsD)
                {
                    RelistedItem relistedItem = ReadRelistedItem(item);
                    relistedItems.Add(relistedItem);
                }
            }
            
            return relistedItems;
        }

        private static RelistedItem ReadRelistedItem(dynamic item)
        {
            string itemId = item.item_id ?? null;
            bool? instantSale = item.instant_sale ?? null;
            double? price = item.price ?? null;
            DateTime? withdrawableAt = null;
            if (item.withdrawable_at != null)
            {
                withdrawableAt = DateTimeExtension.FromUnixTime((long)item.withdrawable_at);
            }

            RelistedItem relistedItem = new RelistedItem(itemId, instantSale, price, withdrawableAt);
            return relistedItem;
        }
    }

    /// <summary>
    /// Relisted item.
    /// </summary>
    public class RelistedItem
    {
        public string ItemId { get; private set; }
        public bool? InstantSale { get; private set; }
        public double? Price { get; private set; }
        public DateTime? WithdrawableAt { get; private set; }

        internal RelistedItem(string itemId, bool? instantSale, double? price, DateTime? withdrawableAt)
        {
            ItemId = itemId;
            InstantSale = instantSale;
            Price = price;
            WithdrawableAt = withdrawableAt;
        }
    }
}
