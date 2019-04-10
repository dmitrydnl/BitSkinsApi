# Item history

In order to get the history of purchases/sales of the item in BitSkins, you need to call the function:

```csharp
BitSkinsApi.Market.ItemHistory.GetItemHistory(BitSkinsApi.Market.AppId.AppName app, int page, List<string> names, BitSkinsApi.Market.ItemHistory.ResultsPerPage resultsPerPage);
```

## GetItemHistory()

### Is in class:

```csharp
BitSkinsApi.Market.ItemHistory
```

### Function:

```csharp
BitSkinsApi.Market.ItemHistory.GetItemHistory(BitSkinsApi.Market.AppId.AppName app, int page, List<string> names, BitSkinsApi.Market.ItemHistory.ResultsPerPage resultsPerPage);
```

### Function parameters:

* BitSkinsApi.Market.AppId.AppName app - game, the history of the item of which is requested.
* int page - page number, the default is 30 events per page.
* List<string> names - list of names of items whose history is requested.
* BitSkinsApi.Market.ItemHistory.ResultsPerPage resultsPerPage - number of events on the page.

### Returns:

```csharp
List<BitSkinsApi.Market.ItemHistoryRecord>
```

Class properties ```BitSkinsApi.Market.ItemHistoryRecord```:
* AppId - id of the game that owns the item.
* ItemId - item id in event.
* MarketHashName - the name of the item in the event.
* Price - price.
* RecordType - event type.
* LastUpdateAt - date of last update event.
* ListedAt - date the item was put up for sale.
* WithdrawnAt - the date when the item can be withdraw from BitSkins, maybe null.
* ListedByMe - is the item for sale by you.
* OnHold - is the item hold.
* OnSale - whether the item is on sale.
* RecordTime - event time, maybe null.

## Example

```csharp
BitSkinsApi.Market.AppId.AppName app = BitSkinsApi.Market.AppId.AppName.CounterStrikGlobalOffensive;
List<string> itemIds = new List<string> { "id1", "id2" };
List<BitSkinsApi.Market.ItemHistoryRecord> records = BitSkinsApi.Market.ItemHistory.GetItemHistory(app, 1, itemIds, BitSkinsApi.Market.ItemHistory.ResultsPerPage.R30);
foreach (BitSkinsApi.Market.ItemHistoryRecord record in records)
{
    Console.WriteLine(record.MarketHashName);
    Console.WriteLine(record.RecordType);
    Console.WriteLine(record.RecordTime);
    Console.WriteLine();
}
```