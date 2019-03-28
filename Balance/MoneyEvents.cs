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
            string url = $"https://bitskins.com/api/v1/get_money_events/?api_key={Account.AccountData.GetApiKey()}&page={page}&code={Account.Secret.GetTwoFactorCode()}";
            string result = Server.ServerRequest.RequestServer(url);

            dynamic responseServer = JsonConvert.DeserializeObject(result);

            if (responseServer.data.events == null)
            {
                return new List<MoneyEvent>();
            }

            List<MoneyEvent> moneyEvents = new List<MoneyEvent>();
            foreach (dynamic moneyEvent in responseServer.data.events)
            {
                MoneyEventType type = StringToMoneyEventType((string)moneyEvent.type);
                if (type == MoneyEventType.Unknown)
                {
                    continue;
                }
                DateTime time = DateTimeExtension.FromUnixTime((long)moneyEvent.time);

                MoneyEvent moneyEv = null;
                if (type == MoneyEventType.ItemBought)
                {
                    double price = moneyEvent.price;
                    int withdrawn = moneyEvent.withdrawn;
                    Medium medium = ReadMedium(moneyEvent);
                    MoneyEvent_ItemBought moneyEvent_ItemBought = new MoneyEvent_ItemBought(type, time, price, medium, withdrawn);
                    moneyEv = moneyEvent_ItemBought;
                }
                else if (type == MoneyEventType.ItemSold)
                {
                    double price = moneyEvent.price;
                    Medium medium = ReadMedium(moneyEvent);
                    MoneyEvent_ItemSold moneyEvent_ItemSold = new MoneyEvent_ItemSold(type, time, price, medium);
                    moneyEv = moneyEvent_ItemSold;
                }
                else if (type == MoneyEventType.SaleFee)
                {
                    string medium = moneyEvent.medium;
                    double amount = moneyEvent.amount;
                    bool pending = moneyEvent.pending;
                    string description = moneyEvent.description;
                    MoneyEvent_SaleFee moneyEvent_SaleFee = new MoneyEvent_SaleFee(type, time, medium, amount, pending, description);
                    moneyEv = moneyEvent_SaleFee;
                }
                else if (type == MoneyEventType.BuyCredit)
                {
                    string medium = moneyEvent.medium;
                    double amount = moneyEvent.amount;
                    bool pending = moneyEvent.pending;
                    string description = moneyEvent.description;
                    MoneyEvent_BuyCredit moneyEvent_BuyCredit = new MoneyEvent_BuyCredit(type, time, medium, amount, pending, description);
                    moneyEv = moneyEvent_BuyCredit;
                }
                else if (type == MoneyEventType.StoreCredit)
                {
                    string medium = moneyEvent.medium;
                    double amount = moneyEvent.amount;
                    bool pending = moneyEvent.pending;
                    string description = moneyEvent.description;
                    MoneyEvent_StoreCredit moneyEvent_StoreCredit = new MoneyEvent_StoreCredit(type, time, medium, amount, pending, description);
                    moneyEv = moneyEvent_StoreCredit;
                }

                moneyEvents.Add(moneyEv);
            }

            return moneyEvents;
        }

        static Medium ReadMedium(dynamic moneyEvent)
        {
            string marketHashName = moneyEvent.medium.market_hash_name;
            Market.AppId.AppName appId = (Market.AppId.AppName)(int)moneyEvent.medium.app_id;
            string classId = moneyEvent.medium.class_id;
            string instanceId = moneyEvent.medium.instance_id;
            Medium medium = new Medium(marketHashName, appId, classId, instanceId);
            return medium;
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
    public abstract class MoneyEvent
    {
        public MoneyEvents.MoneyEventType Type { get; private set; }
        public DateTime Time { get; private set; }

        protected MoneyEvent(MoneyEvents.MoneyEventType type, DateTime time)
        {
            Type = type;
            Time = time;
        }
    }

    /// <summary>
    /// BitSkins medium info about money event.
    /// </summary>
    public class Medium
    {
        public string MarketHashName { get; private set; }
        public Market.AppId.AppName AppId { get; private set; }
        public string ClassId { get; private set; }
        public string InstanceId { get; private set; }

        internal Medium(string marketHashName, Market.AppId.AppName appId, string classId, string instanceId)
        {
            MarketHashName = marketHashName;
            AppId = appId;
            ClassId = classId;
            InstanceId = instanceId;
        }
    }

    /// <summary>
    /// BitSkins bought item money event.
    /// </summary>
    public class MoneyEvent_ItemBought : MoneyEvent
    {
        public double Price { get; private set; }
        public Medium Medium { get; private set; }
        public int Withdrawn { get; private set; }

        internal MoneyEvent_ItemBought(MoneyEvents.MoneyEventType type, DateTime time, double price, Medium medium, int withdrawn)
            : base(type, time)
        {
            Price = price;
            Medium = medium;
            Withdrawn = withdrawn;
        }
    }

    /// <summary>
    /// BitSkins sold item money event.
    /// </summary>
    public class MoneyEvent_ItemSold : MoneyEvent
    {
        public double Price { get; private set; }
        public Medium Medium { get; private set; }

        internal MoneyEvent_ItemSold(MoneyEvents.MoneyEventType type, DateTime time, double price, Medium medium)
            : base(type, time)
        {
            Price = price;
            Medium = medium;
        }
    }

    /// <summary>
    /// BitSkins sale fee money event.
    /// </summary>
    public class MoneyEvent_SaleFee : MoneyEvent
    {
        public string Medium { get; private set; }
        public double Amount { get; private set; }
        public bool Pending { get; private set; }
        public string Description { get; private set; }

        internal MoneyEvent_SaleFee(MoneyEvents.MoneyEventType type, DateTime time, string medium, double amount, bool pending, string description)
            : base(type, time)
        {
            Medium = medium;
            Amount = amount;
            Pending = pending;
            Description = description;
        }
    }

    /// <summary>
    /// BitSkins buy credit money event.
    /// </summary>
    public class MoneyEvent_BuyCredit : MoneyEvent
    {
        public string Medium { get; private set; }
        public double Amount { get; private set; }
        public bool Pending { get; private set; }
        public string Description { get; private set; }

        internal MoneyEvent_BuyCredit(MoneyEvents.MoneyEventType type, DateTime time, string medium, double amount, bool pending, string description)
            : base(type, time)
        {
            Medium = medium;
            Amount = amount;
            Pending = pending;
            Description = description;
        }
    }

    /// <summary>
    /// BitSkins store credit money event.
    /// </summary>
    public class MoneyEvent_StoreCredit : MoneyEvent
    {
        public string Medium { get; private set; }
        public double Amount { get; private set; }
        public bool Pending { get; private set; }
        public string Description { get; private set; }

        internal MoneyEvent_StoreCredit(MoneyEvents.MoneyEventType type, DateTime time, string medium, double amount, bool pending, string description)
            : base(type, time)
        {
            Medium = medium;
            Amount = amount;
            Pending = pending;
            Description = description;
        }
    }
}
