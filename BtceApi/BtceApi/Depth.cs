using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
namespace BtcE
{
	public class OrderInfo
    {
        #region Instance Members
        public decimal Amount { get; private set; }
        public decimal Price { get; private set; }
        #endregion
        #region Instance Methods
        public OrderInfo(JArray o)
        {
            if (o == null)
                return;
            this.Price = o.Value<decimal>(0);
            this.Amount = o.Value<decimal>(1);
            return;
        }
        #endregion
    }
	public class Depth
    {
        #region Instance Members
        public List<OrderInfo> Asks { get; private set; }
        public List<OrderInfo> Bids { get; private set; }
        #endregion
        #region Instance Methods
        public Depth(JObject o)
        {
            if (o == null)
                return;
            this.Asks = o["asks"].OfType<JArray>().Select(order => new OrderInfo(order as JArray)).ToList();
            this.Bids = o["bids"].OfType<JArray>().Select(order => new OrderInfo(order as JArray)).ToList();
            return;
        }
        #endregion
    }
}
