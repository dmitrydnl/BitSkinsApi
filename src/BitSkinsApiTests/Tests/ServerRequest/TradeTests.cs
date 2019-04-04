using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BitSkinsApiTests.ServerRequest
{
    [TestClass]
    public class TradeTests
    {
        [TestMethod]
        public void GetRecentTradeOffersTest()
        {
            List<BitSkinsApi.Trade.RecentTradeOffer> tradeOffersItems = null;
            tradeOffersItems = BitSkinsApi.Trade.RecentOffers.GetRecentTradeOffers(false);
            tradeOffersItems = BitSkinsApi.Trade.RecentOffers.GetRecentTradeOffers(true);
        }

        [TestMethod]
        public void GetTradeDetailsTest()
        {
            Dictionary<string, string> tradeTokenAndTradeId = new Dictionary<string, string>
            {
                { "bd89a3c46ec053fb", "9b8c8840fcf0c473" },
                { "32449bf82d702137", "0eb62f7c7d8387d1" }
            };

            foreach (KeyValuePair<string, string> pair in tradeTokenAndTradeId)
            {
                BitSkinsApi.Trade.TradeDetails tradeDetail = BitSkinsApi.Trade.Details.GetTradeDetails(pair.Key, pair.Value);
            }
        }
    }
}
