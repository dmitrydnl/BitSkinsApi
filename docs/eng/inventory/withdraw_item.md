# Withdraw items from BitSkins

In order to withdraw items from BitSkins, you need to call the function:

```csharp
BitSkinsApi.Inventory.WithdrawalOfItems.WithdrawItem(BitSkinsApi.Market.AppId.AppName app, List<string> itemIds);
```

## WithdrawItem()

### Is in class:

```csharp
BitSkinsApi.Inventory.WithdrawalOfItems
```

### Function:

```csharp
BitSkinsApi.Inventory.WithdrawalOfItems.WithdrawItem(BitSkinsApi.Market.AppId.AppName app, List<string> itemIds);
```

### Function parameters:

* BitSkinsApi.Market.AppId.AppName app - game whose items are withdrawn.
* List<string> itemIds - list of id items that are withdrawn.

### Returns:

```csharp
BitSkinsApi.Inventory.InformationAboutWithdrawn
```

Class properties ```BitSkinsApi.Inventory.InformationAboutWithdrawn```:
* WithdrawnItems - items that were withdrawn from BitSkins.
* TradeTokens - trade tokens.

## Example

```csharp
BitSkinsApi.Market.AppId.AppName app = BitSkinsApi.Market.AppId.AppName.CounterStrikGlobalOffensive;
List<string> itemIds = new List<string> { "id1", "id2" };
BitSkinsApi.Inventory.InformationAboutWithdrawn information = BitSkinsApi.Inventory.WithdrawalOfItems.WithdrawItem(app, itemIds);
Console.WriteLine("Trade tokens:");
foreach (string token in information.TradeTokens)
{
    Console.WriteLine(token);
}
Console.WriteLine("Items:");
foreach (BitSkinsApi.Inventory.WithdrawnItem item in information.WithdrawnItems)
{
    Console.WriteLine(item.ItemId);
}
```