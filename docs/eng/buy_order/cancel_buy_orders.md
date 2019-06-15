# Cancel buy orders

In order to cancel active buy orders (up to 999), you need to call the function:

```csharp
BitSkinsApi.BuyOrder.CancelingBuyOrders.CancelBuyOrders(BitSkinsApi.Market.AppId.AppName app, List<string> buyOrderIds);
```

## CancelBuyOrders()

### Is in class:

```csharp
BitSkinsApi.BuyOrder.CancelingBuyOrders
```

### Function:

```csharp
BitSkinsApi.BuyOrder.CancelingBuyOrders.CancelBuyOrders(BitSkinsApi.Market.AppId.AppName app, List<string> buyOrderIds);
```

### Function parameters:

* BitSkinsApi.Market.AppId.AppName app - game for which buy orders to be canceled.
* List<string> buyOrderIds - list of buy orders id to be canceled.

### Returns:

```csharp
BitSkinsApi.BuyOrder.CanceledBuyOrders
```

Class properties ```BitSkinsApi.BuyOrder.CanceledBuyOrders```:
* Count - number of canceled purchase orders.
* BuyOrderIds - id list of canceled purchase orders.

### Possible exceptions
```ArgumentNullException``` - in case of transfer to the function incorrect data, the message contains detailed information.
\
```ArgumentException``` - in case of transfer to the function incorrect data, the message contains detailed information.
\
```BitSkinsApi.Server.RequestServerException``` - in case of transfer to the function incorrect data or problems on the BitSkins server.

## Example

```csharp
BitSkinsApi.Market.AppId.AppName app = BitSkinsApi.Market.AppId.AppName.CounterStrikGlobalOffensive;
List<string> buyOrderIds = new List<string> { "buy order id 1", "buy order id 2" };
BitSkinsApi.BuyOrder.CanceledBuyOrders canceledBuyOrders = BitSkinsApi.BuyOrder.CancelingBuyOrders.CancelBuyOrders(app, buyOrderIds);
foreach (string id in canceledBuyOrders.BuyOrderIds)
{
    Console.WriteLine(id);
}
```

[<Expected place in the queue for buy order](https://github.com/Captious99/BitSkinsApi/blob/master/docs/eng/buy_order/expected_place_in_queue.md) &nbsp;&nbsp;&nbsp;&nbsp; [Cancel all buy orders>](https://github.com/Captious99/BitSkinsApi/blob/master/docs/eng/buy_order/cancel_all_buy_orders.md)