# Summary of all market buy orders

In order to get a summary of all market buy orders (results include your own buy orders, where applicable), you need to call the function:

```csharp
BitSkinsApi.BuyOrder.SummationBuyOrders.SummarizeBuyOrders(BitSkinsApi.Market.AppId.AppName app);
```

## SummarizeBuyOrders()

### Is in class:

```csharp
BitSkinsApi.BuyOrder.SummationBuyOrders
```

### Function:

```csharp
BitSkinsApi.BuyOrder.SummationBuyOrders.SummarizeBuyOrders(BitSkinsApi.Market.AppId.AppName app);
```

### Function parameters:

* BitSkinsApi.Market.AppId.AppName app - game for which a summary of all market buy orders is requested.

### Returns:

```csharp
List<BitSkinsApi.BuyOrder.ItemBuyOrder>
```

Class properties ```BitSkinsApi.BuyOrder.ItemBuyOrder```:
* MarketHashName - item name.
* NumberOfBuyOrders - number of buy orders.
* MaxPrice - maximum buy order price.
* MinPrice - minimum buy order price.
* NumberOfMyBuyOrders - number of your buy orders.
* MaxPriceMyBuyOrders - maximum price of your buy orders.
* MinPriceMyBuyOrders - minimum price of your buy orders.

### Possible exceptions
```BitSkinsApi.Server.RequestServerException``` - in case of transfer to the function incorrect data or problems on the BitSkins server.

## Example

```csharp
BitSkinsApi.Market.AppId.AppName app = BitSkinsApi.Market.AppId.AppName.CounterStrikGlobalOffensive;
List<BitSkinsApi.BuyOrder.ItemBuyOrder> itemBuyOrders = BitSkinsApi.BuyOrder.SummationBuyOrders.SummarizeBuyOrders(app);
foreach (BitSkinsApi.BuyOrder.ItemBuyOrder itemBuyOrder in itemBuyOrders)
{
    Console.WriteLine(itemBuyOrder.MarketHashName);
    Console.WriteLine(itemBuyOrder.NumberOfBuyOrders);
    Console.WriteLine(itemBuyOrder.MaxPrice);
    Console.WriteLine(itemBuyOrder.MinPrice);
    Console.WriteLine();
}
```

[<Данные о последних продажах](https://github.com/Captious99/BitSkinsApi/blob/master/docs/ru/market/recent_sale.md) &nbsp;&nbsp;&nbsp;&nbsp; [История продаж>](https://github.com/Captious99/BitSkinsApi/blob/master/docs/ru/market/sell_history.md)