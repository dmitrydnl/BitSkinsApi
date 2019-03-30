using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using BitSkinsApi.Extensions;

namespace BitSkinsApi.Market
{
    /// <summary>
    /// Work with BitSkins inventory on sale.
    /// </summary>
    public static class InventoryOnSale
    {
        /// <summary>
        /// Sorting criterion.
        /// </summary>
        public enum SortBy { Price, CreatedAt, BumpedAt, Not };
        /// <summary>
        /// Sort order.
        /// </summary>
        public enum SortOrder { Asc, Desc, Not };
        /// <summary>
        /// Optional parametr.
        /// </summary>
        public enum ThreeChoices { True = 1, False = -1, NotImportant = 0 };
        /// <summary>
        /// Results per page.
        /// </summary>
        public enum ResultsPerPage { R30 = 30, R480 = 480 };

        /// <summary>
        /// Allows you to retrieve the BitSkins inventory currently on sale. This includes items you cannot buy (i.e., items listed for sale by you). By default, upto 30 items per page, and optionally up to 480 items per page. This method allows you to search the inventory just as the search function on the website allows you to search inventory.
        /// </summary>
        /// <param name="app">For the inventory's game.</param>
        /// <param name="page">Page number.</param>
        /// <param name="marketHashName">Full or partial item name. (optional)</param>
        /// <param name="minPrice">Minimum price. (optional)</param>
        /// <param name="maxPrice">Maximum price. (optional)</param>
        /// <param name="sortBy">{created_at|price}. (optional)</param>
        /// <param name="sortOrder">{desc|asc}. (optional)</param>
        /// <param name="hasStickers">{-1|0|1}. For CS:GO only. (optional)</param>
        /// <param name="isStattrak">{-1|0|1}. For CS:GO only. (optional)</param>
        /// <param name="isSouvenir">{-1|0|1}. For CS:GO only. (optional)</param>
        /// <param name="resultsPerPage">Results per page. Must be either 30, or 480.</param>
        /// <param name="tradeDelayedItems">{-1|0|1}. For CS:GO only. (optional)</param>
        /// <returns>List of items on sale.</returns>
        public static List<ItemOnSale> GetInventoryOnSale(AppId.AppName app, int page, string marketHashName, double minPrice, double maxPrice, 
            SortBy sortBy, SortOrder sortOrder, ThreeChoices hasStickers, ThreeChoices isStattrak, ThreeChoices isSouvenir, 
            ResultsPerPage resultsPerPage, ThreeChoices tradeDelayedItems)
        {
            StringBuilder url = new StringBuilder($"https://bitskins.com/api/v1/get_inventory_on_sale/");
            url.Append($"?api_key={Account.AccountData.GetApiKey()}");
            url.Append($"&page={page}");
            url.Append($"&app_id={(int)app}");
            url.Append($"&per_page={(int)resultsPerPage}");
            url.Append($"&code={Account.Secret.GetTwoFactorCode()}");
            
            if (!string.IsNullOrEmpty(marketHashName))
            {
                url.Append($"&market_hash_name={marketHashName}");
            }

            if (minPrice > 0)
            {
                url.Append($"&min_price={minPrice}");
            }

            if (maxPrice > 0)
            {
                url.Append($"&max_price={maxPrice}");
            }

            if (sortBy != SortBy.Not)
            {
                url.Append($"&sort_by={SortByToString(sortBy)}");
            }
            
            if (sortOrder != SortOrder.Not)
            {
                url.Append($"&order={SortOrderToString(sortOrder)}");
            }
            
            if (app == AppId.AppName.CounterStrikGlobalOffensive)
            {
                url.Append($"&has_stickers={(int)hasStickers}");
                url.Append($"&is_stattrak={(int)isStattrak}");
                url.Append($"&is_souvenir={(int)isSouvenir}");
                url.Append($"&show_trade_delayed_items={(int)tradeDelayedItems}");
            }
            
            string result = Server.ServerRequest.RequestServer(url.ToString());
            List<ItemOnSale> itemsOnSale = ReadItemsOnSale(result);
            return itemsOnSale;
        }

        static List<ItemOnSale> ReadItemsOnSale(string result)
        {
            dynamic responseServer = JsonConvert.DeserializeObject(result);
            dynamic items = responseServer.data.items;

            if (items == null)
            {
                return new List<ItemOnSale>();
            }

            List<ItemOnSale> itemsOnSale = new List<ItemOnSale>();
            foreach (dynamic item in items)
            {
                ItemOnSale itemOnSale = ReadItemOnSale(item);
                itemsOnSale.Add(itemOnSale);
            }

            return itemsOnSale;
        }

        internal static ItemOnSale ReadItemOnSale(dynamic item)
        {
            string itemId = item.item_id;
            string marketHashName = item.market_hash_name;
            string itemType = item.item_type;
            string image = item.image;
            double price = item.price;
            double? suggestedPrice = null;
            if (item.suggested_price != null)
            {
                suggestedPrice = (double)item.suggested_price;
            }
            double? floatValue = null;
            if (item.float_value != null)
            {
                floatValue = (double)item.float_value;
            }
            bool isMine = item.is_mine;
            DateTime updatedAt = DateTimeExtension.FromUnixTime((long)item.updated_at);
            DateTime withdrawableAt = DateTimeExtension.FromUnixTime((long)item.withdrawable_at);

            ItemOnSale itemOnSale = new ItemOnSale(itemId, marketHashName, itemType, image, price,
                suggestedPrice, floatValue, isMine, updatedAt, withdrawableAt);
            return itemOnSale;
        }

