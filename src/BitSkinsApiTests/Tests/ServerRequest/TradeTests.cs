using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BitSkinsApi.Trade;

namespace BitSkinsApiTests.ServerRequest
{
    [TestClass]
    public class TradeTests
    {
        [TestMethod]
        public void GetRecentTradeOffersTest()
        {
            List<RecentTradeOffer> tradeOffersItems = null;
            tradeOffersItems = RecentOffers.GetRecentTradeOffers(false);
            tradeOffersItems = RecentOffers.GetRecentTradeOffers(true);
        }

        [TestMethod]
        public void GetTradeDetailsTest()
        {
            Dictionary<string, string> tradeTokenAndTradeId = new Dictionary<string, string>();

            List<RecentTradeOffer> recentTradeOffers = RecentOffers.GetRecentTradeOffers(false);
            foreach(RecentTradeOffer recentTradeOffer in recentTradeOffers)
            {
                if (recentTradeOffer.CreatedAt > DateTime.Now.AddDays(-7))
                {
                    if (!tradeTokenAndTradeId.ContainsKey(recentTradeOffer.BitSkinsTradeId))
                    {
                        tradeTokenAndTradeId.Add(recentTradeOffer.BitSkinsTradeId, recentTradeOffer.BitSkinsTradeToken);
                    }
                }
            }
            
            foreach (KeyValuePair<string, string> pair in tradeTokenAndTradeId)
            {
                TradeDetails tradeDetail = Details.GetTradeDetails(pair.Value, pair.Key);
            }
        }
    }
}
