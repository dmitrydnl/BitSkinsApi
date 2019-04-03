using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using BitSkinsApi.Extensions;

namespace BitSkinsApi.Market
{
    /// <summary>
    /// Work with modify sale items.
    /// </summary>
    public static class ModifySaleItems
    {
        /// <summary>
        /// Allows you to change the price on an item currently on sale.
        /// </summary>
        /// <param name="app">Inventory's game id.</param>
        /// <param name="itemIds">Item IDs to modify.</param>
        /// <param name="itemPrices">New item prices, in order of item_ids.</param>
        /// <returns>List of modified items.</returns>
        public static List<ModifiedItem> ModifySale(AppId.AppName app, List<string> itemIds, List<double> itemPrices)
        {
            string delimiter = ",";
            string itemIdsStr = String.Join(delimiter, itemIds);
            string itemPricesStr = String.Join(delimiter, itemPrices.ConvertAll(x => x.ToString(System.Globalization.CultureInfo.InvariantCulture)));

            Server.UrlCreator urlCreator = new Server.UrlCreator($"https://bitskins.com/api/v1/modify_sale_item/");
            urlCreator.AppendUrl($"&app_id={(int)app}");
            urlCreator.AppendUrl($"&item_ids={itemIdsStr}");
            urlCreator.AppendUrl($"&prices={itemPricesStr}");

            string result = Server.ServerRequest.RequestServer(urlCreator.ReadUrl());
            List<ModifiedItem> modifiedItems = ReadModifiedItems(result);

            return modifiedItems;
        }

        static List<ModifiedItem> ReadModifiedItems(string result)
        {
            dynamic responseServerD = JsonConvert.DeserializeObject(result);
            dynamic itemsD = responseServerD.data.items;

            List<ModifiedItem> modifiedItems = new List<ModifiedItem>();
            if (itemsD != null)
            {
                foreach (dynamic item in itemsD)
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
            }

            return modifiedItems;
        }
    }

    /// <summary>
    /// Modified sale item.
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
