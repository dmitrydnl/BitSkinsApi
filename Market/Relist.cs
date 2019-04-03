using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using BitSkinsApi.Extensions;

namespace BitSkinsApi.Market
{
    /// <summary>
    /// Work with relist for sale.
    /// </summary>
    public static class RelistForSale
    {
        /// <summary>
        /// Allows you to re-list a delisted/purchased item for sale. Re-listed items can be sold instantly, where applicable.
        /// </summary>
        /// <param name="app">Inventory's game id.</param>
        /// <param name="itemIds">List of item IDs.</param>
        /// <param name="itemPrices">Prices for the item Ids.</param>
        /// <returns>List of relisted items.</returns>
        public static List<RelistedItem> RelistItem(AppId.AppName app, List<string> itemIds, List<double> itemPrices)
        {
            const string delimiter = ",";

            Server.UrlCreator urlCreator = new Server.UrlCreator($"https://bitskins.com/api/v1/relist_item/");
            urlCreator.AppendUrl($"&app_id={(int)app}");
            urlCreator.AppendUrl($"&item_ids={itemIds.ToStringWithDelimiter(delimiter)}");
            urlCreator.AppendUrl($"&prices={itemPrices.ToStringWithDelimiter(delimiter)}");
            
            string result = Server.ServerRequest.RequestServer(urlCreator.ReadUrl());
            List<RelistedItem> relistedItems = ReadRelistedItems(result);
            return relistedItems;
        }

        static List<RelistedItem> ReadRelistedItems(string result)
        {
            dynamic responseServerD = JsonConvert.DeserializeObject(result);
            dynamic itemsD = responseServerD.data.items;

            List<RelistedItem> relistedItems = new List<RelistedItem>();
            if (itemsD != null)
            {
                foreach (dynamic item in itemsD)
                {
                    string itemId = item.item_id;
                    bool instantSale = item.instant_sale;
                    double price = item.price;
                    DateTime withdrawableAt = DateTimeExtension.FromUnixTime((long)item.withdrawable_at);

                    RelistedItem relistedItem = new RelistedItem(itemId, instantSale, price, withdrawableAt);
                    relistedItems.Add(relistedItem);
                }
            }
            
            return relistedItems;
        }
    }

    /// <summary>
    /// Relisted item.
    /// </summary>
    public class RelistedItem
    {
        public string ItemId { get; private set; }
        public bool InstantSale { get; private set; }
        public double Price { get; private set; }
        public DateTime WithdrawableAt { get; private set; }

        internal RelistedItem(string itemId, bool instantSale, double price, DateTime withdrawableAt)
        {
            ItemId = itemId;
            InstantSale = instantSale;
            Price = price;
            WithdrawableAt = withdrawableAt;
        }
    }
}
