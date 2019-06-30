# Отменить заказы на покупку

Для того чтобы отменить активные заказы на покупку (до 999), нужно вызвать функцию:

```csharp
BitSkinsApi.BuyOrder.CancelingBuyOrders.CancelBuyOrders(BitSkinsApi.Market.AppId.AppName app, List<string> buyOrderIds);
```

## CancelBuyOrders()

### Находится в классе:

```csharp
BitSkinsApi.BuyOrder.CancelingBuyOrders
```

### Функция:

```csharp
BitSkinsApi.BuyOrder.CancelingBuyOrders.CancelBuyOrders(BitSkinsApi.Market.AppId.AppName app, List<string> buyOrderIds);
```

### Параметры функции:

* BitSkinsApi.Market.AppId.AppName app - игра, для предметов которой отменяются заказы на покупку.
* List<string> buyOrderIds - список id заказов на покупку, которые нужно отменить.

### Возвращает:

```csharp
BitSkinsApi.BuyOrder.CanceledBuyOrders
```

Свойства класса ```BitSkinsApi.BuyOrder.CanceledBuyOrders```:
* Count - количество отмененные закзов на покупку.
* BuyOrderIds - список id отмененных заказов на покупку.

### Возможные исключения
```ArgumentNullException``` - в случае передачи в функцию некорректных данных, в сообщение содержится подробная информация.
\
```ArgumentException``` - в случае передачи в функцию некорректных данных, в сообщение содержится подробная информация.
\
```BitSkinsApi.Server.RequestServerException``` - в случае передачи в функцию некорректных данных или проблем на сервере BitSkins.

## Пример

```csharp
BitSkinsApi.Market.AppId.AppName app = BitSkinsApi.Market.AppId.AppName.CounterStrikGlobalOffensive;
List<string> buyOrderIds = new List<string> { "buy order id 1", "buy order id 2" };
BitSkinsApi.BuyOrder.CanceledBuyOrders canceledBuyOrders = BitSkinsApi.BuyOrder.CancelingBuyOrders.CancelBuyOrders(app, buyOrderIds);
foreach (string id in canceledBuyOrders.BuyOrderIds)
{
    Console.WriteLine(id);
}
```

[<Ожидаемое место в очереди заказов на покупку](https://github.com/dmitrydnl/BitSkinsApi/blob/master/docs/ru/buy_order/expected_place_in_queue.md) &nbsp;&nbsp;&nbsp;&nbsp; [Отменить все заказы на покупку>](https://github.com/dmitrydnl/BitSkinsApi/blob/master/docs/ru/buy_order/cancel_all_buy_orders.md)