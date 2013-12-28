#region Using
using System;
using System.Collections.Generic;
using System.Linq;  
using Newtonsoft.Json.Linq;
#endregion

namespace BtcE
{
    public class Trade
    {
        #region Instance Properties
        #region Flag Properties
        public bool IsYourOrder { get; private set; }
        #endregion
        #region Numeric Properties
        public decimal Amount { get; private set; }
        public int OrderId { get; private set; }
        public decimal Rate { get; private set; }
        public uint Timestamp { get; private set; }
        #endregion
        #region String Properties
        public string Pair { get; private set; }
        public string Type { get; private set; }
        #endregion
        #endregion
        #region Instance Methods
        public Trade(string pair_in, string type_in, decimal amount_in, decimal rate_in, int id_in, bool my_order_in, uint timestamp_in)
        {
            this.Pair = pair_in;
            this.Type = type_in;
            this.Amount = amount_in;
            this.Rate = rate_in;
            this.OrderId = id_in;
            this.IsYourOrder = my_order_in;
            this.Timestamp = timestamp_in;
            return;
        }
        public Trade(JToken t)
        {
            this.Pair = t.Value<string>("pair");
            this.Type = t.Value<string>("type");
            this.Amount = t.Value<decimal>("amount");
            this.Rate = t.Value<decimal>("rate");
            this.OrderId = t.Value<int>("order_id");
            this.IsYourOrder = t.Value<bool>("is_your_order");
            this.Timestamp = t.Value<uint>("timestamp");
            return;
        }
        #endregion
	}
	public class TradeAnswer
    {
        public decimal Received { get; private set; }
        public decimal Remains { get; private set; }
        public int OrderId { get; private set; }
        public Funds funds { get; private set; }

        public TradeAnswer(JObject o)
        {
            this.Received = o.Value<decimal>("received");
            this.Remains = o.Value<decimal>("remains");
            this.OrderId = o.Value<int>("order_id");
            this.funds = new Funds(o["funds"] as JObject);
        }
    }
    public class TradeHistory
    {
        public Dictionary<int, Trade> List { get; private set; }
        public TradeHistory(JObject o)
        {
            if (o == null)
                return;
            this.List = new Dictionary<int, Trade>();
            foreach (KeyValuePair<string, JToken> pair in o)
                this.List.Add(Convert.ToInt32(pair.Key), new Trade(o[pair.Key]));
            return;
        }
    }
}
