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
using BitSkinsApi.Extensions;

namespace BitSkinsApi.Market
{
    /// <summary>
    /// Work with delist from sale.
    /// </summary>
    public static class DelistFromSale
    {
        /// <summary>
        /// Allows you to delist an active sale item.
        /// </summary>
        /// <param name="app">Inventory's game id.</param>
        /// <param name="itemIds">List of item IDs.</param>
        /// <returns>List of delisted items.</returns>
        public static List<DelistedItem> DelistItem(AppId.AppName app, List<string> itemIds)
        {
            CheckParameters(itemIds);
            string urlRequest = GetUrlRequest(app, itemIds);
            string result = Server.ServerRequest.RequestServer(urlRequest);
            List<DelistedItem> delistedItems = ReadDelistedItems(result);
            return delistedItems;
        }

        private static void CheckParameters(List<string> itemIds)
        {
            if (itemIds == null)
            {
                throw new ArgumentNullException("itemIds", "\"itemIds\" must be not null.");
            }
            if (itemIds.Count < 1)
            {
                throw new ArgumentException("In \"itemIds\" count must be at least one.");
            }
        }

        private static string GetUrlRequest(AppId.AppName app, List<string> itemIds)
        {
            const string delimiter = ",";

            Server.UrlCreator urlCreator = new Server.UrlCreator($"https://bitskins.com/api/v1/delist_item/");
            urlCreator.AppendUrl($"&app_id={(int)app}");
            urlCreator.AppendUrl($"&item_ids={itemIds.ToStringWithDelimiter(delimiter)}");

            return urlCreator.ReadUrl();
        }

        private static List<DelistedItem> ReadDelistedItems(string result)
        {
            dynamic responseServerD = JsonConvert.DeserializeObject(result);
            dynamic itemsD = responseServerD.data.items;
            
            List<DelistedItem> delistedItems = new List<DelistedItem>();
            if (itemsD != null)
            {
                foreach (dynamic item in itemsD)
                {
                    DelistedItem delistedItem = ReadDelistedItem(item);
                    delistedItems.Add(delistedItem);
                }
            }

            return delistedItems;
        }

        private static DelistedItem ReadDelistedItem(dynamic item)
        {
            string itemId = item.item_id;
            DateTime withdrawableAt = DateTimeExtension.FromUnixTime((long)item.withdrawable_at);

            DelistedItem delistedItem = new DelistedItem(itemId, withdrawableAt);
            return delistedItem;
        }
    }

    /// <summary>
    /// Delisted item.
    /// </summary>
    public class DelistedItem
    {
        public string ItemId { get; private set; }
        public DateTime WithdrawableAt { get; private set; }

        internal DelistedItem(string itemId, DateTime withdrawableAt)
        {
            ItemId = itemId;
            WithdrawableAt = withdrawableAt;
        }
    }
}
