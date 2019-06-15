# Отменить все заказы на покупку

Для того чтобы отменить все активные заказы на покупку для данного наименования товара, нужно вызвать функцию:

```csharp
BitSkinsApi.BuyOrder.CancelingBuyOrders.CancelAllBuyOrders(BitSkinsApi.Market.AppId.AppName app, string marketHashName);
```

## CancelAllBuyOrders()

### Находится в классе:

```csharp
BitSkinsApi.BuyOrder.CancelingBuyOrders
```

### Функция:

```csharp
BitSkinsApi.BuyOrder.CancelingBuyOrders.CancelAllBuyOrders(BitSkinsApi.Market.AppId.AppName app, string marketHashName);
```

### Параметры функции:

* BitSkinsApi.Market.AppId.AppName app - игра, для предметов которой отменяются заказы на покупку.
* string marketHashName - наименования товара, для которого отменяются все активные заказы на покупку.

### Возвращает:

```csharp
BitSkinsApi.BuyOrder.CanceledBuyOrders
```

Свойства класса ```BitSkinsApi.BuyOrder.CanceledBuyOrders```:
* Count - количество отмененные закзов на покупку.
* BuyOrderIds - список id отмененных заказов на покупку.

### Возможные исключения
```ArgumentException``` - в случае передачи в функцию некорректных данных, в сообщение содержится подробная информация.
\
```BitSkinsApi.Server.RequestServerException``` - в случае передачи в функцию некорректных данных или проблем на сервере BitSkins.

## Пример

```csharp
BitSkinsApi.Market.AppId.AppName app = BitSkinsApi.Market.AppId.AppName.CounterStrikGlobalOffensive;
BitSkinsApi.BuyOrder.CanceledBuyOrders canceledBuyOrders = BitSkinsApi.BuyOrder.CancelingBuyOrders.CancelAllBuyOrders(app, "CS:GO Weapon Case 2");
foreach (string id in canceledBuyOrders.BuyOrderIds)
{
    Console.WriteLine(id);
}
```

[<Отменить заказы на покупку](https://github.com/Captious99/BitSkinsApi/blob/master/docs/ru/buy_order/cancel_buy_orders.md) &nbsp;&nbsp;&nbsp;&nbsp; [Ваши заказы на покупку>](https://github.com/Captious99/BitSkinsApi/blob/master/docs/ru/buy_order/my_buy_orders.md)