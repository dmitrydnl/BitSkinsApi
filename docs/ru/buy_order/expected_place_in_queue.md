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
int placeInQueue
```

placeInQueue - ожидаемое место в очереди для нового заказа на покупку.

### Возможные исключения
```BitSkinsApi.Server.RequestServerException``` - в случае передачи в функцию некорректных данных или проблем на сервере BitSkins.

## Пример

```csharp
BitSkinsApi.Market.AppId.AppName app = BitSkinsApi.Market.AppId.AppName.CounterStrikGlobalOffensive;
int placeInQueue = BitSkinsApi.BuyOrder.PlaceInQueue.GetExpectedPlaceInQueue(app, "CS:GO Weapon Case 2", 0.01);
Console.WriteLine(placeInQueue);
```

[<События с балансом аккаунта](https://github.com/Captious99/BitSkinsApi/blob/master/docs/ru/balance/money_events.md) &nbsp;&nbsp;&nbsp;&nbsp; [Инвентарь аккаунта>](https://github.com/Captious99/BitSkinsApi/blob/master/docs/ru/inventory/account_inventory.md)