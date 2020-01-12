using System;
using System.Collections.Generic;
using NUnit.Framework;
using BitSkinsApi.Market;
using BitSkinsApi.Inventory;

namespace BitSkinsApiTest
{
    [TestFixture]
    public class MarketTests
    {
        [Test]
        public void GetBuyHistoryTest()
        {
            foreach (AppId.AppName appId in Enum.GetValues(typeof(AppId.AppName)))
            {
                int page = 1;
                List<BuyHistoryRecord> buyHistoryRecords = BuyHistory.GetBuyHistory(appId, page);
                while (buyHistoryRecords.Count != 0)
                {
                    page++;
                    buyHistoryRecords = BuyHistory.GetBuyHistory(appId, page);
                }
            }

            Assert.IsTrue(true);
        }

        [Test]
        public void GetSellHistoryTest()
        {
            foreach (AppId.AppName appId in Enum.GetValues(typeof(AppId.AppName)))
            {
                int page = 1;
                List<SellHistoryRecord> sellHistoryRecords = SellHistory.GetSellHistory(appId, page);
                while (sellHistoryRecords.Count != 0)
                {
                    page++;
                    sellHistoryRecords = SellHistory.GetSellHistory(appId, page);
                }
            }

            Assert.IsTrue(true);
        }

        [Test]
        public void GetItemHistoryTest()
        {
            foreach (AppId.AppName appId in Enum.GetValues(typeof(AppId.AppName)))
            {
                int page = 1;
                List<ItemHistoryRecord> itemHistoryRecords = ItemHistory.GetItemHistory(appId, page, new List<string>(), ItemHistory.ResultsPerPage.R480);
                while (itemHistoryRecords.Count != 0)
                {
                    page++;
                    itemHistoryRecords = ItemHistory.GetItemHistory(appId, page, new List<string>(), ItemHistory.ResultsPerPage.R480);
                }
            }

            Assert.IsTrue(true);
        }

        [Test]
        public void GetMarketDataTest()
        {
            foreach (AppId.AppName appId in Enum.GetValues(typeof(AppId.AppName)))
            {
                MarketData.GetMarketData(appId);
            }

            Assert.IsTrue(true);
        }

        [Test]
        public void GetAllItemPricesTest()
        {
            foreach (AppId.AppName appId in Enum.GetValues(typeof(AppId.AppName)))
            {
                PriceDatabase.GetAllItemPrices(appId);
            }

            Assert.IsTrue(true);
        }

        [Test]
        public void GetRawPriceDataTest()
        {
            Dictionary<string, AppId.AppName> appItems = new Dictionary<string, AppId.AppName>();

            foreach (AppId.AppName appId in Enum.GetValues(typeof(AppId.AppName)))
            {
                const int items = 2;
                Random random = new Random();
                List<MarketItem> marketItems = MarketData.GetMarketData(appId);

                for (int i = 0; i < items; i++)
                {
                    int itemNumber = random.Next(0, marketItems.Count);
                    string itemName = marketItems[itemNumber].MarketHashName;
                    appItems.Add(itemName, appId);
                }
            }

            foreach (KeyValuePair<string, AppId.AppName> item in appItems)
            {
                SteamRawPriceData.GetRawPriceData(item.Value, item.Key);
            }

            Assert.IsTrue(true);
        }

        [Test]
        public void GetRecentSaleInfoTest()
        {
            Dictionary<string, AppId.AppName> appItems = new Dictionary<string, AppId.AppName>();

            foreach (AppId.AppName appId in Enum.GetValues(typeof(AppId.AppName)))
            {
                const int items = 2;
                Random random = new Random();
                List<MarketItem> marketItems = MarketData.GetMarketData(appId);

                for (int i = 0; i < items; i++)
                {
                    int itemNumber = random.Next(0, marketItems.Count);
                    string itemName = marketItems[itemNumber].MarketHashName;
                    appItems.Add(itemName, appId);
                }
            }

            foreach (KeyValuePair<string, AppId.AppName> item in appItems)
            {
                for (int page = 1; page <= 5; page++)
                {
                    RecentSaleInfo.GetRecentSaleInfo(item.Value, item.Key, page);
                }
            }

            Assert.IsTrue(true);
        }

