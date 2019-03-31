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
* double ampunt - amount in USD to withdraw. Must be at most equal to available balance, and over $5.00 USD.
* WithdrawalMethod withdrawalMethod - withdrawal method. May be:
1.PayPal
2.Skrill
3.Bitcoin
4.Ethereum
5.Litecoin
6.BankWire

### Returns:
```csharp
bool success
```
success - whether the operation was successful

## Example
```csharp
BitSkinsApi.Balance.Balance balance = BitSkinsApi.Balance.AccountBalance.GetAccountBalance();
if (balance.WithdrawableBalance > 5)
{
    bool success = BitSkinsApi.Balance.WithdrawMoney.MoneyWithdrawal(5, BitSkinsApi.Balance.WithdrawMoney.WithdrawalMethod.PayPal);
    System.Console.WriteLine(success ? "Success" : "Fail");
}
```