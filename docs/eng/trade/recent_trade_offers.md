# Recent trade offers

To get information about the last 50  trade offers sent by BitSkins you need to call the function:

```csharp
BitSkinsApi.Trade.RecentOffers.GetRecentTradeOffers(bool activeOnly);
```

## GetRecentTradeOffers()

### Is in class:

```csharp
BitSkinsApi.Trade.RecentOffers
```

### Function:

```csharp
BitSkinsApi.Trade.RecentOffers.GetRecentTradeOffers(bool activeOnly);
```

### Function parameters:
* bool activeOnly - the value must be 'true' if you need only the currently active trade offers.

### Returns:

```csharp
List<BitSkinsApi.Trade.RecentTradeOffer>
```

Class properties ```BitSkinsApi.Trade.RecentTradeOffer```:
* SteamTradeOfferId - trade offer id.
* SteamTradeOfferStatus - trade offer status.
* SenderUid - sender's uid.
* RecipientUid - recipient's uid.
* NumItemsSent - number of items sent.
* NumItemsRetrieved - number of items received.
* BitSkinsTradeToken - BitSkins trade token, for verification of the trade offer.
* BitSkinsTradeId - BitSkins trade id, for verification of the trade offer.
* TradeMessage - the message in the trade offer, contains BitSkinsTradeToken and BitSkinsTradeId.
* CreatedAt - trade offer creation date.
* UpdatedAt - trade offer update date.

### Possible exceptions
```BitSkinsApi.Server.RequestServerException``` - in case of transfer to the function incorrect data or problems on the BitSkins server.

## Example

```csharp
List<BitSkinsApi.Trade.RecentTradeOffer> offers = BitSkinsApi.Trade.RecentOffers.GetRecentTradeOffers(false);
foreach (BitSkinsApi.Trade.RecentTradeOffer offer in offers)
{
    Console.WriteLine(offer.TradeMessage);
    Console.WriteLine("Items retrieved: " + offer.NumItemsRetrieved + " Items sent: " + offer.NumItemsSent);
    Console.WriteLine("Created at: " + offer.CreatedAt);
}
```

[<Items requiring a reset price](https://github.com/dmitrydnl/BitSkinsApi/blob/master/docs/eng/market/reset_price_items.md) &nbsp;&nbsp;&nbsp;&nbsp; [Trade offer details>](https://github.com/dmitrydnl/BitSkinsApi/blob/master/docs/eng/trade/trade_details.md)