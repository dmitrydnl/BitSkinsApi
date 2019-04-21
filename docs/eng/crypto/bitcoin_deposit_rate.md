# Current exchange rate Bitcoin to USD

To get the current exchange rate of Bitcoin to the USD and the time of expiration of this exchange rate, you need to call the function:

```csharp
BitSkinsApi.Crypto.BitcoinExchangeRate.GetBitcoinDepositRate();
```

## GetBitcoinDepositRate()

### Is in class:

```csharp
BitSkinsApi.Crypto.BitcoinExchangeRate
```

### Function:

```csharp
BitSkinsApi.Crypto.BitcoinExchangeRate.GetBitcoinDepositRate();
```

### Returns:

```csharp
BitSkinsApi.Crypto.BitcoinDepositRate
```

Class properties ```BitSkinsApi.Crypto.BitcoinDepositRate```:
* PricePerBitcoinInUsd - the price of one Bitcoin in USD.
* ExpiresAt - the expiration date of this exchange rate.
* ExpiresIn - seconds before the expiration of this exchange rate.

### Possible exceptions
```BitSkinsApi.Server.RequestServerException``` - in case of transfer to the function incorrect data or problems on the BitSkins server.

## Example

```csharp
BitSkinsApi.Crypto.BitcoinDepositRate bitcoinDepositRate = BitSkinsApi.Crypto.BitcoinExchangeRate.GetBitcoinDepositRate();
Console.WriteLine(bitcoinDepositRate.PricePerBitcoinInUsd);
Console.WriteLine(bitcoinDepositRate.ExpiresIn);
```

[<Bitcoin address for deposit](https://github.com/Captious99/BitSkinsApi/blob/master/docs/eng/crypto/bitcoin_deposit_address.md) &nbsp;&nbsp;&nbsp;&nbsp; [Bitcoin payment request>](https://github.com/Captious99/BitSkinsApi/blob/master/docs/eng/crypto/create_bitcoin_deposit.md)