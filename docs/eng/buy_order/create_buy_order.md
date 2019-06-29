# Create buy order

Buy orders are executed within 30 seconds if someone puts up a item for sale at the price of the order or lower. Funds are not withheld because of pending buy orders. The purchase order will be automatically canceled if there is not enough money in your account to complete the purchase order. In order to create an order for the purchase of an item in BitSkins, you need to call the function:

```csharp
BitSkinsApi.BuyOrder.CreatingBuyOrder.CreateBuyOrder(BitSkinsApi.Market.AppId.AppName app, string name, double price, int quantity);
```

## CreateBuyOrder()

### Is in class:

```csharp
BitSkinsApi.BuyOrder.CreatingBuyOrder
```

### Function:

```csharp
BitSkinsApi.BuyOrder.CreatingBuyOrder.CreateBuyOrder(BitSkinsApi.Market.AppId.AppName app, string name, double price, int quantity);
```

### Function parameters:

* BitSkinsApi.Market.AppId.AppName app - game for which the purchase order is created.
* string name - the name of the item you want to purchase.
* double price - the price at which you want to purchase an item.
* int quantity - number of buy orders at this price for this item.

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
```ArgumentException``` - in case of transfer to the function incorrect data, the message contains detailed information.
\
```BitSkinsApi.Server.RequestServerException``` - in case of transfer to the function incorrect data or problems on the BitSkins server.

## Example

```csharp
BitSkinsApi.Market.AppId.AppName app = BitSkinsApi.Market.AppId.AppName.CounterStrikGlobalOffensive;
List<BitSkinsApi.BuyOrder.BuyOrder> buyOrders = BitSkinsApi.BuyOrder.CreatingBuyOrder.CreateBuyOrder(app, "CS:GO Weapon Case 2", 0.01, 1);
foreach (BitSkinsApi.BuyOrder.BuyOrder buyOrder in buyOrders)
{
    Console.WriteLine(buyOrder.BuyOrderId);
    Console.WriteLine(buyOrder.MarketHashName);
    Console.WriteLine(buyOrder.PlaceInQueue);
    Console.WriteLine(buyOrder.Price);
    Console.WriteLine();
}
```

[<Trade offer details](https://github.com/dmitrydnl/BitSkinsApi/blob/master/docs/eng/trade/trade_details.md) &nbsp;&nbsp;&nbsp;&nbsp; [Expected place in the queue for buy order>](https://github.com/dmitrydnl/BitSkinsApi/blob/master/docs/eng/buy_order/expected_place_in_queue.md)
