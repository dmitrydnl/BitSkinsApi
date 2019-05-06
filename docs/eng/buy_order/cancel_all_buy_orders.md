﻿# Cancel all buy orders

In order to cancel all active buy orders for a given item name, you need to call the function:

```csharp
BitSkinsApi.BuyOrder.CancelingBuyOrders.CancelAllBuyOrders(BitSkinsApi.Market.AppId.AppName app, string marketHashName);
```

## CancelAllBuyOrders()

### Is in class:

```csharp
BitSkinsApi.BuyOrder.CancelingBuyOrders
```

### Function:

```csharp
BitSkinsApi.BuyOrder.CancelingBuyOrders.CancelAllBuyOrders(BitSkinsApi.Market.AppId.AppName app, string marketHashName);
```

### Function parameters:

* BitSkinsApi.Market.AppId.AppName app - game for which buy orders to be canceled.
* string marketHashName - item name for which all active buy orders to be canceled.

### Returns:

```csharp
BitSkinsApi.BuyOrder.CanceledBuyOrders
```

Class properties ```BitSkinsApi.BuyOrder.CanceledBuyOrders```:
* Count - number of canceled purchase orders.
* BuyOrderIds - id list of canceled purchase orders.

### Possible exceptions
```BitSkinsApi.Server.RequestServerException``` - in case of transfer to the function incorrect data or problems on the BitSkins server.

## Example

```csharp
BitSkinsApi.Market.AppId.AppName app = BitSkinsApi.Market.AppId.AppName.CounterStrikGlobalOffensive;
BitSkinsApi.BuyOrder.CanceledBuyOrders canceledBuyOrders = BitSkinsApi.BuyOrder.CancelingBuyOrders.CancelAllBuyOrders(app, "CS:GO Weapon Case 2");
foreach (string id in canceledBuyOrders.BuyOrderIds)
{
    Console.WriteLine(id);
}
```

[<Данные о последних продажах](https://github.com/Captious99/BitSkinsApi/blob/master/docs/ru/market/recent_sale.md) &nbsp;&nbsp;&nbsp;&nbsp; [История продаж>](https://github.com/Captious99/BitSkinsApi/blob/master/docs/ru/market/sell_history.md)