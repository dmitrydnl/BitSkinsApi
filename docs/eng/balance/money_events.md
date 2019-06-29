# Money events

To get the history of events that have changed the balance of a BitSkins account, you need to call the function:

```csharp
BitSkinsApi.Balance.MoneyEvents.GetMoneyEvents(int page);
```

## GetMoneyEvents()

### Is in class:

```csharp
BitSkinsApi.Balance.MoneyEvents
```

### Function:

```csharp
BitSkinsApi.Balance.MoneyEvents.GetMoneyEvents(int page);
```

### Function parameters:

* int page - page number, up to 30 events per page.

### Returns:

```csharp
List<BitSkinsApi.Balance.MoneyEvent>
```

Class properties ```BitSkinsApi.Balance.MoneyEvent```:
* Type - type of this event. Types of events:
  - ItemBought - purchase item.
  - ItemSold – selling an item.
  - SaleFee – sale commission.
  - BuyCredit – refill balance.
  - StoreCredit – bonus accrual.
  - Unknown – types of events that have not yet been processed.
* Amount - amount of money in the specified event (purchase price, selling price, commission etc.).
* Description - description of this event. It is different in each type:
  - ItemBought – “{The game ID of which the item belongs}:{Name of item purchased}”.
  - ItemSold - “{The game ID of which the item belongs}:{Name of item sold}”.
  - SaleFee – “{The name of the game items which were purchased}”.
  - BuyCredit – “{Deposit method}”.
  - StoreCredit – “{Reason for bonuses accrual (usually "steamname" means "BitSkins.com" is inserted in your Steam name)}”.
* Time - the time of this event.

### Possible exceptions
```ArgumentException``` - in case of transfer to the function incorrect data, the message contains detailed information.
\
```BitSkinsApi.Server.RequestServerException``` - in case of transfer to the function incorrect data or problems on the BitSkins server.

## Example

```csharp
List<BitSkinsApi.Balance.MoneyEvent> moneyEvents = BitSkinsApi.Balance.MoneyEvents.GetMoneyEvents(1);
foreach (BitSkinsApi.Balance.MoneyEvent moneyEvent in moneyEvents)
{
    Console.WriteLine(moneyEvent.Type);
    Console.WriteLine(moneyEvent.Amount);
}
```

[<Account balance](https://github.com/dmitrydnl/BitSkinsApi/blob/master/docs/eng/balance/account_balance.md) &nbsp;&nbsp;&nbsp;&nbsp; [Withdrawing money from the balance>](https://github.com/dmitrydnl/BitSkinsApi/blob/master/docs/eng/balance/withdraw_money.md)