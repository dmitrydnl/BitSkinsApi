# Все вещи на продаже в BitSkins

Для того чтобы получить инвентарь BitSkins находящийся в данный момент на продаже, нужно вызвать функцию:

```csharp
BitSkinsApi.Market.InventoryOnSale.GetInventoryOnSale(BitSkinsApi.Market.AppId.AppName app, int page, string marketHashName, double minPrice, double maxPrice, BitSkinsApi.Market.InventoryOnSale.SortBy sortBy, BitSkinsApi.Market.InventoryOnSale.SortOrder sortOrder, BitSkinsApi.Market.InventoryOnSale.ThreeChoices hasStickers, BitSkinsApi.Market.InventoryOnSale.ThreeChoices isStattrak, BitSkinsApi.Market.InventoryOnSale.ThreeChoices isSouvenir, BitSkinsApi.Market.InventoryOnSale.ResultsPerPage resultsPerPage, BitSkinsApi.Market.InventoryOnSale.ThreeChoices tradeDelayedItems);
```

## DelistItem()

### Находится в классе:

```csharp
BitSkinsApi.Market.InventoryOnSale
```

### Функция:

```csharp
BitSkinsApi.Market.InventoryOnSale.GetInventoryOnSale(BitSkinsApi.Market.AppId.AppName app, int page, string marketHashName, double minPrice, double maxPrice, BitSkinsApi.Market.InventoryOnSale.SortBy sortBy, BitSkinsApi.Market.InventoryOnSale.SortOrder sortOrder, BitSkinsApi.Market.InventoryOnSale.ThreeChoices hasStickers, BitSkinsApi.Market.InventoryOnSale.ThreeChoices isStattrak, BitSkinsApi.Market.InventoryOnSale.ThreeChoices isSouvenir, BitSkinsApi.Market.InventoryOnSale.ResultsPerPage resultsPerPage, BitSkinsApi.Market.InventoryOnSale.ThreeChoices tradeDelayedItems);
```

### Параметры функции:

* BitSkinsApi.Market.AppId.AppName app - игра, предметы которой запрашиваются.
* int page - номер страницы.
* string marketHashName - полное или частичное название предмета.
* double minPrice - минимальная цена.
*  double maxPrice - максимальная цена.
*  BitSkinsApi.Market.InventoryOnSale.SortBy sortBy - значение по которому сортируется результат.
*  BitSkinsApi.Market.InventoryOnSale.SortOrder sortOrder - порядок сортировки (asc/desc).
*  BitSkinsApi.Market.InventoryOnSale.ThreeChoices hasStickers - имеетли предмет стикеры (только для CS:GO) принимает 3 значения: True, False, NotImportant.
*  BitSkinsApi.Market.InventoryOnSale.ThreeChoices isStattrak - является ли предмет Stattrak (только для CS:GO) принимает 3 значения: True, False, NotImportant.
*  BitSkinsApi.Market.InventoryOnSale.ThreeChoices isSouvenir - является ли предмет сувенирным  (только для CS:GO) принимает 3 значения: True, False, NotImportant.
*  BitSkinsApi.Market.InventoryOnSale.ResultsPerPage resultsPerPage - количество результатов на странице.
*  BitSkinsApi.Market.InventoryOnSale.ThreeChoices tradeDelayedItems - предметы с задержкой вывода (только для CS:GO) принимает 3 значения: True, False, NotImportant.

### Возвращает:

```csharp
List<BitSkinsApi.Market.ItemOnSale>
```

Свойства класса ```BitSkinsApi.Market.ItemOnSale```:
* ItemId - id предмета.
* MarketHashName - название предмета.
* ItemType - тип предмета.
* Image - ссылка на изображение предмета.
* Price - цена.
* SuggestedPrice - предлагаемая цена, может быть null.
* FloatValue - изношенность предмета, может быть null (только для CS:GO).
* IsMine - является ли предмет вашим.
* UpdatedAt - дата обновления предмета.
* WithdrawableAt - дата, когда предмет можно будет вывести из BitSkins.

### Возможные исключения
```BitSkinsApi.Server.RequestServerException``` - в случае передачи в функцию некорректных данных или проблем на сервере BitSkins.

## Пример

```csharp
BitSkinsApi.Market.AppId.AppName app = BitSkinsApi.Market.AppId.AppName.CounterStrikGlobalOffensive;
string name = "AK-47 | Case Hardened";
double minPrice = 0;
double maxPrice = 50;
BitSkinsApi.Market.InventoryOnSale.SortBy sortBy = BitSkinsApi.Market.InventoryOnSale.SortBy.Price;
BitSkinsApi.Market.InventoryOnSale.SortOrder sortOrder = BitSkinsApi.Market.InventoryOnSale.SortOrder.Asc;
BitSkinsApi.Market.InventoryOnSale.ThreeChoices hasStickers = BitSkinsApi.Market.InventoryOnSale.ThreeChoices.NotImportant;
BitSkinsApi.Market.InventoryOnSale.ThreeChoices isStattrak = BitSkinsApi.Market.InventoryOnSale.ThreeChoices.NotImportant;
BitSkinsApi.Market.InventoryOnSale.ThreeChoices isSouvenir = BitSkinsApi.Market.InventoryOnSale.ThreeChoices.NotImportant;
BitSkinsApi.Market.InventoryOnSale.ResultsPerPage resultsPerPage = BitSkinsApi.Market.InventoryOnSale.ResultsPerPage.R30;
BitSkinsApi.Market.InventoryOnSale.ThreeChoices tradeDelayedItems = BitSkinsApi.Market.InventoryOnSale.ThreeChoices.NotImportant;

List<BitSkinsApi.Market.ItemOnSale> itemsOnSale = BitSkinsApi.Market.InventoryOnSale.GetInventoryOnSale(app, 1, name, 
minPrice, maxPrice, sortBy, sortOrder, hasStickers, isStattrak, isSouvenir, resultsPerPage, tradeDelayedItems);
foreach (BitSkinsApi.Market.ItemOnSale item in itemsOnSale)
{
    Console.WriteLine(item.Price);
    Console.WriteLine(item.ItemId);
    Console.WriteLine();
}
```

[<База данных цен BitSkins](https://github.com/Captious99/BitSkinsApi/blob/master/docs/ru/market/price_database.md) &nbsp;&nbsp;&nbsp;&nbsp; [Конкретные товары на продаже в BitSkins>](https://github.com/Captious99/BitSkinsApi/blob/master/docs/ru/market/specific_items_on_sale.md)