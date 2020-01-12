using System;
using System.Collections.Generic;
using NUnit.Framework;
using BitSkinsApi.Trade;

namespace BitSkinsApiTest
{
    [TestFixture]
    public class TradeTests
    {
        [Test]
        public void GetRecentTradeOffersTest()
        {
            RecentOffers.GetRecentTradeOffers(false);
            RecentOffers.GetRecentTradeOffers(true);

            Assert.IsTrue(true);
        }

        [Test]
        public void GetTradeDetailsTest()
        {
            Dictionary<string, string> tradeTokenAndTradeId = new Dictionary<string, string>();

            List<RecentTradeOffer> recentTradeOffers = RecentOffers.GetRecentTradeOffers(false);
            foreach (RecentTradeOffer recentTradeOffer in recentTradeOffers)
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
                Details.GetTradeDetails(pair.Value, pair.Key);
            }

            Assert.IsTrue(true);
        }
    }
}
