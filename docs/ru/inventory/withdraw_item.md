# Вывод предметов из BitSkins

Для того чтобы вывести предметы из BitSkins, нужно вызвать функцию:

```csharp
BitSkinsApi.Inventory.WithdrawalOfItems.WithdrawItem(BitSkinsApi.Market.AppId.AppName app, List<string> itemIds);
```

## WithdrawItem()

### Находится в классе:

```csharp
BitSkinsApi.Inventory.WithdrawalOfItems
```

### Функция:

```csharp
BitSkinsApi.Inventory.WithdrawalOfItems.WithdrawItem(BitSkinsApi.Market.AppId.AppName app, List<string> itemIds);
```

### Параметры функции:

* BitSkinsApi.Market.AppId.AppName app - игра, предметы которой выводятся.
* List<string> itemIds - список id предметов, которые выводятся.

### Возвращает:

```csharp
BitSkinsApi.Inventory.InformationAboutWithdrawn
```

Свойства класса ```BitSkinsApi.Inventory.InformationAboutWithdrawn```:
* WithdrawnItems - предметы, которые были выведены из BitSkins.
* TradeTokens - токены предложений обмена.

## Пример

```csharp
BitSkinsApi.Market.AppId.AppName app = BitSkinsApi.Market.AppId.AppName.CounterStrikGlobalOffensive;
List<string> itemIds = new List<string> { "id1", "id2" };
BitSkinsApi.Inventory.InformationAboutWithdrawn information = BitSkinsApi.Inventory.WithdrawalOfItems.WithdrawItem(app, itemIds);
Console.WriteLine("Trade tokens:");
foreach (string token in information.TradeTokens)
{
    Console.WriteLine(token);
}
Console.WriteLine("Items:");
foreach (BitSkinsApi.Inventory.WithdrawnItem item in information.WithdrawnItems)
{
    Console.WriteLine(item.ItemId);
}
```

[<Инвентарь аккаунта](https://github.com/Captious99/BitSkinsApi/blob/master/docs/ru/inventory/account_inventory.md) &nbsp;&nbsp;&nbsp;&nbsp; [Рыночные данные BitSkins>](https://github.com/Captious99/BitSkinsApi/blob/master/docs/ru/market/market_data.md)