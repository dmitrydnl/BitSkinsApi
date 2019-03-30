using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using BitSkinsApi.Extensions;

namespace BitSkinsApi.Trade
{
    /// <summary>
    /// Work with BitSkins trade offers.
    /// </summary>
    public static class RecentTradeOffers
    {
        /// <summary>
        /// All types Steam trade offer states.
        /// </summary>
        public enum TradeOfferStateType { Active = 2, Accept = 3, OutOfTime = 6, Cancel = 7, Unknown = 0 };

        /// <summary>
        /// Allows you to retrieve information about 50 most recent trade offers sent by BitSkins. Response contains 'steam_trade_offer_state,' which is '2' if the only is currently active.
        /// </summary>
        /// <param name="activeOnly">Value is 'true' if you only need trade offers currently active.</param>
        /// <returns>List of recent trade offers.</returns>
        public static List<TradeOffer> GetRecentTradeOffers(bool activeOnly)
        {
            StringBuilder url = new StringBuilder($"https://bitskins.com/api/v1/get_recent_trade_offers/");
            url.Append($"?api_key={Account.AccountData.GetApiKey()}");
            url.Append($"&active_only={activeOnly}");
            url.Append($"&code={Account.Secret.GetTwoFactorCode()}");

            string result = Server.ServerRequest.RequestServer(url.ToString());
            List<TradeOffer> tradeOffersItems = ReadTradeOffers(result);
            return tradeOffersItems;
        }

        static List<TradeOffer> ReadTradeOffers(string result)
        {
            dynamic responseServer = JsonConvert.DeserializeObject(result);
            dynamic offers = responseServer.data.offers;

            if (offers == null)
            {
                return new List<TradeOffer>();
            }

            List<TradeOffer> tradeOffers = new List<TradeOffer>();
            foreach (dynamic offer in offers)
            {
                string steamTradeOfferId = offer.steam_trade_offer_id;
                TradeOfferStateType steamTradeOfferState = (TradeOfferStateType)(int)offer.steam_trade_offer_state;
                string senderUid = offer.sender_uid;
                string recipientUid = offer.recipient_uid;
                int numItemsSent = offer.num_items_sent;
                int numItemsRetrieved = offer.num_items_retrieved;
                string tradeMessage = offer.trade_message;
                TradeTokenAndTradeIdFromString(tradeMessage, out string bitSkinsTradeToken, out string bitSkinsTradeId);
                DateTime createdAt = DateTimeExtension.FromUnixTime((long)offer.created_at);
                DateTime updatedAt = DateTimeExtension.FromUnixTime((long)offer.updated_at);

                TradeOffer tradeOffer = new TradeOffer(steamTradeOfferId, steamTradeOfferState, senderUid, recipientUid, numItemsSent,
                    numItemsRetrieved, bitSkinsTradeToken, bitSkinsTradeId, tradeMessage, createdAt, updatedAt);
                tradeOffers.Add(tradeOffer);
            }

            return tradeOffers;
        }

        static void TradeTokenAndTradeIdFromString(string tradeMessage, out string bitSkinsTradeToken, out string bitSkinsTradeId)
        {
            const string tokenStr = "BitSkins Trade Token: ";
            const string idStr = ", Trade ID: ";

            int tokenStart = tradeMessage.IndexOf(tokenStr, StringComparison.InvariantCulture) + tokenStr.Length;
            int tokenEnd = tradeMessage.IndexOf(idStr, StringComparison.InvariantCulture);
            bitSkinsTradeToken = tradeMessage.Remove(tokenEnd).Remove(0, tokenStart);

            int idStart = tradeMessage.IndexOf(idStr, StringComparison.InvariantCulture) + idStr.Length;
            bitSkinsTradeId = tradeMessage.Remove(0, idStart);
        }
    }

    /// <summary>
    /// Info about trade offer.
    /// </summary>
    public class TradeOffer
    {
        public string SteamTradeOfferId { get; private set; }
        public RecentTradeOffers.TradeOfferStateType SteamTradeOfferState { get; private set; }
        public string SenderUid { get; private set; }
        public string RecipientUid { get; private set; }
        public int NumItemsSent { get; private set; }
        public int NumItemsRetrieved { get; private set; }
        public string BitSkinsTradeToken { get; private set; }
        public string BitSkinsTradeId { get; private set; }
        public string TradeMessage { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        internal TradeOffer(string steamTradeOfferId, RecentTradeOffers.TradeOfferStateType steamTradeOfferState, string senderUid, 
            string recipientUid, int numItemsSent, int numItemsRetrieved, string bitSkinsTradeToken, 
            string bitSkinsTradeId, string tradeMessage, DateTime createdAt, DateTime updatedAt)
        {
            SteamTradeOfferId = steamTradeOfferId;
            SteamTradeOfferState = steamTradeOfferState;
            SenderUid = senderUid;
            RecipientUid = recipientUid;
            NumItemsSent = numItemsSent;
            NumItemsRetrieved = numItemsRetrieved;
            BitSkinsTradeToken = bitSkinsTradeToken;
            BitSkinsTradeId = bitSkinsTradeId;
            TradeMessage = tradeMessage;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}
