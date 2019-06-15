# Рыночные заказы на покупку

Для того чтобы получить все рыночные заказы на покупку от всех покупателей (кроме ваших), которые нуждаются в исполнении, нужно вызвать функцию:

```csharp
BitSkinsApi.BuyOrder.MarketBuyOrders.GetMarketBuyOrders(BitSkinsApi.Market.AppId.AppName app, string name, int page);
```

## GetMarketBuyOrders()

### Находится в классе:

```csharp
BitSkinsApi.BuyOrder.MarketBuyOrders
```

### Функция:

```csharp
BitSkinsApi.BuyOrder.MarketBuyOrders.GetMarketBuyOrders(BitSkinsApi.Market.AppId.AppName app, string name, int page);
```

### Параметры функции:

* BitSkinsApi.Market.AppId.AppName app - игра, которой принадлежит предмет.
* string name - название предмета, для которого запрашиваются заказы на покупку.
* int page - номер страницы.

### Возвращает:

```csharp
List<BitSkinsApi.BuyOrder.MarketBuyOrder>
```

Свойства класса ```BitSkinsApi.BuyOrder.MarketBuyOrder```:
* BuyOrderId - id заказа на покупку.
* MarketHashName - название предмета.
* Price - цена.
* SuggestedPrice - средняя цена этого предмета в BitSkins, может быть null.
* IsMine - является ли заказ на покупку вашим.
* CreatedAt - дата создания.
* PlaceInQueue - номер в очереди заказов на покупку, может быть null.

### Возможные исключения
```ArgumentException``` - в случае передачи в функцию некорректных данных, в сообщение содержится подробная информация.
\
```BitSkinsApi.Server.RequestServerException``` - в случае передачи в функцию некорректных данных или проблем на сервере BitSkins.

## Пример

```csharp
BitSkinsApi.Market.AppId.AppName app = BitSkinsApi.Market.AppId.AppName.CounterStrikGlobalOffensive;
List<BitSkinsApi.BuyOrder.MarketBuyOrder> marketBuyOrders = BitSkinsApi.BuyOrder.MarketBuyOrders.GetMarketBuyOrders(app, "CS:GO Weapon Case 2", 1);
foreach (BitSkinsApi.BuyOrder.MarketBuyOrder buyOrder in marketBuyOrders)
{
    Console.WriteLine(buyOrder.BuyOrderId);
    Console.WriteLine(buyOrder.MarketHashName);
    Console.WriteLine(buyOrder.PlaceInQueue);
    Console.WriteLine(buyOrder.Price);
    Console.WriteLine();
}
```

[<Ваши заказы на покупку](https://github.com/Captious99/BitSkinsApi/blob/master/docs/ru/buy_order/my_buy_orders.md) &nbsp;&nbsp;&nbsp;&nbsp; [Сводка всех рыночных заказов на покупку>](https://github.com/Captious99/BitSkinsApi/blob/master/docs/ru/buy_order/summarize_buy_orders.md)
