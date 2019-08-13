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
using BitSkinsApi.CheckParameters;

namespace BitSkinsApi.Market
{
    /// <summary>
    /// Work with raw Steam Market price data.
    /// </summary>
    public static class SteamRawPriceData
    {
        /// <summary>
        /// Allows you to retrieve raw Steam Community Market price data for a given item. 
        /// You can use this data to create your own pricing algorithm if you need it.
        /// </summary>
        /// <param name="app">Inventory's game id.</param>
        /// <param name="marketHashName">The item's name.</param>
        /// <returns>Raw Steam Market price data for a given item.</returns>
        public static SteamItemRawPriceData GetRawPriceData(AppId.AppName app, string marketHashName)
        {
            CheckParameters(marketHashName);
            string urlRequest = GetUrlRequest(app, marketHashName);
            string result = Server.ServerRequest.RequestServer(urlRequest);
            SteamItemRawPriceData steamItemRawPriceData = ReadSteamItemRawPrice(result);
            return steamItemRawPriceData;
        }

        private static void CheckParameters(string marketHashName)
        {
            Checking.NotEmptyString(marketHashName, "marketHashName");
        }

        private static string GetUrlRequest(AppId.AppName app, string marketHashName)
        {
            Server.UrlCreator urlCreator = new Server.UrlCreator($"https://bitskins.com/api/v1/get_steam_price_data/");
            urlCreator.AppendUrl($"&app_id={(int)app}");
            urlCreator.AppendUrl($"&market_hash_name={marketHashName}");

            return urlCreator.ReadUrl();
        }

        private static SteamItemRawPriceData ReadSteamItemRawPrice(string result)
        {
            dynamic responseServerD = JsonConvert.DeserializeObject(result);
            dynamic updatedAtD = responseServerD.data.updated_at;
            dynamic rawDataD = responseServerD.data.raw_data;

            DateTime? updatedAt = ReadUpdatedAt(updatedAtD);
            List<ItemRawPrice> itemRawPrices = ReadItemRawPrices(rawDataD);

            SteamItemRawPriceData steamItemRawPriceData = new SteamItemRawPriceData(itemRawPrices, updatedAt);
            return steamItemRawPriceData;
        }

        private static DateTime? ReadUpdatedAt(dynamic updatedAtD)
        {
            DateTime? updatedAt = null;
            if (updatedAtD != null)
            {
                updatedAt = DateTimeExtension.FromUnixTime((long)updatedAtD);
            }

            return updatedAt;
        }

        private static List<ItemRawPrice> ReadItemRawPrices(dynamic rawDataD)
        {
            List<ItemRawPrice> itemRawPrices = new List<ItemRawPrice>();
            if (rawDataD != null)
            {
                foreach (dynamic item in rawDataD)
                {
                    ItemRawPrice itemRawPrice = ReadItemRawPrice(item);
                    itemRawPrices.Add(itemRawPrice);
                }
            }

            return itemRawPrices;
        }

        private static ItemRawPrice ReadItemRawPrice(dynamic item)
        {
            DateTime? time = null;
            if (item.time != null)
            {
                time = DateTimeExtension.FromUnixTime((long)item.time);
            }
            double? price = item.price ?? null;
            int? volume = item.volume ?? null;

            ItemRawPrice itemRawPrice = new ItemRawPrice(time, price, volume);
            return itemRawPrice;
        }
    }

    /// <summary>
    /// Item's sales data in Steam.
    /// </summary>
    public class SteamItemRawPriceData
    {
        public List<ItemRawPrice> ItemRawPrices { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        internal SteamItemRawPriceData(List<ItemRawPrice> rawPriceItems, DateTime? updatedAt)
        {
            ItemRawPrices = rawPriceItems;
            UpdatedAt = updatedAt;
        }
    }

    /// <summary>
    /// Sales data about Steam item at a certain time.
    /// </summary>
    public class ItemRawPrice
    {
        public DateTime? Time { get; private set; }
        public double? Price { get; private set; }
        public int? Volume { get; private set; }

        internal ItemRawPrice(DateTime? time, double? price, int? volume)
        {
            Time = time;
            Price = price;
            Volume = volume;
        }
    }
}
