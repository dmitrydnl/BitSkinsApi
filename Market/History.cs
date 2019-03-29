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
            string url = $"https://bitskins.com/api/v1/get_buy_history/?api_key={Account.AccountData.GetApiKey()}&page={page}&app_id={(int)app}&code={Account.Secret.GetTwoFactorCode()}";
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
            string url = $"https://bitskins.com/api/v1/get_sell_history/?api_key={Account.AccountData.GetApiKey()}&page={page}&app_id={(int)app}&code={Account.Secret.GetTwoFactorCode()}";
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
    /// BitSkins record about item buy/sell.
    /// </summary>
    public abstract class HistoryRecord
    {
        public AppId.AppName AppId { get; private set; }
        public string ItemId { get; private set; }
        public string MarketHashName { get; private set; }
        public DateTime Time { get; private set; }

        protected HistoryRecord(AppId.AppName appId, string itemId, string marketHashName, DateTime time)
        {
            AppId = appId;
            ItemId = itemId;
            MarketHashName = marketHashName;
            Time = time;
        }
    }

    /// <summary>
    /// BitSkins record about item buy.
    /// </summary>
    public class BuyHistoryRecord : HistoryRecord
    {
        public double BuyPrice { get; private set; }
        public bool Withdrawn { get; private set; }

        internal BuyHistoryRecord(AppId.AppName appId, string itemId, string marketHashName, double buyPrice, bool withdrawn, DateTime time)
            : base (appId, itemId, marketHashName, time)
        {
            BuyPrice = buyPrice;
            Withdrawn = withdrawn;
        }
    }

    /// <summary>
    /// BitSkins record about item sell.
    /// </summary>
    public class SellHistoryRecord : HistoryRecord
    {
        public double SalePrice { get; private set; }

        internal SellHistoryRecord(AppId.AppName appId, string itemId, string marketHashName, double salePrice, DateTime time)
            : base(appId, itemId, marketHashName, time)
        {
            SalePrice = salePrice;
        }
    }
}
