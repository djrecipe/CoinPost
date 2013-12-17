using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BtcE
{
	public class Trade
	{
		public BtcePair Pair { get; private set; }
		public TradeType Type { get; private set; }
		public decimal Amount { get; private set; }
		public decimal Rate { get; private set; }
		public int OrderId { get; private set; }
		public bool IsYourOrder { get; private set; }
		public UInt32 Timestamp { get; private set; }
        public Trade(string pair_in, string type_in, decimal amount_in, decimal rate_in, int id_in, bool my_order_in, uint timestamp_in)
        {
            this.Pair = BtcePairHelper.FromString(pair_in);
            this.Type = TradeTypeHelper.FromString(type_in);
            this.Amount = amount_in;
            this.Rate = rate_in;
            this.OrderId = id_in;
            this.IsYourOrder = my_order_in;
            this.Timestamp = timestamp_in;
        }
        public Trade(JToken t)
        {
            this.Pair = BtcePairHelper.FromString(t.Value<string>("pair"));
            this.Type = TradeTypeHelper.FromString(t.Value<string>("type"));
            this.Amount = t.Value<decimal>("amount");
            this.Rate = t.Value<decimal>("rate");
            this.OrderId = t.Value<int>("order_id");
            this.IsYourOrder = t.Value<bool>("is_your_order");
            this.Timestamp = t.Value<UInt32>("timestamp");
        }
	}
	public class TradeHistory
	{
		public Dictionary<int, Trade> List { get; private set; }
        public TradeHistory()
        {
            this.List = new Dictionary<int, Trade>();
        }
		public static TradeHistory ReadFromJObject(JObject o)
        {
            TradeHistory retval = new TradeHistory();
            foreach (KeyValuePair<string, JToken> pair in o)
                retval.List.Add(Convert.ToInt32(pair.Key), new Trade(o[pair.Key]));
            return retval;
		}
	}
}
