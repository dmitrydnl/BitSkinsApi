using System;
using System.Collections.Generic;
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
            string url = $"https://bitskins.com/api/v1/get_inventory_on_sale/" +
                $"?api_key={Account.AccountData.GetApiKey()}" +
                $"&page={page}" +
                $"&app_id={(int)app}" +
                $"&per_page={(int)resultsPerPage}" +
                $"&code={Account.Secret.GetTwoFactorCode()}";

            if (!string.IsNullOrEmpty(marketHashName))
            {
                url += $"&market_hash_name={marketHashName}";
            }

            if (minPrice > 0)
            {
                url += $"&min_price={minPrice}";
            }

            if (maxPrice > 0)
            {
                url += $"&max_price={maxPrice}";
            }

            if (sortBy != SortBy.Not)
            {
                url += $"&sort_by={SortByToString(sortBy)}";
            }
            
            if (sortOrder != SortOrder.Not)
            {
                url += $"&order={SortOrderToString(sortOrder)}";
            }
            
            if (app == AppId.AppName.CounterStrikGlobalOffensive)
            {
                url += $"&has_stickers={(int)hasStickers}";
                url += $"&is_stattrak={(int)isStattrak}";
                url += $"&is_souvenir={(int)isSouvenir}";
                url += $"&show_trade_delayed_items={(int)tradeDelayedItems}";
            }
            
            string result = Server.ServerRequest.RequestServer(url);
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
                itemsOnSale.Add(itemOnSale);
            }

            return itemsOnSale;
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
                    return "asc";
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
                    return "price";
            }
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
}