        [Test]
        public void GetInventoryOnSaleTest()
        {
            foreach (AppId.AppName appId in Enum.GetValues(typeof(AppId.AppName)))
            {
                for (int page = 1; page <= 3; page++)
                {
                    try
                    {
                        InventoryOnSale.GetInventoryOnSale(appId, page, "", 0, 0,
                            InventoryOnSale.SortBy.Not,
                            InventoryOnSale.SortOrder.Not,
                            InventoryOnSale.ThreeChoices.NotImportant,
                            InventoryOnSale.ThreeChoices.NotImportant,
                            InventoryOnSale.ThreeChoices.NotImportant,
                            InventoryOnSale.ResultsPerPage.R480,
                            InventoryOnSale.ThreeChoices.NotImportant);
                    }
                    catch (BitSkinsApi.Server.RequestServerException exception)
                    {
                        if (!exception.Message.Contains("(500) Internal Server Error"))
                        {
                            throw exception;
                        }
                    }
                }
            }

            Assert.IsTrue(true);
        }

        [Test]
        public void GetSpecificItemsOnSaleTest()
        {
            foreach (AppId.AppName appId in Enum.GetValues(typeof(AppId.AppName)))
            {
                List<string> ids = new List<string>();

                List<ItemOnSale> itemsOnSale = new List<ItemOnSale>();
                try
                {
                    itemsOnSale = InventoryOnSale.GetInventoryOnSale(appId, 1, "", 0, 0,
                        InventoryOnSale.SortBy.Not,
                        InventoryOnSale.SortOrder.Not,
                        InventoryOnSale.ThreeChoices.NotImportant,
                        InventoryOnSale.ThreeChoices.NotImportant,
                        InventoryOnSale.ThreeChoices.NotImportant,
                        InventoryOnSale.ResultsPerPage.R480,
                        InventoryOnSale.ThreeChoices.NotImportant);
                }
                catch (BitSkinsApi.Server.RequestServerException exception)
                {
                    if (!exception.Message.Contains("(500) Internal Server Error"))
                    {
                        throw exception;
                    }
                }

                if (itemsOnSale.Count > 0)
                {
                    ids.Add(itemsOnSale[0].ItemId);

                    if (itemsOnSale.Count > 1)
                    {
                        ids.Add(itemsOnSale[itemsOnSale.Count - 1].ItemId);
                    }
                }

                if (ids.Count > 0)
                {
                    SpecificItemsOnSale.GetSpecificItemsOnSale(appId, ids);
                }
            }

            Assert.IsTrue(true);
        }

        [Test]
        public void GetResetPriceItems()
        {
            foreach (AppId.AppName appId in Enum.GetValues(typeof(AppId.AppName)))
            {
                int page = 1;
                List<ResetPriceItem> resetPriceItems = ResetPriceItems.GetResetPriceItems(appId, page);
                while (resetPriceItems.Count != 0)
                {
                    page++;
                    resetPriceItems = ResetPriceItems.GetResetPriceItems(appId, page);
                }
            }

            Assert.IsTrue(true);
        }

