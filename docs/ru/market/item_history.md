# История предмета

Для того чтобы получить историю покупок/продаж предмета в BitSkins, нужно вызвать функцию:

```csharp
BitSkinsApi.Market.ItemHistory.GetItemHistory(BitSkinsApi.Market.AppId.AppName app, int page, List<string> names, BitSkinsApi.Market.ItemHistory.ResultsPerPage resultsPerPage);
```

## GetItemHistory()

### Находится в классе:

```csharp
BitSkinsApi.Market.ItemHistory
```

### Функция:

```csharp
BitSkinsApi.Market.ItemHistory.GetItemHistory(BitSkinsApi.Market.AppId.AppName app, int page, List<string> names, BitSkinsApi.Market.ItemHistory.ResultsPerPage resultsPerPage);
```

### Параметры функции:

* BitSkinsApi.Market.AppId.AppName app - игра, история предмета которой запрашивается.
* int page - номер страницы, по умолчанию 30 событий на странице.
* List<string> names - список названий предметов, история которых запрашивается.
* BitSkinsApi.Market.ItemHistory.ResultsPerPage resultsPerPage - кол-во событий на странице.

### Возвращает:

```csharp
List<BitSkinsApi.Market.ItemHistoryRecord>
```

Свойства класса ```BitSkinsApi.Market.ItemHistoryRecord```:
* AppId - id игры, которой принадлежит предмет.
* ItemId - id предмета в событии.
* MarketHashName - название предмета в событии.
* Price - цена.
* RecordType - тип события.
* LastUpdateAt - дата последнего обновления события.
* ListedAt - дата выставления предмета на продажу.
* WithdrawnAt - дата, когда предмет можно будет вывести из BitSkins, может быть null.
* ListedByMe - выставлен ли предмет на продажу вами.
* OnHold - удерживается ли предмет.
* OnSale - находится ли предмет на продаже.
* RecordTime - время события, может быть null.

## Пример

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