# Bitcoin адрес для депозита

Чтобы получить постоянный bitcoin адрес для депозита на ваш аккаунт BitSkins, нужно вызвать функцию:

```csharp
BitSkinsApi.Crypto.AccountBitcoinDepositAddress.GetBitcoinDepositAddress();
```

## GetBitcoinDepositAddress()

### Находится в классе:

```csharp
BitSkinsApi.Crypto.AccountBitcoinDepositAddress
```

### Функция:

```csharp
BitSkinsApi.Crypto.AccountBitcoinDepositAddress.GetBitcoinDepositAddress();
```

### Возвращает:

```csharp
BitSkinsApi.Crypto.BitcoinDepositAddress
```

Свойства класса ```BitSkinsApi.Crypto.BitcoinDepositAddress```:
* Address - ваш постоянный bitcoin адрес на сайте BitSkins.
* MinimumAcceptableDepositAmount - минимальная допустимая сумма для перевода.

### Возможные исключения
```BitSkinsApi.Server.RequestServerException``` - в случае передачи в функцию некорректных данных или проблем на сервере BitSkins.

## Пример

```csharp
BitSkinsApi.Crypto.BitcoinDepositAddress bitcoinDepositAddress = BitSkinsApi.Crypto.AccountBitcoinDepositAddress.GetBitcoinDepositAddress();
Console.WriteLine(bitcoinDepositAddress.Address);
Console.WriteLine(bitcoinDepositAddress.MinimumAcceptableDepositAmount);
```

[<Сводка всех рыночных заказов на покупку](https://github.com/Captious99/BitSkinsApi/blob/master/docs/ru/buy_order/summarize_buy_orders.md) &nbsp;&nbsp;&nbsp;&nbsp; [Текущий курс обмена Bitcoin к доллару США>](https://github.com/Captious99/BitSkinsApi/blob/master/docs/ru/crypto/bitcoin_deposit_rate.md)