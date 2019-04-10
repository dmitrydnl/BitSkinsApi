# Инвентарь аккаунта

Для получения инвентаря аккаунта (инвентарь Steam, выставленные на продажу предметы в BitSkins, предметы ожидающие вывода с BitSkins) нужно вызвать функцию:

```csharp
BitSkinsApi.Inventory.Inventories.GetAccountInventory(BitSkinsApi.Market.AppId.AppName app, int page);
```

## GetAccountInventory()

### Находится в классе:

```csharp
BitSkinsApi.Inventory.Inventories
```

### Функция:

```csharp
BitSkinsApi.Inventory.Inventories.GetAccountInventory(BitSkinsApi.Market.AppId.AppName app, int page);
```

### Параметры функции:
* BitSkinsApi.Market.AppId.AppName app - игра, инвентарь которой запрашивается.
* int page - номер страницы BitSkins инвентаря (предметы в данный момент на продаже).

### Возвращает:

```csharp
BitSkinsApi.Inventory.AccountInventory
```

Свойства класса ```BitSkinsApi.Inventory.AccountInventory```:
* SteamInventory - предметы Steam инвентаря, которые доступны для продажи.
* BitSkinsInventory - предметы BitSkins инвентаря, которые в данный момент находятся на продаже в BitSkins.
* PendingWithdrawalFromBitskinsInventory - предметы ожидающие вывода с BitSkins.

## Пример

```csharp
BitSkinsApi.Market.AppId.AppName app = BitSkinsApi.Market.AppId.AppName.CounterStrikGlobalOffensive;
BitSkinsApi.Inventory.AccountInventory accountInventory = BitSkinsApi.Inventory.Inventories.GetAccountInventory(app, 1);
foreach (BitSkinsApi.Inventory.SteamInventoryItem item in accountInventory.SteamInventory.SteamInventoryItems)
{
    Console.WriteLine(item.MarketHashName);
    foreach (string id in item.ItemIds)
    {
        Console.WriteLine("- " + id);
    }
}
```

[<Вывод денег с баланса](https://github.com/Captious99/BitSkinsApi/blob/master/docs/ru/balance/withdraw_money.md) &nbsp;&nbsp;&nbsp;&nbsp; [Вывод предметов из BitSkins>](https://github.com/Captious99/BitSkinsApi/blob/master/docs/ru/inventory/withdraw_item.md)