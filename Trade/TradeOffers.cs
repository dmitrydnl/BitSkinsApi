using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using BitSkinsApi.Extensions;

namespace BitSkinsApi.Trade
{
    /// <summary>
    /// Work with BitSkins trade offers.
    /// </summary>
    public static class TradeOffers
    {
        /// <summary>
        /// Allows you to retrieve information about 50 most recent trade offers sent by BitSkins. Response contains 'steam_trade_offer_state,' which is '2' if the only is currently active.
        /// </summary>
        /// <param name="activeOnly">Value is 'true' if you only need trade offers currently active.</param>
        /// <returns>List of recent trade offers.</returns>
        public static List<TradeOffersItem> GetRecentTradeOffers(bool activeOnly)
        {
            string url = $"https://bitskins.com/api/v1/get_recent_trade_offers/?api_key={Account.AccountData.GetApiKey()}&active_only={activeOnly}&code={Account.Secret.GetTwoFactorCode()}";
            if (!Server.ServerRequest.RequestServer(url, out string result))
            {
                throw new Server.RequestServerException(result);
            }

            dynamic responseServer = JsonConvert.DeserializeObject(result);

            if (responseServer.data.offers == null)
            {
                return new List<TradeOffersItem>();
            }

            List<TradeOffersItem> tradeOffersItems = new List<TradeOffersItem>();
            foreach (dynamic item in responseServer.data.offers)
            {
                string steamTradeOfferId = item.steam_trade_offer_id;
                string steamTradeOfferState = item.steam_trade_offer_state;
                string senderUid = item.sender_uid;
                string recipientUid = item.recipient_uid;
                Market.AppId.AppName appId = (Market.AppId.AppName)(int)item.app_id;
                int numItemsSent = item.num_items_sent;
                int numItemsRetrieved = item.num_items_retrieved;
                string tradeMessage = item.trade_message;
                DateTime createdAt = DateTimeExtension.FromUnixTime((long)item.created_at);
                DateTime updatedAt = DateTimeExtension.FromUnixTime((long)item.updated_at);

                TradeOffersItem tradeOffersItem = new TradeOffersItem(steamTradeOfferId, steamTradeOfferState, senderUid, recipientUid, appId,
                    numItemsSent, numItemsRetrieved, tradeMessage, createdAt, updatedAt);
                tradeOffersItems.Add(tradeOffersItem);
            }

            return tradeOffersItems;
        }
    }

    /// <summary>
    /// Info about trade offer.
    /// </summary>
    public class TradeOffersItem
    {
        public string SteamTradeOfferId { get; private set; }
        public string SteamTradeOfferState { get; private set; }
        public string SenderUid { get; private set; }
        public string RecipientUid { get; private set; }
        public Market.AppId.AppName AppId { get; private set; }
        public int NumItemsSent { get; private set; }
        public int NumItemsRetrieved { get; private set; }
        public string TradeMessage { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        internal TradeOffersItem(string steamTradeOfferId, string steamTradeOfferState, string senderUid, string recipientUid,
            Market.AppId.AppName appId, int numItemsSent, int numItemsRetrieved, string tradeMessage, DateTime createdAt, DateTime updatedAt)
        {
            SteamTradeOfferId = steamTradeOfferId;
            SteamTradeOfferState = steamTradeOfferState;
            SenderUid = senderUid;
            RecipientUid = recipientUid;
            AppId = appId;
            NumItemsSent = numItemsSent;
            NumItemsRetrieved = numItemsRetrieved;
            TradeMessage = tradeMessage;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}
