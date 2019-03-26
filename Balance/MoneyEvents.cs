using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using BitSkinsApi.Extensions;

namespace BitSkinsApi.Balance
{
    /// <summary>
    /// Work with BitSkins money events.
    /// </summary>
    public static class MoneyEvents
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
            string url = $"https://bitskins.com/api/v1/get_money_events/?api_key={Account.AccountData.ApiKey}&page={page}&code={Account.Secret.Code}";
            if (!Server.ServerRequest.RequestServer(url, out string result))
                throw new Exception(result);
            
            dynamic responseServer = JsonConvert.DeserializeObject(result);
            dynamic events = responseServer.data.events;

            List<MoneyEvent> moneyEvents = new List<MoneyEvent>();
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
                    moneyEvents.Add(moneyEvent);
                }
            }

            return moneyEvents;
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
        public MoneyEvents.MoneyEventType Type { get; private set; }
        public double Money { get; private set; }
        public DateTime Time { get; private set; }

        internal MoneyEvent(MoneyEvents.MoneyEventType type, double money, DateTime time)
        {
            Type = type;
            Money = money;
            Time = time;
        }
    }
}
