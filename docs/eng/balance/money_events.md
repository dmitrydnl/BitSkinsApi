# Money events
To get the history of events that have changed the balance of a BitSkins account, you need to call the function:
```csharp
BitSkinsApi.Balance.AccountBalance.GetAccountBalance(int page);
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
* int page - page number, up to 30 events per page

### Returns:
```csharp
List<BitSkinsApi.Balance.MoneyEvent>
```
Class properties ```BitSkinsApi.Balance.MoneyEvent```:
* Type - type of this event. Types of events:
1.ItemBought - purchase item
2.ItemSold – selling an item
3.SaleFee – sale commission
4.BuyCredit – refill balance
5.StoreCredit – bonus accrual
6.Unknown – types of events that have not yet been processed
* Amount - amount of money in the specified event (purchase price, selling price, commission etc.)
* Description - description of this event. It is different in each type:
1.ItemBought – “{The game ID of which the item belongs}:{Name of item purchased}”
2.ItemSold - “{The game ID of which the item belongs}:{Name of item sold}”
3.SaleFee – “{The name of the game items which were purchased}”
4.BuyCredit – “{Deposit method}”
5.StoreCredit – “{Reason for bonuses accrual (usually "steamname" means "BitSkins.com" is inserted in your Steam name)}”
* Time - the time of this event

## Example
```csharp
System.Collections.Generic.List<BitSkinsApi.Balance.MoneyEvent> moneyEvents = BitSkinsApi.Balance.MoneyEvents.GetMoneyEvents(1);
foreach (BitSkinsApi.Balance.MoneyEvent moneyEvent in moneyEvents)
{
    System.Console.WriteLine(moneyEvent.Type);
    System.Console.WriteLine(moneyEvent.Amount);
}
```