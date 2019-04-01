# Вывод денег с баланса

Для того чтобы вывести деньги с баланса BitSkins аккаунта, нужно вызвать функцию:

```csharp
BitSkinsApi.Balance.WithdrawMoney.MoneyWithdrawal(double amount, WithdrawalMethod withdrawalMethod);
```

## MoneyWithdrawal()

### Находится в классе:

```csharp
BitSkinsApi.Balance.WithdrawMoney
```

### Функция:

```csharp
BitSkinsApi.Balance.WithdrawMoney.MoneyWithdrawal(double amount, WithdrawalMethod withdrawalMethod);
```

### Параметры функции:

* double amount - сумма в долларах США для вывода. Не должна быть меньше доступного для вывода баланса и быть больше 5,00 долларов США.
* WithdrawalMethod withdrawalMethod - способ вывода денег. Может быть:
* * PayPal
* * Skrill
* * Bitcoin
* * Ethereum
* * Litecoin
* * BankWire

### Возвращает:

```csharp
bool success
```

success - успешно ли выполнена операция.

## Пример

```csharp
BitSkinsApi.Balance.Balance balance = BitSkinsApi.Balance.AccountBalance.GetAccountBalance();
if (balance.WithdrawableBalance > 5)
{
    bool success = BitSkinsApi.Balance.WithdrawMoney.MoneyWithdrawal(5, BitSkinsApi.Balance.WithdrawMoney.WithdrawalMethod.PayPal);
    System.Console.WriteLine(success ? "Success" : "Fail");
}
```

[<События с балансом аккаунта](https://github.com/Captious99/BitSkinsApi/blob/master/docs/ru/balance/money_events.md) &nbsp;&nbsp;&nbsp;&nbsp; [Слудующий>]()