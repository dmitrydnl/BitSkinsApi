﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using BitSkinsApi.Extensions;

namespace BitSkinsApi.BitSkinsMarket.Balance
{
    /// <summary>
    /// Work with BitSkins money events.
    /// </summary>
    public class MoneyEvents
    {
        /// <summary>
        /// All types BitSkins money events.
        /// </summary>
        public enum MoneyEventType { ItemBought, ItemSold, SaleFee, BuyCredit, StoreCredit, Default };

        /// <summary>
        /// Allows you to retrieve historical events that caused changes in your BitSkins balance. Upto 30 items per page.
        /// </summary>
        /// <param name="page">Page number.</param>
        /// <returns>List of money events.</returns>
        public static List<MoneyEvent> GetMoneyEvents(int page)
        {
            string url = $"https://bitskins.com/api/v1/get_money_events/?api_key={Account.AccountData.apiKey}&page={page}&code={Account.Secret.GetCode()}";
            if (!Server.ServerRequest.RequestServer(url, out string result))
                throw new Exception(result);
            
            dynamic responseServer = JsonConvert.DeserializeObject(result);
            dynamic events = responseServer.data.events;
            List<MoneyEvent> pageEvents = new List<MoneyEvent>();
            if (events.Count != 0)
            {
                foreach (dynamic ev in events)
                {
                    MoneyEventType type = StringToMoneyEventType((string)ev.type);
                    if (type == MoneyEventType.Default)
                        continue;

                    double money = 0;
                    switch (type)
                    {
                        case MoneyEventType.ItemBought:
                        case MoneyEventType.ItemSold:
                            money = ev.price;
                            break;
                        case MoneyEventType.BuyCredit:
                        case MoneyEventType.SaleFee:
                        case MoneyEventType.StoreCredit:
                            money = ev.amount;
                            break;
                    }

                    DateTime time = DateTimeExtension.FromUnixTime((long)ev.time);

                    MoneyEvent moneyEvent = new MoneyEvent(type, money, time);
                    pageEvents.Add(moneyEvent);
                }
            }

            return pageEvents;
        }

        private static MoneyEventType StringToMoneyEventType(string eventType)
        {
            switch (eventType)
            {
                case "item bought":
                    return MoneyEventType.ItemBought;
                case "item sold":
                    return MoneyEventType.ItemSold;
                case "sale fee":
                    return MoneyEventType.SaleFee;
                case "buy credit":
                    return MoneyEventType.BuyCredit;
                case "store credit":
                    return MoneyEventType.StoreCredit;
                default:
                    return MoneyEventType.Default;
            }
        }
    }

    /// <summary>
    /// BitSkins money event.
    /// </summary>
    public class MoneyEvent
    {
        public MoneyEvents.MoneyEventType type { get; private set; }
        public double money { get; private set; }
        public DateTime time { get; private set; }

        public MoneyEvent(MoneyEvents.MoneyEventType type, double money, DateTime time)
        {
            this.type = type;
            this.money = money;
            this.time = time;
        }
    }
}
