# События с балансом аккаунта

Для получения истории событий, которые изменили баланс BitSkins аккаунта нужно вызвать функцию:

```csharp
BitSkinsApi.Balance.MoneyEvents.GetMoneyEvents(int page);
```

## GetMoneyEvents()
### Находится в классе:

```csharp
BitSkinsApi.Balance.MoneyEvents
```

### Функция:

```csharp
BitSkinsApi.Balance.MoneyEvents.GetMoneyEvents(int page);
```

### Параметры функции:
* int page - номер страницы, на одной странице содержится до 30 событий.

### Возвращает:

```csharp
List<BitSkinsApi.Balance.MoneyEvent>
```
Свойства класса ```BitSkinsApi.Balance.MoneyEvent```:
* Type - тип данного события. Типы событий:
  - ItemBought - покупка предмета.
  - ItemSold – продажа предмета.
  - SaleFee – комиссия при продаже.
  - BuyCredit – пополнение счета.
  - StoreCredit – начисление бонусов.
  - Unknown – типы событий, обработка которых ещё не создана.
* Amount - кол-во денег в данном событии (цена покупки, цена продажи, комиссия и т. д.).
* Description - описание данного события. В каждом типе оно разное:
  - ItemBought – “{ID игры которой принадлежит предмет}:{Название купленного предмета}”.
  - ItemSold - “{ID игры которой принадлежит предмет}:{Название проданного предмета}”.
  - SaleFee – “{Название игры предметы которой были куплены}”.
  - BuyCredit – “{Способ пополнения счета}”.
  - StoreCredit – “{Прчина начисления бонусов (обычно "steamname" – значит в вашем имени Steam вставлено "BitSkins.com")}”.
* Time - время данного события.

### Возможные исключения
```BitSkinsApi.Server.RequestServerException``` - в случае передачи в функцию некорректных данных или проблем на сервере BitSkins.

## Пример

```csharp
List<BitSkinsApi.Balance.MoneyEvent> moneyEvents = BitSkinsApi.Balance.MoneyEvents.GetMoneyEvents(1);
foreach (BitSkinsApi.Balance.MoneyEvent moneyEvent in moneyEvents)
{
    Console.WriteLine(moneyEvent.Type);
    Console.WriteLine(moneyEvent.Amount);
}
```

[<Баланс аккаунта](https://github.com/Captious99/BitSkinsApi/blob/master/docs/ru/balance/account_balance.md) &nbsp;&nbsp;&nbsp;&nbsp; [Вывод денег с баланса>](https://github.com/Captious99/BitSkinsApi/blob/master/docs/ru/balance/withdraw_money.md)