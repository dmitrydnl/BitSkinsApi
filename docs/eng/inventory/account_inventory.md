# Account inventory

To get an account inventory (Steam inventory, BitSkins items for sale, items pending withdrawal from BitSkins) you need to call the function:

```csharp
BitSkinsApi.Inventory.Inventories.GetAccountInventory(BitSkinsApi.Market.AppId.AppName app, int page);
```

## GetAccountInventory()

### Is in class:

```csharp
BitSkinsApi.Inventory.Inventories
```

### Function:

```csharp
BitSkinsApi.Inventory.Inventories.GetAccountInventory(BitSkinsApi.Market.AppId.AppName app, int page);
```

### Function parameters:
* BitSkinsApi.Market.AppId.AppName app - game whose inventory is requested.
* int page - page number for BitSkins inventory (items currently on sale).

### Returns:

```csharp
BitSkinsApi.Inventory.AccountInventory
```

Class properties ```BitSkinsApi.Inventory.AccountInventory```:
* SteamInventory - account's inventory on Steam (items listable for sale).
* BitSkinsInventory - BitSkins inventory items currently on sale at BitSkins.
* PendingWithdrawalFromBitskinsInventory - items pending withdrawal from BitSkins.

### Possible exceptions
```ArgumentException``` - in case of transfer to the function incorrect data, the message contains detailed information.
\
```BitSkinsApi.Server.RequestServerException``` - in case of transfer to the function incorrect data or problems on the BitSkins server.

## Example

```csharp
BitSkinsApi.Market.AppId.AppName app = BitSkinsApi.Market.AppId.AppName.CounterStrikGlobalOffensive;
BitSkinsApi.Inventory.AccountInventory accountInventory = BitSkinsApi.Inventory.Inventories.GetAccountInventory(app, 1);
foreach (BitSkinsApi.Inventory.SteamInventoryItem item in accountInventory.SteamInventory.SteamInventoryItems)
{
    Console.WriteLine(item.MarketHashName);
    foreach (string id in item.ItemIds)
    {
        Console.WriteLine("- " + id);
    }
}
```

[<Withdrawing money from the balance](https://github.com/Captious99/BitSkinsApi/blob/master/docs/eng/balance/withdraw_money.md) &nbsp;&nbsp;&nbsp;&nbsp; [Withdraw items from BitSkins>](https://github.com/Captious99/BitSkinsApi/blob/master/docs/eng/inventory/withdraw_item.md)