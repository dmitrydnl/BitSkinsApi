# Ожидаемое место в очереди заказов на покупку

Для того чтобы получить ожидаемое место в очереди для нового заказа на покупку без создания заказа на покупку, нужно вызвать функцию:

```csharp
BitSkinsApi.BuyOrder.PlaceInQueue.GetExpectedPlaceInQueue(BitSkinsApi.Market.AppId.AppName app, string name, double price);
```

## GetExpectedPlaceInQueue()

### Находится в классе:

```csharp
BitSkinsApi.BuyOrder.PlaceInQueue
```

### Функция:

```csharp
BitSkinsApi.BuyOrder.PlaceInQueue.GetExpectedPlaceInQueue(BitSkinsApi.Market.AppId.AppName app, string name, double price);
```

### Параметры функции:

* BitSkinsApi.Market.AppId.AppName app - игра, которой принадлежит предмет.
* string name - название предмета.
* double price - цена.

### Возвращает:

```csharp
int? placeInQueue
```

placeInQueue - ожидаемое место в очереди для нового заказа на покупку, может быть null.

### Возможные исключения
```ArgumentException``` - в случае передачи в функцию некорректных данных, в сообщение содержится подробная информация.
\
```BitSkinsApi.Server.RequestServerException``` - в случае передачи в функцию некорректных данных или проблем на сервере BitSkins.

## Пример

```csharp
BitSkinsApi.Market.AppId.AppName app = BitSkinsApi.Market.AppId.AppName.CounterStrikGlobalOffensive;
int placeInQueue = BitSkinsApi.BuyOrder.PlaceInQueue.GetExpectedPlaceInQueue(app, "CS:GO Weapon Case 2", 0.01);
Console.WriteLine(placeInQueue);
```

[<Создать заказ на покупку](https://github.com/dmitrydnl/BitSkinsApi/blob/master/docs/ru/buy_order/create_buy_order.md) &nbsp;&nbsp;&nbsp;&nbsp; [Отменить заказы на покупку>](https://github.com/dmitrydnl/BitSkinsApi/blob/master/docs/ru/buy_order/cancel_buy_orders.md)
