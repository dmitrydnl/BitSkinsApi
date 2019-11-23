using System.Collections.Generic;
using Newtonsoft.Json;
using BitSkinsApi.Market;
using BitSkinsApi.Extensions;
using BitSkinsApi.CheckParameters;

namespace BitSkinsApi.BuyOrder
{
    /// <summary>
    /// Work with canceling buy orders.
    /// </summary>
    public static class CancelingBuyOrders
    {
        /// <summary>
        /// Allows you to cancel upto 999 active buy orders.
        /// </summary>
        /// <param name="app">Inventory's game id.</param>
        /// <param name="buyOrderIds">Up to 999 buy order IDs.</param>
        /// <returns>Canceled buy orders.</returns>
        public static CanceledBuyOrders CancelBuyOrders(AppId.AppName app, List<string> buyOrderIds)
        {
            CheckParametersForCancelBuyOrders(buyOrderIds);
            string urlRequest = GetUrlRequestForBuyOrders(app, buyOrderIds);
            string result = Server.ServerRequest.RequestServer(urlRequest);
            CanceledBuyOrders canceledBuyOrders = ReadCanceledBuyOrders(result);
            return canceledBuyOrders;
        }

        /// <summary>
        /// Allows you to cancel all buy orders for a given item name.
        /// </summary>
        /// <param name="app">Inventory's game id.</param>
        /// <param name="marketHashName">The item name.</param>
        /// <returns>Canceled buy orders.</returns>
        public static CanceledBuyOrders CancelAllBuyOrders(AppId.AppName app, string marketHashName)
        {
            CheckParametersCancelAllBuyOrders(marketHashName);
            string urlRequest = GetUrlRequestForAllBuyOrders(app, marketHashName);
            string result = Server.ServerRequest.RequestServer(urlRequest);
            CanceledBuyOrders canceledBuyOrders = ReadCanceledBuyOrders(result);
            return canceledBuyOrders;
        }

        private static void CheckParametersForCancelBuyOrders(List<string> buyOrderIds)
        {
            Checking.NotEmptyList(buyOrderIds, "buyOrderIds");
        }

        private static void CheckParametersCancelAllBuyOrders(string marketHashName)
        {
            Checking.NotEmptyString(marketHashName, "marketHashName");
        }

        private static string GetUrlRequestForBuyOrders(AppId.AppName app, List<string> buyOrderIds)
        {
            const string delimiter = ",";

            Server.UrlCreator urlCreator = new Server.UrlCreator($"https://bitskins.com/api/v1/cancel_buy_orders/");
            urlCreator.AppendUrl($"&app_id={(int)app}");
            urlCreator.AppendUrl($"&buy_order_ids={buyOrderIds.ToStringWithDelimiter(delimiter)}");

            return urlCreator.ReadUrl();
        }

        private static string GetUrlRequestForAllBuyOrders(AppId.AppName app, string marketHashName)
        {
            Server.UrlCreator urlCreator = new Server.UrlCreator($"https://bitskins.com/api/v1/cancel_all_buy_orders/");
            urlCreator.AppendUrl($"&app_id={(int)app}");
            urlCreator.AppendUrl($"&market_hash_name={marketHashName}");

            return urlCreator.ReadUrl();
        }

        private static CanceledBuyOrders ReadCanceledBuyOrders(string result)
        {
            dynamic responseServerD = JsonConvert.DeserializeObject(result);
            dynamic dataD = responseServerD.data;

            int? count = null;
            List<string> buyOrderIds = new List<string>();
            if (dataD != null)
            {
                count = dataD.num ?? null;

                foreach (dynamic buyOrderId in dataD.buy_order_ids)
                {
                    buyOrderIds.Add((string)buyOrderId);
                }
            }

            CanceledBuyOrders canceledBuyOrders = new CanceledBuyOrders(count, buyOrderIds);
            return canceledBuyOrders;
        }
    }

    /// <summary>
    /// Canceled buy orders.
    /// </summary>
    public class CanceledBuyOrders
    {
        public int? Count { get; private set; }
        public List<string> BuyOrderIds { get; private set; }

        internal CanceledBuyOrders(int? count, List<string> buyOrderIds)
        {
            Count = count;
            BuyOrderIds = buyOrderIds;
        }
    }
}
