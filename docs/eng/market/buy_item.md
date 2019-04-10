# Purchase item

In order to buy a item that is currently being sold on BitSkins, you need to call the function:

```csharp
BitSkinsApi.Market.Purchase.BuyItem(BitSkinsApi.Market.AppId.AppName app, List<string> itemIds, List<double> itemPrices, bool autoTrade, bool allowTradeDelayedPurchases);
```

## BuyItem()

### Is in class:

```csharp
BitSkinsApi.Market.Purchase
```

### Function:

```csharp
BitSkinsApi.Market.Purchase.BuyItem(BitSkinsApi.Market.AppId.AppName app, List<string> itemIds, List<double> itemPrices, bool autoTrade, bool allowTradeDelayedPurchases);
```

### Function parameters:

* BitSkinsApi.Market.AppId.AppName app - game for which price database is requested.
* List<string> itemIds - id list of purchased items.
* List<double> itemPrices - price list of purchased items.
* bool autoTrade - whether to withdraw items to Steam immediately after purchase.
* bool allowTradeDelayedPurchases - whether to allow the purchase of items that currently can not be withdraw from BitSkins.

### Returns:

```csharp
List<BitSkinsApi.Market.BoughtItem>
```

Class properties ```BitSkinsApi.Market.BoughtItem```
* ItemId - item id.
* MarketHashName - item name.
* Price - price.
* WithdrawableAt - the date when the item can be withdraw from BitSkins.

## Example

```csharp
BitSkinsApi.Market.AppId.AppName app = BitSkinsApi.Market.AppId.AppName.CounterStrikGlobalOffensive;
List<string> itemIds = new List<string> { "id1", "id2" };
List<double> itemPrices = new List<double> { 1.12, 4.23 };
List<BitSkinsApi.Market.BoughtItem> boughtItems = BitSkinsApi.Market.Purchase.BuyItem(app, itemIds, itemPrices, false, false);

foreach (BitSkinsApi.Market.BoughtItem item in boughtItems)
{
    Console.WriteLine(item.MarketHashName);
    Console.WriteLine(item.Price);
    Console.WriteLine();
}
```