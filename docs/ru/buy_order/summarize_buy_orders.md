# Сводка всех рыночных заказов на покупку

Для того чтобы получить сводку всех рыночных ордеров на покупку (результаты включают ваши собственные заказы на покупку, где это применимо), нужно вызвать функцию:

```csharp
BitSkinsApi.BuyOrder.SummationBuyOrders.SummarizeBuyOrders(BitSkinsApi.Market.AppId.AppName app);
```

## SummarizeBuyOrders()

### Находится в классе:

```csharp
BitSkinsApi.BuyOrder.SummationBuyOrders
```

### Функция:

```csharp
BitSkinsApi.BuyOrder.SummationBuyOrders.SummarizeBuyOrders(BitSkinsApi.Market.AppId.AppName app);
```

### Параметры функции:

* BitSkinsApi.Market.AppId.AppName app - игра, для которой запрашивается сводка всех рыночных ордеров на покупку.

### Возвращает:

```csharp
List<BitSkinsApi.BuyOrder.ItemBuyOrder>
```

Свойства класса ```BitSkinsApi.BuyOrder.ItemBuyOrder```:
* MarketHashName - название предмета.
* NumberOfBuyOrders - количество запросов на покупку.
* MaxPrice - максимальная цена запроса на покупку.
* MinPrice - минимальная цена запроса на покупку.
* NumberOfMyBuyOrders - количество ваших запросов на покупку.
* MaxPriceMyBuyOrders - максимальная цена ваших запросов на покупку.
* MinPriceMyBuyOrders - минимальная цена ваших запросов на покупку.

### Возможные исключения
```BitSkinsApi.Server.RequestServerException``` - в случае передачи в функцию некорректных данных или проблем на сервере BitSkins.

## Пример

```csharp
BitSkinsApi.Market.AppId.AppName app = BitSkinsApi.Market.AppId.AppName.CounterStrikGlobalOffensive;
List<BitSkinsApi.BuyOrder.ItemBuyOrder> itemBuyOrders = BitSkinsApi.BuyOrder.SummationBuyOrders.SummarizeBuyOrders(app);
foreach (BitSkinsApi.BuyOrder.ItemBuyOrder itemBuyOrder in itemBuyOrders)
{
    Console.WriteLine(itemBuyOrder.MarketHashName);
    Console.WriteLine(itemBuyOrder.NumberOfBuyOrders);
    Console.WriteLine(itemBuyOrder.MaxPrice);
    Console.WriteLine(itemBuyOrder.MinPrice);
    Console.WriteLine();
}
```

[<Рыночные заказы на покупку](https://github.com/dmitrydnl/BitSkinsApi/blob/master/docs/ru/buy_order/market_buy_orders.md) &nbsp;&nbsp;&nbsp;&nbsp; [Bitcoin адрес для депозита>](https://github.com/dmitrydnl/BitSkinsApi/blob/master/docs/ru/crypto/bitcoin_deposit_address.md)