# Expected place in the queue for buy order

In order to get the expected place in the queue for a new buy order without creating a buy order, you need to call the function:

```csharp
BitSkinsApi.BuyOrder.PlaceInQueue.GetExpectedPlaceInQueue(BitSkinsApi.Market.AppId.AppName app, string name, double price);
```

## GetExpectedPlaceInQueue()

### Is in class:

```csharp
BitSkinsApi.BuyOrder.PlaceInQueue
```

### Function:

```csharp
BitSkinsApi.BuyOrder.PlaceInQueue.GetExpectedPlaceInQueue(BitSkinsApi.Market.AppId.AppName app, string name, double price);
```

### Function parameters:

* BitSkinsApi.Market.AppId.AppName app - game, which owns the purchased items.
* string name - item name.
* double price - price.

### Returns:

```csharp
int placeInQueue
```

placeInQueue - expected place in queue for new buy order, may be null.

### Possible exceptions
```BitSkinsApi.Server.RequestServerException``` - in case of transfer to the function incorrect data or problems on the BitSkins server.

## Example

```csharp
BitSkinsApi.Market.AppId.AppName app = BitSkinsApi.Market.AppId.AppName.CounterStrikGlobalOffensive;
int placeInQueue = BitSkinsApi.BuyOrder.PlaceInQueue.GetExpectedPlaceInQueue(app, "CS:GO Weapon Case 2", 0.01);
Console.WriteLine(placeInQueue);
```

[<Create buy order](https://github.com/Captious99/BitSkinsApi/blob/master/docs/eng/buy_order/create_buy_order.md) &nbsp;&nbsp;&nbsp;&nbsp; [Cancel buy orders>](https://github.com/Captious99/BitSkinsApi/blob/master/docs/eng/buy_order/cancel_buy_orders.md)
