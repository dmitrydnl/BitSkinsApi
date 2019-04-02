using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using BitSkinsApi.Extensions;

namespace BitSkinsApi.Balance
{
    /// <summary>
    /// Work with money events.
    /// </summary>
    public static class MoneyEvents
    {
        /// <summary>
        /// Types BitSkins money events.
        /// </summary>
        public enum MoneyEventType { ItemBought, ItemSold, SaleFee, BuyCredit, StoreCredit, Unknown };

        /// <summary>
        /// Allows you to retrieve historical events that caused changes in your BitSkins balance. 
        /// Upto 30 items per page.
        /// </summary>
        /// <param name="page">Page number.</param>
        /// <returns>List of money events.</returns>
        public static List<MoneyEvent> GetMoneyEvents(int page)
        {
            Server.UrlCreator urlCreator = new Server.UrlCreator($"https://bitskins.com/api/v1/get_money_events/");
            urlCreator.AppendUrl($"&page={page}");

            string result = Server.ServerRequest.RequestServer(urlCreator.ReadUrl());
            List<MoneyEvent> moneyEvents = ReadMoneyEvents(result);
            return moneyEvents;
        }

        static List<MoneyEvent> ReadMoneyEvents(string result)
        {
            dynamic responseServerD = JsonConvert.DeserializeObject(result);
            dynamic moneyEventsD = responseServerD.data.events;
            
            List<MoneyEvent> moneyEvents = new List<MoneyEvent>();
            if (moneyEventsD != null)
            {
                foreach (dynamic moneyEventD in moneyEventsD)
                {
                    MoneyEventType type = StringToMoneyEventType((string)moneyEventD.type);
                    if (type == MoneyEventType.Unknown)
                    {
                        continue;
                    }

                    DateTime time = DateTimeExtension.FromUnixTime((long)moneyEventD.time);

                    double amount = 0;
                    string description = "";
                    if (type == MoneyEventType.ItemBought || type == MoneyEventType.ItemSold)
                    {
                        amount = moneyEventD.price;
                        description = $"{moneyEventD.medium.app_id}:{moneyEventD.medium.market_hash_name}";
                    }
                    else if (type == MoneyEventType.SaleFee || type == MoneyEventType.BuyCredit || type == MoneyEventType.StoreCredit)
                    {
                        amount = moneyEventD.amount;
                        description = $"{moneyEventD.medium}";
                    }

                    MoneyEvent moneyEvent = new MoneyEvent(type, amount, description, time);
                    moneyEvents.Add(moneyEvent);
                }
            }

            return moneyEvents;
        }

        static MoneyEventType StringToMoneyEventType(string eventType)
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
                    return MoneyEventType.Unknown;
            }
        }
    }

    /// <summary>
    /// Money event.
    /// </summary>
    public class MoneyEvent
    {
        public MoneyEvents.MoneyEventType Type { get; private set; }
        public double Amount { get; private set; }
        public string Description { get; private set; }
        public DateTime Time { get; private set; }

        internal MoneyEvent(MoneyEvents.MoneyEventType type, double amount, string description, DateTime time)
        {
            Type = type;
            Amount = amount;
            Description = description;
            Time = time;
        }
    }
}
