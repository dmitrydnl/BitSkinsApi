# Change sale item

In order to change the price of the item you are selling, you need to call the function:

```csharp
BitSkinsApi.Market.ModifySaleItems.ModifySale(BitSkinsApi.Market.AppId.AppName app, List<string> itemIds, List<double> itemPrices);
```

## ModifySale()

### Is in class:

```csharp
BitSkinsApi.Market.ModifySaleItems
```

### Function:

```csharp
BitSkinsApi.Market.ModifySaleItems.ModifySale(BitSkinsApi.Market.AppId.AppName app, List<string> itemIds, List<double> itemPrices);
```

### Function parameters:

* BitSkinsApi.Market.AppId.AppName app - game that owns changeable items.
* List<string> itemIds - list of id items to be changed.
* List<double> itemPrices - list of new prices for items.

### Returns:

```csharp
List<BitSkinsApi.Market.ModifiedItem>
```

Class properties ```BitSkinsApi.Market.ModifiedItem```:
* ItemId - item id.
* MarketHashName - item name.
* Image - item image link.
* Price - new item price.
* OldPrice - old item price.
* Discount - discount.
* WithdrawableAt - date when the item can be withdrawal from BitSkins.

## Example

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