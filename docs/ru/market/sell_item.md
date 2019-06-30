# Продажа предмета

Для того чтобы продать товар из вашего инвентаря Steam на BitSkins, нужно вызвать функцию:

```csharp
BitSkinsApi.Market.Sale.SellItem(BitSkinsApi.Market.AppId.AppName app, List<string> itemIds, List<double> itemPrices);
```

## SellItem()

### Находится в классе:

```csharp
BitSkinsApi.Market.Sale
```

### Функция:

```csharp
BitSkinsApi.Market.Sale.SellItem(BitSkinsApi.Market.AppId.AppName app, List<string> itemIds, List<double> itemPrices);
```

### Параметры функции:

* BitSkinsApi.Market.AppId.AppName app - игра, которой принадлежат продаваемые предметы.
* List<string> itemIds - списиок id продаваемых предметов.
* List<double> itemPrices - список цен продаваемых предметов.

### Возвращает:

```csharp
BitSkinsApi.Market.InformationAboutSale
```

Свойства класса ```BitSkinsApi.Market.InformationAboutSale```
* SoldItems - список продданных предметов.
* TradeTokens - токены обмена.
* InformationAboutSellerBot - информация о боте приславшем предложение на обмен.

### Возможные исключения
```ArgumentNullException``` - в случае передачи в функцию некорректных данных, в сообщение содержится подробная информация.
\
```ArgumentException``` - в случае передачи в функцию некорректных данных, в сообщение содержится подробная информация.
\
```BitSkinsApi.Server.RequestServerException``` - в случае передачи в функцию некорректных данных или проблем на сервере BitSkins.

## Пример

```csharp
BitSkinsApi.Market.AppId.AppName app = BitSkinsApi.Market.AppId.AppName.CounterStrikGlobalOffensive;
List<string> itemIds = new List<string> { "id1", "id2" };
List<double> itemPrices = new List<double> { 54.12, 123.3 };
BitSkinsApi.Market.InformationAboutSale information = BitSkinsApi.Market.Sale.SellItem(app, itemIds, itemPrices);
foreach (BitSkinsApi.Market.SoldItem item in information.SoldItems)
{
    Console.WriteLine(item.ItemId);
    Console.WriteLine();
}
```

[<Покупка предмета](https://github.com/dmitrydnl/BitSkinsApi/blob/master/docs/ru/market/buy_item.md) &nbsp;&nbsp;&nbsp;&nbsp; [Удалить предмет с продажи>](https://github.com/dmitrydnl/BitSkinsApi/blob/master/docs/ru/market/delist_item.md)