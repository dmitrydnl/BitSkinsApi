/*
 * BitSkinsApi
 * Copyright (C) 2019 dmitrydnl
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

            int count = 0;
            List<string> buyOrderIds = new List<string>();
            if (dataD != null)
            {
                count = dataD.num;

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
        public int Count { get; private set; }
        public List<string> BuyOrderIds { get; private set; }

        internal CanceledBuyOrders(int count, List<string> buyOrderIds)
        {
            Count = count;
            BuyOrderIds = buyOrderIds;
        }
    }
}
