# Trade offer details

To get the details about trade offer (the information will not be available 7 days after the creation the trade offer), you need to call the function:

```csharp
BitSkinsApi.Trade.Details.GetTradeDetails(string tradeToken, string tradeId);
```

## GetTradeDetails()

### Is in class:

```csharp
BitSkinsApi.Trade.Details
```

### Function:

```csharp
BitSkinsApi.Trade.Details.GetTradeDetails(string tradeToken, string tradeId);
```

### Function parameters:
* string tradeToken - trade offer token, is in the message attached to the trade offer.
* string tradeId - trade offer id, is in the message attached to the trade offer.

### Returns:

```csharp
BitSkinsApi.Trade.TradeDetails
```

Class properties ```BitSkinsApi.Trade.TradeDetails```:
* SentItems - list of sent items.
* RetrievedItems - list of retrieved items.
* CreatedAt - trade offer creation date, may be null.

### Possible exceptions
```BitSkinsApi.Server.RequestServerException``` - in case of transfer to the function incorrect data or problems on the BitSkins server.

## Example

```csharp
BitSkinsApi.Trade.TradeDetails details = BitSkinsApi.Trade.Details.GetTradeDetails("trade token", "trade id");
Console.WriteLine("Sent items:");
foreach (BitSkinsApi.Trade.SentItem item in details.SentItems)
{
    Console.WriteLine(" - " + item.MarketHashName + " " + item.ItemId);
}
Console.WriteLine("Retrieved items:");
foreach (BitSkinsApi.Trade.RetrievedItem item in details.RetrievedItems)
{
    Console.WriteLine(" - " + item.ItemId);
}
```

[<Recent trade offers](https://github.com/Captious99/BitSkinsApi/blob/master/docs/eng/trade/recent_trade_offers.md) &nbsp;&nbsp;&nbsp;&nbsp; [Create buy order>](https://github.com/Captious99/BitSkinsApi/blob/master/docs/eng/buy_order/create_buy_order.md)