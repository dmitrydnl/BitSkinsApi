# Account balance
To get a BitSkins account balance, you need to call the function:
```csharp
BitSkinsApi.Balance.AccountBalance.GetAccountBalance();
```

## GetAccountBalance()
### Is in class:
```csharp
BitSkinsApi.Balance.AccountBalance
```

### Function:
```csharp
BitSkinsApi.Balance.AccountBalance.GetAccountBalance();
```
### Returns:
```csharp
BitSkinsApi.Balance.Balance
```
Class properties ```BitSkinsApi.Balance.Balance```:
* AvailableBalance - funds available for spending
* PendingWithdrawals - funds pending withdrawals
* WithdrawableBalance - funds that can be withdraw
* CouponableBalance - coupon funds

## Example
```csharp
BitSkinsApi.Balance.Balance balance = BitSkinsApi.Balance.AccountBalance.GetAccountBalance();
double availableBalance = balance.AvailableBalance;
System.Console.WriteLine(availableBalance);
```