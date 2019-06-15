# My buy orders

In order to receive all buy orders placed by you (regardless of whether they are active or not), you need to call the function:

```csharp
BitSkinsApi.BuyOrder.MyBuyOrders.GetMyBuyOrders(BitSkinsApi.Market.AppId.AppName app, string name, BitSkinsApi.BuyOrder.MyBuyOrders.BuyOrderType type, int page);
```

## GetMyBuyOrders()

### Is in class:

```csharp
BitSkinsApi.BuyOrder.MyBuyOrders
```

### Function:

```csharp
BitSkinsApi.BuyOrder.MyBuyOrders.GetMyBuyOrders(BitSkinsApi.Market.AppId.AppName app, string name, BitSkinsApi.BuyOrder.MyBuyOrders.BuyOrderType type, int page);
```

### Function parameters:

* BitSkinsApi.Market.AppId.AppName app - game for which your buy orders are requested.
* string name - the name of the item for which your buy orders are being requested.
* BitSkinsApi.BuyOrder.MyBuyOrders.BuyOrderType type - buy order type. May be:
  * Listed - is active.
  * Settled - fulfilled.
  * CancelledByUser - canceled by user.
  * CancelledBySystem - cancelled by system.
  * NotImportant - type is not important.
* int page - page number.

### Returns:

```csharp
List<BitSkinsApi.BuyOrder.BuyOrder>
```

Class properties ```BitSkinsApi.BuyOrder.BuyOrder```:
* BuyOrderId - buy order id.
* MarketHashName - item name.
* Price - price.
* SuggestedPrice - the average price of this item in BitSkins, may be null.
* State - buy order status.
* CreatedAt - date of creation.
* UpdatedAt - update date.
* PlaceInQueue - number in the queue of purchase orders, may be null.

### Possible exceptions
```BitSkinsApi.Server.RequestServerException``` - in case of transfer to the function incorrect data or problems on the BitSkins server.

## Example

```csharp
BitSkinsApi.Market.AppId.AppName app = BitSkinsApi.Market.AppId.AppName.CounterStrikGlobalOffensive;
BitSkinsApi.BuyOrder.MyBuyOrders.BuyOrderType buyOrderType = BitSkinsApi.BuyOrder.MyBuyOrders.BuyOrderType.Listed;
List<BitSkinsApi.BuyOrder.BuyOrder> buyOrders = BitSkinsApi.BuyOrder.MyBuyOrders.GetMyBuyOrders(app, "CS:GO Weapon Case 2", buyOrderType, 1);
foreach (BitSkinsApi.BuyOrder.BuyOrder buyOrder in buyOrders)
{
    Console.WriteLine(buyOrder.BuyOrderId);
    Console.WriteLine(buyOrder.MarketHashName);
    Console.WriteLine(buyOrder.PlaceInQueue);
    Console.WriteLine(buyOrder.Price);
    Console.WriteLine();
}
```

[<Cancel all buy orders](https://github.com/Captious99/BitSkinsApi/blob/master/docs/eng/buy_order/cancel_all_buy_orders.md) &nbsp;&nbsp;&nbsp;&nbsp; [Market buy orders>](https://github.com/Captious99/BitSkinsApi/blob/master/docs/eng/buy_order/market_buy_orders.md)