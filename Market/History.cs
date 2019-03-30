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
        /// Allows you to retrieve your history of bought items on BitSkins. Defaults to 30 items per page, with most recent appearing first.
        /// </summary>
        /// <param name="app">For the inventory's game.</param>
        /// <param name="page">Page number.</param>
        /// <returns>List of buy history records.</returns>
        public static List<BuyHistoryRecord> GetBuyHistory(AppId.AppName app, int page)
        {
            string url = $"https://bitskins.com/api/v1/get_buy_history/" +
                $"?api_key={Account.AccountData.GetApiKey()}" +
                $"&page={page}" +
                $"&app_id={(int)app}" +
                $"&code={Account.Secret.GetTwoFactorCode()}";

            string result = Server.ServerRequest.RequestServer(url);
            List<BuyHistoryRecord> historyBuyRecords = ReadBuyHistoryRecors(result);
            return historyBuyRecords;
        }

        static List<BuyHistoryRecord> ReadBuyHistoryRecors(string result)
        {
            dynamic responseServer = JsonConvert.DeserializeObject(result);
            dynamic items = responseServer.data.items;

            if (items == null)
            {
                return new List<BuyHistoryRecord>();
            }

            List<BuyHistoryRecord> historyBuyRecords = new List<BuyHistoryRecord>();
            foreach (dynamic item in items)
            {
                AppId.AppName appId = (AppId.AppName)(int)item.app_id;
                string itemId = item.item_id;
                string marketHashName = item.market_hash_name;
                double buyPrice = item.buy_price;
                bool withdrawn = item.withdrawn;
                DateTime time = DateTimeExtension.FromUnixTime((long)item.time);

                BuyHistoryRecord historyBuyRecord = new BuyHistoryRecord(appId, itemId, marketHashName, buyPrice, withdrawn, time);
                historyBuyRecords.Add(historyBuyRecord);
            }

            return historyBuyRecords;
        }
    }

    /// <summary>
    /// Work with your buy history on BitSkins.
    /// </summary>
    public static class SellHistory
    {
        /// <summary>
        /// Allows you to retrieve your history of sold items on BitSkins. Defaults to 30 items per page, with most recent appearing first.
        /// </summary>
        /// <param name="app">For the inventory's game.</param>
        /// <param name="page">Page number.</param>
        /// <returns>List of sell history records.</returns>
        public static List<SellHistoryRecord> GetSellHistory(AppId.AppName app, int page)
        {
            string url = $"https://bitskins.com/api/v1/get_sell_history/" +
                $"?api_key={Account.AccountData.GetApiKey()}" +
                $"&page={page}" +
                $"&app_id={(int)app}" +
                $"&code={Account.Secret.GetTwoFactorCode()}";

            string result = Server.ServerRequest.RequestServer(url);
            List<SellHistoryRecord> historySellRecords = ReadSellHistoryRecors(result);
            return historySellRecords;
        }

        static List<SellHistoryRecord> ReadSellHistoryRecors(string result)
        {
            dynamic responseServer = JsonConvert.DeserializeObject(result);
            dynamic items = responseServer.data.items;

            if (items == null)
            {
                return new List<SellHistoryRecord>();
            }

            List<SellHistoryRecord> historySellRecords = new List<SellHistoryRecord>();
            foreach (dynamic item in items)
            {
                AppId.AppName appId = (AppId.AppName)(int)item.app_id;
                string itemId = item.item_id;
                string marketHashName = item.market_hash_name;
                double salePrice = item.sale_price;
                DateTime time = DateTimeExtension.FromUnixTime((long)item.time);

                SellHistoryRecord historySellRecord = new SellHistoryRecord(appId, itemId, marketHashName, salePrice, time);
                historySellRecords.Add(historySellRecord);
            }

            return historySellRecords;
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
        /// Allows you to retrieve bought/sold/listed item history. By default, upto 30 items per page, and optionally up to 480 items per page.
        /// </summary>
        /// <param name="app">For the inventory's game.</param>
        /// <param name="page">Page number.</param>
        /// <param name="names">Item names. (optional)</param>
        /// <param name="resultsPerPage">Results per page.</param>
        /// <returns>List of item's history records.</returns>
        public static List<ItemHistoryRecord> GetItemHistory(AppId.AppName app, int page, List<string> names, ResultsPerPage resultsPerPage)
        {
            string delimiter = ",";

            string namesStr = "";
            for (int i = 0; i < names.Count; i++)
            {
                namesStr += names[i];
                namesStr += (i < names.Count - 1) ? delimiter : "";
            }

            string url = $"https://bitskins.com/api/v1/get_item_history/" +
                $"?api_key={Account.AccountData.GetApiKey()}" +
                $"&page={page}" +
                $"&app_id={(int)app}" +
                $"&per_page={(int)resultsPerPage}" +
                $"&code={Account.Secret.GetTwoFactorCode()}";

            if (names.Count > 0)
            {
                url += $"&names={namesStr}";
                url += $"&delimiter={delimiter}";
            }

            string result = Server.ServerRequest.RequestServer(url);
            List<ItemHistoryRecord> itemHistoryRecords = ReadItemHistoryRecords(result);
            return itemHistoryRecords;
        }

        static List<ItemHistoryRecord> ReadItemHistoryRecords(string result)
        {
            dynamic responseServer = JsonConvert.DeserializeObject(result);
            dynamic items = responseServer.data.items;

            if (items == null)
            {
                return new List<ItemHistoryRecord>();
            }

            List<ItemHistoryRecord> itemHistoryRecords = new List<ItemHistoryRecord>();
            foreach (dynamic item in items)
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
                DateTime? recordTime = null;
                if (recordType == ItemHistoryRecordType.BoughtAt)
                {
                    recordTime = DateTimeExtension.FromUnixTime((long)item.bought_at);
                }
                else if (recordType == ItemHistoryRecordType.SoldAt)
                {
                    if (item.sold_at != null)
                    {
                        recordTime = DateTimeExtension.FromUnixTime((long)item.sold_at);
                    }
                }

                ItemHistoryRecord itemHistoryRecord = new ItemHistoryRecord(appId, itemId, marketHashName, price, 
                    recordType, lastUpdateAt, listedAt, withdrawnAt, listedByMe, onHold, onSale, recordTime);
                itemHistoryRecords.Add(itemHistoryRecord);
            }

            return itemHistoryRecords;
        }
    }

    /// <summary>
    /// BitSkins record about item buy/sell.
    /// </summary>
    public abstract class HistoryRecord
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
