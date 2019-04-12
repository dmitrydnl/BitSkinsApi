using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using BitSkinsApi.Extensions;

namespace BitSkinsApi.Trade
{
    /// <summary>
    /// Work with trade details.
    /// </summary>
    public static class Details
    {
        /// <summary>
        /// Allows you to retrieve information about items requested/sent in a given trade from BitSkins. 
        /// Trade details will be unretrievable 7 days after the initiation of the trade.
        /// </summary>
        /// <param name="tradeToken">The trade token in the Steam trade's message.</param>
        /// <param name="tradeId">The trade ID in the Steam trade's message.</param>
        /// <returns>Details of this trade.</returns>
        public static TradeDetails GetTradeDetails(string tradeToken, string tradeId)
        {
            string urlRequest = GetUrlRequest(tradeToken, tradeId);
            string result = Server.ServerRequest.RequestServer(urlRequest);
            TradeDetails tradeDetails = ReadTradeDetails(result);

            return tradeDetails;
        }

        private static string GetUrlRequest(string tradeToken, string tradeId)
        {
            Server.UrlCreator urlCreator = new Server.UrlCreator($"https://bitskins.com/api/v1/get_trade_details/");
            urlCreator.AppendUrl($"&trade_token={tradeToken}");
            urlCreator.AppendUrl($"&trade_id={tradeId}");

            return urlCreator.ReadUrl();
        }

        private static TradeDetails ReadTradeDetails(string result)
        {
            dynamic responseServerD = JsonConvert.DeserializeObject(result);
            dynamic createdAtD = responseServerD.data.created_at;
            dynamic itemsSentD = responseServerD.data.items_sent;
            dynamic itemsRetrievedD = responseServerD.data.items_retrieved;

            DateTime? createdAt = ReadCreatedAt(createdAtD);
            List<SentItem> sentItems = ReadSentItems(itemsSentD);
            List<RetrievedItem> retrievedItems = ReadRetrievedItems(itemsRetrievedD);

            TradeDetails tradeDetails = new TradeDetails(sentItems, retrievedItems, createdAt);

            return tradeDetails;
        }

        private static DateTime? ReadCreatedAt(dynamic createdAtD)
        {
            DateTime? createdAt;
            if (createdAtD != null)
            {
                createdAt = DateTimeExtension.FromUnixTime((long)createdAtD);
            }
            else
            {
                createdAt = null;
            }

            return createdAt;
        }

        private static List<SentItem> ReadSentItems(dynamic itemsSentD)
        {
            List<SentItem> sentItems = new List<SentItem>();
            if (itemsSentD != null)
            {
                foreach (dynamic item in itemsSentD)
                {
                    SentItem sentItem = ReadSentItem(item);
                    sentItems.Add(sentItem);
                }
            }

            return sentItems;
        }

        private static SentItem ReadSentItem(dynamic item)
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
            return sentItem;
        }

        private static List<RetrievedItem> ReadRetrievedItems(dynamic itemsRetrievedD)
        {
            List<RetrievedItem> retrievedItems = new List<RetrievedItem>();
            if (itemsRetrievedD != null)
            {
                foreach (dynamic item in itemsRetrievedD)
                {
                    RetrievedItem retrievedItem = ReadRetrievedItem(item);
                    retrievedItems.Add(retrievedItem);
                }
            }

            return retrievedItems;
        }

        private static RetrievedItem ReadRetrievedItem(dynamic item)
        {
            Market.AppId.AppName appId = (Market.AppId.AppName)(int)item.app_id;
            string itemId = item.item_id;

            RetrievedItem retrievedItem = new RetrievedItem(appId, itemId);
            return retrievedItem;
        }
    }

    /// <summary>
    /// Trade's details.
    /// </summary>
    public class TradeDetails
    {
        public List<SentItem> SentItems { get; private set; }
        public List<RetrievedItem> RetrievedItems { get; private set; }
        public DateTime? CreatedAt { get; private set; }

        internal TradeDetails(List<SentItem> sentItems, List<RetrievedItem> retrievedItem, DateTime? createdAt)
        {
            SentItems = sentItems;
            RetrievedItems = retrievedItem;
            CreatedAt = createdAt;
        }
    }

    /// <summary>
    /// Sent items in this trade.
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
    /// Retrieved items in this trade.
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
