using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
namespace BtcE
{
	public class Order
	{
		public BtcePair Pair { get; private set; }
		public TradeType Type { get; private set; }
		public decimal Amount { get; private set; }
		public decimal Rate { get; private set; }
		public UInt32 TimestampCreated { get; private set; }
		public int Status { get; private set; }
        public Order(string pair_in, string type_in, decimal amount_in, decimal rate_in, uint timestamp_in, int status_in)
        {
            this.Pair = BtcePairHelper.FromString(pair_in);
            this.Type = TradeTypeHelper.FromString(type_in);
            this.Amount = amount_in;
            this.Rate = rate_in;
            this.TimestampCreated = timestamp_in;
            this.Status = status_in;
        }
        public Order(JToken t)
        {
            this.Pair = BtcePairHelper.FromString(t.Value<string>("pair"));
            this.Type = TradeTypeHelper.FromString(t.Value<string>("type"));
            this.Amount = t.Value<decimal>("amount");
            this.Rate = t.Value<decimal>("rate");
            this.TimestampCreated = t.Value<UInt32>("timestamp_created");
            this.Status = t.Value<int>("status");
        }
	}

	public class OrderList
	{
		public Dictionary<int, Order> List { get; private set; }
        public OrderList()
        {
            this.List = new Dictionary<int, Order>();
        }
		public static OrderList ReadFromJObject(JObject o)
        {
            OrderList retval = new OrderList();
            foreach (KeyValuePair<string, JToken> pair in o)
                retval.List.Add(Convert.ToInt32(pair.Key), new Order(o[pair.Key]));
            return retval;
		}
	}
}
