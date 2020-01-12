using System;
using System.Collections.Generic;
using NUnit.Framework;
using BitSkinsApi.Market;
using BitSkinsApi.BuyOrder;

namespace BitSkinsApiTest
{
    [TestFixture]
    public class BuyOrderTests
    {
        [Test]
        public void CreateBuyOrderTest()
        {
            AppId.AppName app = AppId.AppName.CounterStrikGlobalOffensive;
            string name = "CS:GO Weapon Case 2";
            double price = 0.01;
            int quantity = 1;

            CreatingBuyOrder.CreateBuyOrder(app, name, price, quantity);

            Assert.IsTrue(true);
        }

        [Test]
        public void GetExpectedPlaceInQueueTest()
        {
            AppId.AppName app = AppId.AppName.CounterStrikGlobalOffensive;
            string name = "CS:GO Weapon Case 2";
            double price = 0.01;

            PlaceInQueue.GetExpectedPlaceInQueue(app, name, price);

            Assert.IsTrue(true);
        }

        [Test]
        public void CancelBuyOrdersTest()
        {
            AppId.AppName app = AppId.AppName.CounterStrikGlobalOffensive;
            string name = "CS:GO Weapon Case 2";
            double price = 0.01;
            int quantity = 2;

            List<BuyOrder> buyOrders = CreatingBuyOrder.CreateBuyOrder(app, name, price, quantity);
            List<string> buyOrderIds = new List<string>();
            foreach (BuyOrder buyOrder in buyOrders)
            {
                buyOrderIds.Add(buyOrder.BuyOrderId);
            }
            CancelingBuyOrders.CancelBuyOrders(app, buyOrderIds);

            Assert.IsTrue(true);
        }

        [Test]
        public void CancelAllBuyOrdersTest()
        {
            AppId.AppName app = AppId.AppName.CounterStrikGlobalOffensive;
            int page = 1;

            List<BuyOrder> buyOrders = MyBuyOrders.GetMyBuyOrders(app, "", MyBuyOrders.BuyOrderType.Listed, page);
            List<string> names = new List<string>();
            while (buyOrders.Count != 0)
            {
                foreach (BuyOrder buyOrder in buyOrders)
                {
                    names.Add(buyOrder.MarketHashName);
                }

                page++;
                buyOrders = MyBuyOrders.GetMyBuyOrders(app, "", MyBuyOrders.BuyOrderType.Listed, page);
            }

            string name = "";
            List<ItemPrice> itemPrices = PriceDatabase.GetAllItemPrices(app);
            int i = 0;
            while (String.IsNullOrEmpty(name))
            {
                string marketHashName = itemPrices[i].MarketHashName;
                if (!names.Contains(marketHashName))
                {
                    name = marketHashName;
                }
                i++;
            }

            CreatingBuyOrder.CreateBuyOrder(app, name, 0.01, 2);
            CancelingBuyOrders.CancelAllBuyOrders(app, name);

            Assert.IsTrue(true);
        }

        [Test]
        public void GetMyBuyOrdersTest()
        {
            foreach (AppId.AppName appId in Enum.GetValues(typeof(AppId.AppName)))
            {
                MyBuyOrders.GetMyBuyOrders(appId, "", MyBuyOrders.BuyOrderType.NotImportant, 1);
            }

            Assert.IsTrue(true);
        }

        [Test]
        public void GetMarketBuyOrdersTest()
        {
            AppId.AppName app = AppId.AppName.CounterStrikGlobalOffensive;
            string name = "CS:GO Weapon Case 2";

            MarketBuyOrders.GetMarketBuyOrders(app, name, 1);

            Assert.IsTrue(true);
        }

        [Test]
        public void SummarizeBuyOrdersTest()
        {
            AppId.AppName app = AppId.AppName.CounterStrikGlobalOffensive;

            SummationBuyOrders.SummarizeBuyOrders(app);

            Assert.IsTrue(true);
        }
    }
}
