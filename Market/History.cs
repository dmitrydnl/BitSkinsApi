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
            string url = $"https://bitskins.com/api/v1/get_buy_history/?api_key={Account.AccountData.ApiKey}&page={page}&app_id={(int)app}&code={Account.Secret.Code}";
            if (!Server.ServerRequest.RequestServer(url, out string result))
                throw new Exception(result);

            dynamic responseServer = JsonConvert.DeserializeObject(result);

            List<BuyHistoryRecord> historyBuyRecords = new List<BuyHistoryRecord>();
            foreach (dynamic item in responseServer.data.items)
            {
                string itemId = item.item_id;
                string assetId = item.asset_id;
                string classId = item.class_id;
                string instanceId = item.instance_id;
                string name = item.market_hash_name;
                double buyPrice = item.buy_price;
                bool withdrawn = item.withdrawn;
                DateTime time = DateTimeExtension.FromUnixTime((long)item.time);

                BuyHistoryRecord historyBuyRecord = new BuyHistoryRecord(itemId, assetId, classId, instanceId, name, buyPrice, withdrawn, time);
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
            string url = $"https://bitskins.com/api/v1/get_sell_history/?api_key={Account.AccountData.ApiKey}&page={page}&app_id={(int)app}&code={Account.Secret.Code}";
            if (!Server.ServerRequest.RequestServer(url, out string result))
                throw new Exception(result);

            dynamic responseServer = JsonConvert.DeserializeObject(result);

            List<SellHistoryRecord> historySellRecords = new List<SellHistoryRecord>();
            foreach (dynamic item in responseServer.data.items)
            {
                string itemId = item.item_id;
                string assetId = item.asset_id;
                string classId = item.class_id;
                string instanceId = item.instance_id;
                string name = item.market_hash_name;
                double sellPrice = item.sale_price;
                DateTime time = DateTimeExtension.FromUnixTime((long)item.time);

                SellHistoryRecord historySellRecord = new SellHistoryRecord(itemId, assetId, classId, instanceId, name, sellPrice, time);
                historySellRecords.Add(historySellRecord);
            }

            return historySellRecords;
        }
    }

    /// <summary>
    /// BitSkins record about item buy.
    /// </summary>
    public class BuyHistoryRecord
    {
        public string ItemId { get; private set; }
        public string AssetId { get; private set; }
        public string ClassId { get; private set; }
        public string InstanceId { get; private set; }
        public string Name { get; private set; }
        public double BuyPrice { get; private set; }
        public bool Withdrawn { get; private set; }
        public DateTime Time { get; private set; }

        internal BuyHistoryRecord(string itemId, string assetId, string classId, string instanceId, 
            string name, double buyPrice, bool withdrawn, DateTime time)
        {
            ItemId = itemId;
            AssetId = assetId;
            ClassId = classId;
            InstanceId = instanceId;
            Name = name;
            BuyPrice = buyPrice;
            Withdrawn = withdrawn;
            Time = time;
        }
    }

    /// <summary>
    /// BitSkins record about item sell.
    /// </summary>
    public class SellHistoryRecord
    {
        public string ItemId { get; private set; }
        public string AssetId { get; private set; }
        public string ClassId { get; private set; }
        public string InstanceId { get; private set; }
        public string Name { get; private set; }
        public double SellPrice { get; private set; }
        public DateTime Time { get; private set; }

        internal SellHistoryRecord(string itemId, string assetId, string classId, 
            string instanceId, string name, double sellPrice, DateTime time)
        {
            ItemId = itemId;
            AssetId = assetId;
            ClassId = classId;
            InstanceId = instanceId;
            Name = name;
            SellPrice = sellPrice;
            Time = time;
        }
    }
}
