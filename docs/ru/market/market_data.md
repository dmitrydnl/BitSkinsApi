# Рыночные данные BitSkins

Для того чтобы получить рыночные данные BitSkins по всем предметам конкретной игры, нужно вызвать функцию:

```csharp
BitSkinsApi.Market.MarketData.GetMarketData(BitSkinsApi.Market.AppId.AppName app);
```

## GetMarketData()

### Находится в классе:

```csharp
BitSkinsApi.Market.MarketData
```

### Функция:

```csharp
BitSkinsApi.Market.MarketData.GetMarketData(BitSkinsApi.Market.AppId.AppName app);
```

### Параметры функции:

* BitSkinsApi.Market.AppId.AppName app - игра, рыночные данные которой запрашиваются.

### Возвращает:

```csharp
List<BitSkinsApi.Market.MarketItem>
```

Свойства класса ```BitSkinsApi.Market.MarketItem```:
* MarketHashName - название предмета.
* TotalItems - всего предметов.
* LowestPrice - минимальная цена.
* HighestPrice - максимальная цена.
* CumulativePrice - стоимость всех предметов вместе.
* RecentAveragePrice - средняя цена за последнее время.
* UpdatedAt - дата обновления предмета, может быть null.

### Возможные исключения
```BitSkinsApi.Server.RequestServerException``` - в случае передачи в функцию некорректных данных или проблем на сервере BitSkins.

## Пример

```csharp
BitSkinsApi.Market.AppId.AppName app = BitSkinsApi.Market.AppId.AppName.CounterStrikGlobalOffensive;
List<BitSkinsApi.Market.MarketItem> marketItems = BitSkinsApi.Market.MarketData.GetMarketData(app);
foreach (BitSkinsApi.Market.MarketItem item in marketItems)
{
    Console.WriteLine(item.MarketHashName);
    Console.WriteLine(item.TotalItems);
    Console.WriteLine($"Min price: {item.LowestPrice} Max price: {item.HighestPrice}");
    Console.WriteLine();
}
```

[<Вывод предметов из BitSkins](https://github.com/Captious99/BitSkinsApi/blob/master/docs/ru/inventory/withdraw_item.md) &nbsp;&nbsp;&nbsp;&nbsp; [База данных цен BitSkins>](https://github.com/Captious99/BitSkinsApi/blob/master/docs/ru/market/price_database.md)