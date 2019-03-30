using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using BitSkinsApi.Extensions;

namespace BitSkinsApi.Inventory
{
    /// <summary>
    /// Work with account inventorys.
    /// </summary>
    public static class AccountInventory
    {
        /// <summary>
        /// Allows you to retrieve your account's available inventory on Steam (items listable for sale), your BitSkins inventory (items currently on sale), and your pending withdrawal inventory (items you delisted or purchased). 
        /// </summary>
        /// <param name="app">For the inventory's game.</param>
        /// <param name="page">Page number for BitSkins inventory.</param>
        /// <returns>User's inventory.</returns>
        public static AccountInventorys GetAccountInventory(Market.AppId.AppName app, int page)
        {
            string url = $"https://bitskins.com/api/v1/get_my_inventory/" +
                $"?api_key={Account.AccountData.GetApiKey()}" +
                $"&page={page}" +
                $"&app_id={(int)app}" +
                $"&code={Account.Secret.GetTwoFactorCode()}";

            string result = Server.ServerRequest.RequestServer(url);
            AccountInventorys accountInventorys = ReadAccountInventorys(result);
            return accountInventorys;
        }

        static AccountInventorys ReadAccountInventorys(string result)
        {
            dynamic responseServer = JsonConvert.DeserializeObject(result);
            dynamic data = responseServer.data;

            SteamInventory steamInventory = null;
            if (data.steam_inventory != null)
            {
                dynamic inventory = data.steam_inventory;
                dynamic items = inventory.items;

                int totalItems = inventory.total_items;

                List<SteamInventoryItem> steamInventoryItems = new List<SteamInventoryItem>();
                if (items != null)
                {
                    foreach (dynamic item in items)
                    {
                        ReadMyInventoryItem(item, out string marketHashName, out double suggestedPrice, out string itemType, out string image);
                        int numberOfItems = item.number_of_items;
                        double recentAveragePrice = (item.recent_sales_info != null) ? (double)item.recent_sales_info.average_price : 0;

                        List<string> itemIds = new List<string>();
                        foreach (dynamic itemId in item.item_ids)
                        {
                            itemIds.Add((string)itemId);
                        }

                        SteamInventoryItem steamInventoryItem = new SteamInventoryItem(marketHashName, suggestedPrice, 
                            itemType, image, numberOfItems, itemIds, recentAveragePrice);
                        steamInventoryItems.Add(steamInventoryItem);
                    }
                }

                steamInventory = new SteamInventory(totalItems, steamInventoryItems);
            }

            BitSkinsInventory bitSkinsInventory = null;
            if (data.bitskins_inventory != null)
            {
                dynamic inventory = data.bitskins_inventory;
                dynamic items = inventory.items;

                int totalItems = inventory.total_items;
                double TotalPrice = inventory.total_price;

                List<BitSkinsInventoryItem> bitSkinsInventoryItems = new List<BitSkinsInventoryItem>();
                if (items != null)
                {
                    foreach (dynamic item in items)
                    {
                        ReadMyInventoryItem(item, out string marketHashName, out double suggestedPrice, out string itemType, out string image);
                        int numberOfItems = item.number_of_items;
                        double recentAveragePrice = (item.recent_sales_info != null) ? (double)item.recent_sales_info.average_price : 0;

                        List<string> itemIds = new List<string>();
                        foreach (dynamic itemId in item.item_ids)
                        {
                            itemIds.Add((string)itemId);
                        }

                        List<double> prices = new List<double>();
                        foreach (dynamic price in item.prices)
                        {
                            prices.Add((double)price);
                        }

                        List<DateTime> createdAt = new List<DateTime>();
                        foreach (dynamic date in item.created_at)
                        {
                            createdAt.Add(DateTimeExtension.FromUnixTime((long)date));
                        }

                        List<DateTime> updatedAt = new List<DateTime>();
                        foreach (dynamic date in item.updated_at)
                        {
                            updatedAt.Add(DateTimeExtension.FromUnixTime((long)date));
                        }

                        List<DateTime> withdrawableAt = new List<DateTime>();
                        foreach (dynamic date in item.withdrawable_at)
                        {
                            withdrawableAt.Add(DateTimeExtension.FromUnixTime((long)date));
                        }

                        BitSkinsInventoryItem bitSkinsInventoryItem = new BitSkinsInventoryItem(marketHashName, suggestedPrice, itemType, image,
                            numberOfItems, itemIds, prices, createdAt, updatedAt, withdrawableAt, recentAveragePrice);
                        bitSkinsInventoryItems.Add(bitSkinsInventoryItem);
                    }
                }

                bitSkinsInventory = new BitSkinsInventory(totalItems, TotalPrice, bitSkinsInventoryItems);
            }

            PendingWithdrawalFromBitskinsInventory pendingWithdrawalFromBitskinsInventory = null;
            if (data.pending_withdrawal_from_bitskins != null)
            {
                dynamic inventory = data.pending_withdrawal_from_bitskins;
                dynamic items = inventory.items;

                int totalItems = inventory.total_items;

                List<PendingWithdrawalFromBitskinsInventoryItem> pendingWithdrawalFromBitskinsInventoryItems = new List<PendingWithdrawalFromBitskinsInventoryItem>();
                if (items != null)
                {
                    foreach (dynamic item in items)
                    {
                        ReadMyInventoryItem(item, out string marketHashName, out double suggestedPrice, out string itemType, out string image);
                        string itemId = item.item_id;
                        DateTime withdrawableAt = DateTimeExtension.FromUnixTime((long)item.withdrawable_at);
                        double lastPrice = item.last_price;

                        PendingWithdrawalFromBitskinsInventoryItem pendingWithdrawalFromBitskinsInventoryItem = new PendingWithdrawalFromBitskinsInventoryItem(
                            marketHashName, suggestedPrice, itemType, image, itemId, withdrawableAt, lastPrice);
                        pendingWithdrawalFromBitskinsInventoryItems.Add(pendingWithdrawalFromBitskinsInventoryItem);
                    }
                }

                pendingWithdrawalFromBitskinsInventory = new PendingWithdrawalFromBitskinsInventory(totalItems, pendingWithdrawalFromBitskinsInventoryItems);
            }

            AccountInventorys accountInventorys = new AccountInventorys(steamInventory, bitSkinsInventory, pendingWithdrawalFromBitskinsInventory);
            return accountInventorys;
        }
        
