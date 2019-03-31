# События с балансом аккаунта
Для получения истории событий, которые изменили баланс BitSkins аккаунта нужно вызвать функцию:
```csharp
BitSkinsApi.Balance.AccountBalance.GetAccountBalance(int page);
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
* int page - номер страницы, на одной странице содержится до 30 событий

### Возвращает:
```csharp
List<BitSkinsApi.Balance.MoneyEvent>
```
Свойства класса ```BitSkinsApi.Balance.MoneyEvent```:
* Type - тип данного события. Типы событий:
1.ItemBought - покупка предмета
2.ItemSold – продажа предмета
3.SaleFee – комиссия при продаже
4.BuyCredit – пополнение счета
5.StoreCredit – начисление бонусов
6.Unknown – типы событий, обработка которых ещё не создана
* Amount - кол-во денег в данном событии (цена покупки, цена продажи, комиссия и т. д.)
* Description - описание данного события. В каждом типе оно разное:
1.ItemBought – “{ID игры которой принадлежит предмет}:{Название купленного предмета}”
2.ItemSold - “{ID игры которой принадлежит предмет}:{Название проданного предмета}”
3.SaleFee – “{Название игры предметы которой были куплены}”
4.BuyCredit – “{Способ пополнения счета}”
5.StoreCredit – “{Прчина начисления бонусов (обычно "steamname" – значит в вашем имени Steam вставлено "BitSkins.com")}”
* Time - время данного события

## Пример
```csharp
System.Collections.Generic.List<BitSkinsApi.Balance.MoneyEvent> moneyEvents = BitSkinsApi.Balance.MoneyEvents.GetMoneyEvents(1);
foreach (BitSkinsApi.Balance.MoneyEvent moneyEvent in moneyEvents)
{
    System.Console.WriteLine(moneyEvent.Type);
    System.Console.WriteLine(moneyEvent.Amount);
}
```