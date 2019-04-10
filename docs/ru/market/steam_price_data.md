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