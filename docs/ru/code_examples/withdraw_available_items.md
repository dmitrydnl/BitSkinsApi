# Вывод доступных предметов с BitSkins

```csharp
using System;
using System.Collections.Generic;
using BitSkinsApi.Account;
using BitSkinsApi.Inventory;
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

            AccountInventory accountInventory = Inventories.GetAccountInventory(app, 1);
            Console.WriteLine($"Total items pending withdrawal from BitSkins inventory: " +
                $"{accountInventory.PendingWithdrawalFromBitskinsInventory.TotalItems}");

            List<string> itemsIds = new List<string>();
            foreach(var inventoryItem in accountInventory.PendingWithdrawalFromBitskinsInventory.PendingWithdrawalFromBitskinsInventoryItems)
            {
                if (inventoryItem.WithdrawableAt <= DateTime.Now)
                    itemsIds.Add(inventoryItem.ItemId);
            }
            Console.WriteLine($"Total items that would be withdrawal: {itemsIds.Count}");

            WithdrawalOfItems.WithdrawItem(app, itemsIds);
            Console.WriteLine("Items was withdrawal");

            Console.ReadKey();
        }
    }
}
```

[<Поиск выгодных предметов в BitSkins](https://github.com/Captious99/BitSkinsApi/blob/master/docs/ru/code_examples/find_profitable_items.md)