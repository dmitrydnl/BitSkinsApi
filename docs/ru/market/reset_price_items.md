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