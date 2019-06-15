# Items requiring a reset price

In order to get a list of items for which you need to reset prices (items for which a price reset is required, always have a reserved price of 4985.11), you need to call the function:

```csharp
BitSkinsApi.Market.ResetPriceItems.GetResetPriceItems(BitSkinsApi.Market.AppId.AppName app, int page);
```

## GetResetPriceItems()

### Is in class:

```csharp
BitSkinsApi.Market.ResetPriceItems
```

### Function:

```csharp
BitSkinsApi.Market.ResetPriceItems.GetResetPriceItems(BitSkinsApi.Market.AppId.AppName app, int page);
```

### Function parameters:

* BitSkinsApi.Market.AppId.AppName app - game that owns items.
* int page - page number.

### Returns:

```csharp
List<BitSkinsApi.Market.ResetPriceItem>
```

Class properties ``BitSkinsApi.Market.ResetPriceItem```
* MarketHashName - item name.
* Price - item price, may be null.

### Possible exceptions
```BitSkinsApi.Server.RequestServerException``` - in case of transfer to the function incorrect data or problems on the BitSkins server.

## Example

```csharp
BitSkinsApi.Market.AppId.AppName app = BitSkinsApi.Market.AppId.AppName.CounterStrikGlobalOffensive;
List<BitSkinsApi.Market.ResetPriceItem> resetPriceItems = BitSkinsApi.Market.ResetPriceItems.GetResetPriceItems(app, 1);
foreach (BitSkinsApi.Market.ResetPriceItem item in resetPriceItems)
{
    Console.WriteLine(item.MarketHashName);
    Console.WriteLine(item.Price);
    Console.WriteLine();
}
```

[<Market data of items on Steam](https://github.com/Captious99/BitSkinsApi/blob/master/docs/eng/market/steam_price_data.md) &nbsp;&nbsp;&nbsp;&nbsp; [Recent trade offers>](https://github.com/Captious99/BitSkinsApi/blob/master/docs/eng/trade/recent_trade_offers.md)