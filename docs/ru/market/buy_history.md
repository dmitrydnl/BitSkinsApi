# История покупок

Для того чтобы получить вашу историю покупок в BitSkins, нужно вызвать функцию:

```csharp
BitSkinsApi.Market.BuyHistory.GetBuyHistory(BitSkinsApi.Market.AppId.AppName app, int page);
```

## GetBuyHistory()

### Находится в классе:

```csharp
BitSkinsApi.Market.BuyHistory
```

### Функция:

```csharp
BitSkinsApi.Market.BuyHistory.GetBuyHistory(BitSkinsApi.Market.AppId.AppName app, int page);
```

### Параметры функции:

* BitSkinsApi.Market.AppId.AppName app - игра, история покупок предметов которой запрашивается.
* int page - номер страницы, по умолчанию 30 событий на странице.

### Возвращает:

```csharp
List<BitSkinsApi.Market.BuyHistoryRecord>
```

Свойства класса ```BitSkinsApi.Market.BuyHistoryRecord```:
* AppId - id игры, которой принадлежит купленный предмет.
* ItemId - id купленного предмета.
* MarketHashName - название купленного предмета.
* Price - цена покупки.
* Withdrawn - можно ли вывести предмет.
* Time - время покупки.

## Пример

```csharp
BitSkinsApi.Market.AppId.AppName app = BitSkinsApi.Market.AppId.AppName.CounterStrikGlobalOffensive;
List<BitSkinsApi.Market.BuyHistoryRecord> buyHistoryRecords = BitSkinsApi.Market.BuyHistory.GetBuyHistory(app, 1);
foreach (BitSkinsApi.Market.BuyHistoryRecord record in buyHistoryRecords)
{
    Console.WriteLine(record.MarketHashName);
    Console.WriteLine(record.Price);
    Console.WriteLine(record.Time);
    Console.WriteLine();
}
```

[<Данные о последних продажах](https://github.com/Captious99/BitSkinsApi/blob/master/docs/ru/market/recent_sale.md) &nbsp;&nbsp;&nbsp;&nbsp; [История продаж>](https://github.com/Captious99/BitSkinsApi/blob/master/docs/ru/market/sell_history.md)