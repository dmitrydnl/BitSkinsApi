using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using BitSkinsApi.Extensions;

namespace BitSkinsApi.Trade
{
    /// <summary>
    /// Wotk with trade details.
    /// </summary>
    public static class TradeDetails
    {
        /// <summary>
        /// Allows you to retrieve information about items requested/sent in a given trade from BitSkins. Trade details will be unretrievable 7 days after the initiation of the trade.
        /// </summary>
        /// <param name="tradeToken">The trade token in the Steam trade's message.</param>
        /// <param name="tradeId">The trade ID in the Steam trade's message.</param>
        /// <returns>Trade detail.</returns>
        public static TradeDetail GetTradeDetails(string tradeToken, string tradeId)
        {
            StringBuilder url = new StringBuilder($"https://bitskins.com/api/v1/get_trade_details/");
            url.Append($"?api_key={Account.AccountData.GetApiKey()}");
            url.Append($"&trade_token={tradeToken}");
            url.Append($"&trade_id={tradeId}");
            url.Append($"&code={Account.Secret.GetTwoFactorCode()}");

            string result = Server.ServerRequest.RequestServer(url.ToString());
            TradeDetail tradeDetail = ReadTradeDetail(result);
            return tradeDetail;
        }

        static TradeDetail ReadTradeDetail(string result)
        {
            dynamic responseServer = JsonConvert.DeserializeObject(result);
            dynamic data = responseServer.data;

            DateTime? createdAt = null;
            if (data.created_at != null)
            {
                createdAt = DateTimeExtension.FromUnixTime((long)data.created_at);
            }

            List<SentItem> sentItems = new List<SentItem>();
            if (data.items_sent != null)
            {
                dynamic itemsSent = data.items_sent;

                foreach (dynamic item in itemsSent)
                {
                    Market.AppId.AppName appId = (Market.AppId.AppName)(int)item.app_id;
                    string itemId = item.item_id;
                    string marketHashName = item.market_hash_name;
                    string image = item.image;
                    double price = item.price;
                    double suggestedPrice = item.suggested_price;
                    DateTime withdrawableAt = DateTimeExtension.FromUnixTime((long)item.withdrawable_at);
                    DateTime? deliveredAt = null;
                    if (item.delivered_at != null)
                    {
                        deliveredAt = DateTimeExtension.FromUnixTime((long)item.delivered_at);
                    }

                    SentItem sentItem = new SentItem(appId, itemId, marketHashName, image, price, suggestedPrice, withdrawableAt, deliveredAt);
                    sentItems.Add(sentItem);
                }
            }

            List<RetrievedItem> retrievedItems = new List<RetrievedItem>();
            if (data.items_retrieved != null)
            {
                dynamic itemsRetrieved = data.items_retrieved;

                foreach (dynamic item in itemsRetrieved)
                {
                    Market.AppId.AppName appId = (Market.AppId.AppName)(int)item.app_id;
                    string itemId = item.item_id;

                    RetrievedItem retrievedItem = new RetrievedItem(appId, itemId);
                    retrievedItems.Add(retrievedItem);
                }
            }

            TradeDetail tradeDetail = new TradeDetail(sentItems, retrievedItems, createdAt);
            return tradeDetail;
        }
    }

    /// <summary>
    /// Info about trade.
    /// </summary>
    public class TradeDetail
    {
        public List<SentItem> SentItems { get; private set; }
        public List<RetrievedItem> RetrievedItems { get; private set; }
        public DateTime? CreatedAt { get; private set; }

        internal TradeDetail(List<SentItem> sentItems, List<RetrievedItem> retrievedItem, DateTime? createdAt)
        {
            SentItems = sentItems;
            RetrievedItems = retrievedItem;
            CreatedAt = createdAt;
        }
    }

    /// <summary>
    /// Sent items in trade.
    /// </summary>
    public class SentItem
    {
        public Market.AppId.AppName AppId { get; private set; }
        public string ItemId { get; private set; }
        public string MarketHashName { get; private set; }
        public string Image { get; private set; }
        public double Price { get; private set; }
        public double SuggestedPrice { get; private set; }
        public DateTime WithdrawableAt { get; private set; }
        public DateTime? DeliveredAt { get; private set; }

        internal SentItem(Market.AppId.AppName appId, string itemId, string marketHashName, string image, 
            double price, double suggestedPrice, DateTime withdrawableAt, DateTime? deliveredAt)
        {
            AppId = appId;
            ItemId = itemId;
            MarketHashName = marketHashName;
            Image = image;
            Price = price;
            SuggestedPrice = suggestedPrice;
            WithdrawableAt = withdrawableAt;
            DeliveredAt = deliveredAt;
        }
    }

    /// <summary>
    /// Retrieved items in trade.
    /// </summary>
    public class RetrievedItem
    {
        public Market.AppId.AppName AppId { get; private set; }
        public string ItemId { get; private set; }

        internal RetrievedItem(Market.AppId.AppName appId, string itemId)
        {
            AppId = appId;
            ItemId = itemId;
        }
    }
}
