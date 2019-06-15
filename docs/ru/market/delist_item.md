# Удалить предмет с продажи

Для того чтобы удалить предмет с продажи в BitSkins, нужно вызвать функцию:

```csharp
BitSkinsApi.Market.DelistFromSale.DelistItem(BitSkinsApi.Market.AppId.AppName app, List<string> itemIds);
```

## DelistItem()

### Находится в классе:

```csharp
BitSkinsApi.Market.DelistFromSale
```

### Функция:

```csharp
BitSkinsApi.Market.DelistFromSale.DelistItem(BitSkinsApi.Market.AppId.AppName app, List<string> itemIds);
```

### Параметры функции:

* BitSkinsApi.Market.AppId.AppName app - игра, предметы которой удаляются с продажи.
* List<string> itemIds - список id предметов, которые удаляются с продажи.

### Возвращает:

```csharp
List<BitSkinsApi.Market.DelistedItem>
```

Свойства класса ```BitSkinsApi.Market.DelistedItem```:
* ItemId - id предмета.
* WithdrawableAt - дата, когда предмет можно будет вывести из BitSkins.

### Возможные исключения
```ArgumentNullException``` - в случае передачи в функцию некорректных данных, в сообщение содержится подробная информация.
\
```ArgumentException``` - в случае передачи в функцию некорректных данных, в сообщение содержится подробная информация.
\
```BitSkinsApi.Server.RequestServerException``` - в случае передачи в функцию некорректных данных или проблем на сервере BitSkins.

## Пример

```csharp
BitSkinsApi.Market.AppId.AppName app = BitSkinsApi.Market.AppId.AppName.CounterStrikGlobalOffensive;
List<string> itemIds = new List<string> { "id1", "id2" };
List<BitSkinsApi.Market.DelistedItem> delistedItems = BitSkinsApi.Market.DelistFromSale.DelistItem(app, itemIds);
foreach (BitSkinsApi.Market.DelistedItem item in delistedItems)
{
    Console.WriteLine(item.ItemId);
    Console.WriteLine(item.WithdrawableAt);
    Console.WriteLine();
}
```

[<Продажа предмета](https://github.com/Captious99/BitSkinsApi/blob/master/docs/ru/market/sell_item.md) &nbsp;&nbsp;&nbsp;&nbsp; [Повторно выставить купленный предмет на продажу>](https://github.com/Captious99/BitSkinsApi/blob/master/docs/ru/market/relist_item.md)