# Изменить продаваемый предмет

Для того чтобы изменить цену продаваемого вами предмета, нужно вызвать функцию:

```csharp
BitSkinsApi.Market.ModifySaleItems.ModifySale(BitSkinsApi.Market.AppId.AppName app, List<string> itemIds, List<double> itemPrices);
```

## ModifySale()

### Находится в классе:

```csharp
BitSkinsApi.Market.ModifySaleItems
```

### Функция:

```csharp
BitSkinsApi.Market.ModifySaleItems.ModifySale(BitSkinsApi.Market.AppId.AppName app, List<string> itemIds, List<double> itemPrices);
```

### Параметры функции:

* BitSkinsApi.Market.AppId.AppName app - игра, которой принадлежат изменяемые предметы.
* List<string> itemIds - список id изменяемых предметов.
* List<double> itemPrices - список новых цен для предметов.

### Возвращает:

```csharp
List<BitSkinsApi.Market.ModifiedItem>
```

Свойства класса ```BitSkinsApi.Market.ModifiedItem```:
* ItemId - id предмета.
* MarketHashName - название предмета.
* Image - ссылка на изображение предмета.
* Price - новая цена предмета.
* OldPrice - старая цена предмета.
* Discount - скидка.
* WithdrawableAt - дата когда предмет можно будет вывести из BitSkins.

### Возможные исключения
```BitSkinsApi.Server.RequestServerException``` - в случае передачи в функцию некорректных данных или проблем на сервере BitSkins.

## Пример

```csharp
BitSkinsApi.Market.AppId.AppName app = BitSkinsApi.Market.AppId.AppName.CounterStrikGlobalOffensive;
List<string> itemIds = new List<string> { "id1", "id2" };
List<double> itemPrices = new List<double> { 2.28, 34.12 };
List<BitSkinsApi.Market.ModifiedItem> modifiedItems = BitSkinsApi.Market.ModifySaleItems.ModifySale(app, itemIds, itemPrices);

foreach (BitSkinsApi.Market.ModifiedItem item in modifiedItems)
{
    Console.WriteLine(item.MarketHashName);
    Console.WriteLine(item.Price);
    Console.WriteLine(item.OldPrice);
    Console.WriteLine();
}
```

[<Повторно выставить купленный предмет на продажу](https://github.com/Captious99/BitSkinsApi/blob/master/docs/ru/market/relist_item.md) &nbsp;&nbsp;&nbsp;&nbsp; [Рыночные данные о стоимости предметов в Steam>](https://github.com/Captious99/BitSkinsApi/blob/master/docs/ru/market/steam_price_data.md)