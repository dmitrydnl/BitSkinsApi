# Specific items on sale at BitSkins

In order to obtain data for specific items that are currently on sale in BitSkins, you need to call the function:

```csharp
BitSkinsApi.Market.SpecificItemsOnSale.GetSpecificItemsOnSale(BitSkinsApi.Market.AppId.AppName app, List<string> itemIds);
```

## GetSpecificItemsOnSale()

### Is in class:

```csharp
BitSkinsApi.Market.SpecificItemsOnSale
```

### Function:

```csharp
BitSkinsApi.Market.SpecificItemsOnSale.GetSpecificItemsOnSale(BitSkinsApi.Market.AppId.AppName app, List<string> itemIds);
```

### Function parameters:

* BitSkinsApi.Market.AppId.AppName app - game whose items are requested.
* List<string> itemIds - list of requested items id.

### Returns:

```csharp
BitSkinsApi.Market.SpecificItems
```

Class properties ```BitSkinsApi.Market.SpecificItems```:
* ItemsOnSale - items that are on sale.
* ItemsNotOnSale - list of id items that are not on sale.

### Possible exceptions
```ArgumentException``` - in case of transfer to the function incorrect data, the message contains detailed information.
\
```BitSkinsApi.Server.RequestServerException``` - in case of transfer to the function incorrect data or problems on the BitSkins server.

## Example

```csharp
BitSkinsApi.Market.AppId.AppName app = BitSkinsApi.Market.AppId.AppName.CounterStrikGlobalOffensive;
List<string> itemIds = new List<string> { "id1", "id2" };

BitSkinsApi.Market.SpecificItems specificItems = BitSkinsApi.Market.SpecificItemsOnSale.GetSpecificItemsOnSale(app, itemIds);
Console.WriteLine("Items on sale:");
foreach (var a in specificItems.ItemsOnSale)
{
    Console.WriteLine(a.MarketHashName);
    Console.WriteLine(a.Price);
    Console.WriteLine();
}
Console.WriteLine("Items not on sale:");
foreach (string itemId in specificItems.ItemsNotOnSale)
{
    Console.WriteLine(itemId);
    Console.WriteLine();
}
```

[<All items on sale at BitSkins](https://github.com/dmitrydnl/BitSkinsApi/blob/master/docs/eng/market/inventory_on_sale.md) &nbsp;&nbsp;&nbsp;&nbsp; [Recent sales data>](https://github.com/dmitrydnl/BitSkinsApi/blob/master/docs/eng/market/recent_sale.md)