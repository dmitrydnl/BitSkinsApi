using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BitSkinsApiTests.ServerRequest
{
    [TestClass]
    public class BalanceTests
    {
        [TestMethod]
        public void GetAccountBalanceTest()
        {
            BitSkinsApi.Balance.AccountBalance balance = BitSkinsApi.Balance.CurrentBalance.GetAccountBalance();
        }

        [TestMethod]
        public void GetMoneyEventsTest()
        {
            int page = 1;
            List<BitSkinsApi.Balance.MoneyEvent> moneyEvents = BitSkinsApi.Balance.MoneyEvents.GetMoneyEvents(page);
            while (moneyEvents.Count != 0)
            {
                page++;
                moneyEvents = BitSkinsApi.Balance.MoneyEvents.GetMoneyEvents(page);
            }
        }
    }
}
