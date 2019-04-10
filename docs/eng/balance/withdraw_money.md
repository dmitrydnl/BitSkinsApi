# Withdrawing money from the balance

In order to withdraw money from the account balance BitSkins, you need to call the function:

```csharp
BitSkinsApi.Balance.MoneyWithdraw.WithdrawMoney(double amount, WithdrawalMethod withdrawalMethod);
```

## MoneyWithdrawal()

### Is in class:

```csharp
BitSkinsApi.Balance.MoneyWithdraw
```

### Function:

```csharp
BitSkinsApi.Balance.MoneyWithdraw.WithdrawMoney(double amount, WithdrawalMethod withdrawalMethod);
```

### Function parameters:

* double ampunt - amount in USD to withdraw. Should not be less than available for withdrawal of balance and be more than $5.00 USD.
* WithdrawalMethod withdrawalMethod - withdrawal method. May be:
  - PayPal
  - Skrill
  - Bitcoin
  - Ethereum
  - Litecoin
  - BankWire

### Returns:

```csharp
bool success
```

success - whether the operation was successful.

### Possible exceptions
```BitSkinsApi.Server.RequestServerException``` - in case of transfer to the function incorrect data or problems on the BitSkins server.

## Example

```csharp
BitSkinsApi.Balance.AccountBalance balance = BitSkinsApi.Balance.CurrentBalance.GetAccountBalance();
if (balance.WithdrawableBalance > 5)
{
    bool success = BitSkinsApi.Balance.MoneyWithdraw.WithdrawMoney(5, BitSkinsApi.Balance.MoneyWithdraw.WithdrawalMethod.PayPal);
    Console.WriteLine(success ? "Success" : "Fail");
}
```

[<Money events](https://github.com/Captious99/BitSkinsApi/blob/master/docs/eng/balance/money_events.md) &nbsp;&nbsp;&nbsp;&nbsp; [Account inventory>](https://github.com/Captious99/BitSkinsApi/blob/master/docs/eng/inventory/account_inventory.md)