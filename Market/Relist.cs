using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using BitSkinsApi.Extensions;

namespace BitSkinsApi.Market
{
    /// <summary>
    /// Work with relist items.
    /// </summary>
    public static class Relist
    {
        /// <summary>
        /// Allows you to re-list a delisted/purchased item for sale. Re-listed items can be sold instantly, where applicable.
        /// </summary>
        /// <param name="app">For the inventory's game.</param>
        /// <param name="itemIds">List of item IDs.</param>
        /// <param name="itemPrices">Prices for want for the itemIds.</param>
        /// <returns>List of relisted items.</returns>
        public static List<RelistedItem> RelistItem(AppId.AppName app, List<string> itemIds, List<double> itemPrices)
        {
            string delimiter = ",";

            string itemIdsStr = String.Join(delimiter, itemIds);
            string itemPricesStr = String.Join(delimiter, itemPrices.ConvertAll(x => x.ToString(System.Globalization.CultureInfo.InvariantCulture)));

            StringBuilder url = new StringBuilder($"https://bitskins.com/api/v1/relist_item/");
            url.Append($"?api_key={Account.AccountData.GetApiKey()}");
            url.Append($"&app_id={(int)app}");
            url.Append($"&item_ids={itemIdsStr}");
            url.Append($"&prices={itemPricesStr}");
            url.Append($"&code={Account.Secret.GetTwoFactorCode()}");
            
            string result = Server.ServerRequest.RequestServer(url.ToString());
            List<RelistedItem> relistedItems = ReadRelistedItems(result);
            return relistedItems;
        }

        static List<RelistedItem> ReadRelistedItems(string result)
        {
            dynamic responseServer = JsonConvert.DeserializeObject(result);
            dynamic items = responseServer.data.items;

            if (items == null)
            {
                return new List<RelistedItem>();
            }

            List<RelistedItem> relistedItems = new List<RelistedItem>();
            foreach (dynamic item in items)
            {
                string itemId = item.item_id;
                bool instantSale = item.instant_sale;
                double price = item.price;
                DateTime withdrawableAt = DateTimeExtension.FromUnixTime((long)item.withdrawable_at);

                RelistedItem relistedItem = new RelistedItem(itemId, instantSale, price, withdrawableAt);
                relistedItems.Add(relistedItem);
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
