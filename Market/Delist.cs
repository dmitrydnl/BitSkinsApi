using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using BitSkinsApi.Extensions;

namespace BitSkinsApi.Market
{
    /// <summary>
    /// Work with delist items.
    /// </summary>
    public static class Delist
    {
        /// <summary>
        /// Allows you to delist an active sale item.
        /// </summary>
        /// <param name="app">For the inventory's game.</param>
        /// <param name="itemIds">List of item IDs.</param>
        /// <returns>List of delisted items.</returns>
        public static List<DelistedItem> DelistItem(AppId.AppName app, List<string> itemIds)
        {
            string delimiter = ",";

            string itemIdsStr = String.Join(delimiter, itemIds);

            StringBuilder url = new StringBuilder($"https://bitskins.com/api/v1/delist_item/");
            url.Append($"?api_key={Account.AccountData.GetApiKey()}");
            url.Append($"&app_id={(int)app}");
            url.Append($"&item_ids={itemIdsStr}");
            url.Append($"&code={Account.Secret.GetTwoFactorCode()}");

            string result = Server.ServerRequest.RequestServer(url.ToString());
            List<DelistedItem> delistedItems = ReadDelistedItems(result);
            return delistedItems;
        }

        static List<DelistedItem> ReadDelistedItems(string result)
        {
            dynamic responseServer = JsonConvert.DeserializeObject(result);
            dynamic items = responseServer.data.items;

            if (items == null)
            {
                return new List<DelistedItem>();
            }

            List<DelistedItem> delistedItems = new List<DelistedItem>();
            foreach (dynamic item in items)
            {
                string itemId = item.item_id;
                DateTime withdrawableAt = DateTimeExtension.FromUnixTime((long)item.withdrawable_at);

                DelistedItem delistedItem = new DelistedItem(itemId, withdrawableAt);
                delistedItems.Add(delistedItem);
            }

            return delistedItems;
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
