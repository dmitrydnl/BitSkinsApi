# Bitcoin payment request

To form a request for payment of bitcoins (you can deposit more than 0.0002 BTC to your Bitcoin address), you need to call the function:

```csharp
BitSkinsApi.Crypto.CreatingBitcoinDeposit.CreateBitcoinDeposit(double amount);
```

## CreateBitcoinDeposit()

### Is in class:

```csharp
BitSkinsApi.Crypto.CreatingBitcoinDeposit
```

### Function:

```csharp
BitSkinsApi.Crypto.CreatingBitcoinDeposit.CreateBitcoinDeposit(double amount);
```

### Function parameters:

* double amount - amount to transfer in USD (minimum 0.0002 BTC).

### Returns:

```csharp
BitSkinsApi.Crypto.CreatedBitcoinDeposit
```

Class properties ```BitSkinsApi.Crypto.CreatedBitcoinDeposit```:
* Id - request id.
* AmountInUsd? - amount in USD.
* BitcoinAddress - your bitcoin deposit address.
* BitcoinAmount? - amount in Bitcoin.
* BitcoinUri - Bitcoin URI.
* CurrentPricePerBitcoinInUsd? - the price of one Bitcoin in USD.
* CreatedAt? - request creation date.
* ExpiresAt? - request expiration date.
* Note - note.
* Provider - provider.

### Possible exceptions
```ArgumentException``` - in case of transfer to the function incorrect data, the message contains detailed information.
\
```BitSkinsApi.Server.RequestServerException``` - in case of transfer to the function incorrect data or problems on the BitSkins server.

## Example

```csharp
BitSkinsApi.Crypto.CreatedBitcoinDeposit createdBitcoinDeposit = BitSkinsApi.Crypto.CreatingBitcoinDeposit.CreateBitcoinDeposit(10);
Console.WriteLine(createdBitcoinDeposit.Id);
Console.WriteLine(createdBitcoinDeposit.BitcoinAddress);
Console.WriteLine(createdBitcoinDeposit.BitcoinAmount);
Console.WriteLine(createdBitcoinDeposit.BitcoinUri);
```

[<Current exchange rate Bitcoin to USD](https://github.com/dmitrydnl/BitSkinsApi/blob/master/docs/eng/crypto/bitcoin_deposit_rate.md) &nbsp;&nbsp;&nbsp;&nbsp; [Search for profitable items in BitSkins>](https://github.com/dmitrydnl/BitSkinsApi/blob/master/docs/eng/code_examples/find_profitable_items.md)