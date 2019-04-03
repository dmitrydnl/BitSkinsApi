using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using BitSkinsApi.Extensions;

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
            const string delimiter = ",";

            Server.UrlCreator urlCreator = new Server.UrlCreator($"https://bitskins.com/api/v1/buy_item/");
            urlCreator.AppendUrl($"&app_id={(int)app}");
            urlCreator.AppendUrl($"&item_ids={itemIds.ToStringWithDelimiter(delimiter)}");
            urlCreator.AppendUrl($"&prices={itemPrices.ToStringWithDelimiter(delimiter)}");
            urlCreator.AppendUrl($"&auto_trade={autoTrade}");
            urlCreator.AppendUrl($"&allow_trade_delayed_purchases={allowTradeDelayedPurchases}");
            
            string result = Server.ServerRequest.RequestServer(urlCreator.ReadUrl());
            List<BoughtItem> boughtItems = ReadBoughtItems(result);
            return boughtItems;
        }

        static List<BoughtItem> ReadBoughtItems(string result)
        {
            dynamic responseServerD = JsonConvert.DeserializeObject(result);
            dynamic itemsD = responseServerD.data.items;

            List<BoughtItem> boughtItems = new List<BoughtItem>();
            if (itemsD != null)
            {
                foreach (dynamic item in itemsD)
                {
                    string itemId = item.item_id;
                    string marketHashName = item.market_hash_name;
                    double price = item.price;
                    DateTime withdrawableAt = DateTimeExtension.FromUnixTime((long)item.withdrawable_at);

                    BoughtItem boughtItem = new BoughtItem(itemId, marketHashName, price, withdrawableAt);
                    boughtItems.Add(boughtItem);
                }
            }

            return boughtItems;
        }
    }

    /// <summary>
    /// Purchased item BitSkins.
    /// </summary>
    public class BoughtItem
    {
        public string ItemId { get; private set; }
        public string MarketHashName { get; private set; }
        public double Price { get; private set; }
        public DateTime WithdrawableAt { get; private set; }

        internal BoughtItem(string itemId, string marketHashName, double price, DateTime withdrawableAt)
        {
            ItemId = itemId;
            MarketHashName = marketHashName;
            Price = price;
            WithdrawableAt = withdrawableAt;
        }
    }
}