        static void ReadMyInventoryItem(dynamic item, out string marketHashName, out double suggestedPrice, out string itemType, out string image)
        {
            marketHashName = item.market_hash_name;
            suggestedPrice = item.suggested_price;
            itemType = item.item_type;
            image = item.image;
        }
    }

    /// <summary>
    /// All user's inventorys.
    /// </summary>
    public class AccountInventorys
    {
        public SteamInventory SteamInventory { get; private set; }
        public BitSkinsInventory BitSkinsInventory { get; private set; }
        public PendingWithdrawalFromBitskinsInventory PendingWithdrawalFromBitskinsInventory { get; private set; }

        internal AccountInventorys(SteamInventory steamInventory, BitSkinsInventory bitSkinsInventory,
            PendingWithdrawalFromBitskinsInventory pendingWithdrawalFromBitskinsInventory)
        {
            SteamInventory = steamInventory;
            BitSkinsInventory = bitSkinsInventory;
            PendingWithdrawalFromBitskinsInventory = pendingWithdrawalFromBitskinsInventory;
        }
    }

    /// <summary>
    /// User's inventory.
    /// </summary>
    public abstract class MyInventory
    {
        public int TotalItems { get; private set; }

        protected MyInventory(int totalItems)
        {
            TotalItems = totalItems;
        }
    }

    /// <summary>
    /// User's Steam inventory.
    /// </summary>
    public class SteamInventory : MyInventory
    {
        public List<SteamInventoryItem> SteamInventoryItems { get; private set; }

        internal SteamInventory(int totalItems, List<SteamInventoryItem> steamInventoryItems)
            : base(totalItems)
        {
            SteamInventoryItems = steamInventoryItems;
        }
    }

