# Ваши заказы на покупку

Для того чтобы получить все размещенные вами заказы на покупку (независимо от того, активны они или нет), нужно вызвать функцию:

```csharp
BitSkinsApi.BuyOrder.MyBuyOrders.GetMyBuyOrders(BitSkinsApi.Market.AppId.AppName app, string name, BitSkinsApi.BuyOrder.MyBuyOrders.BuyOrderType type, int page);
```

## GetMyBuyOrders()

### Находится в классе:

```csharp
BitSkinsApi.BuyOrder.MyBuyOrders
```

### Функция:

```csharp
BitSkinsApi.BuyOrder.MyBuyOrders.GetMyBuyOrders(BitSkinsApi.Market.AppId.AppName app, string name, BitSkinsApi.BuyOrder.MyBuyOrders.BuyOrderType type, int page);
```

### Параметры функции:

* BitSkinsApi.Market.AppId.AppName app - игра, для которой запрашиваются ваши заказы на покупку.
* string name - название предмета, для которого запрашиваются ваши заказы на покупку.
* BitSkinsApi.BuyOrder.MyBuyOrders.BuyOrderType type - тип заказа на покупку. Может быть:
  * Listed - активен.
  * Settled - выполнен.
  * CancelledByUser - отменен пользователем.
  * CancelledBySystem - отменен системой.
  * NotImportant - тип не важен.
* int page - номер страницы.

### Возвращает:

```csharp
List<BitSkinsApi.BuyOrder.BuyOrder>
```

Свойства класса ```BitSkinsApi.BuyOrder.BuyOrder```:
* BuyOrderId - id заказа на покупку.
* MarketHashName - название предмета.
* Price - цена.
* SuggestedPrice - средняя цена этого предмета в BitSkins, может быть null.
* State - статус заказа на покупку.
* CreatedAt - дата создания.
* UpdatedAt - дата обновления.
* PlaceInQueue - номер в очереди заказов на покупку, может быть null.

### Возможные исключения
```BitSkinsApi.Server.RequestServerException``` - в случае передачи в функцию некорректных данных или проблем на сервере BitSkins.

## Пример

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

[<Отменить все заказы на покупку](https://github.com/Captious99/BitSkinsApi/blob/master/docs/ru/buy_order/cancel_all_buy_orders.md) &nbsp;&nbsp;&nbsp;&nbsp; [Рыночные заказы на покупку>](https://github.com/Captious99/BitSkinsApi/blob/master/docs/ru/buy_order/market_buy_orders.md)