using System;
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
        }
    }
}