    /// <summary>
    /// User's BitSkins inventory.
    /// </summary>
    public class BitSkinsInventory : MyInventory
    {
        public double TotalPrice { get; private set; }
        public List<BitSkinsInventoryItem> BitSkinsInventoryItems { get; private set; }

        internal BitSkinsInventory(int totalItems, double totalPrice, List<BitSkinsInventoryItem> bitSkinsInventoryItems)
             : base(totalItems)
        {
            TotalPrice = totalPrice;
            BitSkinsInventoryItems = bitSkinsInventoryItems;
        }
    }

    /// <summary>
    /// User's pending withdrawal from BitSkins inventory.
    /// </summary>
    public class PendingWithdrawalFromBitskinsInventory : MyInventory
    {
        public List<PendingWithdrawalFromBitskinsInventoryItem> PendingWithdrawalFromBitskinsInventoryItems { get; private set; }

        internal PendingWithdrawalFromBitskinsInventory(int totalItems, 
            List<PendingWithdrawalFromBitskinsInventoryItem> pendingWithdrawalFromBitskinsInventoryItems)
            : base(totalItems)
        {
            PendingWithdrawalFromBitskinsInventoryItems = pendingWithdrawalFromBitskinsInventoryItems;
        }
    }

    /// <summary>
    /// Inventory's item.
    /// </summary>
    public abstract class MyInventoryItem
    {
        public string MarketHashName { get; private set; }
        public double SuggestedPrice { get; private set; }
        public string ItemType { get; private set; }
        public string Image { get; private set; }

        protected MyInventoryItem(string marketHashName, double suggestedPrice, string itemType, string image)
        {
            MarketHashName = marketHashName;
            SuggestedPrice = suggestedPrice;
            ItemType = itemType;
            Image = image;
        }
    }

    /// <summary>
    /// Steam inventory's item.
    /// </summary>
    public class SteamInventoryItem : MyInventoryItem
    {
        public int NumberOfItems { get; private set; }
        public List<string> ItemIds { get; private set; }
        public double RecentAveragePrice { get; private set; }

        internal SteamInventoryItem(string marketHashName, double suggestedPrice, string itemType, string image, 
            int numberOfItems, List<string> itemIds, double recentAveragePrice)
            : base(marketHashName, suggestedPrice, itemType, image)
        {
            NumberOfItems = numberOfItems;
            ItemIds = itemIds;
            RecentAveragePrice = recentAveragePrice;
        }
    }

    /// <summary>
    /// BitSkins inventory's item.
    /// </summary>
    public class BitSkinsInventoryItem : MyInventoryItem
    {
        public int NumberOfItems { get; private set; }
        public List<string> ItemIds { get; private set; }
        public List<double> Prices { get; private set; }
        public List<DateTime> CreatedAt { get; private set; }
        public List<DateTime> UpdatedAt { get; private set; }
        public List<DateTime> WithdrawableAt { get; private set; }
        public double RecentAveragePrice { get; private set; }

        internal BitSkinsInventoryItem(string marketHashName, double suggestedPrice, string itemType, string image,
            int numberOfItems, List<string> itemIds, List<double> prices, List<DateTime> createdAt, List<DateTime> updatedAt,
            List<DateTime> withdrawableAt, double recentAveragePrice)
            : base(marketHashName, suggestedPrice, itemType, image)
        {
            NumberOfItems = numberOfItems;
            ItemIds = itemIds;
            Prices = prices;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            WithdrawableAt = withdrawableAt;
            RecentAveragePrice = recentAveragePrice;
        }
    }

    /// <summary>
    /// Pending withdrawal from BitSkins inventory's item.
    /// </summary>
    public class PendingWithdrawalFromBitskinsInventoryItem : MyInventoryItem
    {
        public string ItemId { get; private set; }
        public DateTime WithdrawableAt { get; private set; }
        public double LastPrice { get; private set; }

        internal PendingWithdrawalFromBitskinsInventoryItem(string marketHashName, double suggestedPrice, string itemType, string image,
            string itemId, DateTime withdrawableAt, double lastPrice)
            : base(marketHashName, suggestedPrice, itemType, image)
        {
            ItemId = itemId;
            WithdrawableAt = withdrawableAt;
            LastPrice = lastPrice;
        }
    }
}
