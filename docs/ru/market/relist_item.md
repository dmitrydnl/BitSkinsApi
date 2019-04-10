# Повторно выставить купленный предмет на продажу

Для того чтобы повторно выставить купленный предмет на продажу, нужно вызвать функцию:

```csharp
BitSkinsApi.Market.RelistForSale.RelistItem(BitSkinsApi.Market.AppId.AppName app, List<string> itemIds, List<double> itemPrices);
```

## RelistItem()

### Находится в классе:

```csharp
BitSkinsApi.Market.RelistForSale
```

### Функция:

```csharp
BitSkinsApi.Market.RelistForSale.RelistItem(BitSkinsApi.Market.AppId.AppName app, List<string> itemIds, List<double> itemPrices);
```

### Параметры функции:

* BitSkinsApi.Market.AppId.AppName app - игра, которой принадлежат выставляемые на продажу предметы.
* List<string> itemIds - список id выставляемых на продажу предметов.
* List<double> itemPrices - список цен выставляемых на продажу предметов.

### Возвращает:

```csharp
List<BitSkinsApi.Market.RelistedItem>
```

Свойства класса ```BitSkinsApi.Market.RelistedItem```:
* ItemId - id предмета.
* InstantSale - цена мгновенной продажи предмета.
* Price - цена предмета.
* WithdrawableAt - дата, когда предмет можно будет вывести из BitSkins.

### Возможные исключения
```BitSkinsApi.Server.RequestServerException``` - в случае передачи в функцию некорректных данных или проблем на сервере BitSkins.

## Пример

```csharp
BitSkinsApi.Market.AppId.AppName app = BitSkinsApi.Market.AppId.AppName.CounterStrikGlobalOffensive;
List<string> itemIds = new List<string> { "id1", "id2" };
List<double> itemPrices = new List<double> { 12.1, 42,1 };
List<BitSkinsApi.Market.RelistedItem> relistedItems = BitSkinsApi.Market.RelistForSale.RelistItem(app, itemIds, itemPrices);
foreach (BitSkinsApi.Market.RelistedItem item in relistedItems)
{
    Console.WriteLine(item.ItemId);
    Console.WriteLine(item.Price);
    Console.WriteLine();
}
```

[<Удалить предмет с продажи](https://github.com/Captious99/BitSkinsApi/blob/master/docs/ru/market/delist_item.md) &nbsp;&nbsp;&nbsp;&nbsp; [Изменить продаваемый предмет>](https://github.com/Captious99/BitSkinsApi/blob/master/docs/ru/market/modify_sale.md)