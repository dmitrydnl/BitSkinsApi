# История продаж

Для того чтобы получить вашу историю продаж в BitSkins, нужно вызвать функцию:

```csharp
BitSkinsApi.Market.SellHistory.GetSellHistory(BitSkinsApi.Market.AppId.AppName app, int page);
```

## GetSellHistory()

### Находится в классе:

```csharp
BitSkinsApi.Market.SellHistory
```

### Функция:

```csharp
BitSkinsApi.Market.SellHistory.GetSellHistory(BitSkinsApi.Market.AppId.AppName app, int page);
```

### Параметры функции:

* BitSkinsApi.Market.AppId.AppName app - игра, история продаж предметов которой запрашивается.
* int page - номер страницы, по умолчанию 30 событий на странице.

### Возвращает:

```csharp
List<BitSkinsApi.Market.SellHistoryRecord>
```

Свойства класса ```BitSkinsApi.Market.SellHistoryRecord```:
* AppId - id игры, которой принадлежит проданный предмет.
* ItemId - id проданного предмета.
* MarketHashName - название проданного предмета.
* Price - цена продажи.
* Time - время продажи.

## Пример

```csharp
BitSkinsApi.Market.AppId.AppName app = BitSkinsApi.Market.AppId.AppName.CounterStrikGlobalOffensive;
List<BitSkinsApi.Market.SellHistoryRecord> sellHistoryRecords = BitSkinsApi.Market.SellHistory.GetSellHistory(app, 1);
foreach (BitSkinsApi.Market.SellHistoryRecord record in sellHistoryRecords)
{
    Console.WriteLine(record.MarketHashName);
    Console.WriteLine(record.Price);
    Console.WriteLine(record.Time);
    Console.WriteLine();
}
```

[<История покупок](https://github.com/Captious99/BitSkinsApi/blob/master/docs/ru/market/buy_history.md) &nbsp;&nbsp;&nbsp;&nbsp; [История предмета>](https://github.com/Captious99/BitSkinsApi/blob/master/docs/ru/market/item_history.md)