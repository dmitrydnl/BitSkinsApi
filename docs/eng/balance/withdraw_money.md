# Withdrawing money from the balance

In order to withdraw money from the account balance BitSkins, you need to call the function:

```csharp
BitSkinsApi.Balance.WithdrawMoney.MoneyWithdrawal(double amount, WithdrawalMethod withdrawalMethod);
```

## MoneyWithdrawal()

### Is in class:

```csharp
BitSkinsApi.Balance.WithdrawMoney
```

### Function:

```csharp
BitSkinsApi.Balance.WithdrawMoney.MoneyWithdrawal(double amount, WithdrawalMethod withdrawalMethod);
```

### Function parameters:

* double ampunt - amount in USD to withdraw. Should not be less than available for withdrawal of balance and be more than $5.00 USD.
* WithdrawalMethod withdrawalMethod - withdrawal method. May be:
* * PayPal
* * Skrill
* * Bitcoin
* * Ethereum
* * Litecoin
* * BankWire

### Returns:

```csharp
bool success
```

success - whether the operation was successful.

## Example

```csharp
BitSkinsApi.Balance.Balance balance = BitSkinsApi.Balance.AccountBalance.GetAccountBalance();
if (balance.WithdrawableBalance > 5)
{
    bool success = BitSkinsApi.Balance.WithdrawMoney.MoneyWithdrawal(5, BitSkinsApi.Balance.WithdrawMoney.WithdrawalMethod.PayPal);
    System.Console.WriteLine(success ? "Success" : "Fail");
}
```

[<Money events](https://github.com/Captious99/BitSkinsApi/blob/master/docs/eng/balance/money_events.md) &nbsp;&nbsp;&nbsp;&nbsp; [Next>]()