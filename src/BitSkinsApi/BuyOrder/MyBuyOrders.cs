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
            string buyOrderId = order.buy_order_id ?? null;
            string marketHashName = order.market_hash_name ?? null;
            double? price = order.price ?? null;
            double? suggestedPrice = order.suggested_price ?? null;
            string state = order.state ?? null;
            DateTime? createdAt = null;
            if (order.created_at != null)
            {
                createdAt = DateTimeExtension.FromUnixTime((long)order.created_at);
            }
            DateTime? updatedAt = null;
            if (order.updated_at != null)
            {
                updatedAt = DateTimeExtension.FromUnixTime((long)order.updated_at);
            }
            int? placeInQueue = order.place_in_queue ?? null;

            BuyOrder myBuyOrder = new BuyOrder(buyOrderId, marketHashName, price, suggestedPrice, state, createdAt, updatedAt, placeInQueue);
            return myBuyOrder;
        }
    }
}
