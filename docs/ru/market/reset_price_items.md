# Предметы требующие сброса цены

Для того чтобы получить список товаров, для которых необходимо сбросить цены (товары, для которых требуется сброс цены, всегда имеют зарезервированную цену 4985.11), нужно вызвать функцию:

```csharp
BitSkinsApi.Market.ResetPriceItems.GetResetPriceItems(BitSkinsApi.Market.AppId.AppName app, int page);
```

## GetResetPriceItems()

### Находится в классе:

```csharp
BitSkinsApi.Market.ResetPriceItems
```

### Функция:

```csharp
BitSkinsApi.Market.ResetPriceItems.GetResetPriceItems(BitSkinsApi.Market.AppId.AppName app, int page);
```

### Параметры функции:

* BitSkinsApi.Market.AppId.AppName app - игра, которой принадлежат предметы.
* int page - номер страницы.

### Возвращает:

```csharp
List<BitSkinsApi.Market.ResetPriceItem>
```

Свойства класса ``BitSkinsApi.Market.ResetPriceItem```
* MarketHashName - название предмета.
* Price - цена предмета, может быть null.

### Возможные исключения
```BitSkinsApi.Server.RequestServerException``` - в случае передачи в функцию некорректных данных или проблем на сервере BitSkins.

## Пример

```csharp
BitSkinsApi.Market.AppId.AppName app = BitSkinsApi.Market.AppId.AppName.CounterStrikGlobalOffensive;
List<BitSkinsApi.Market.ResetPriceItem> resetPriceItems = BitSkinsApi.Market.ResetPriceItems.GetResetPriceItems(app, 1);
foreach (BitSkinsApi.Market.ResetPriceItem item in resetPriceItems)
{
    Console.WriteLine(item.MarketHashName);
    Console.WriteLine(item.Price);
    Console.WriteLine();
}
```

[<Рыночные данные о стоимости предметов в Steam](https://github.com/Captious99/BitSkinsApi/blob/master/docs/ru/market/steam_price_data.md) &nbsp;&nbsp;&nbsp;&nbsp; [Недавние торговые предложения>](https://github.com/Captious99/BitSkinsApi/blob/master/docs/ru/trade/recent_trade_offers.md)