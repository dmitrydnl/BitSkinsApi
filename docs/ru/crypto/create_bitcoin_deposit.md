# Запрос на оплату Bitcoin

Чтобы сформировать запрос на оплату биткойнов (вы можете внести более 0,0002 BTC на ваш Bitcoin адрес), нужно вызвать функцию:

```csharp
BitSkinsApi.Crypto.CreatingBitcoinDeposit.CreateBitcoinDeposit(double amount);
```

## CreateBitcoinDeposit()

### Находится в классе:

```csharp
BitSkinsApi.Crypto.CreatingBitcoinDeposit
```

### Функция:

```csharp
BitSkinsApi.Crypto.CreatingBitcoinDeposit.CreateBitcoinDeposit(double amount);
```

### Параметры функции:

* double amount - сумма для перевода в долларах США (минимум 0,0002 BTC).

### Возвращает:

```csharp
BitSkinsApi.Crypto.CreatedBitcoinDeposit
```

Свойства класса ```BitSkinsApi.Crypto.CreatedBitcoinDeposit```:
* Id - id запроса, может быть null.
* AmountInUsd - сумма в долларах США.
* BitcoinAddress - ваш Bitcoin адрес для пополнения.
* BitcoinAmount - сумма в Bitcoin.
* BitcoinUri - Bitcoin URI.
* CurrentPricePerBitcoinInUsd - цена одного Bitcoin в долларах США.
* CreatedAt - дата создания запроса, может быть null.
* ExpiresAt - дата истечения запроса, может быть null.
* Note - заметка.
* Provider - провайдер.

### Возможные исключения
```ArgumentException``` - в случае передачи в функцию некорректных данных, в сообщение содержится подробная информация.
\
```BitSkinsApi.Server.RequestServerException``` - в случае передачи в функцию некорректных данных или проблем на сервере BitSkins.

## Пример

```csharp
BitSkinsApi.Crypto.CreatedBitcoinDeposit createdBitcoinDeposit = BitSkinsApi.Crypto.CreatingBitcoinDeposit.CreateBitcoinDeposit(10);
Console.WriteLine(createdBitcoinDeposit.Id);
Console.WriteLine(createdBitcoinDeposit.BitcoinAddress);
Console.WriteLine(createdBitcoinDeposit.BitcoinAmount);
Console.WriteLine(createdBitcoinDeposit.BitcoinUri);
```

[<Текущий курс обмена Bitcoin к доллару США](https://github.com/dmitrydnl/BitSkinsApi/blob/master/docs/ru/crypto/bitcoin_deposit_rate.md) &nbsp;&nbsp;&nbsp;&nbsp; [Поиск выгодных предметов в BitSkins>](https://github.com/dmitrydnl/BitSkinsApi/blob/master/docs/ru/code_examples/find_profitable_items.md)