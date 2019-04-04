using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
                BitSkinsApi.Inventory.AccountInventory accountInventorys = BitSkinsApi.Inventory.Inventories.GetAccountInventory(appId, 1);
            }
        }
    }
}
