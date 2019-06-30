# Недавние торговые предложения

Чтобы получить информацию о 50 последних торговых предложениях, отправленных BitSkins нужно вызвать функцию:

```csharp
BitSkinsApi.Trade.RecentOffers.GetRecentTradeOffers(bool activeOnly);
```

## GetRecentTradeOffers()

### Находится в классе:

```csharp
BitSkinsApi.Trade.RecentOffers
```

### Функция:

```csharp
BitSkinsApi.Trade.RecentOffers.GetRecentTradeOffers(bool activeOnly);
```

### Параметры функции:
* bool activeOnly - значение должно быть равно 'true', если вам нужны только торговые предложения, активные в данный момент.

### Возвращает:

```csharp
List<BitSkinsApi.Trade.RecentTradeOffer>
```

Свойства класса ```BitSkinsApi.Trade.RecentTradeOffer```:
* SteamTradeOfferId - id торгового предложения.
* SteamTradeOfferStatus - статус торгового предложения.
* SenderUid - uid отправителя.
* RecipientUid - uid получателя.
* NumItemsSent - кол-во отправленных предметов.
* NumItemsRetrieved - кол-во полученных предметов.
* BitSkinsTradeToken - BitSkins trade токен, для верификации торгового предложения.
* BitSkinsTradeId - BitSkins trade id, для верификации торгового предложения.
* TradeMessage - сообщение в торговом предложении, содержит BitSkinsTradeToken и BitSkinsTradeId.
* CreatedAt - дата создания торгового предложения.
* UpdatedAt - дата обновления торгового предложения.

### Возможные исключения
```BitSkinsApi.Server.RequestServerException``` - в случае передачи в функцию некорректных данных или проблем на сервере BitSkins.

## Пример

```csharp
List<BitSkinsApi.Trade.RecentTradeOffer> offers = BitSkinsApi.Trade.RecentOffers.GetRecentTradeOffers(false);
foreach (BitSkinsApi.Trade.RecentTradeOffer offer in offers)
{
    Console.WriteLine(offer.TradeMessage);
    Console.WriteLine("Items retrieved: " + offer.NumItemsRetrieved + " Items sent: " + offer.NumItemsSent);
    Console.WriteLine("Created at: " + offer.CreatedAt);
}
```

[<Предметы требующие сброса цены](https://github.com/dmitrydnl/BitSkinsApi/blob/master/docs/ru/market/reset_price_items.md) &nbsp;&nbsp;&nbsp;&nbsp; [Подробности торгового предложения>](https://github.com/dmitrydnl/BitSkinsApi/blob/master/docs/ru/trade/trade_details.md)