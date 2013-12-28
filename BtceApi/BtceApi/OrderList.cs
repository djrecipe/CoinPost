using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
namespace BtcE
{
	public class Order
	{
		public string Pair { get; private set; }
		public string Type { get; private set; }
		public decimal Amount { get; private set; }
		public decimal Rate { get; private set; }
		public uint TimestampCreated { get; private set; }
		public int Status { get; private set; }
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
            this.Pair = t.Value<string>("pair");
            this.Type = t.Value<string>("type");
            this.Amount = t.Value<decimal>("amount");
            this.Rate = t.Value<decimal>("rate");
            this.TimestampCreated = t.Value<UInt32>("timestamp_created");
            this.Status = t.Value<int>("status");
            return;
        }
	}

	public class OrderList
	{
		public Dictionary<int, Order> List { get; private set; }
        public OrderList(JObject o)
        {
            if (o == null)
                return;
            this.List = new Dictionary<int, Order>();
            foreach (KeyValuePair<string, JToken> pair in o)
                this.List.Add(Convert.ToInt32(pair.Key), new Order(o[pair.Key]));
            return;
        }
	}
}
