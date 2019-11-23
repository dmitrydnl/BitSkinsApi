using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using BitSkinsApi.Extensions;
using BitSkinsApi.Market;
using BitSkinsApi.CheckParameters;

namespace BitSkinsApi.BuyOrder
{
    /// <summary>
    /// Work with market buy orders.
    /// </summary>
    public static class MarketBuyOrders
    {
        /// <summary>
        /// Allows you to retrieve all market orders by all buyers (except your own) that need fulfillment.
        /// </summary>
        /// <param name="app">Inventory's game id.</param>
        /// <param name="name">The item name for which you seek buy orders.</param>
        /// <param name="page">Page number.</param>
        /// <returns>List of market buy orders.</returns>
        public static List<MarketBuyOrder> GetMarketBuyOrders(AppId.AppName app, string name, int page)
        {
            CheckParameters(name, page);
            string urlRequest = GetUrlRequest(app, name, page);
            string result = Server.ServerRequest.RequestServer(urlRequest);
            List<MarketBuyOrder> marketBuyOrders = ReadMarketBuyOrders(result);
            return marketBuyOrders;
        }

        private static void CheckParameters(string name, int page)
        {
            Checking.NotEmptyString(name, "name");
            Checking.PositiveInt(page, "page");
        }

        private static string GetUrlRequest(AppId.AppName app, string name, int page)
        {
            Server.UrlCreator urlCreator = new Server.UrlCreator($"https://bitskins.com/api/v1/get_market_buy_orders/");
            urlCreator.AppendUrl($"&app_id={(int)app}");
            urlCreator.AppendUrl($"&market_hash_name={name}");
            urlCreator.AppendUrl($"&page={page}");

            return urlCreator.ReadUrl();
        }

        private static List<MarketBuyOrder> ReadMarketBuyOrders(string result)
        {
            dynamic responseServerD = JsonConvert.DeserializeObject(result);
            dynamic ordersD = responseServerD.data.orders;

            List<MarketBuyOrder> marketBuyOrders = new List<MarketBuyOrder>();
            if (ordersD != null)
            {
                foreach (dynamic order in ordersD)
                {
                    MarketBuyOrder marketBuyOrder = ReadMarketBuyOrder(order);
                    marketBuyOrders.Add(marketBuyOrder);
                }
            }

            return marketBuyOrders;
        }

        private static MarketBuyOrder ReadMarketBuyOrder(dynamic order)
        {
            string buyOrderId = order.buy_order_id ?? null;
            string marketHashName = order.market_hash_name ?? null;
            double? price = order.price ?? null;
            double? suggestedPrice = order.suggested_price ?? null;
            bool? isMine = order.is_mine ?? null;
            DateTime? createdAt = null;
            if (order.created_at != null)
            {
                createdAt = DateTimeExtension.FromUnixTime((long)order.created_at);
            }
            int? placeInQueue = order.place_in_queue ?? null;

            MarketBuyOrder marketBuyOrder = new MarketBuyOrder(buyOrderId, marketHashName, price, suggestedPrice, isMine, createdAt, placeInQueue);
            return marketBuyOrder;
        }
    }

    /// <summary>
    /// Market buy order.
    /// </summary>
    public class MarketBuyOrder
    {
        public string BuyOrderId { get; private set; }
        public string MarketHashName { get; private set; }
        public double? Price { get; private set; }
        public double? SuggestedPrice { get; private set; }
        public bool? IsMine { get; private set; }
        public DateTime? CreatedAt { get; private set; }
        public int? PlaceInQueue { get; private set; }

        internal MarketBuyOrder(string buyOrderId, string marketHashName, double? price, double? suggestedPrice,
            bool? isMine, DateTime? createdAt, int? placeInQueue)
        {
            BuyOrderId = buyOrderId;
            MarketHashName = marketHashName;
            Price = price;
            SuggestedPrice = suggestedPrice;
            IsMine = isMine;
            CreatedAt = createdAt;
            PlaceInQueue = placeInQueue;
        }
    }
}
