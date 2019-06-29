# BitSkins market data

In order to obtain BitSkins market data on all items of a specific game, you need to call the function:

```csharp
BitSkinsApi.Market.MarketData.GetMarketData(BitSkinsApi.Market.AppId.AppName app);
```

## GetMarketData()

### Is in class:

```csharp
BitSkinsApi.Market.MarketData
```

### Function:

```csharp
BitSkinsApi.Market.MarketData.GetMarketData(BitSkinsApi.Market.AppId.AppName app);
```

### Function parameters:

* BitSkinsApi.Market.AppId.AppName app - game whose market data is requested.

### Returns:

```csharp
List<BitSkinsApi.Market.MarketItem>
```

Class properties ```BitSkinsApi.Market.MarketItem```:
* MarketHashName - item name.
* TotalItems - total items.
* LowestPrice - minimum price.
* HighestPrice - maximum price.
* CumulativePrice - cost of all items together.
* RecentAveragePrice - average price lately, may be null.
* UpdatedAt - item update date, may be null.

### Possible exceptions
```BitSkinsApi.Server.RequestServerException``` - in case of transfer to the function incorrect data or problems on the BitSkins server.

## Example

```csharp
BitSkinsApi.Market.AppId.AppName app = BitSkinsApi.Market.AppId.AppName.CounterStrikGlobalOffensive;
List<BitSkinsApi.Market.MarketItem> marketItems = BitSkinsApi.Market.MarketData.GetMarketData(app);
foreach (BitSkinsApi.Market.MarketItem item in marketItems)
{
    Console.WriteLine(item.MarketHashName);
    Console.WriteLine(item.TotalItems);
    Console.WriteLine($"Min price: {item.LowestPrice} Max price: {item.HighestPrice}");
    Console.WriteLine();
}
```

[<Withdraw items from BitSkins](https://github.com/dmitrydnl/BitSkinsApi/blob/master/docs/eng/inventory/withdraw_item.md) &nbsp;&nbsp;&nbsp;&nbsp; [BitSkins price database>](https://github.com/dmitrydnl/BitSkinsApi/blob/master/docs/eng/market/price_database.md)
