using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using BitSkinsApi.Extensions;

namespace BitSkinsApi.Market
{
    /// <summary>
    /// Work with your history on BitSkins.
    /// </summary>
    public static class History
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
            if (!Server.ServerRequest.RequestServer(url, out string result))
            {
                throw new Server.RequestServerException(result);
            }

            dynamic responseServer = JsonConvert.DeserializeObject(result);

            if (responseServer.data.items == null)
            {
                return new List<BuyHistoryRecord>();
            }

            List<BuyHistoryRecord> historyBuyRecords = new List<BuyHistoryRecord>();
            foreach (dynamic item in responseServer.data.items)
            {
                AppId.AppName appId = (AppId.AppName)(int)item.app_id;
                string itemId = item.item_id;
                string assetId = item.asset_id;
                string classId = item.class_id;
                string instanceId = item.instance_id;
                string marketHashName = item.market_hash_name;
                double buyPrice = item.buy_price;
                bool withdrawn = item.withdrawn;
                DateTime time = DateTimeExtension.FromUnixTime((long)item.time);
                BuyHistoryRecord historyBuyRecord = new BuyHistoryRecord(appId, itemId, assetId, classId, instanceId, marketHashName, buyPrice, withdrawn, time);
                historyBuyRecords.Add(historyBuyRecord);
            }

            return historyBuyRecords;
        }

        /// <summary>
        /// Allows you to retrieve your history of sold items on BitSkins. Defaults to 30 items per page, with most recent appearing first.
        /// </summary>
        /// <param name="app">For the inventory's game.</param>
        /// <param name="page">Page number.</param>
        /// <returns>List of sell history records.</returns>
        public static List<SellHistoryRecord> GetSellHistory(AppId.AppName app, int page)
        {
            string url = $"https://bitskins.com/api/v1/get_sell_history/?api_key={Account.AccountData.GetApiKey()}&page={page}&app_id={(int)app}&code={Account.Secret.GetTwoFactorCode()}";
            if (!Server.ServerRequest.RequestServer(url, out string result))
            {
                throw new Server.RequestServerException(result);
            }

            dynamic responseServer = JsonConvert.DeserializeObject(result);

            if (responseServer.data.items == null)
            {
                return new List<SellHistoryRecord>();
            }

            List<SellHistoryRecord> historySellRecords = new List<SellHistoryRecord>();
            foreach (dynamic item in responseServer.data.items)
            {
                AppId.AppName appId = (AppId.AppName)(int)item.app_id;
                string itemId = item.item_id;
                string assetId = item.asset_id;
                string classId = item.class_id;
                string instanceId = item.instance_id;
                string marketHashName = item.market_hash_name;
                double salePrice = item.sale_price;
                DateTime time = DateTimeExtension.FromUnixTime((long)item.time);
                SellHistoryRecord historySellRecord = new SellHistoryRecord(appId, itemId, assetId, classId, instanceId, marketHashName, salePrice, time);
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
        public string AssetId { get; private set; }
        public string ClassId { get; private set; }
        public string InstanceId { get; private set; }
        public string MarketHashName { get; private set; }
        public DateTime Time { get; private set; }

        protected HistoryRecord(AppId.AppName appId, string itemId, string assetId, 
            string classId, string instanceId, string marketHashName, DateTime time)
        {
            AppId = appId;
            ItemId = itemId;
            AssetId = assetId;
            ClassId = classId;
            InstanceId = instanceId;
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

        internal BuyHistoryRecord(AppId.AppName appId, string itemId, string assetId, string classId, 
            string instanceId, string marketHashName, double buyPrice, bool withdrawn, DateTime time)
            : base (appId, itemId, assetId, classId, instanceId, marketHashName, time)
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

        internal SellHistoryRecord(AppId.AppName appId, string itemId, string assetId, string classId, 
            string instanceId, string marketHashName, double salePrice, DateTime time)
            : base(appId, itemId, assetId, classId, instanceId, marketHashName, time)
        {
            SalePrice = salePrice;
        }
    }
}
