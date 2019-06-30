# Баланс аккаунта

Для получения баланса BitSkins аккаунта нужно вызвать функцию:

```csharp
BitSkinsApi.Balance.CurrentBalance.GetAccountBalance();
```

## GetAccountBalance()

### Находится в классе:

```csharp
BitSkinsApi.Balance.CurrentBalance
```

### Функция:

```csharp
BitSkinsApi.Balance.CurrentBalance.GetAccountBalance();
```

### Возвращает:

```csharp
BitSkinsApi.Balance.AccountBalance
```

Свойства класса ```BitSkinsApi.Balance.AccountBalance```:
* AvailableBalance - средства доступные для трат.
* PendingWithdrawals - средства ожидающие снятия.
* WithdrawableBalance - средства, которые можно снять.
* CouponableBalance - купонные средства.

### Возможные исключения
```BitSkinsApi.Server.RequestServerException``` - в случае передачи в функцию некорректных данных или проблем на сервере BitSkins.

## Пример

```csharp
BitSkinsApi.Balance.AccountBalance balance = BitSkinsApi.Balance.CurrentBalance.GetAccountBalance();
double availableBalance = balance.AvailableBalance;
Console.WriteLine(availableBalance);
```

[<Двухфакторная аутентификация](https://github.com/dmitrydnl/BitSkinsApi/blob/master/docs/ru/account/two_factor_authentication.md) &nbsp;&nbsp;&nbsp;&nbsp; [События с балансом аккаунта>](https://github.com/dmitrydnl/BitSkinsApi/blob/master/docs/ru/balance/money_events.md)