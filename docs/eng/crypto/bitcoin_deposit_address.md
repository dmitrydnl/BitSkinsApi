# Bitcoin address for deposit

To get a permanent bitcoin address for a deposit to your BitSkins account, you need to call the function:

```csharp
BitSkinsApi.Crypto.AccountBitcoinDepositAddress.GetBitcoinDepositAddress();
```

## GetBitcoinDepositAddress()

### Is in class:

```csharp
BitSkinsApi.Crypto.AccountBitcoinDepositAddress
```

### Function:

```csharp
BitSkinsApi.Crypto.AccountBitcoinDepositAddress.GetBitcoinDepositAddress();
```

### Returns:

```csharp
BitSkinsApi.Crypto.BitcoinDepositAddress
```

Class properties ```BitSkinsApi.Crypto.BitcoinDepositAddress```:
* Address - your permanent bitcoin address on the BitSkins website.
* MinimumAcceptableDepositAmount? - minimum allowable amount for transfer.

### Possible exceptions
```BitSkinsApi.Server.RequestServerException``` - in case of transfer to the function incorrect data or problems on the BitSkins server.

## Example

```csharp
BitSkinsApi.Crypto.BitcoinDepositAddress bitcoinDepositAddress = BitSkinsApi.Crypto.AccountBitcoinDepositAddress.GetBitcoinDepositAddress();
Console.WriteLine(bitcoinDepositAddress.Address);
Console.WriteLine(bitcoinDepositAddress.MinimumAcceptableDepositAmount);
```

[<Summary of all market buy orders](https://github.com/dmitrydnl/BitSkinsApi/blob/master/docs/eng/buy_order/summarize_buy_orders.md) &nbsp;&nbsp;&nbsp;&nbsp; [Current exchange rate Bitcoin to USD>](https://github.com/dmitrydnl/BitSkinsApi/blob/master/docs/eng/crypto/bitcoin_deposit_rate.md)