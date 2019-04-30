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

namespace BitSkinsApi.BuyOrder
{
    /// <summary>
    /// Created buy order.
    /// </summary>
    public class BuyOrder
    {
        public string BuyOrderId { get; private set; }
        public string MarketHashName { get; private set; }
        public double Price { get; private set; }
        public double SuggestedPrice { get; private set; }
        public string State { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public int? PlaceInQueue { get; private set; }

        internal BuyOrder(string buyOrderId, string marketHashName, double price, double suggestedPrice,
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
