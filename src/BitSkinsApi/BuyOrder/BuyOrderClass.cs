using System;

namespace BitSkinsApi.BuyOrder
{
    /// <summary>
    /// Created buy order.
    /// </summary>
    public class BuyOrder
    {
        public string BuyOrderId { get; private set; }
        public string MarketHashName { get; private set; }
        public double? Price { get; private set; }
        public double? SuggestedPrice { get; private set; }
        public string State { get; private set; }
        public DateTime? CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public int? PlaceInQueue { get; private set; }

        internal BuyOrder(string buyOrderId, string marketHashName, double? price, double? suggestedPrice,
            string state, DateTime? createdAt, DateTime? updatedAt, int? placeInQueue)
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
