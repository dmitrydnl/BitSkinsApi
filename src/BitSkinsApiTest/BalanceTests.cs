using System.Collections.Generic;
using NUnit.Framework;
using BitSkinsApi.Balance;

namespace BitSkinsApiTest
{
    [TestFixture]
    public class BalanceTests
    {
        [Test]
        public void GetAccountBalanceTest()
        {
            CurrentBalance.GetAccountBalance();

            Assert.IsTrue(true);
        }

        [Test]
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
