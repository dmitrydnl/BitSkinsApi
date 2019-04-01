using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using BitSkinsApi.Extensions;

namespace BitSkinsApi.Inventory
{
    /// <summary>
    /// Work with withdraw.
    /// </summary>
    public static class WithdrawToSteam
    {
        /// <summary>
        /// Allows you to delist an active sale item and/or re-attempt an item pending withdrawal.
        /// </summary>
        /// <param name="app">For the inventory's game.</param>
        /// <param name="itemIds">List of item IDs.</param>
        /// <returns>Withdrawn information.</returns>
        public static WithdrawnInformation WithdrawItem(Market.AppId.AppName app, List<string> itemIds)
        {
            string delimiter = ",";

            string itemIdsStr = String.Join(delimiter, itemIds);

            StringBuilder url = new StringBuilder($"https://bitskins.com/api/v1/withdraw_item/");
            url.Append($"?api_key={Account.AccountData.GetApiKey()}");
            url.Append($"&app_id={(int)app}");
            url.Append($"&item_ids={itemIdsStr}");
            url.Append($"&code={Account.Secret.GetTwoFactorCode()}");

            string result = Server.ServerRequest.RequestServer(url.ToString());
            WithdrawnInformation withdrawnInformation = ReadWithdrawnInformation(result);
            return withdrawnInformation;
        }

        static WithdrawnInformation ReadWithdrawnInformation(string result)
        {
            dynamic responseServer = JsonConvert.DeserializeObject(result);
            dynamic items = responseServer.data.items;
            dynamic tradeTokensD = responseServer.data.trade_tokens;

            List<WithdrawnItem> withdrawnItems = new List<WithdrawnItem>(); 
            if (items != null)
            {
                foreach (dynamic item in items)
                {
                    Market.AppId.AppName appId = (Market.AppId.AppName)(int)item.app_id;
                    string itemId = item.item_id;
                    DateTime withdrawableAt = DateTimeExtension.FromUnixTime((long)item.withdrawable_at);

                    WithdrawnItem withdrawnItem = new WithdrawnItem(appId, itemId, withdrawableAt);
                    withdrawnItems.Add(withdrawnItem);
                }
            }

            List<string> tradeTokens = new List<string>();
            if (tradeTokensD != null)
            {
                foreach (dynamic token in tradeTokensD)
                {
                    string tradeToken = (string)token;
                    tradeTokens.Add(tradeToken);
                }
            }

            WithdrawnInformation withdrawnInformation = new WithdrawnInformation(withdrawnItems, tradeTokens);
            return withdrawnInformation;
        }
    }

    /// <summary>
    /// Information about withdrawn.
    /// </summary>
    public class WithdrawnInformation
    {
        public List<WithdrawnItem> WithdrawnItems { get; private set; }
        public List<string> TradeTokens { get; private set; }

        internal WithdrawnInformation(List<WithdrawnItem> withdrawnItems, List<string> tradeTokens)
        {
            WithdrawnItems = withdrawnItems;
            TradeTokens = tradeTokens;
        }
    }

    /// <summary>
    /// Information about withdrawn item.
    /// </summary>
    public class WithdrawnItem
    {
        public Market.AppId.AppName AppId { get; private set; }
        public string ItemId { get; private set; }
        public DateTime WithdrawableAt { get; private set; }

        internal WithdrawnItem(Market.AppId.AppName appId, string itemId, DateTime withdrawableAt)
        {
            AppId = appId;
            ItemId = itemId;
            WithdrawableAt = withdrawableAt;
        }
    }
}
