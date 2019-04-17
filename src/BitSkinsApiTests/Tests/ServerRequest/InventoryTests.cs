using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BitSkinsApi.Inventory;

namespace BitSkinsApiTests.ServerRequest
{
    [TestClass]
    public class InventoryTests
    {
        [TestMethod]
        public void GetAccountInventoryTest()
        {
            foreach (BitSkinsApi.Market.AppId.AppName appId in Enum.GetValues(typeof(BitSkinsApi.Market.AppId.AppName)))
            {
                int page = 1;
                AccountInventory accountInventorys = Inventories.GetAccountInventory(appId, page);
                while (accountInventorys.BitSkinsInventory.BitSkinsInventoryItems.Count != 0)
                {
                    page++;
                    accountInventorys = Inventories.GetAccountInventory(appId, page);
                }
            }

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void WithdrawItemTest()
        {
            BitSkinsApi.Market.AppId.AppName app = BitSkinsApi.Market.AppId.AppName.CounterStrikGlobalOffensive;
            string itemId = "";
            foreach (BitSkinsApi.Market.AppId.AppName appId in Enum.GetValues(typeof(BitSkinsApi.Market.AppId.AppName)))
            {
                AccountInventory accountInventorys = Inventories.GetAccountInventory(appId, 1);
                foreach (var item in accountInventorys.PendingWithdrawalFromBitskinsInventory.PendingWithdrawalFromBitskinsInventoryItems)
                {
                    if (item.WithdrawableAt < DateTime.Now)
                    {
                        app = appId;
                        itemId = item.ItemId;
                        break;
                    }
                }

                if (!String.IsNullOrEmpty(itemId))
                {
                    break;
                }
            }

            if (!String.IsNullOrEmpty(itemId))
            {
                InformationAboutWithdrawn information = WithdrawalOfItems.WithdrawItem(app, new List<string> { itemId });
                string id = information.WithdrawnItems[0].ItemId;
                Assert.AreEqual(itemId, id);
            }

            Assert.IsTrue(true);
        }
    }
}
