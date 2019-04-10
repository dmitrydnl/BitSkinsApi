# Recent sales data

In order to obtain data on the latest sales of the item, you need to call the function:

```csharp
BitSkinsApi.Market.RecentSaleInfo.GetRecentSaleInfo(BitSkinsApi.Market.AppId.AppName app, string marketHashName, int page);
```

## GetRecentSaleInfo()

### Is in class:

```csharp
BitSkinsApi.Market.RecentSaleInfo
```

### Function:

```csharp
BitSkinsApi.Market.RecentSaleInfo.GetRecentSaleInfo(BitSkinsApi.Market.AppId.AppName app, string marketHashName, int page);
```

### Function parameters:

* BitSkinsApi.Market.AppId.AppName app - game that owns the requested item.
* string marketHashName - item name.
* int page - page number (up to 5).

### Returns:

```csharp
List<BitSkinsApi.Market.ItemRecentSale>
```

Class properties ```BitSkinsApi.Market.ItemRecentSale```
* Price - item sales price.
* WearValue - item wear.
* SoldAt - item sale date.

## Example

```csharp
BitSkinsApi.Market.AppId.AppName app = BitSkinsApi.Market.AppId.AppName.CounterStrikGlobalOffensive;
string name = "AK-47 | Case Hardened";
List<BitSkinsApi.Market.ItemRecentSale> itemRecentSales = BitSkinsApi.Market.RecentSaleInfo.GetRecentSaleInfo(app, name, 1);
foreach (BitSkinsApi.Market.ItemRecentSale recentSale in itemRecentSales)
{
    Console.WriteLine(recentSale.Price);
    Console.WriteLine(recentSale.SoldAt);
    Console.WriteLine();
}
```

[<Specific items on sale at BitSkins](https://github.com/Captious99/BitSkinsApi/blob/master/docs/eng/market/get_specific_items_on_sale.md) &nbsp;&nbsp;&nbsp;&nbsp; [Purchases history>](https://github.com/Captious99/BitSkinsApi/blob/master/docs/eng/market/buy_history.md)