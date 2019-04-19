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

namespace BitSkinsApi.Inventory
{
    /// <summary>
    /// Work with items withdrawal.
    /// </summary>
    public static class WithdrawalOfItems
    {
        /// <summary>
        /// Allows you to delist an active sale item and/or re-attempt an item pending withdrawal.
        /// </summary>
        /// <param name="app">Inventory's game id.</param>
        /// <param name="itemIds">List of item id.</param>
        /// <returns>Information about withdrawn.</returns>
        public static InformationAboutWithdrawn WithdrawItem(Market.AppId.AppName app, List<string> itemIds)
        {
            string urlRequest = GetUrlRequest(app, itemIds);
            string result = Server.ServerRequest.RequestServer(urlRequest);
            InformationAboutWithdrawn informationAboutWithdrawn = ReadInformationAboutWithdrawn(result);
            return informationAboutWithdrawn;
        }

        private static string GetUrlRequest(Market.AppId.AppName app, List<string> itemIds)
        {
            const string delimiter = ",";

            Server.UrlCreator urlCreator = new Server.UrlCreator($"https://bitskins.com/api/v1/withdraw_item/");
            urlCreator.AppendUrl($"&app_id={(int)app}");
            urlCreator.AppendUrl($"&item_ids={itemIds.ToStringWithDelimiter(delimiter)}");

            return urlCreator.ReadUrl();
        }

        private static InformationAboutWithdrawn ReadInformationAboutWithdrawn(string result)
        {
            dynamic responseServerD = JsonConvert.DeserializeObject(result);
            dynamic itemsD = responseServerD.data.items;
            dynamic tradeTokensD = responseServerD.data.trade_tokens;

            List<WithdrawnItem> withdrawnItems = ReadWithdrawnItems(itemsD);
            List<string> tradeTokens = ReadTradeTokens(tradeTokensD);

            InformationAboutWithdrawn withdrawnInformation = new InformationAboutWithdrawn(withdrawnItems, tradeTokens);
            return withdrawnInformation;
        }

        private static List<WithdrawnItem> ReadWithdrawnItems(dynamic itemsD)
        {
            List<WithdrawnItem> withdrawnItems = new List<WithdrawnItem>();
            if (itemsD != null)
            {
                foreach (dynamic item in itemsD)
                {
                    WithdrawnItem withdrawnItem = ReadWithdrawnItem(item);
                    withdrawnItems.Add(withdrawnItem);
                }
            }

            return withdrawnItems;
        }

        private static WithdrawnItem ReadWithdrawnItem(dynamic item)
        {
            Market.AppId.AppName appId = (Market.AppId.AppName)(int)item.app_id;
            string itemId = item.item_id;
            DateTime withdrawableAt = DateTimeExtension.FromUnixTime((long)item.withdrawable_at);

            WithdrawnItem withdrawnItem = new WithdrawnItem(appId, itemId, withdrawableAt);
            return withdrawnItem;
        }

        static List<string> ReadTradeTokens(dynamic tradeTokensD)
        {
            List<string> tradeTokens = new List<string>();
            if (tradeTokensD != null)
            {
                foreach (dynamic token in tradeTokensD)
                {
                    string tradeToken = (string)token;
                    tradeTokens.Add(tradeToken);
                }
            }

            return tradeTokens;
        }
    }

    /// <summary>
    /// Information about withdrawn.
    /// </summary>
    public class InformationAboutWithdrawn
    {
        public List<WithdrawnItem> WithdrawnItems { get; private set; }
        public List<string> TradeTokens { get; private set; }

        internal InformationAboutWithdrawn(List<WithdrawnItem> withdrawnItems, List<string> tradeTokens)
        {
            WithdrawnItems = withdrawnItems;
            TradeTokens = tradeTokens;
        }
    }

    /// <summary>
    /// Information about withdrawn item.
    /// </summary>
    public class WithdrawnItem
    {
        public Market.AppId.AppName AppId { get; private set; }
        public string ItemId { get; private set; }
        public DateTime WithdrawableAt { get; private set; }

        internal WithdrawnItem(Market.AppId.AppName appId, string itemId, DateTime withdrawableAt)
        {
            AppId = appId;
            ItemId = itemId;
            WithdrawableAt = withdrawableAt;
        }
    }
}
