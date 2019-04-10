# Delist item from sale

In order to delist item from sale in BitSkins, you need to call the function:

```csharp
BitSkinsApi.Market.DelistFromSale.DelistItem(BitSkinsApi.Market.AppId.AppName app, List<string> itemIds);
```

## DelistItem()

### Is in class:

```csharp
BitSkinsApi.Market.DelistFromSale
```

### Function:

```csharp
BitSkinsApi.Market.DelistFromSale.DelistItem(BitSkinsApi.Market.AppId.AppName app, List<string> itemIds);
```

### Function parameters:

* BitSkinsApi.Market.AppId.AppName app - a game whose items are removed from sale.
* List<string> itemIds - id list of items that are removed from sale.

### Returns:

```csharp
List<BitSkinsApi.Market.DelistedItem>
```

Class properties ```BitSkinsApi.Market.DelistedItem```:
* ItemId - item id.
* WithdrawableAt - the date when the item can be withdraw from BitSkins.

## Example

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