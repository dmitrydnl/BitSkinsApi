# Market buy orders

In order to get all market buy orders from all buyers (except yours) who need to be executed, you need to call the function:

```csharp
BitSkinsApi.BuyOrder.MarketBuyOrders.GetMarketBuyOrders(BitSkinsApi.Market.AppId.AppName app, string name, int page);
```

## GetMarketBuyOrders()

### Is in class:

```csharp
BitSkinsApi.BuyOrder.MarketBuyOrders
```

### Function:

```csharp
BitSkinsApi.BuyOrder.MarketBuyOrders.GetMarketBuyOrders(BitSkinsApi.Market.AppId.AppName app, string name, int page);
```

### Function parameters:

* BitSkinsApi.Market.AppId.AppName app - game, which owns the purchased items.
* string name - name of the item for which buy orders are requested.
* int page - page number.

### Returns:

```csharp
List<BitSkinsApi.BuyOrder.MarketBuyOrder>
```

Class properties ```BitSkinsApi.BuyOrder.MarketBuyOrder```:
* BuyOrderId - buy order id.
* MarketHashName - item name.
* Price - price.
* SuggestedPrice - the average price of this item in BitSkins, maybe null.
* IsMine - is your purchase order.
* CreatedAt - date of creation.
* PlaceInQueue - number in the queue of purchase orders, maybe null.

### Possible exceptions
```BitSkinsApi.Server.RequestServerException``` - in case of transfer to the function incorrect data or problems on the BitSkins server.

## Example

```csharp
BitSkinsApi.Market.AppId.AppName app = BitSkinsApi.Market.AppId.AppName.CounterStrikGlobalOffensive;
List<BitSkinsApi.BuyOrder.MarketBuyOrder> marketBuyOrders = BitSkinsApi.BuyOrder.MarketBuyOrders.GetMarketBuyOrders(app, "CS:GO Weapon Case 2", 1);
foreach (BitSkinsApi.BuyOrder.MarketBuyOrder buyOrder in marketBuyOrders)
{
    Console.WriteLine(buyOrder.BuyOrderId);
    Console.WriteLine(buyOrder.MarketHashName);
    Console.WriteLine(buyOrder.PlaceInQueue);
    Console.WriteLine(buyOrder.Price);
    Console.WriteLine();
}
```

[<My buy orders](https://github.com/Captious99/BitSkinsApi/blob/master/docs/eng/buy_order/my_buy_orders.md) &nbsp;&nbsp;&nbsp;&nbsp; [Summary of all market buy orders>](https://github.com/Captious99/BitSkinsApi/blob/master/docs/eng/buy_order/summarize_buy_orders.md)
