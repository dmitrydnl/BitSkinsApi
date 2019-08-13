/*
 * BitSkinsApi
 * Copyright (C) 2019 dmitrydnl
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using BitSkinsApi.Extensions;
using BitSkinsApi.CheckParameters;

namespace BitSkinsApi.Market
{
    /// <summary>
    /// Work with BitSkins inventory on sale.
    /// </summary>
    public static class InventoryOnSale
    {
        /// <summary>
        /// Type of sorting criterion.
        /// </summary>
        public enum SortBy { Price, CreatedAt, BumpedAt, Not };
        /// <summary>
        /// Type of sort order.
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
        /// Allows you to retrieve the BitSkins inventory currently on sale. 
        /// This includes items you cannot buy (i.e., items listed for sale by you).
        /// </summary>
        /// <param name="app">Inventory's game id.</param>
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
            CheckParameters(page);
            string urlRequest = GetUrlRequest(app, page, marketHashName, minPrice, maxPrice, sortBy, sortOrder, hasStickers, 
                isStattrak, isSouvenir, resultsPerPage, tradeDelayedItems);
            string result = Server.ServerRequest.RequestServer(urlRequest);
            List<ItemOnSale> itemsOnSale = ReadItemsOnSale(result);
            return itemsOnSale;
        }

        private static void CheckParameters(int page)
        {
            Checking.PositiveInt(page, "page");
        }

        private static string GetUrlRequest(AppId.AppName app, int page, string marketHashName, double minPrice, double maxPrice,
            SortBy sortBy, SortOrder sortOrder, ThreeChoices hasStickers, ThreeChoices isStattrak, ThreeChoices isSouvenir,
            ResultsPerPage resultsPerPage, ThreeChoices tradeDelayedItems)
        {
            Server.UrlCreator urlCreator = new Server.UrlCreator($"https://bitskins.com/api/v1/get_inventory_on_sale/");
            urlCreator.AppendUrl($"&page={page}");
            urlCreator.AppendUrl($"&app_id={(int)app}");
            urlCreator.AppendUrl($"&per_page={(int)resultsPerPage}");

            if (!string.IsNullOrEmpty(marketHashName))
            {
                urlCreator.AppendUrl($"&market_hash_name={marketHashName}");
            }

            if (minPrice > 0)
            {
                urlCreator.AppendUrl($"&min_price={minPrice}");
            }

            if (maxPrice > 0)
            {
                urlCreator.AppendUrl($"&max_price={maxPrice}");
            }

            if (sortBy != SortBy.Not)
            {
                urlCreator.AppendUrl($"&sort_by={SortByToString(sortBy)}");
            }

            if (sortOrder != SortOrder.Not)
            {
                urlCreator.AppendUrl($"&order={SortOrderToString(sortOrder)}");
            }

            if (app == AppId.AppName.CounterStrikGlobalOffensive)
            {
                urlCreator.AppendUrl($"&has_stickers={(int)hasStickers}");
                urlCreator.AppendUrl($"&is_stattrak={(int)isStattrak}");
                urlCreator.AppendUrl($"&is_souvenir={(int)isSouvenir}");
                urlCreator.AppendUrl($"&show_trade_delayed_items={(int)tradeDelayedItems}");
            }

            return urlCreator.ReadUrl();
        }

        private static List<ItemOnSale> ReadItemsOnSale(string result)
        {
            dynamic responseServerD = JsonConvert.DeserializeObject(result);
            dynamic itemsD = responseServerD.data.items;

            List<ItemOnSale> itemsOnSale = new List<ItemOnSale>();
            if (itemsD != null)
            {
                foreach (dynamic item in itemsD)
                {
                    ItemOnSale itemOnSale = ReadItemOnSale(item);
                    itemsOnSale.Add(itemOnSale);
                }
            }

            return itemsOnSale;
        }

        internal static ItemOnSale ReadItemOnSale(dynamic item)
        {
            string itemId = item.item_id ?? null;
            string marketHashName = item.market_hash_name ?? null;
            string itemType = item.item_type ?? null;
            string image = item.image ?? null;
            double? price = item.price ?? null;
            double? suggestedPrice = item.suggested_price ?? null;
            double? floatValue = item.float_value ?? null;
            bool? isMine = item.is_mine ?? null;
            DateTime? updatedAt = null;
            if (item.updated_at != null)
            {
                updatedAt = DateTimeExtension.FromUnixTime((long)item.updated_at);
            }
            DateTime? withdrawableAt = null;
            if (item.withdrawable_at != null)
            {
                withdrawableAt = DateTimeExtension.FromUnixTime((long)item.withdrawable_at);
            }

            ItemOnSale itemOnSale = new ItemOnSale(itemId, marketHashName, itemType, image, price,
                suggestedPrice, floatValue, isMine, updatedAt, withdrawableAt);
            return itemOnSale;
        }

        private static string SortOrderToString(SortOrder sortOrder)
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

        private static string SortByToString(SortBy sortBy)
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
        /// Allows you to retrieve data for specific Item IDs that are currently on sale. 
        /// </summary>
        /// <param name="app">Inventory's game id.</param>
        /// <param name="itemIds">List of item ids. Upto 250 item IDs.</param>
        /// <returns>Specific items on sale on BitSkins.</returns>
        public static SpecificItems GetSpecificItemsOnSale(AppId.AppName app, List<string> itemIds)
        {
            CheckParameters(itemIds);
            string urlRequest = GetUrlRequest(app, itemIds);
            string result = Server.ServerRequest.RequestServer(urlRequest);
            SpecificItems specificItems = ReadSpecificItems(result);
            return specificItems;
        }

        private static void CheckParameters(List<string> itemIds)
        {
            Checking.NotEmptyList(itemIds, "itemIds");
            Checking.CountUptoList(itemIds, "itemIds", 250);
        }

        private static string GetUrlRequest(AppId.AppName app, List<string> itemIds)
        {
            const string delimiter = ",";

            Server.UrlCreator urlCreator = new Server.UrlCreator($"https://bitskins.com/api/v1/get_specific_items_on_sale/");
            urlCreator.AppendUrl($"&item_ids={itemIds.ToStringWithDelimiter(delimiter)}");
            urlCreator.AppendUrl($"&app_id={(int)app}");

            return urlCreator.ReadUrl();
        }

        private static SpecificItems ReadSpecificItems(string result)
        {
            dynamic responseServerD = JsonConvert.DeserializeObject(result);
            dynamic itemsOnSaleD = responseServerD.data.items_on_sale;
            dynamic itemsNotOnSaleD = responseServerD.data.items_not_on_sale;

            List<ItemOnSale> itemsOnSale = ReadItemsOnSale(itemsOnSaleD);
            List<string> itemsNotOnSale = ReadItemsNotOnSale(itemsNotOnSaleD);

            SpecificItems specificItems = new SpecificItems(itemsOnSale, itemsNotOnSale);
            return specificItems;
        }

        private static List<ItemOnSale> ReadItemsOnSale(dynamic itemsOnSaleD)
        {
            List<ItemOnSale> itemsOnSale = new List<ItemOnSale>();
            if (itemsOnSaleD != null)
            {
                foreach (dynamic item in itemsOnSaleD)
                {
                    ItemOnSale itemOnSale = InventoryOnSale.ReadItemOnSale(item);
                    itemsOnSale.Add(itemOnSale);
                }
            }

            return itemsOnSale;
        }

        private static List<string> ReadItemsNotOnSale(dynamic itemsNotOnSaleD)
        {
            List<string> itemsNotOnSale = new List<string>();
            if (itemsNotOnSaleD != null)
            {
                foreach (dynamic item in itemsNotOnSaleD)
                {
                    string itemNotOnSale = (string)item;
                    itemsNotOnSale.Add(itemNotOnSale);
                }
            }

            return itemsNotOnSale;
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
        public double? Price { get; private set; }
        public double? SuggestedPrice { get; private set; }
        public double? FloatValue { get; private set; }
        public bool? IsMine { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public DateTime? WithdrawableAt { get; private set; }

        internal ItemOnSale(string itemId, string marketHashName, string itemType, string image, double? price, 
            double? suggestedPrice, double? floatValue, bool? isMine, DateTime? updatedAt, DateTime? withdrawableAt)
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
