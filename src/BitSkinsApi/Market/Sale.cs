using System.Collections.Generic;
using Newtonsoft.Json;
using BitSkinsApi.Extensions;

namespace BitSkinsApi.Market
{
    /// <summary>
    /// Work with sale.
    /// </summary>
    public static class Sale
    {
        /// <summary>
        /// Allows you to list an item for sale. This item comes from your Steam inventory. 
        /// If successful, bots will ask you to trade in the item you want listed for sale. 
        /// </summary>
        /// <param name="app">Inventory's game id.</param>
        /// <param name="itemIds">List of item IDs from your Steam inventory.</param>
        /// <param name="itemPrices">List of prices for each item ID you want to list for sale.</param>
        /// <returns>Info about sale.</returns>
        public static InformationAboutSale SellItem(AppId.AppName app, List<string> itemIds, List<double> itemPrices)
        {
            string urlRequest = GetUrlRequest(app, itemIds, itemPrices);
            string result = Server.ServerRequest.RequestServer(urlRequest);
            InformationAboutSale informationAboutSale = ReadInformationAboutSale(result);
            return informationAboutSale;
        }

        private static string GetUrlRequest(AppId.AppName app, List<string> itemIds, List<double> itemPrices)
        {
            const string delimiter = ",";

            Server.UrlCreator urlCreator = new Server.UrlCreator($"https://bitskins.com/api/v1/list_item_for_sale/");
            urlCreator.AppendUrl($"&app_id={(int)app}");
            urlCreator.AppendUrl($"&item_ids={itemIds.ToStringWithDelimiter(delimiter)}");
            urlCreator.AppendUrl($"&prices={itemPrices.ToStringWithDelimiter(delimiter)}");

            return urlCreator.ReadUrl();
        }

        private static InformationAboutSale ReadInformationAboutSale(string result)
        {
            dynamic responseServerD = JsonConvert.DeserializeObject(result);
            dynamic soldItemsD = responseServerD.data.items;
            dynamic tradeTokensD = responseServerD.data.trade_tokens;
            dynamic botInfoD = responseServerD.data.bot_info;

            List<SoldItem> soldItems = ReadSoldItems(soldItemsD);
            List<string> tradeTokens = ReadTradeTokens(tradeTokensD);
            InformationAboutSellerBot soldBotInformation = ReadSoldBotInformation(botInfoD);

            InformationAboutSale soldInformation = new InformationAboutSale(soldItems, tradeTokens, soldBotInformation);
            return soldInformation;
        }

        private static List<SoldItem> ReadSoldItems(dynamic soldItemsD)
        {
            List<SoldItem> soldItems = new List<SoldItem>();
            if (soldItemsD != null)
            {
                foreach (dynamic item in soldItemsD)
                {
                    SoldItem soldItem = ReadSoldItem(item);
                    soldItems.Add(soldItem);
                }
            }

            return soldItems;
        }

        private static SoldItem ReadSoldItem(dynamic item)
        {
            string itemId = item.item_id;

            SoldItem soldItem = new SoldItem(itemId);
            return soldItem;
        }

        private static List<string> ReadTradeTokens(dynamic tradeTokensD)
        {
            List<string> tradeTokens = new List<string>();
            if (tradeTokensD != null)
            {
                foreach (dynamic token in tradeTokensD)
                {
                    tradeTokens.Add((string)token);
                }
            }

            return tradeTokens;
        }

        private static InformationAboutSellerBot ReadSoldBotInformation(dynamic botInfoD)
        {
            InformationAboutSellerBot soldBotInformation = null;
            if (botInfoD != null)
            {
                string botUid = botInfoD.uid;
                string namePrefix = botInfoD.name_prefix;

                soldBotInformation = new InformationAboutSellerBot(botUid, namePrefix);
            }

            return soldBotInformation;
        }
    }

    /// <summary>
    /// Information about sale.
    /// </summary>
    public class InformationAboutSale
    {
        public List<SoldItem> SoldItems { get; private set; }
        public List<string> TradeTokens { get; private set; }
        public InformationAboutSellerBot InformationAboutSellerBot { get; private set; }

        internal InformationAboutSale(List<SoldItem> soldItems, List<string> tradeTokens, InformationAboutSellerBot informationAboutSellerBot)
        {
            SoldItems = soldItems;
            TradeTokens = tradeTokens;
            InformationAboutSellerBot = informationAboutSellerBot;
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
    public class InformationAboutSellerBot
    {
        public string Uid { get; private set; }
        public string NamePrefix { get; private set; }

        internal InformationAboutSellerBot(string uid, string namePrefix)
        {
            Uid = uid;
            NamePrefix = namePrefix;
        }
    }
}
