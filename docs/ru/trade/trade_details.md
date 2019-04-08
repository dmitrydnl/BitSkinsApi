# Подробности торгового предложения

Чтобы получить детали торгового предложения (информация станет не доступна через 7 дней после создания торгового предложения), нужно вызвать функцию:

```csharp
BitSkinsApi.Trade.Details.GetTradeDetails(string tradeToken, string tradeId);
```

## GetTradeDetails()

### Находится в классе:

```csharp
BitSkinsApi.Trade.Details
```

### Функция:

```csharp
BitSkinsApi.Trade.Details.GetTradeDetails(string tradeToken, string tradeId);
```

### Параметры функции:
* string tradeToken - токен торгового предложения, находится в прикреплённом к торговому предложению сообщении.
* string tradeId - id торгового предложения, находится в прикреплённом к торговому предложению сообщении.

### Возвращает:

```csharp
BitSkinsApi.Trade.TradeDetails
```

Свойства класса ```BitSkinsApi.Trade.TradeDetails```:
* SentItems - список отправленных предметов.
* RetrievedItems - список полученных предметов.
* CreatedAt - дата создания торгового предложения, может быть равна null.

## Пример

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