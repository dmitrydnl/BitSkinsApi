# Покупка предмета

Для того чтобы купить товар, который в настоящее время продается на BitSkins, нужно вызвать функцию:

```csharp
BitSkinsApi.Market.Purchase.BuyItem(BitSkinsApi.Market.AppId.AppName app, List<string> itemIds, List<double> itemPrices, bool autoTrade, bool allowTradeDelayedPurchases);
```

## BuyItem()

### Находится в классе:

```csharp
BitSkinsApi.Market.Purchase
```

### Функция:

```csharp
BitSkinsApi.Market.Purchase.BuyItem(BitSkinsApi.Market.AppId.AppName app, List<string> itemIds, List<double> itemPrices, bool autoTrade, bool allowTradeDelayedPurchases);
```

### Параметры функции:

* BitSkinsApi.Market.AppId.AppName app - игра, которой принадлежат покупаемые предметы.
* List<string> itemIds - списиок id покупаемых предметов.
* List<double> itemPrices - список цен покупаемых предметов.
* bool autoTrade - вывести ли предметы в Steam сразу после покупки.
* bool allowTradeDelayedPurchases - разрешить ли покупку предметов, которые в данный момент нельзя вывести из BitSkins.

### Возвращает:

```csharp
List<BitSkinsApi.Market.BoughtItem>
```

Свойства класса ```BitSkinsApi.Market.BoughtItem```
* ItemId - id предмета.
* MarketHashName - название предмета.
* Price - цена.
* WithdrawableAt - дата, когда предмет можно будет вывести из BitSkins.

## Пример

```csharp
BitSkinsApi.Market.AppId.AppName app = BitSkinsApi.Market.AppId.AppName.CounterStrikGlobalOffensive;
List<string> itemIds = new List<string> { "id1", "id2" };
List<double> itemPrices = new List<double> { 1.12, 4.23 };
List<BitSkinsApi.Market.BoughtItem> boughtItems = BitSkinsApi.Market.Purchase.BuyItem(app, itemIds, itemPrices, false, false);

foreach (BitSkinsApi.Market.BoughtItem item in boughtItems)
{
    Console.WriteLine(item.MarketHashName);
    Console.WriteLine(item.Price);
    Console.WriteLine();
}
```

[<История предмета](https://github.com/Captious99/BitSkinsApi/blob/master/docs/ru/market/item_history.md) &nbsp;&nbsp;&nbsp;&nbsp; [Продажа предмета>](https://github.com/Captious99/BitSkinsApi/blob/master/docs/ru/market/sell_item.md)