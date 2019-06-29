# Re-list the purchased item for sale

In order to re-list the purchased item for sale, you need to call the function:

```csharp
BitSkinsApi.Market.RelistForSale.RelistItem(BitSkinsApi.Market.AppId.AppName app, List<string> itemIds, List<double> itemPrices);
```

## RelistItem()

### Is in class:

```csharp
BitSkinsApi.Market.RelistForSale
```

### Function:

```csharp
BitSkinsApi.Market.RelistForSale.RelistItem(BitSkinsApi.Market.AppId.AppName app, List<string> itemIds, List<double> itemPrices);
```

### Function parameters:

* BitSkinsApi.Market.AppId.AppName app - game that owns items for sale.
* List<string> itemIds - a list of id items for sale.
* List<double> itemPrices - price list of items for sale.

### Returns:

```csharp
List<BitSkinsApi.Market.RelistedItem>
```

Class properties ```BitSkinsApi.Market.RelistedItem```:
* ItemId - item id.
* InstantSale - price of instant sale item.
* Price - item price.
* WithdrawableAt - the date when the item can be withdrawal from BitSkins.

### Possible exceptions
```ArgumentNullException``` - in case of transfer to the function incorrect data, the message contains detailed information.
\
```ArgumentException``` - in case of transfer to the function incorrect data, the message contains detailed information.
\
```BitSkinsApi.Server.RequestServerException``` - in case of transfer to the function incorrect data or problems on the BitSkins server.

## Example

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

[<Delist item from sale](https://github.com/dmitrydnl/BitSkinsApi/blob/master/docs/eng/market/delist_item.md) &nbsp;&nbsp;&nbsp;&nbsp; [Change sale item>](https://github.com/dmitrydnl/BitSkinsApi/blob/master/docs/eng/market/modify_sale.md)