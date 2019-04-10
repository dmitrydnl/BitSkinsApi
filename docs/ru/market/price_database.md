# База данных цен BitSkins

Для того чтобы получить полную базу цен используемую BitSkins, нужно вызвать функцию:

```csharp
BitSkinsApi.Market.PriceDatabase.GetAllItemPrices(BitSkinsApi.Market.AppId.AppName app);
```

## GetAllItemPrices()

### Находится в классе:

```csharp
BitSkinsApi.Market.PriceDatabase
```

### Функция:

```csharp
BitSkinsApi.Market.PriceDatabase.GetAllItemPrices(BitSkinsApi.Market.AppId.AppName app);
```

### Параметры функции:

* BitSkinsApi.Market.AppId.AppName app - игра, для которой запрашивается база данных цен.

### Возвращает:

```csharp
List<BitSkinsApi.Market.ItemPrice>
```

Свойства класса ```BitSkinsApi.Market.ItemPrice```
* MarketHashName - название предмета.
* Price - цена.
* PricingMode - модель ценообразования.
* CreatedAt - дата создания предмета.
* IconUrl - ссылка на изображение предмета.
* InstantSalePrice - цена моментальной продажи предмета, может быть null.

## Пример

```csharp
BitSkinsApi.Market.AppId.AppName app = BitSkinsApi.Market.AppId.AppName.CounterStrikGlobalOffensive;
List<BitSkinsApi.Market.ItemPrice> itemPrices = BitSkinsApi.Market.PriceDatabase.GetAllItemPrices(app);
foreach (BitSkinsApi.Market.ItemPrice item in itemPrices)
{
    Console.WriteLine(item.MarketHashName);
    Console.WriteLine(item.Price);
    Console.WriteLine();
}
```

[<Рыночные данные BitSkins](https://github.com/Captious99/BitSkinsApi/blob/master/docs/ru/market/market_data.md) &nbsp;&nbsp;&nbsp;&nbsp; [Все вещи на продаже в BitSkins>](https://github.com/Captious99/BitSkinsApi/blob/master/docs/ru/market/inventory_on_sale.md)