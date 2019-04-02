using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using BitSkinsApi.Extensions;

namespace BitSkinsApi.Market
{
    /// <summary>
    /// Work with modify item.
    /// </summary>
    public static class Modify
    {
        /// <summary>
        /// Allows you to change the price on an item currently on sale.
        /// </summary>
        /// <param name="app">For the inventory's game.</param>
        /// <param name="itemIds">Item IDs to modify.</param>
        /// <param name="itemPrices">New item prices, in order of item_ids.</param>
        /// <returns>List of modified items.</returns>
        public static List<ModifiedItem> ModifySale(AppId.AppName app, List<string> itemIds, List<double> itemPrices)
        {
            string delimiter = ",";

            string itemIdsStr = String.Join(delimiter, itemIds);
            string itemPricesStr = String.Join(delimiter, itemPrices.ConvertAll(x => x.ToString(System.Globalization.CultureInfo.InvariantCulture)));

            StringBuilder url = new StringBuilder($"https://bitskins.com/api/v1/modify_sale_item/");
            url.Append($"?api_key={Account.AccountData.GetApiKey()}");
            url.Append($"&app_id={(int)app}");
            url.Append($"&item_ids={itemIdsStr}");
            url.Append($"&prices={itemPricesStr}");
            url.Append($"&code={Account.Secret.GetTwoFactorCode()}");

            string result = Server.ServerRequest.RequestServer(url.ToString());
            List<ModifiedItem> modifiedItems = ReadModifiedItems(result);
            return modifiedItems;
        }

        static List<ModifiedItem> ReadModifiedItems(string result)
        {
            dynamic responseServer = JsonConvert.DeserializeObject(result);
            dynamic items = responseServer.data.items;

            if (items == null)
            {
                return new List<ModifiedItem>();
            }

            List<ModifiedItem> modifiedItems = new List<ModifiedItem>();
            foreach (dynamic item in items)
            {
                string itemId = item.item_id;
                string marketHashName = item.market_hash_name;
                string image = item.image;
                double price = item.price;
                double oldPrice = item.old_price;
                double discount = item.discount;
                DateTime withdrawableAt = DateTimeExtension.FromUnixTime((long)item.withdrawable_at);

                ModifiedItem modifiedItem = new ModifiedItem(itemId, marketHashName, image, price, oldPrice, discount, withdrawableAt);
                modifiedItems.Add(modifiedItem);
            }

            return modifiedItems;
        }
    }

    /// <summary>
    /// Modified item.
    /// </summary>
    public class ModifiedItem
    {
        public string ItemId { get; private set; }
        public string MarketHashName { get; private set; }
        public string Image { get; private set; }
        public double Price { get; private set; }
        public double OldPrice { get; private set; }
        public double Discount { get; private set; }
        public DateTime WithdrawableAt { get; private set; }

        internal ModifiedItem(string itemId, string marketHashName, string image, double price, double oldPrice, double discount, DateTime withdrawableAt)
        {
            ItemId = itemId;
            MarketHashName = marketHashName;
            Image = image;
            Price = price;
            OldPrice = oldPrice;
            Discount = discount;
            WithdrawableAt = withdrawableAt;
        }
    }
}
