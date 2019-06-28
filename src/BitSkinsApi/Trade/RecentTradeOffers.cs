/*
 * BitSkinsApi
 * Copyright (C) 2019 dmitrydnl
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using BitSkinsApi.Extensions;

namespace BitSkinsApi.Trade
{
    /// <summary>
    /// Work with trade offers.
    /// </summary>
    public static class RecentOffers
    {
        /// <summary>
        /// Types status Steam trade offer.
        /// </summary>
        public enum TradeOfferStatusType { Active = 2, Accept = 3, OutOfTime = 6, Cancel = 7, Unknown = 0 };

        /// <summary>
        /// Allows you to retrieve information about 50 most recent trade offers sent by BitSkins.
        /// </summary>
        /// <param name="activeOnly">Value is 'true' if you only need trade offers currently active.</param>
        /// <returns>List of recent trade offers.</returns>
        public static List<RecentTradeOffer> GetRecentTradeOffers(bool activeOnly)
        {
            string urlRequest = GetUrlRequest(activeOnly);
            string result = Server.ServerRequest.RequestServer(urlRequest);
            List<RecentTradeOffer> recentTradeOffers = ReadRecentTradeOffers(result);
            return recentTradeOffers;
        }

        private static string GetUrlRequest(bool activeOnly)
        {
            Server.UrlCreator urlCreator = new Server.UrlCreator($"https://bitskins.com/api/v1/get_recent_trade_offers/");
            urlCreator.AppendUrl($"&active_only={activeOnly}");

            return urlCreator.ReadUrl();
        }

        private static List<RecentTradeOffer> ReadRecentTradeOffers(string result)
        {
            dynamic responseServerD = JsonConvert.DeserializeObject(result);
            dynamic offersD = responseServerD.data.offers;

            List<RecentTradeOffer> recentTradeOffers = new List<RecentTradeOffer>();
            if (offersD != null)
            {
                foreach (dynamic offer in offersD)
                {
                    RecentTradeOffer recentTradeOffer = ReadRecentTradeOffer(offer);
                    recentTradeOffers.Add(recentTradeOffer);
                }
            }

            return recentTradeOffers;
        }

        private static RecentTradeOffer ReadRecentTradeOffer(dynamic offer)
        {
            string steamTradeOfferId = offer.steam_trade_offer_id;
            TradeOfferStatusType steamTradeOfferStatus = (TradeOfferStatusType)(int)offer.steam_trade_offer_state;
            string senderUid = offer.sender_uid;
            string recipientUid = offer.recipient_uid;
            int numItemsSent = offer.num_items_sent;
            int numItemsRetrieved = offer.num_items_retrieved;
            string tradeMessage = offer.trade_message;
            TradeTokenAndTradeIdFromString(tradeMessage, out string bitSkinsTradeToken, out string bitSkinsTradeId);
            DateTime createdAt = DateTimeExtension.FromUnixTime((long)offer.created_at);
            DateTime updatedAt = DateTimeExtension.FromUnixTime((long)offer.updated_at);

            RecentTradeOffer recentTradeOffer = new RecentTradeOffer(steamTradeOfferId, steamTradeOfferStatus, senderUid, recipientUid, numItemsSent,
                numItemsRetrieved, bitSkinsTradeToken, bitSkinsTradeId, tradeMessage, createdAt, updatedAt);
            return recentTradeOffer;
        }

        private static void TradeTokenAndTradeIdFromString(string tradeMessage, out string bitSkinsTradeToken, out string bitSkinsTradeId)
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
    /// Information about trade offer.
    /// </summary>
    public class RecentTradeOffer
    {
        public string SteamTradeOfferId { get; private set; }
        public RecentOffers.TradeOfferStatusType SteamTradeOfferStatus { get; private set; }
        public string SenderUid { get; private set; }
        public string RecipientUid { get; private set; }
        public int NumItemsSent { get; private set; }
        public int NumItemsRetrieved { get; private set; }
        public string BitSkinsTradeToken { get; private set; }
        public string BitSkinsTradeId { get; private set; }
        public string TradeMessage { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        internal RecentTradeOffer(string steamTradeOfferId, RecentOffers.TradeOfferStatusType steamTradeOfferStatus, string senderUid, 
            string recipientUid, int numItemsSent, int numItemsRetrieved, string bitSkinsTradeToken, 
            string bitSkinsTradeId, string tradeMessage, DateTime createdAt, DateTime updatedAt)
        {
            SteamTradeOfferId = steamTradeOfferId;
            SteamTradeOfferStatus = steamTradeOfferStatus;
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
