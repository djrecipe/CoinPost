using Newtonsoft.Json.Linq;

namespace BtcE
{
	public class CancelOrderAnswer
	{
		public int OrderId { get; private set; }
		public Funds funds { get; private set; }
        public CancelOrderAnswer(JObject o)
        {
            this.OrderId = o.Value<int>("order_id");
            this.funds = new Funds(o["funds"] as JObject);
            return;
        }
	}
}
