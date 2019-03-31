# Баланс аккаунта
Для получения баланса BitSkins аккаунта нужно вызвать функцию:
```csharp
BitSkinsApi.Balance.AccountBalance.GetAccountBalance();
```

## GetAccountBalance()
### Находится в классе:
```csharp
BitSkinsApi.Balance.AccountBalance
```

### Функция:
```csharp
BitSkinsApi.Balance.AccountBalance.GetAccountBalance();
```
### Возвращает:
```csharp
BitSkinsApi.Balance.Balance
```
Свойства класса ```BitSkinsApi.Balance.Balance```:
* AvailableBalance - средства доступные для трат
* PendingWithdrawals - средства ожидающие снятия
* WithdrawableBalance - средства, которые можно снять
* CouponableBalance - купонные средства

## Пример
```csharp
BitSkinsApi.Balance.Balance balance = BitSkinsApi.Balance.AccountBalance.GetAccountBalance();
double availableBalance = balance.AvailableBalance;
System.Console.WriteLine(availableBalance);
```