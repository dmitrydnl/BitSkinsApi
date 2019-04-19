/*
 * BitSkinsApi
 * Copyright (C) 2019 Captious99
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

namespace BitSkinsApi.Market
{
    /// <summary>
    /// Work with your buy history on BitSkins.
    /// </summary>
    public static class BuyHistory
    {
        /// <summary>
        /// Allows you to retrieve your history of bought items on BitSkins. 
        /// Defaults to 30 items per page, with most recent appearing first.
        /// </summary>
        /// <param name="app">Iinventory's game id.</param>
        /// <param name="page">Page number.</param>
        /// <returns>List of buy history records.</returns>
        public static List<BuyHistoryRecord> GetBuyHistory(AppId.AppName app, int page)
        {
            string urlRequest = GetUrlRequest(app, page);
            string result = Server.ServerRequest.RequestServer(urlRequest);
            List<BuyHistoryRecord> buyHistoryRecords = ReadBuyHistoryRecors(result);
            return buyHistoryRecords;
        }

        private static string GetUrlRequest(AppId.AppName app, int page)
        {
            Server.UrlCreator urlCreator = new Server.UrlCreator($"https://bitskins.com/api/v1/get_buy_history/");
            urlCreator.AppendUrl($"&page={page}");
            urlCreator.AppendUrl($"&app_id={(int)app}");

            return urlCreator.ReadUrl();
        }

        private static List<BuyHistoryRecord> ReadBuyHistoryRecors(string result)
        {
            dynamic responseServerD = JsonConvert.DeserializeObject(result);
            dynamic itemsD = responseServerD.data.items;

            List<BuyHistoryRecord> historyBuyRecords = new List<BuyHistoryRecord>();
            if (itemsD != null)
            {
                foreach (dynamic item in itemsD)
                {
                    BuyHistoryRecord historyBuyRecord = ReadBuyHistoryRecord(item);
                    historyBuyRecords.Add(historyBuyRecord);
                }
            }

            return historyBuyRecords;
        }

        private static BuyHistoryRecord ReadBuyHistoryRecord(dynamic item)
        {
            AppId.AppName appId = (AppId.AppName)(int)item.app_id;
            string itemId = item.item_id;
            string marketHashName = item.market_hash_name;
            double buyPrice = item.buy_price;
            bool withdrawn = item.withdrawn;
            DateTime time = DateTimeExtension.FromUnixTime((long)item.time);

            BuyHistoryRecord historyBuyRecord = new BuyHistoryRecord(appId, itemId, marketHashName, buyPrice, withdrawn, time);
            return historyBuyRecord;
        }
    }

    /// <summary>
    /// Work with your sell history on BitSkins.
    /// </summary>
    public static class SellHistory
    {
        /// <summary>
        /// Allows you to retrieve your history of sold items on BitSkins.
        /// Defaults to 30 items per page, with most recent appearing first.
        /// </summary>
        /// <param name="app">Inventory's game id.</param>
        /// <param name="page">Page number.</param>
        /// <returns>List of sell history records.</returns>
        public static List<SellHistoryRecord> GetSellHistory(AppId.AppName app, int page)
        {
            string urlRequest = GetUrlRequest(app, page);
            string result = Server.ServerRequest.RequestServer(urlRequest);
            List<SellHistoryRecord> sellHistoryRecords = ReadSellHistoryRecors(result);
            return sellHistoryRecords;
        }

        private static string GetUrlRequest(AppId.AppName app, int page)
        {
            Server.UrlCreator urlCreator = new Server.UrlCreator($"https://bitskins.com/api/v1/get_sell_history/");
            urlCreator.AppendUrl($"&page={page}");
            urlCreator.AppendUrl($"&app_id={(int)app}");

            return urlCreator.ReadUrl();
        }

        private static List<SellHistoryRecord> ReadSellHistoryRecors(string result)
        {
            dynamic responseServerD = JsonConvert.DeserializeObject(result);
            dynamic itemsD = responseServerD.data.items;

            List<SellHistoryRecord> sellHistoryRecords = new List<SellHistoryRecord>();
            if (itemsD != null)
            {
                foreach (dynamic item in itemsD)
                {
                    SellHistoryRecord sellHistoryRecord = ReadSellHistoryRecord(item);
                    sellHistoryRecords.Add(sellHistoryRecord);
                }
            }

            return sellHistoryRecords;
        }

        private static SellHistoryRecord ReadSellHistoryRecord(dynamic item)
        {
            AppId.AppName appId = (AppId.AppName)(int)item.app_id;
            string itemId = item.item_id;
            string marketHashName = item.market_hash_name;
            double salePrice = item.sale_price;
            DateTime time = DateTimeExtension.FromUnixTime((long)item.time);

            SellHistoryRecord sellHistoryRecord = new SellHistoryRecord(appId, itemId, marketHashName, salePrice, time);
            return sellHistoryRecord;
        }
    }

    /// <summary>
    /// Work with your items history on BitSkins.
    /// </summary>
    public static class ItemHistory
    {
        /// <summary>
        /// Results per page.
        /// </summary>
        public enum ResultsPerPage { R30 = 30, R480 = 480 };
        /// <summary>
        /// Type of item's history record.
        /// </summary>
        public enum ItemHistoryRecordType { BoughtAt, SoldAt };

        /// <summary>
        /// Allows you to retrieve bought/sold/listed item history.
        /// </summary>
        /// <param name="app">Inventory's game id.</param>
        /// <param name="page">Page number.</param>
        /// <param name="names">Item names. (optional)</param>
        /// <param name="resultsPerPage">Results per page.</param>
        /// <returns>List of item's history records.</returns>
        public static List<ItemHistoryRecord> GetItemHistory(AppId.AppName app, int page, List<string> names, ResultsPerPage resultsPerPage)
        {
            string urlRequest = GetUrlRequest(app, page, names, resultsPerPage);
            string result = Server.ServerRequest.RequestServer(urlRequest);
            List<ItemHistoryRecord> itemHistoryRecords = ReadItemHistoryRecords(result);
            return itemHistoryRecords;
        }

        private static string GetUrlRequest(AppId.AppName app, int page, List<string> names, ResultsPerPage resultsPerPage)
        {
            const string delimiter = ",";

            Server.UrlCreator urlCreator = new Server.UrlCreator($"https://bitskins.com/api/v1/get_item_history/");
            urlCreator.AppendUrl($"&page={page}");
            urlCreator.AppendUrl($"&app_id={(int)app}");
            urlCreator.AppendUrl($"&per_page={(int)resultsPerPage}");

            if (names.Count > 0)
            {
                urlCreator.AppendUrl($"&names={names.ToStringWithDelimiter(delimiter)}");
                urlCreator.AppendUrl($"&delimiter={delimiter}");
            }

            return urlCreator.ReadUrl();
        }

        private static List<ItemHistoryRecord> ReadItemHistoryRecords(string result)
        {
            dynamic responseServerD = JsonConvert.DeserializeObject(result);
            dynamic itemsD = responseServerD.data.items;

            List<ItemHistoryRecord> itemHistoryRecords = new List<ItemHistoryRecord>();
            if (itemsD != null)
            {
                foreach (dynamic item in itemsD)
                {
                    ItemHistoryRecord itemHistoryRecord = ReadItemHistoryRecord(item);
                    itemHistoryRecords.Add(itemHistoryRecord);
                }
            }

            return itemHistoryRecords;
        }

        private static ItemHistoryRecord ReadItemHistoryRecord(dynamic item)
        {
            AppId.AppName appId = (AppId.AppName)(int)item.app_id;
            string itemId = item.item_id;
            string marketHashName = item.market_hash_name;
            double price = item.price;
            DateTime lastUpdateAt = DateTimeExtension.FromUnixTime((long)item.last_update_at);
            DateTime listedAt = DateTimeExtension.FromUnixTime((long)item.listed_at);
            DateTime? withdrawnAt = null;
            if (item.withdrawn_at != null)
            {
                withdrawnAt = DateTimeExtension.FromUnixTime((long)item.withdrawn_at);
            }
            bool listedByMe = item.listed_by_me;
            bool onHold = item.on_hold;
            bool onSale = item.on_sale;
            ItemHistoryRecordType recordType = (item.bought_at != null) ? ItemHistoryRecordType.BoughtAt : ItemHistoryRecordType.SoldAt;
            DateTime? recordTime;
            if (recordType == ItemHistoryRecordType.BoughtAt)
            {
                recordTime = DateTimeExtension.FromUnixTime((long)item.bought_at);
            }
            else
            {
                if (item.sold_at != null)
                {
                    recordTime = DateTimeExtension.FromUnixTime((long)item.sold_at);
                }
                else
                {
                    recordTime = null;
                }
            }

            ItemHistoryRecord itemHistoryRecord = new ItemHistoryRecord(appId, itemId, marketHashName, price,
                recordType, lastUpdateAt, listedAt, withdrawnAt, listedByMe, onHold, onSale, recordTime);
            return itemHistoryRecord;
        }
    }

    /// <summary>
    /// BitSkins record about item buy/sell.
    /// </summary>
    public class HistoryRecord
    {
        public AppId.AppName AppId { get; private set; }
        public string ItemId { get; private set; }
        public string MarketHashName { get; private set; }
        public double Price { get; private set; }

        protected HistoryRecord(AppId.AppName appId, string itemId, string marketHashName, double price)
        {
            AppId = appId;
            ItemId = itemId;
            MarketHashName = marketHashName;
            Price = price;
        }
    }

    /// <summary>
    /// BitSkins record about item buy.
    /// </summary>
    public class BuyHistoryRecord : HistoryRecord
    {
        public bool Withdrawn { get; private set; }
        public DateTime Time { get; private set; }

        internal BuyHistoryRecord(AppId.AppName appId, string itemId, string marketHashName, double buyPrice, bool withdrawn, DateTime time)
            : base(appId, itemId, marketHashName, buyPrice)
        {
            Withdrawn = withdrawn;
            Time = time;
        }
    }

    /// <summary>
    /// BitSkins record about item sell.
    /// </summary>
    public class SellHistoryRecord : HistoryRecord
    {
        public DateTime Time { get; private set; }

        internal SellHistoryRecord(AppId.AppName appId, string itemId, string marketHashName, double salePrice, DateTime time)
            : base(appId, itemId, marketHashName, salePrice)
        {
            Time = time;
        }
    }

    /// <summary>
    /// BitSkins record about item history.
    /// </summary>
    public class ItemHistoryRecord : HistoryRecord
    {
        public ItemHistory.ItemHistoryRecordType RecordType { get; private set; }
        public DateTime LastUpdateAt { get; private set; }
        public DateTime ListedAt { get; private set; }
        public DateTime? WithdrawnAt { get; private set; }
        public bool ListedByMe { get; private set; }
        public bool OnHold { get; private set; }
        public bool OnSale { get; private set; }
        public DateTime? RecordTime { get; private set; }

        internal ItemHistoryRecord(AppId.AppName appId, string itemId, string marketHashName, double price, ItemHistory.ItemHistoryRecordType recordType,
            DateTime lastUpdateAt, DateTime listedAt, DateTime? withdrawnAt, bool listedByMe, bool onHold, bool onSale, DateTime? recordTime)
            : base(appId, itemId, marketHashName, price)
        {
            RecordType = recordType;
            LastUpdateAt = lastUpdateAt;
            ListedAt = listedAt;
            WithdrawnAt = withdrawnAt;
            ListedByMe = listedByMe;
            OnHold = onHold;
            OnSale = onSale;
            RecordTime = recordTime;
        }
    }
}
