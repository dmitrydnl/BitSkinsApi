/*
 * BitSkinsApi
 * Copyright (C) 2019 Captious99
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

namespace BitSkinsApi.BuyOrder
{
    /// <summary>
    /// Work with creating buy orders.
    /// </summary>
    public static class CreatingBuyOrder
    {
        /// <summary>
        /// Allows you to create a buy order for a single item. 
        /// Buy orders are executed within 30 seconds if someone lists an item for sale that is at or below your buy order price. 
        /// Funds are not withheld because of pending buy orders, 
        /// but will be automatically cancelled if your account has insufficient funds to execute a buy order when an eligible item is up for sale.
        /// </summary>
        /// <param name="app">Inventory's game id.</param>
        /// <param name="name">The name of the item you want to purchase.</param>
        /// <param name="price">The price at which you want to purchase the item.</param>
        /// <param name="quantity">Number of buy orders to create at this price for this item.</param>
        /// <returns>List of created buy orders.</returns>
        public static List<CreatedBuyOrder> CreateBuyOrder(AppId.AppName app, string name, double price, int quantity)
        {
            string urlRequest = GetUrlRequest(app, name, price, quantity);
            string result = Server.ServerRequest.RequestServer(urlRequest);
            List<CreatedBuyOrder> createdBuyOrders = ReadCreatedBuyOrders(result);
            return createdBuyOrders;
        }

        private static string GetUrlRequest(AppId.AppName app, string name, double price, int quantity)
        {
            Server.UrlCreator urlCreator = new Server.UrlCreator($"https://bitskins.com/api/v1/create_buy_order/");
            urlCreator.AppendUrl($"&app_id={(int)app}");
            urlCreator.AppendUrl($"&name={name}");
            urlCreator.AppendUrl($"&price={price.ToString(System.Globalization.CultureInfo.InvariantCulture)}");
            urlCreator.AppendUrl($"&quantity={quantity}");

            return urlCreator.ReadUrl();
        }

        private static List<CreatedBuyOrder> ReadCreatedBuyOrders(string result)
        {
            dynamic responseServerD = JsonConvert.DeserializeObject(result);
            dynamic ordersD = responseServerD.data.orders;

            List<CreatedBuyOrder> createdBuyOrders = new List<CreatedBuyOrder>();
            if (ordersD != null)
            {
                foreach (dynamic order in ordersD)
                {
                    CreatedBuyOrder createdBuyOrder = ReadCreatedBuyOrder(order);
                    createdBuyOrders.Add(createdBuyOrder);
                }
            }

            return createdBuyOrders;
        }

        private static CreatedBuyOrder ReadCreatedBuyOrder(dynamic order)
        {
            string buyOrderId = order.buy_order_id;
            string marketHashName = order.market_hash_name;
            double price = order.price;
            double suggestedPrice = order.suggested_price;
            string state = order.state;
            DateTime createdAt = DateTimeExtension.FromUnixTime((long)order.created_at);
            DateTime updatedAt = DateTimeExtension.FromUnixTime((long)order.updated_at);
            int? placeInQueue = order.place_in_queue;

            CreatedBuyOrder createdBuyOrder = new CreatedBuyOrder(buyOrderId, marketHashName, price, suggestedPrice, state, createdAt, updatedAt, placeInQueue);
            return createdBuyOrder;
        }
    }

    /// <summary>
    /// Created buy order.
    /// </summary>
    public class CreatedBuyOrder
    {
        public string BuyOrderId { get; private set; }
        public string MarketHashName { get; private set; }
        public double Price { get; private set; }
        public double SuggestedPrice { get; private set; }
        public string State { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public int? PlaceInQueue { get; private set; }

        internal CreatedBuyOrder(string buyOrderId, string marketHashName, double price, double suggestedPrice, 
            string state, DateTime createdAt, DateTime updatedAt, int? placeInQueue)
        {
            BuyOrderId = buyOrderId;
            MarketHashName = marketHashName;
            Price = price;
            SuggestedPrice = suggestedPrice;
            State = state;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            PlaceInQueue = placeInQueue;
        }
    }
}
