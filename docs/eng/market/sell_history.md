# Sales history

In order to get sales history in BitSkins, you need to call the function:

```csharp
BitSkinsApi.Market.SellHistory.GetSellHistory(BitSkinsApi.Market.AppId.AppName app, int page);
```

## GetSellHistory()

### Is in class:

```csharp
BitSkinsApi.Market.SellHistory
```

### Function:

```csharp
BitSkinsApi.Market.SellHistory.GetSellHistory(BitSkinsApi.Market.AppId.AppName app, int page);
```

### Function parameters:

* BitSkinsApi.Market.AppId.AppName app - game whose sales history of items is requested.
* int page - page number, the default is 30 events per page.

### Returns:

```csharp
List<BitSkinsApi.Market.SellHistoryRecord>
```

Class properties ```BitSkinsApi.Market.SellHistoryRecord```:
* AppId? - id of the game that owns the item sold.
* ItemId - id of the item sold.
* MarketHashName - title of item sold.
* Price? - selling price.
* Time? - time of sale.

### Possible exceptions
```ArgumentException``` - in case of transfer to the function incorrect data, the message contains detailed information.
\
```BitSkinsApi.Server.RequestServerException``` - in case of transfer to the function incorrect data or problems on the BitSkins server.

## Example

```csharp
BitSkinsApi.Market.AppId.AppName app = BitSkinsApi.Market.AppId.AppName.CounterStrikGlobalOffensive;
List<BitSkinsApi.Market.SellHistoryRecord> sellHistoryRecords = BitSkinsApi.Market.SellHistory.GetSellHistory(app, 1);
foreach (BitSkinsApi.Market.SellHistoryRecord record in sellHistoryRecords)
{
    Console.WriteLine(record.MarketHashName);
    Console.WriteLine(record.Price);
    Console.WriteLine(record.Time);
    Console.WriteLine();
}
```

[<Purchases history](https://github.com/dmitrydnl/BitSkinsApi/blob/master/docs/eng/market/buy_history.md) &nbsp;&nbsp;&nbsp;&nbsp; [Item history>](https://github.com/dmitrydnl/BitSkinsApi/blob/master/docs/eng/market/item_history.md)