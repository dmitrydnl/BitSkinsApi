# Selling item

In order to sell a item from your Steam inventory on BitSkins, you need to call the function:

```csharp
BitSkinsApi.Market.Sale.SellItem(BitSkinsApi.Market.AppId.AppName app, List<string> itemIds, List<double> itemPrices);
```

## SellItem()

### Is in class:

```csharp
BitSkinsApi.Market.Sale
```

### Function:

```csharp
BitSkinsApi.Market.Sale.SellItem(BitSkinsApi.Market.AppId.AppName app, List<string> itemIds, List<double> itemPrices);
```

### Function parameters:

* BitSkinsApi.Market.AppId.AppName app - the game that owns the items being sold.
* List<string> itemIds - id list of items sold.
* List<double> itemPrices - price list of items sold.

### Returns:

```csharp
BitSkinsApi.Market.InformationAboutSale
```

Class properties ```BitSkinsApi.Market.InformationAboutSale```
* SoldItems - list of items sold.
* TradeTokens - trade tokens.
* InformationAboutSellerBot - information about the bot that sent the trade offer.

### Possible exceptions
```BitSkinsApi.Server.RequestServerException``` - in case of transfer to the function incorrect data or problems on the BitSkins server.

## Example

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

[<Purchase item](https://github.com/Captious99/BitSkinsApi/blob/master/docs/eng/market/buy_item.md) &nbsp;&nbsp;&nbsp;&nbsp; [Delist item from sale>](https://github.com/Captious99/BitSkinsApi/blob/master/docs/eng/market/delist_item.md)