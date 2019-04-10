# Account balance

To get a BitSkins account balance, you need to call the function:

```csharp
BitSkinsApi.Balance.CurrentBalance.GetAccountBalance();
```

## GetAccountBalance()

### Is in class:

```csharp
BitSkinsApi.Balance.CurrentBalance
```

### Function:

```csharp
BitSkinsApi.Balance.CurrentBalance.GetAccountBalance();
```

### Returns:

```csharp
BitSkinsApi.Balance.AccountBalance
```

Class properties ```BitSkinsApi.Balance.AccountBalance```:
* AvailableBalance - funds available for spending.
* PendingWithdrawals - funds pending withdrawals.
* WithdrawableBalance - funds that can be withdraw.
* CouponableBalance - coupon funds.

### Possible exceptions
```BitSkinsApi.Server.RequestServerException``` - in case of transfer to the function incorrect data or problems on the BitSkins server.

## Example

```csharp
BitSkinsApi.Balance.AccountBalance balance = BitSkinsApi.Balance.CurrentBalance.GetAccountBalance();
double availableBalance = balance.AvailableBalance;
Console.WriteLine(availableBalance);
```

[<Two-factor authentication](https://github.com/Captious99/BitSkinsApi/blob/master/docs/eng/account/two_factor_authentication.md) &nbsp;&nbsp;&nbsp;&nbsp; [Money events>](https://github.com/Captious99/BitSkinsApi/blob/master/docs/eng/balance/money_events.md)