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

using System.Collections.Generic;
using Newtonsoft.Json;
using BitSkinsApi.Market;

namespace BitSkinsApi.BuyOrder
{
    /// <summary>
    /// Work with summation buy orders.
    /// </summary>
    public static class SummationBuyOrders
    {
        /// <summary>
        /// Allows you to retrieve a summary of all market buy orders. Results include your own buy orders, where applicable.
        /// </summary>
        /// <param name="app">Inventory's game id.</param>
        /// <returns>List of items of buy order.</returns>
        public static List<ItemBuyOrder> SummarizeBuyOrders(AppId.AppName app)
        {
            string urlRequest = GetUrlRequest(app);
            string result = Server.ServerRequest.RequestServer(urlRequest);
            List<ItemBuyOrder> itemsBuyOrder = ReadItemsBuyOrder(result);
            return itemsBuyOrder;
        }

        private static string GetUrlRequest(AppId.AppName app)
        {
            Server.UrlCreator urlCreator = new Server.UrlCreator($"https://bitskins.com/api/v1/summarize_buy_orders/");
            urlCreator.AppendUrl($"&app_id={(int)app}");

            return urlCreator.ReadUrl();
        }

        private static List<ItemBuyOrder> ReadItemsBuyOrder(string result)
        {
            dynamic responseServerD = JsonConvert.DeserializeObject(result);
            dynamic itemsD = responseServerD.data.items;

            List<ItemBuyOrder> itemsBuyOrder = new List<ItemBuyOrder>();
            if (itemsD != null)
            {
                foreach (dynamic item in itemsD)
                {
                    ItemBuyOrder itemBuyOrder = ReadItemBuyOrder(item);
                    itemsBuyOrder.Add(itemBuyOrder);
                }
            }

            return itemsBuyOrder;
        }

        private static ItemBuyOrder ReadItemBuyOrder(dynamic item)
        {
            string marketHashName = item[0];
            int numberOfBuyOrders = item[1].number_of_buy_orders;
            double maxPrice = item[1].max_price;
            double minPrice = item[1].min_price;
            int numberOfMyBuyOrders = 0;
            double maxPriceMyBuyOrders = 0;
            double minPriceMyBuyOrders = 0;
            if (item[1].my_buy_orders != null)
            {
                numberOfMyBuyOrders = item[1].my_buy_orders.number_of_buy_orders;
                maxPriceMyBuyOrders = item[1].my_buy_orders.max_price;
                minPriceMyBuyOrders = item[1].my_buy_orders.min_price;
            }

            ItemBuyOrder itemBuyOrder = new ItemBuyOrder(marketHashName, numberOfBuyOrders, maxPrice, minPrice,
                numberOfMyBuyOrders, maxPriceMyBuyOrders, minPriceMyBuyOrders);
            return itemBuyOrder;
        }
    }

    /// <summary>
    /// Item of buy order.
    /// </summary>
    public class ItemBuyOrder
    {
        public string MarketHashName { get; private set; }
        public int NumberOfBuyOrders { get; private set; }
        public double MaxPrice { get; private set; }
        public double MinPrice { get; private set; }
        public int NumberOfMyBuyOrders { get; private set; }
        public double MaxPriceMyBuyOrders { get; private set; }
        public double MinPriceMyBuyOrders { get; private set; }

        internal ItemBuyOrder(string marketHashName, int numberOfBuyOrders, double maxPrice, double minPrice,
            int numberOfMyBuyOrders, double maxPriceMyBuyOrders, double minPriceMyBuyOrders)
        {
            MarketHashName = marketHashName;
            NumberOfBuyOrders = numberOfBuyOrders;
            MaxPrice = maxPrice;
            MinPrice = minPrice;
            NumberOfMyBuyOrders = numberOfMyBuyOrders;
            MaxPriceMyBuyOrders = maxPriceMyBuyOrders;
            MinPriceMyBuyOrders = minPriceMyBuyOrders;
        }
    }
}
