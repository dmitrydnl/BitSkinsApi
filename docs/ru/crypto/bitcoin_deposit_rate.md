# Текущий курс обмена Bitcoin к доллару США

Чтобы получить текущий курс обмена Bitcoin к доллару США и время истечения этого курса обмена, нужно вызвать функцию:

```csharp
BitSkinsApi.Crypto.BitcoinExchangeRate.GetBitcoinDepositRate();
```

## GetBitcoinDepositRate()

### Находится в классе:

```csharp
BitSkinsApi.Crypto.BitcoinExchangeRate
```

### Функция:

```csharp
BitSkinsApi.Crypto.BitcoinExchangeRate.GetBitcoinDepositRate();
```

### Возвращает:

```csharp
BitSkinsApi.Crypto.BitcoinDepositRate
```

Свойства класса ```BitSkinsApi.Crypto.BitcoinDepositRate```:
* PricePerBitcoinInUsd - цена одного Bitcoin в долларах США.
* ExpiresAt - дата истечения этого курса обмена.
* ExpiresIn - секунд до истечения этого курса обмена.

### Возможные исключения
```BitSkinsApi.Server.RequestServerException``` - в случае передачи в функцию некорректных данных или проблем на сервере BitSkins.

## Пример

```csharp
BitSkinsApi.Crypto.BitcoinDepositRate bitcoinDepositRate = BitSkinsApi.Crypto.BitcoinExchangeRate.GetBitcoinDepositRate();
Console.WriteLine(bitcoinDepositRate.PricePerBitcoinInUsd);
Console.WriteLine(bitcoinDepositRate.ExpiresIn);
```

[<Bitcoin адрес для депозита](https://github.com/dmitrydnl/BitSkinsApi/blob/master/docs/ru/crypto/bitcoin_deposit_address.md) &nbsp;&nbsp;&nbsp;&nbsp; [Запрос на оплату Bitcoin>](https://github.com/dmitrydnl/BitSkinsApi/blob/master/docs/ru/crypto/create_bitcoin_deposit.md)