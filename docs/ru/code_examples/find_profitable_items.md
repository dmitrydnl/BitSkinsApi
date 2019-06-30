# Поиск выгодных предметов в BitSkins

```csharp
using System;
using System.Collections.Generic;
using BitSkinsApi.Account;
using BitSkinsApi.Balance;
using BitSkinsApi.Market;

namespace Example
{
    class Program
    {
        const AppId.AppName app = AppId.AppName.CounterStrikGlobalOffensive;

        static void Main(string[] args)
        {
            AccountData.SetupAccount("API KEY", "SECRET CODE");
            Console.WriteLine("Account was successfully setup");

            double availableBalance = CurrentBalance.GetAccountBalance().AvailableBalance;
            Console.WriteLine($"Account available balance: {availableBalance}");

            List<MarketItem> marketItems = MarketData.GetMarketData(app);
            Console.WriteLine($"Count items: {marketItems.Count}");

            marketItems = SortMarketItems(marketItems, availableBalance);
            Console.WriteLine($"Count items after first sorting: {marketItems.Count}");

            marketItems = SortItemRecentSales(marketItems);
            Console.WriteLine($"Count items after second sorting: {marketItems.Count}");
            
            Console.WriteLine("Profitable items:");
            FindProfitableItems(marketItems);

            Console.ReadKey();
        }

        static List<MarketItem> SortMarketItems(List<MarketItem> marketItems, double availableBalance)
        {
            List<MarketItem> marketItemsSort = new List<MarketItem>();
            foreach (MarketItem item in marketItems)
            {
                int minMarketTotalItems = 10;
                if (item.TotalItems < minMarketTotalItems)
                    continue;

                double minLowestPrice = 0.1;
                double maxLowestPrice = availableBalance / 4;
                if (item.LowestPrice < minLowestPrice || item.LowestPrice > maxLowestPrice)
                    continue;

                double minHighestPrice = item.LowestPrice * 1.3;
                if (item.HighestPrice < minHighestPrice)
                    continue;

                double minRecentSalesAveragePrice = item.LowestPrice * 1.3;
                if (item.RecentAveragePrice < minRecentSalesAveragePrice)
                    continue;


                marketItemsSort.Add(item);
            }

            return marketItemsSort;
        }

        static List<MarketItem> SortItemRecentSales(List<MarketItem> marketItems)
        {
            List<MarketItem> marketItemsSort = new List<MarketItem>();
            foreach (MarketItem item in marketItems)
            {
                List<ItemRecentSale> itemRecentSales = RecentSaleInfo.GetRecentSaleInfo(app, item.MarketHashName, 1);

                int minRecentSales = 5;
                if (itemRecentSales.Count < minRecentSales)
                    continue;

                marketItemsSort.Add(item);
            }

            return marketItemsSort;
        }

        static void FindProfitableItems(List<MarketItem> marketItems)
        {
            foreach (MarketItem item in marketItems)
            {
                List<ItemOnSale> itemsOnSale = InventoryOnSale.GetInventoryOnSale(app, 1, item.MarketHashName, 0, 0, InventoryOnSale.SortBy.Price, 
                    InventoryOnSale.SortOrder.Asc, InventoryOnSale.ThreeChoices.NotImportant, InventoryOnSale.ThreeChoices.NotImportant, 
                    InventoryOnSale.ThreeChoices.NotImportant, InventoryOnSale.ResultsPerPage.R30, InventoryOnSale.ThreeChoices.False);

                if (itemsOnSale.Count < 1)
                    continue;

                if (itemsOnSale[0].Price > item.LowestPrice)
                    continue;

                Console.WriteLine(item.MarketHashName + " ID: " + itemsOnSale[0].ItemId + " PRICE: " + itemsOnSale[0].Price);
            }
        }
    }
}
```

[<Запрос на оплату Bitcoin](https://github.com/dmitrydnl/BitSkinsApi/blob/master/docs/ru/crypto/create_bitcoin_deposit.md) &nbsp;&nbsp;&nbsp;&nbsp; [Вывод доступных предметов с BitSkins>](https://github.com/dmitrydnl/BitSkinsApi/blob/master/docs/ru/code_examples/withdraw_available_items.md)