# Рыночные данные о стоимости предметов в Steam

Для того чтобы получить данные о рыночной цене предмета в Steam, нужно вызвать функцию:

```csharp
BitSkinsApi.Market.SteamRawPriceData.GetRawPriceData(BitSkinsApi.Market.AppId.AppName app, string marketHashName);
```

## GetRawPriceData()

### Находится в классе:

```csharp
BitSkinsApi.Market.SteamRawPriceData
```

### Функция:

```csharp
BitSkinsApi.Market.SteamRawPriceData.GetRawPriceData(BitSkinsApi.Market.AppId.AppName app, string marketHashName);
```

### Параметры функции:

* BitSkinsApi.Market.AppId.AppName app - игра, которой принадлежит запрашиваемый предмет.
* string marketHashName - название предмета, для которого запрашиваются данные.

### Возвращает:

```csharp
BitSkinsApi.Market.SteamItemRawPriceData
```

Свойства класса ```BitSkinsApi.Market.SteamItemRawPriceData```:
* ItemRawPrices - список записей о продаже предмета в конкретный период времени.
* UpdatedAt - дата обновления данных, может быть null.

### Возможные исключения
```ArgumentException``` - в случае передачи в функцию некорректных данных, в сообщение содержится подробная информация.
\
```BitSkinsApi.Server.RequestServerException``` - в случае передачи в функцию некорректных данных или проблем на сервере BitSkins.

## Пример

```csharp
BitSkinsApi.Market.AppId.AppName app = BitSkinsApi.Market.AppId.AppName.CounterStrikGlobalOffensive;
string name = "Operation Phoenix Weapon Case";
BitSkinsApi.Market.SteamItemRawPriceData rawPriceData = BitSkinsApi.Market.SteamRawPriceData.GetRawPriceData(app, name);
foreach (BitSkinsApi.Market.ItemRawPrice rawPrice in rawPriceData.ItemRawPrices)
{
    Console.WriteLine(rawPrice.Time);
    Console.WriteLine(rawPrice.Volume);
    Console.WriteLine(rawPrice.Price);
    Console.WriteLine();
}
```

[<Изменить продаваемый предмет](https://github.com/dmitrydnl/BitSkinsApi/blob/master/docs/ru/market/modify_sale.md) &nbsp;&nbsp;&nbsp;&nbsp; [Предметы требующие сброса цены>](https://github.com/dmitrydnl/BitSkinsApi/blob/master/docs/ru/market/reset_price_items.md)