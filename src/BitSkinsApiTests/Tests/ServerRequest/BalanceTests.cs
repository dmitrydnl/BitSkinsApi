using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BitSkinsApi.Balance;

namespace BitSkinsApi.Tests.ServerRequest
{
    [TestClass]
    public class BalanceTests
    {
        [TestMethod]
        public void GetAccountBalanceTest()
        {
            CurrentBalance.GetAccountBalance();

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void GetMoneyEventsTest()
        {
            int page = 1;
            List<MoneyEvent> moneyEvents = MoneyEvents.GetMoneyEvents(page);
            while (moneyEvents.Count != 0)
            {
                page++;
                moneyEvents = MoneyEvents.GetMoneyEvents(page);
            }

            Assert.IsTrue(true);
        }
    }
}
