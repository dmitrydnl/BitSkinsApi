# All items on sale at BitSkins

In order to get the inventory BitSkins currently on sale, you need to call the function:

```csharp
BitSkinsApi.Market.InventoryOnSale.GetInventoryOnSale(BitSkinsApi.Market.AppId.AppName app, int page, string marketHashName, double minPrice, double maxPrice, BitSkinsApi.Market.InventoryOnSale.SortBy sortBy, BitSkinsApi.Market.InventoryOnSale.SortOrder sortOrder, BitSkinsApi.Market.InventoryOnSale.ThreeChoices hasStickers, BitSkinsApi.Market.InventoryOnSale.ThreeChoices isStattrak, BitSkinsApi.Market.InventoryOnSale.ThreeChoices isSouvenir, BitSkinsApi.Market.InventoryOnSale.ResultsPerPage resultsPerPage, BitSkinsApi.Market.InventoryOnSale.ThreeChoices tradeDelayedItems);
```

## DelistItem()

### Is in class:

```csharp
BitSkinsApi.Market.InventoryOnSale
```

### Function:

```csharp
BitSkinsApi.Market.InventoryOnSale.GetInventoryOnSale(BitSkinsApi.Market.AppId.AppName app, int page, string marketHashName, double minPrice, double maxPrice, BitSkinsApi.Market.InventoryOnSale.SortBy sortBy, BitSkinsApi.Market.InventoryOnSale.SortOrder sortOrder, BitSkinsApi.Market.InventoryOnSale.ThreeChoices hasStickers, BitSkinsApi.Market.InventoryOnSale.ThreeChoices isStattrak, BitSkinsApi.Market.InventoryOnSale.ThreeChoices isSouvenir, BitSkinsApi.Market.InventoryOnSale.ResultsPerPage resultsPerPage, BitSkinsApi.Market.InventoryOnSale.ThreeChoices tradeDelayedItems);
```

### Function parameters:

* BitSkinsApi.Market.AppId.AppName app - game whose items are requested.
* int page - page number.
* string marketHashName - full or partial item name.
* double minPrice - minimum price.
*  double maxPrice - maximum price.
*  BitSkinsApi.Market.InventoryOnSale.SortBy sortBy - the value by which the result is sorted.
*  BitSkinsApi.Market.InventoryOnSale.SortOrder sortOrder - sort order (asc/desc).
*  BitSkinsApi.Market.InventoryOnSale.ThreeChoices hasStickers - has the item of stickers (only CS:GO) accepts 3 values: True, False, NotImportant.
*  BitSkinsApi.Market.InventoryOnSale.ThreeChoices isStattrak - is the item of stattrak (only CS:GO) accepts 3 values: True, False, NotImportant.
*  BitSkinsApi.Market.InventoryOnSale.ThreeChoices isSouvenir - is the item a souvenir (only CS:GO) accepts 3 values: True, False, NotImportant.
*  BitSkinsApi.Market.InventoryOnSale.ResultsPerPage resultsPerPage - number of results per page.
*  BitSkinsApi.Market.InventoryOnSale.ThreeChoices tradeDelayedItems - items with a delay of withdrawal (only CS:GO) accepts 3 values: True, False, NotImportant.

### Returns:

```csharp
List<BitSkinsApi.Market.ItemOnSale>
```

Class properties ```BitSkinsApi.Market.ItemOnSale```:
* ItemId - item id.
* MarketHashName - item name.
* ItemType - item type.
* Image - item image link.
* Price - item price.
* SuggestedPrice - suggested price, maybe null, может быть null.
* FloatValue - item wear, maybe null (only CS:GO).
* IsMine - is the item your.
* UpdatedAt - item update date.
* WithdrawableAt - the date when the item can be withdraw from BitSkins.

## Example

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