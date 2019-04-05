using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BitSkinsApi.Balance;

namespace BitSkinsApiTests.ServerRequest
{
    [TestClass]
    public class BalanceTests
    {
        [TestMethod]
        public void GetAccountBalanceTest()
        {
            AccountBalance balance = CurrentBalance.GetAccountBalance();
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
        }
    }
}