        static string SortOrderToString(SortOrder sortOrder)
        {
            switch (sortOrder)
            {
                case SortOrder.Asc:
                    return "asc";
                case SortOrder.Desc:
                    return "desc";
                default:
                    return "";
            }
        }

        static string SortByToString(SortBy sortBy)
        {
            switch (sortBy)
            {
                case SortBy.Price:
                    return "price";
                case SortBy.CreatedAt:
                    return "created_at";
                case SortBy.BumpedAt:
                    return "bumped_at";
                default:
                    return "";
            }
        }
    }

    /// <summary>
    /// Work with specific items on sale.
    /// </summary>
    public static class SpecificItemsOnSale
    {
        /// <summary>
        /// Allows you to retrieve data for specific Item IDs that are currently on sale. To gather Item IDs you wish to track/query, see the 'Get Inventory on Sale' API call for items currently on sale.
        /// </summary>
        /// <param name="app">For the inventory's game.</param>
        /// <param name="itemIds">Upto 250 item IDs.</param>
        /// <returns>Specific items on sale on BitSkins.</returns>
        public static SpecificItems GetSpecificItemsOnSale(AppId.AppName app, List<string> itemIds)
        {
            string delimiter = ",";

            StringBuilder itemIdsStr = new StringBuilder();
            for (int i = 0; i < itemIds.Count; i++)
            {
                itemIdsStr.Append(itemIds[i]);
                itemIdsStr.Append((i < itemIds.Count - 1) ? delimiter : "");
            }

            StringBuilder url = new StringBuilder($"https://bitskins.com/api/v1/get_specific_items_on_sale/");
            url.Append($"?api_key={Account.AccountData.GetApiKey()}");
            url.Append($"&item_ids={itemIdsStr.ToString()}");
            url.Append($"&app_id={(int)app}");
            url.Append($"&code={Account.Secret.GetTwoFactorCode()}");

            string result = Server.ServerRequest.RequestServer(url.ToString());
            SpecificItems specificItems = ReadSpecificItems(result);
            return specificItems;
        }

        static SpecificItems ReadSpecificItems(string result)
        {
            dynamic responseServer = JsonConvert.DeserializeObject(result);
            dynamic itemsOnSaleD = responseServer.data.items_on_sale;
            dynamic itemsNotOnSaleD = responseServer.data.items_not_on_sale;

            List<ItemOnSale> itemsOnSale = new List<ItemOnSale>();
            if (itemsOnSaleD != null)
            {
                foreach (dynamic item in itemsOnSaleD)
                {
                    ItemOnSale itemOnSale = InventoryOnSale.ReadItemOnSale(item);
                    itemsOnSale.Add(itemOnSale);
                }
            }

            List<string> itemsNotOnSale = new List<string>();
            if (itemsNotOnSaleD != null)
            {
                foreach (dynamic item in itemsNotOnSaleD)
                {
                    string itemNotOnSale = (string)item;
                    itemsNotOnSale.Add(itemNotOnSale);
                }
            }

            SpecificItems specificItems = new SpecificItems(itemsOnSale, itemsNotOnSale);
            return specificItems;
        }
    }

    /// <summary>
    /// Item on sale in BitSkins inventory.
    /// </summary>
    public class ItemOnSale
    {
        public string ItemId { get; private set; }
        public string MarketHashName { get; private set; }
        public string ItemType { get; private set; }
        public string Image { get; private set; }
        public double Price { get; private set; }
        public double? SuggestedPrice { get; private set; }
        public double? FloatValue { get; private set; }
        public bool IsMine { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public DateTime WithdrawableAt { get; private set; }

        internal ItemOnSale(string itemId, string marketHashName, string itemType, string image, double price, 
            double? suggestedPrice, double? floatValue, bool isMine, DateTime updatedAt, DateTime withdrawableAt)
        {
            ItemId = itemId;
            MarketHashName = marketHashName;
            ItemType = itemType;
            Image = image;
            Price = price;
            SuggestedPrice = suggestedPrice;
            FloatValue = floatValue;
            IsMine = isMine;
            UpdatedAt = updatedAt;
            WithdrawableAt = withdrawableAt;
        }
    }

    /// <summary>
    /// Specific items on sale on BitSkins.
    /// </summary>
    public class SpecificItems
    {
        public List<ItemOnSale> ItemsOnSale { get; private set; }
        public List<string> ItemsNotOnSale { get; private set; }

        internal SpecificItems(List<ItemOnSale> itemsOnSale, List<string> itemsNotOnSale)
        {
            ItemsOnSale = itemsOnSale;
            ItemsNotOnSale = itemsNotOnSale;
        }
    }
}
