# Конкретные товары на продаже в BitSkins

Для того чтобы получить данные для конкретных предметов, которые в данный момент находятся на продаже в BitSkins, нужно вызвать функцию:

```csharp
BitSkinsApi.Market.SpecificItemsOnSale.GetSpecificItemsOnSale(BitSkinsApi.Market.AppId.AppName app, List<string> itemIds);
```

## GetSpecificItemsOnSale()

### Находится в классе:

```csharp
BitSkinsApi.Market.SpecificItemsOnSale
```

### Функция:

```csharp
BitSkinsApi.Market.SpecificItemsOnSale.GetSpecificItemsOnSale(BitSkinsApi.Market.AppId.AppName app, List<string> itemIds);
```

### Параметры функции:

* BitSkinsApi.Market.AppId.AppName app - игра, предметы которой запрашиваются.
* List<string> itemIds - список id запрашиваемых предметов.

### Возвращает:

```csharp
BitSkinsApi.Market.SpecificItems
```

Свойства класса ```BitSkinsApi.Market.SpecificItems```:
* ItemsOnSale - предметы, которые находятся на продаже.
* ItemsNotOnSale - список id предметов, которые не находятся на продаже.

### Возможные исключения
```ArgumentException``` - в случае передачи в функцию некорректных данных, в сообщение содержится подробная информация.
\
```BitSkinsApi.Server.RequestServerException``` - в случае передачи в функцию некорректных данных или проблем на сервере BitSkins.

## Пример

```csharp
BitSkinsApi.Market.AppId.AppName app = BitSkinsApi.Market.AppId.AppName.CounterStrikGlobalOffensive;
List<string> itemIds = new List<string> { "id1", "id2" };

BitSkinsApi.Market.SpecificItems specificItems = BitSkinsApi.Market.SpecificItemsOnSale.GetSpecificItemsOnSale(app, itemIds);
Console.WriteLine("Items on sale:");
foreach (var a in specificItems.ItemsOnSale)
{
    Console.WriteLine(a.MarketHashName);
    Console.WriteLine(a.Price);
    Console.WriteLine();
}
Console.WriteLine("Items not on sale:");
foreach (string itemId in specificItems.ItemsNotOnSale)
{
    Console.WriteLine(itemId);
    Console.WriteLine();
}
```

[<Все вещи на продаже в BitSkins](https://github.com/dmitrydnl/BitSkinsApi/blob/master/docs/ru/market/inventory_on_sale.md) &nbsp;&nbsp;&nbsp;&nbsp; [Данные о последних продажах>](https://github.com/dmitrydnl/BitSkinsApi/blob/master/docs/ru/market/recent_sale.md)