        [Test]
        public void RelistAndDelistItemTest()
        {
            AppId.AppName app = AppId.AppName.CounterStrikGlobalOffensive;
            BitSkinsInventoryItem item = null;
            foreach (AppId.AppName appId in Enum.GetValues(typeof(AppId.AppName)))
            {
                AccountInventory accountInventorys = Inventories.GetAccountInventory(appId, 1);
                if (accountInventorys.BitSkinsInventory.TotalItems > 0)
                {
                    item = accountInventorys.BitSkinsInventory.BitSkinsInventoryItems[0];
                    app = appId;
                    break;
                }
            }

            if (item != null)
            {
                string itemId = item.ItemIds[0];
                double itemPrice = item.Prices[0];
                Console.WriteLine(item.MarketHashName + " " + itemPrice);
                List<DelistedItem> delistedItems = DelistFromSale.DelistItem(app, new List<string> { itemId });
                Assert.AreEqual(itemId, delistedItems[0].ItemId);
                List<RelistedItem> relistedItems = RelistForSale.RelistItem(app, new List<string> { itemId }, new List<double> { itemPrice });
            }

            Assert.IsTrue(true);
        }

        [Test]
        public void ModifySaleTest()
        {
            AppId.AppName app = AppId.AppName.CounterStrikGlobalOffensive;
            BitSkinsInventoryItem item = null;
            foreach (AppId.AppName appId in Enum.GetValues(typeof(AppId.AppName)))
            {
                AccountInventory accountInventorys = Inventories.GetAccountInventory(appId, 1);
                if (accountInventorys.BitSkinsInventory.TotalItems > 0)
                {
                    item = accountInventorys.BitSkinsInventory.BitSkinsInventoryItems[0];
                    app = appId;
                    break;
                }
            }

            if (item != null)
            {
                string itemId = item.ItemIds[0];
                double itemPrice = item.Prices[0];
                double itemNewPrice = itemPrice * 5;
                List<ModifiedItem> modifiedItems = ModifySaleItems.ModifySale(app, new List<string> { itemId }, new List<double> { itemNewPrice });
                Assert.AreEqual(itemId, modifiedItems[0].ItemId);
                modifiedItems = ModifySaleItems.ModifySale(app, new List<string> { itemId }, new List<double> { itemPrice });
                Assert.AreEqual(itemId, modifiedItems[0].ItemId);
            }

            Assert.IsTrue(true);
        }

        [Test]
        public void BuyItemTest()
        {
            AppId.AppName app = AppId.AppName.CounterStrikGlobalOffensive;
            List<ItemOnSale> itemsOnSale = InventoryOnSale.GetInventoryOnSale(app, 1, "", 0, 0.01, InventoryOnSale.SortBy.Price,
                        InventoryOnSale.SortOrder.Asc, InventoryOnSale.ThreeChoices.NotImportant, InventoryOnSale.ThreeChoices.NotImportant,
                        InventoryOnSale.ThreeChoices.NotImportant, InventoryOnSale.ResultsPerPage.R30, InventoryOnSale.ThreeChoices.False);

            if (itemsOnSale.Count > 0)
            {
                ItemOnSale item = itemsOnSale[0];
                List<BoughtItem> boughtItems = Purchase.BuyItem(app, new List<string> { item.ItemId }, new List<double> { item.Price.Value }, false, false);
                string itemId = boughtItems[0].ItemId;
                Assert.AreEqual(item.ItemId, itemId);
            }

            Assert.IsTrue(true);
        }

        [Test]
        public void SellItemTest()
        {
            AppId.AppName app = AppId.AppName.CounterStrikGlobalOffensive;
            SteamInventoryItem item = null;
            foreach (AppId.AppName appId in Enum.GetValues(typeof(AppId.AppName)))
            {
                AccountInventory accountInventorys = Inventories.GetAccountInventory(appId, 1);
                if (accountInventorys.SteamInventory.TotalItems > 0)
                {
                    item = accountInventorys.SteamInventory.SteamInventoryItems[0];
                    app = appId;
                    break;
                }
            }

            if (item != null)
            {
                string itemId = item.ItemIds[0];
                double itemPrice = item.SuggestedPrice.Value * 5;
                InformationAboutSale information = Sale.SellItem(app, new List<string> { itemId }, new List<double> { itemPrice });
                string id = information.SoldItems[0].ItemId;
                Assert.AreEqual(itemId, id);
            }

            Assert.IsTrue(true);
        }
    }
}
