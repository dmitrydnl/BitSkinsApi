using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BitSkinsApiTests.ServerRequest
{
    [TestClass]
    public class MarketTests
    {
        [TestMethod]
        public void GetBuyHistoryTest()
        {
            foreach (BitSkinsApi.Market.AppId.AppName appId in Enum.GetValues(typeof(BitSkinsApi.Market.AppId.AppName)))
            {
                int page = 1;
                List<BitSkinsApi.Market.BuyHistoryRecord> buyHistoryRecords = BitSkinsApi.Market.BuyHistory.GetBuyHistory(appId, page);
                while (buyHistoryRecords.Count != 0)
                {
                    page++;
                    buyHistoryRecords = BitSkinsApi.Market.BuyHistory.GetBuyHistory(appId, page);
                }
            }
        }

        [TestMethod]
        public void GetSellHistoryTest()
        {
            foreach (BitSkinsApi.Market.AppId.AppName appId in Enum.GetValues(typeof(BitSkinsApi.Market.AppId.AppName)))
            {
                int page = 1;
                List<BitSkinsApi.Market.SellHistoryRecord> sellHistoryRecords = BitSkinsApi.Market.SellHistory.GetSellHistory(appId, page);
                while (sellHistoryRecords.Count != 0)
                {
                    page++;
                    sellHistoryRecords = BitSkinsApi.Market.SellHistory.GetSellHistory(appId, page);
                }
            }
        }

        [TestMethod]
        public void GetItemHistoryTest()
        {
            foreach (BitSkinsApi.Market.AppId.AppName appId in Enum.GetValues(typeof(BitSkinsApi.Market.AppId.AppName)))
            {
                int page = 1;
                List<BitSkinsApi.Market.ItemHistoryRecord> itemHistoryRecords = BitSkinsApi.Market.ItemHistory.GetItemHistory(appId, page,
                        new List<string>(), BitSkinsApi.Market.ItemHistory.ResultsPerPage.R480);
                while (itemHistoryRecords.Count != 0)
                {
                    page++;
                    itemHistoryRecords = BitSkinsApi.Market.ItemHistory.GetItemHistory(appId, page,
                        new List<string>(), BitSkinsApi.Market.ItemHistory.ResultsPerPage.R480);
                }
            }
        }

        [TestMethod]
        public void GetMarketDataTest()
        {
            foreach (BitSkinsApi.Market.AppId.AppName appId in Enum.GetValues(typeof(BitSkinsApi.Market.AppId.AppName)))
            {
                List<BitSkinsApi.Market.MarketItem> marketDataItems = BitSkinsApi.Market.MarketData.GetMarketData(appId);
            }
        }

        [TestMethod]
        public void GetAllItemPricesTest()
        {
            foreach (BitSkinsApi.Market.AppId.AppName appId in Enum.GetValues(typeof(BitSkinsApi.Market.AppId.AppName)))
            {
                List<BitSkinsApi.Market.ItemPrice> priceDatabaseItems = BitSkinsApi.Market.PriceDatabase.GetAllItemPrices(appId);
            }
        }

        [TestMethod]
        public void GetRawPriceDataTest()
        {
            Dictionary<string, BitSkinsApi.Market.AppId.AppName> appItems = new Dictionary<string, BitSkinsApi.Market.AppId.AppName>
            {
                { "AK-47 | Aquamarine Revenge (Battle-Scarred)", BitSkinsApi.Market.AppId.AppName.CounterStrikGlobalOffensive },
                { "Sealed Graffiti | Speechless (Battle Green)", BitSkinsApi.Market.AppId.AppName.CounterStrikGlobalOffensive },
                { "2014 National Electronic Sports Tournament", BitSkinsApi.Market.AppId.AppName.DefenseOfTheAncients2 },
                { "Infused Book of the Vizier Exile", BitSkinsApi.Market.AppId.AppName.DefenseOfTheAncients2 },
                { "2015 Invitational Crate", BitSkinsApi.Market.AppId.AppName.Z1BattleRoyale },
                { "Mandala Effect Pants", BitSkinsApi.Market.AppId.AppName.Z1BattleRoyale },
                { "Haunted Macabre Mask", BitSkinsApi.Market.AppId.AppName.TeamFortress2 },
                { "Strange Specialized Killstreak Frying Pan", BitSkinsApi.Market.AppId.AppName.TeamFortress2 },
                { "BIKER CRATE", BitSkinsApi.Market.AppId.AppName.PlayerUnknownsBattlegrounds },
                { "Padded Jacket (Camo)", BitSkinsApi.Market.AppId.AppName.PlayerUnknownsBattlegrounds },
                { "3D Glasses", BitSkinsApi.Market.AppId.AppName.Unturned },
                { "Harvest Crossbow", BitSkinsApi.Market.AppId.AppName.Unturned },
                { "AK5 RIFLE | Billy, Well-Used", BitSkinsApi.Market.AppId.AppName.PayDay2 },
                { "KRINKOV SUBMACHINE GUN | Chopper, Mint-Condition", BitSkinsApi.Market.AppId.AppName.PayDay2 },
                { "Ammo Wooden Box", BitSkinsApi.Market.AppId.AppName.Rust },
                { "Metal", BitSkinsApi.Market.AppId.AppName.Rust },
                { "Locked Battle Royale Wearables Crate", BitSkinsApi.Market.AppId.AppName.JustSurvive },
                { "Skin: Forest Camo T-Shirt", BitSkinsApi.Market.AppId.AppName.JustSurvive },
                { "Bone Crusher Encrypted USB", BitSkinsApi.Market.AppId.AppName.KillingFloor2 },
                { "Halloween Treat Ticket", BitSkinsApi.Market.AppId.AppName.KillingFloor2 },
                { "Kar98k S | Blut (Battle Hardened)", BitSkinsApi.Market.AppId.AppName.Battalion1944 },
                { "MP40 | Flames (War Torn)", BitSkinsApi.Market.AppId.AppName.Battalion1944 },
                { "DPV - Halftone", BitSkinsApi.Market.AppId.AppName.Depth },
                { "Sea Mine - Cerulean", BitSkinsApi.Market.AppId.AppName.Depth },
                { "TOMAHAWK SHOCK", BitSkinsApi.Market.AppId.AppName.BlackSquad },
                { "TYPE95 MOSAIC", BitSkinsApi.Market.AppId.AppName.BlackSquad }
            };

            foreach (KeyValuePair<string, BitSkinsApi.Market.AppId.AppName> item in appItems)
            {
                BitSkinsApi.Market.SteamItemRawPriceData steamMarketItems = BitSkinsApi.Market.SteamRawPriceData.GetRawPriceData(item.Value, item.Key);
            }
        }

        [TestMethod]
        public void GetRecentSaleInfoTest()
        {
            Dictionary<string, BitSkinsApi.Market.AppId.AppName> appItems = new Dictionary<string, BitSkinsApi.Market.AppId.AppName>
            {
                { "AK-47 | Aquamarine Revenge (Battle-Scarred)", BitSkinsApi.Market.AppId.AppName.CounterStrikGlobalOffensive },
                { "Sealed Graffiti | Speechless (Battle Green)", BitSkinsApi.Market.AppId.AppName.CounterStrikGlobalOffensive },
                { "2014 National Electronic Sports Tournament", BitSkinsApi.Market.AppId.AppName.DefenseOfTheAncients2 },
                { "Infused Book of the Vizier Exile", BitSkinsApi.Market.AppId.AppName.DefenseOfTheAncients2 },
                { "2015 Invitational Crate", BitSkinsApi.Market.AppId.AppName.Z1BattleRoyale },
                { "Mandala Effect Pants", BitSkinsApi.Market.AppId.AppName.Z1BattleRoyale },
                { "Haunted Macabre Mask", BitSkinsApi.Market.AppId.AppName.TeamFortress2 },
                { "Strange Specialized Killstreak Frying Pan", BitSkinsApi.Market.AppId.AppName.TeamFortress2 },
                { "BIKER CRATE", BitSkinsApi.Market.AppId.AppName.PlayerUnknownsBattlegrounds },
                { "Padded Jacket (Camo)", BitSkinsApi.Market.AppId.AppName.PlayerUnknownsBattlegrounds },
                { "3D Glasses", BitSkinsApi.Market.AppId.AppName.Unturned },
                { "Harvest Crossbow", BitSkinsApi.Market.AppId.AppName.Unturned },
                { "AK5 RIFLE | Billy, Well-Used", BitSkinsApi.Market.AppId.AppName.PayDay2 },
                { "KRINKOV SUBMACHINE GUN | Chopper, Mint-Condition", BitSkinsApi.Market.AppId.AppName.PayDay2 },
                { "Ammo Wooden Box", BitSkinsApi.Market.AppId.AppName.Rust },
                { "Metal", BitSkinsApi.Market.AppId.AppName.Rust },
                { "Locked Battle Royale Wearables Crate", BitSkinsApi.Market.AppId.AppName.JustSurvive },
                { "Skin: Forest Camo T-Shirt", BitSkinsApi.Market.AppId.AppName.JustSurvive },
                { "Bone Crusher Encrypted USB", BitSkinsApi.Market.AppId.AppName.KillingFloor2 },
                { "Halloween Treat Ticket", BitSkinsApi.Market.AppId.AppName.KillingFloor2 },
                { "Kar98k S | Blut (Battle Hardened)", BitSkinsApi.Market.AppId.AppName.Battalion1944 },
                { "MP40 | Flames (War Torn)", BitSkinsApi.Market.AppId.AppName.Battalion1944 },
                { "DPV - Halftone", BitSkinsApi.Market.AppId.AppName.Depth },
                { "Sea Mine - Cerulean", BitSkinsApi.Market.AppId.AppName.Depth },
                { "TOMAHAWK SHOCK", BitSkinsApi.Market.AppId.AppName.BlackSquad },
                { "TYPE95 MOSAIC", BitSkinsApi.Market.AppId.AppName.BlackSquad }
            };

            foreach (KeyValuePair<string, BitSkinsApi.Market.AppId.AppName> item in appItems)
            {
                List<BitSkinsApi.Market.ItemRecentSale> recentSaleItems = BitSkinsApi.Market.RecentSaleInfo.GetRecentSaleInfo(item.Value, item.Key, 1);
            }
        }

        [TestMethod]
        public void GetInventoryOnSaleTest()
        {
            foreach (BitSkinsApi.Market.AppId.AppName appId in Enum.GetValues(typeof(BitSkinsApi.Market.AppId.AppName)))
            {
                List<BitSkinsApi.Market.ItemOnSale> itemsOnSale = BitSkinsApi.Market.InventoryOnSale.GetInventoryOnSale(appId, 1, "", 0, 0,
                    BitSkinsApi.Market.InventoryOnSale.SortBy.Not,
                    BitSkinsApi.Market.InventoryOnSale.SortOrder.Not,
                    BitSkinsApi.Market.InventoryOnSale.ThreeChoices.NotImportant,
                    BitSkinsApi.Market.InventoryOnSale.ThreeChoices.NotImportant,
                    BitSkinsApi.Market.InventoryOnSale.ThreeChoices.NotImportant,
                    BitSkinsApi.Market.InventoryOnSale.ResultsPerPage.R480,
                    BitSkinsApi.Market.InventoryOnSale.ThreeChoices.NotImportant);
            }
        }

        [TestMethod]
        public void GetSpecificItemsOnSaleTest()
        {
            List<string> itemIdsCS = new List<string>
            {
                "15735619901",
                "15774801077"
            };
            BitSkinsApi.Market.SpecificItems specificItems = BitSkinsApi.Market.SpecificItemsOnSale.GetSpecificItemsOnSale(
                BitSkinsApi.Market.AppId.AppName.CounterStrikGlobalOffensive, itemIdsCS);

            List<string> itemIdsDOTA = new List<string>
            {
                "15674143492",
                "15668094916"
            };
            specificItems = BitSkinsApi.Market.SpecificItemsOnSale.GetSpecificItemsOnSale(
                BitSkinsApi.Market.AppId.AppName.DefenseOfTheAncients2, itemIdsDOTA);
        }

        [TestMethod]
        public void GetResetPriceItems()
        {
            foreach (BitSkinsApi.Market.AppId.AppName appId in Enum.GetValues(typeof(BitSkinsApi.Market.AppId.AppName)))
            {
                int page = 1;
                List<BitSkinsApi.Market.ResetPriceItem> resetPriceItems = BitSkinsApi.Market.ResetPriceItems.GetResetPriceItems(appId, page);
                while (resetPriceItems.Count != 0)
                {
                    page++;
                    resetPriceItems = BitSkinsApi.Market.ResetPriceItems.GetResetPriceItems(appId, page);
                }
            }
        }
    }
}
