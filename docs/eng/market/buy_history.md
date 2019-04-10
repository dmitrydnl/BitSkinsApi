# Purchases history

In order to get the purchase history in BitSkins, you need to call the function:

```csharp
BitSkinsApi.Market.BuyHistory.GetBuyHistory(BitSkinsApi.Market.AppId.AppName app, int page);
```

## GetBuyHistory()

### Is in class:

```csharp
BitSkinsApi.Market.BuyHistory
```

### Function:

```csharp
BitSkinsApi.Market.BuyHistory.GetBuyHistory(BitSkinsApi.Market.AppId.AppName app, int page);
```

### Function parameters:

* BitSkinsApi.Market.AppId.AppName app - game, the history of the purchase of items which is requested.
* int page - page number, the default is 30 events per page.

### Returns:

```csharp
List<BitSkinsApi.Market.BuyHistoryRecord>
```

Class properties ```BitSkinsApi.Market.BuyHistoryRecord```:
* AppId - id of the game that owns the purchased item.
* ItemId - purchased item id.
* MarketHashName - title of item purchased.
* Price - purchase price.
* Withdrawn - is it possible to withdraw the item.
* Time - buying time.

## Example

```csharp
BitSkinsApi.Market.AppId.AppName app = BitSkinsApi.Market.AppId.AppName.CounterStrikGlobalOffensive;
List<BitSkinsApi.Market.BuyHistoryRecord> buyHistoryRecords = BitSkinsApi.Market.BuyHistory.GetBuyHistory(app, 1);
foreach (BitSkinsApi.Market.BuyHistoryRecord record in buyHistoryRecords)
{
    Console.WriteLine(record.MarketHashName);
    Console.WriteLine(record.Price);
    Console.WriteLine(record.Time);
    Console.WriteLine();
}
```