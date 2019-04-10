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