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
        public enum MoneyEventType { ItemBought, ItemSold, SaleFee, BuyCredit, StoreCredit, Unknown };

        /// <summary>
        /// Allows you to retrieve historical events that caused changes in your BitSkins balance. Upto 30 items per page.
        /// </summary>
        /// <param name="page">Page number.</param>
        /// <returns>List of money events.</returns>
        public static List<MoneyEvent> GetMoneyEvents(int page)
        {
            string url = $"https://bitskins.com/api/v1/get_money_events/" +
                $"?api_key={Account.AccountData.GetApiKey()}" +
                $"&page={page}" +
                $"&code={Account.Secret.GetTwoFactorCode()}";

            string result = Server.ServerRequest.RequestServer(url);
            List<MoneyEvent> moneyEvents = ReadMoneyEvents(result);
            return moneyEvents;
        }

        static List<MoneyEvent> ReadMoneyEvents(string result)
        {
            dynamic responseServer = JsonConvert.DeserializeObject(result);
            dynamic events = responseServer.data.events;

            if (events == null)
            {
                return new List<MoneyEvent>();
            }

            List<MoneyEvent> moneyEvents = new List<MoneyEvent>();
            foreach (dynamic moneyEvent in events)
            {
                MoneyEventType type = StringToMoneyEventType((string)moneyEvent.type);
                if (type == MoneyEventType.Unknown)
                {
                    continue;
                }

                DateTime time = DateTimeExtension.FromUnixTime((long)moneyEvent.time);

                double amount = 0;
                string description = "";
                if (type == MoneyEventType.ItemBought || type == MoneyEventType.ItemSold)
                {
                    amount = moneyEvent.price;
                    description = $"{moneyEvent.medium.app_id}:{moneyEvent.medium.market_hash_name}";
                }
                else if (type == MoneyEventType.SaleFee || type == MoneyEventType.BuyCredit || type == MoneyEventType.StoreCredit)
                {
                    amount = moneyEvent.amount;
                    description = $"{moneyEvent.medium}";
                }

                MoneyEvent moneyEv = new MoneyEvent(type, amount, description, time);
                moneyEvents.Add(moneyEv);
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
    /// BitSkins money event.
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
