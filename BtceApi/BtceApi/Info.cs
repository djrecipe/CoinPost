#region Using
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
#endregion
namespace BtcE
{
    public class Funds
    {
        #region Instance Members
        public Dictionary<string, decimal> funds { get; private set; }
        #endregion
        #region Instance Methods
        #region Initialization Methods
        public Funds(JObject o)
        {
            this.funds = new Dictionary<string, decimal>();
            if (o == null)
                return;
            foreach (string s in Exchange.usd_pairs)
                this.funds.Add(s, o.Value<decimal>(s.ToLower()));
            foreach (string s in Exchange.non_usd_pairs)
                this.funds.Add(s, o.Value<decimal>(s.ToLower()));
            this.funds.Add(Exchange.target_pairs[1], o.Value<decimal>(Exchange.target_pairs[1].ToLower()));
            return;
        }
        #endregion
        #region Getter Methods
        public decimal GetBalance(string key)
        {
            if (this.funds != null && this.funds.ContainsKey(key))
                return this.funds[key];
            else
                return 0;
        }
        #endregion
        #endregion

    };

    public class Info
    {
        private static bool InfoWasDisabled = false;
        #region Instance Properties
        #region Numeric Properties
        public int OpenOrders { get; private set; }
        public int ServerTime { get; private set; }
        public int TransactionCount { get; private set; }
        #endregion
        #region Object Properties
        public Funds funds { get; private set; }
        public Rights rights { get; private set; }
        #endregion
        #endregion
        #region Instance Methods
        #region Initialization Methods
        public Info(JObject o)
        {
            if (o == null)
                return;
            // ~ Determine Rights
            this.rights = new Rights(o["rights"] as JObject);
            if (this.rights.Info)
            {
                // ~ Initialize Object Properties
                this.funds = new Funds(o["funds"] as JObject);
                // ~ Initialize Numeric Properties
                this.OpenOrders = o.Value<int>("open_orders");
                this.ServerTime = o.Value<int>("server_time");
                this.TransactionCount = o.Value<int>("transaction_count");
                //
                Info.InfoWasDisabled = false;
            }
            else if(!Info.InfoWasDisabled)
            {
                System.Windows.Forms.MessageBox.Show("Info permissions were disabled on this API key.");
                Info.InfoWasDisabled = true;
            }
            return;
        }
        #endregion
        #endregion
    }
    public class Order
    {
        #region Instance Properties
        #region Numeric Properties
        public decimal Amount { get; private set; }
        public decimal Rate { get; private set; }
        public uint TimestampCreated { get; private set; }
        public int Status { get; private set; }
        #endregion
        #region String Properties
        public string Pair { get; private set; }
        public string Type { get; private set; }
        #endregion
        #endregion
        #region Instance Methods
        public Order(string pair_in, string type_in, decimal amount_in, decimal rate_in, uint timestamp_in, int status_in)
        {
            this.Pair = pair_in;
            this.Type = type_in;
            this.Amount = amount_in;
            this.Rate = rate_in;
            this.TimestampCreated = timestamp_in;
            this.Status = status_in;
            return;
        }
        public Order(JToken t)
        {
            if (t == null)
                return;
            this.Pair = t.Value<string>("pair");
            this.Type = t.Value<string>("type");
            this.Amount = t.Value<decimal>("amount");
            this.Rate = t.Value<decimal>("rate");
            this.TimestampCreated = t.Value<uint>("timestamp_created");
            this.Status = t.Value<int>("status");
            return;
        }
        #endregion
    }

    public class OrderList
    {
        #region Instance Properties
        public Dictionary<int, Order> List { get; private set; }
        #endregion
        #region Instance Methods
        public OrderList(JObject o)
        {
            if (o == null)
                return;
            this.List = new Dictionary<int, Order>();
            foreach (KeyValuePair<string, JToken> pair in o)
                this.List.Add(Convert.ToInt32(pair.Key), new Order(o[pair.Key]));
            return;
        }
        #endregion
    }
    public class Rights
    {
        public bool Info { get; private set; }
        public bool Trade { get; private set; }
        public Rights(JObject o)
        {
            if (o == null)
                return;
            Info = (o.Value<int>("info") == 1);
            Trade = (o.Value<int>("trade") == 1);
            return;
        }
    }
    public class Transaction
    {
        #region Instance Properties
        #region Numeric Properties
        public decimal Amount { get; private set; }
        public int Status { get; private set; }
        public uint Timestamp { get; private set; }
        public int Type { get; private set; }
        #endregion
        #region String Properties
        public string Currency { get; private set; }
        public string Description { get; private set; }
        #endregion
        #endregion
        #region Instance Methods
        public Transaction(JObject o)
        {
            if (o == null)
                return;
            this.Type = o.Value<int>("type");
            this.Amount = o.Value<decimal>("amount");
            this.Currency = o.Value<string>("currency");
            this.Timestamp = o.Value<uint>("timestamp");
            this.Status = o.Value<int>("status");
            this.Description = o.Value<string>("desc");
            return;
        }
        #endregion
    }
    public class TransHistory
    {
        #region Instance Members
        public Dictionary<int, Transaction> List { get; private set; }
        #endregion
        #region Instance Methods
        public TransHistory(JObject o)
        {
            if (o == null)
                return;
            this.List = o.OfType<KeyValuePair<string, JToken>>().ToDictionary(a => int.Parse(a.Key), a => new Transaction(a.Value as JObject));
            return;
        }
        #endregion
    }
}
