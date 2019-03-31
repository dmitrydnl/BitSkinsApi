using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace BitSkinsApi.Market
{
    /// <summary>
    /// Work with sale.
    /// </summary>
    public static class Sale
    {
        /// <summary>
        /// Allows you to list an item for sale. This item comes from your Steam inventory. If successful, bots will ask you to trade in the item you want listed for sale. 
        /// </summary>
        /// <param name="app">For the inventory's game.</param>
        /// <param name="itemIds">List of item IDs from your Steam inventory.</param>
        /// <param name="itemPrices">List of prices for each item ID you want to list for sale.</param>
        /// <returns>Info about sale.</returns>
        public static SaleInformation SellItem(AppId.AppName app, List<string> itemIds, List<double> itemPrices)
        {
            string delimiter = ",";

            StringBuilder itemIdsStr = new StringBuilder();
            for (int i = 0; i < itemIds.Count; i++)
            {
                itemIdsStr.Append(itemIds[i]);
                itemIdsStr.Append((i < itemIds.Count - 1) ? delimiter : "");
            }

            StringBuilder itemPricesStr = new StringBuilder();
            for (int i = 0; i < itemPrices.Count; i++)
            {
                itemPricesStr.Append(itemPrices[i].ToString().Replace(',', '.'));
                itemPricesStr.Append((i < itemPrices.Count - 1) ? delimiter : "");
            }

            StringBuilder url = new StringBuilder($"https://bitskins.com/api/v1/list_item_for_sale/");
            url.Append($"?api_key={Account.AccountData.GetApiKey()}");
            url.Append($"&app_id={(int)app}");
            url.Append($"&item_ids={itemIdsStr.ToString()}");
            url.Append($"&prices={itemPricesStr.ToString()}");
            url.Append($"&code={Account.Secret.GetTwoFactorCode()}");
            
            string result = Server.ServerRequest.RequestServer(url.ToString());
            SaleInformation saleInformation = ReadSalesInformation(result);
            return saleInformation;
        }

        static SaleInformation ReadSalesInformation(string result)
        {
            dynamic responseServer = JsonConvert.DeserializeObject(result);
            dynamic items = responseServer.data.items;
            dynamic tradeTokensD = responseServer.data.trade_tokens;
            dynamic botInfo = responseServer.data.bot_info;

            List<SoldItem> soldItems = new List<SoldItem>();
            if (items != null)
            {
                foreach (dynamic item in items)
                {
                    string itemId = item.item_id;

                    SoldItem soldItem = new SoldItem(itemId);
                    soldItems.Add(soldItem);
                }
            }

            List<string> tradeTokens = new List<string>();
            if (tradeTokensD != null)
            {
                foreach (dynamic token in tradeTokensD)
                {
                    tradeTokens.Add((string)token);
                }
            }

            SaleBotInfo saleBotInfo = null;
            if (botInfo != null)
            {
                string botUid = botInfo.uid;
                string namePrefix = botInfo.name_prefix;

                saleBotInfo = new SaleBotInfo(botUid, namePrefix);
            }

            SaleInformation saleInformation = new SaleInformation(soldItems, tradeTokens, saleBotInfo);
            return saleInformation;
        }
    }

    /// <summary>
    /// Info about sale.
    /// </summary>
    public class SaleInformation
    {
        public List<SoldItem> SoldItems { get; private set; }
        public List<string> TradeTokens { get; private set; }
        public SaleBotInfo SaleBotInfo { get; private set; }

        internal SaleInformation(List<SoldItem> soldItems, List<string> tradeTokens, SaleBotInfo saleBotInfo)
        {
            SoldItems = soldItems;
            TradeTokens = tradeTokens;
            SaleBotInfo = saleBotInfo;
        }
    }

    /// <summary>
    /// Info about sold item.
    /// </summary>
    public class SoldItem
    {
        public string ItemId { get; private set; }

        internal SoldItem(string itemId)
        {
            ItemId = itemId;
        }
    }

    /// <summary>
    /// Information about the bot that offers the trade.
    /// </summary>
    public class SaleBotInfo
    {
        public string Uid { get; private set; }
        public string NamePrefix { get; private set; }

        internal SaleBotInfo(string uid, string namePrefix)
        {
            Uid = uid;
            NamePrefix = namePrefix;
        }
    }
}
