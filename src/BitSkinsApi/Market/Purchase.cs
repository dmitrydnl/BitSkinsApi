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
    /// Work with the purchase.
    /// </summary>
    public static class Purchase
    {
        /// <summary>
        /// Allows you to buy the item currently on sale on BitSkins. Item must not be currently be on sale to you.
        /// </summary>
        /// <param name="app">Inventory's game id.</param>
        /// <param name="itemIds">List of item IDs.</param>
        /// <param name="itemPrices">Prices at which you want to make the purchase. 
        /// Important to specify this in case the prices change by the time you make this call.</param>
        /// <param name="autoTrade">Initiate trade offer for purchased items' delivery.</param>
        /// <param name="allowTradeDelayedPurchases">Use 'true' if you want to purchase items that are trade-delayed.</param>
        /// <returns>List of purchased items.</returns>
        public static List<BoughtItem> BuyItem(AppId.AppName app, List<string> itemIds, List<double> itemPrices, 
            bool autoTrade, bool allowTradeDelayedPurchases)
        {
            CheckParameters(itemIds, itemPrices);
            string urlRequest = GetUrlRequest(app, itemIds, itemPrices, autoTrade, allowTradeDelayedPurchases);
            string result = Server.ServerRequest.RequestServer(urlRequest);
            List<BoughtItem> boughtItems = ReadBoughtItems(result);
            return boughtItems;
        }

        private static void CheckParameters(List<string> itemIds, List<double> itemPrices)
        {
            Checking.NotEmptyList(itemIds, "itemIds");
            Checking.NotEmptyList(itemPrices, "itemPrices");
        }

        private static string GetUrlRequest(AppId.AppName app, List<string> itemIds, List<double> itemPrices,
            bool autoTrade, bool allowTradeDelayedPurchases)
        {
            const string delimiter = ",";

            Server.UrlCreator urlCreator = new Server.UrlCreator($"https://bitskins.com/api/v1/buy_item/");
            urlCreator.AppendUrl($"&app_id={(int)app}");
            urlCreator.AppendUrl($"&item_ids={itemIds.ToStringWithDelimiter(delimiter)}");
            urlCreator.AppendUrl($"&prices={itemPrices.ToStringWithDelimiter(delimiter)}");
            urlCreator.AppendUrl($"&auto_trade={autoTrade}");
            urlCreator.AppendUrl($"&allow_trade_delayed_purchases={allowTradeDelayedPurchases}");

            return urlCreator.ReadUrl();
        }

        private static List<BoughtItem> ReadBoughtItems(string result)
        {
            dynamic responseServerD = JsonConvert.DeserializeObject(result);
            dynamic itemsD = responseServerD.data.items;

            List<BoughtItem> boughtItems = new List<BoughtItem>();
            if (itemsD != null)
            {
                foreach (dynamic item in itemsD)
                {
                    BoughtItem boughtItem = ReadBoughtItem(item);
                    boughtItems.Add(boughtItem);
                }
            }

            return boughtItems;
        }

        private static BoughtItem ReadBoughtItem(dynamic item)
        {
            string itemId = item.item_id;
            string marketHashName = item.market_hash_name;
            double? price = item.price ?? null;
            DateTime? withdrawableAt = null;
            if (item.withdrawable_at != null)
            {
                withdrawableAt = DateTimeExtension.FromUnixTime((long)item.withdrawable_at);
            }

            BoughtItem boughtItem = new BoughtItem(itemId, marketHashName, price, withdrawableAt);
            return boughtItem;
        }
    }

    /// <summary>
    /// Purchased item BitSkins.
    /// </summary>
    public class BoughtItem
    {
        public string ItemId { get; private set; }
        public string MarketHashName { get; private set; }
        public double? Price { get; private set; }
        public DateTime? WithdrawableAt { get; private set; }

        internal BoughtItem(string itemId, string marketHashName, double? price, DateTime? withdrawableAt)
        {
            ItemId = itemId;
            MarketHashName = marketHashName;
            Price = price;
            WithdrawableAt = withdrawableAt;
        }
    }
}
