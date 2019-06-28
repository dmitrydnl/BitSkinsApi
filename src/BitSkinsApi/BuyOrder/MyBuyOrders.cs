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

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using BitSkinsApi.Extensions;
using BitSkinsApi.Market;
using BitSkinsApi.CheckParameters;

namespace BitSkinsApi.BuyOrder
{
    /// <summary>
    /// Working with my buy orders.
    /// </summary>
    public static class MyBuyOrders
    {
        /// <summary>
        /// Types of buy orders.
        /// </summary>
        public enum BuyOrderType { Listed, Settled, CancelledByUser, CancelledBySystem, NotImportant };

        /// <summary>
        /// Allows you to retrieve all buy orders you have placed, whether active or not.
        /// </summary>
        /// <param name="app">Inventory's game id.</param>
        /// <param name="name">An item's name. (optional)</param>
        /// <param name="type">Type of buy orders. (optional)</param>
        /// <param name="page">The page number.</param>
        /// <returns>My buy orders.</returns>
        public static List<BuyOrder> GetMyBuyOrders(AppId.AppName app, string name, BuyOrderType type, int page)
        {
            CheckParameters(page);
            string urlRequest = GetUrlRequest(app, name, type, page);
            string result = Server.ServerRequest.RequestServer(urlRequest);
            List<BuyOrder> myBuyOrders = ReadMyBuyOrders(result);
            return myBuyOrders;
        }

        private static void CheckParameters(int page)
        {
            Checking.PositiveInt(page, "page");
        }

        private static string GetUrlRequest(AppId.AppName app, string name, BuyOrderType type, int page)
        {
            Server.UrlCreator urlCreator = new Server.UrlCreator($"https://bitskins.com/api/v1/get_buy_order_history/");
            urlCreator.AppendUrl($"&app_id={(int)app}");
            urlCreator.AppendUrl($"&page={page}");
            if (!String.IsNullOrEmpty(name))
            {
                urlCreator.AppendUrl($"&market_hash_name={name}");
            }
            if (type != BuyOrderType.NotImportant)
            {
                urlCreator.AppendUrl($"&type={BuyOrderTypeToString(type)}");
            }

            return urlCreator.ReadUrl();
        }

        private static string BuyOrderTypeToString(BuyOrderType type)
        {
            switch (type)
            {
                case BuyOrderType.Listed:
                    return "listed";
                case BuyOrderType.Settled:
                    return "settled";
                case BuyOrderType.CancelledByUser:
                    return "cancelled_by_user";
                case BuyOrderType.CancelledBySystem:
                    return "cancelled_by_system";
                default:
                    return "";
            }
        }

        private static List<BuyOrder> ReadMyBuyOrders(string result)
        {
            dynamic responseServerD = JsonConvert.DeserializeObject(result);
            dynamic ordersD = responseServerD.data.orders;

            List<BuyOrder> myBuyOrders = new List<BuyOrder>();
            if (ordersD != null)
            {
                foreach (dynamic order in ordersD)
                {
                    BuyOrder myBuyOrder = ReadMyBuyOrder(order);
                    myBuyOrders.Add(myBuyOrder);
                }
            }

            return myBuyOrders;
        }

        private static BuyOrder ReadMyBuyOrder(dynamic order)
        {
            string buyOrderId = order.buy_order_id;
            string marketHashName = order.market_hash_name;
            double price = order.price;
            double? suggestedPrice = order.suggested_price;
            string state = order.state;
            DateTime createdAt = DateTimeExtension.FromUnixTime((long)order.created_at);
            DateTime updatedAt = DateTimeExtension.FromUnixTime((long)order.updated_at);
            int? placeInQueue = order.place_in_queue;

            BuyOrder myBuyOrder = new BuyOrder(buyOrderId, marketHashName, price, suggestedPrice, state, createdAt, updatedAt, placeInQueue);
            return myBuyOrder;
        }
    }
}
