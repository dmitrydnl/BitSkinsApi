using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace BitSkinsApi.Market
{
    /// <summary>
    /// Work with reset price items.
    /// </summary>
    public static class ResetPriceItems
    {
        /// <summary>
        /// Returns a paginated list of items that need their prices reset. Items need prices reset when Steam changes tracker so we are unable to match specified prices to the received items when you list them for sale. Upto 30 items per page. Items that need price resets always have the reserved price of 4985.11.
        /// </summary>
        /// <param name="app">For the inventory's game.</param>
        /// <param name="page">Page number.</param>
        /// <returns>List of reset price items.</returns>
        public static List<ResetPriceItem> GetResetPriceItems(AppId.AppName app, int page)
        {
            StringBuilder url = new StringBuilder($"https://bitskins.com/api/v1/get_reset_price_items/");
            url.Append($"?api_key={Account.AccountData.GetApiKey()}");
            url.Append($"&app_id={(int)app}");
            url.Append($"&page={page}");
            url.Append($"&code={Account.Secret.GetTwoFactorCode()}");

            string result = Server.ServerRequest.RequestServer(url.ToString());
            List<ResetPriceItem> resetPriceItems = ReadResetPriceItems(result);
            return resetPriceItems;
        }

        static List<ResetPriceItem> ReadResetPriceItems(string result)
        {
            dynamic responseServer = JsonConvert.DeserializeObject(result);
            dynamic items = responseServer.data.items;

            if (items == null)
            {
                return new List<ResetPriceItem>();
            }

            List<ResetPriceItem> resetPriceItems = new List<ResetPriceItem>();
            foreach (dynamic item in items)
            {
                string marketHashName = item.market_hash_name;
                double? price = null;
                if (item.price != null)
                {
                    price = (double)item.price;
                }

                ResetPriceItem resetPriceItem = new ResetPriceItem(marketHashName, price);
                resetPriceItems.Add(resetPriceItem);
            }

            return resetPriceItems;
        }
    }

    /// <summary>
    /// BitSkins reset price item.
    /// </summary>
    public class ResetPriceItem
    {
        public string MarketHashName { get; private set; }
        public double? Price { get; private set; }

        internal ResetPriceItem(string marketHashName, double? price)
        {
            MarketHashName = MarketHashName;
            Price = price;
        }
    }
}
