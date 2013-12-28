#region Using
using Newtonsoft.Json.Linq;
#endregion
namespace BtcE
{
    public class UserInfo
    {
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
        public UserInfo(JObject o)
        {
            // ~ Initialize Object Properties
            this.funds = new Funds(o["funds"] as JObject);
            this.rights = new Rights(o["rights"] as JObject);
            // ~ Initialize Numeric Properties
            this.OpenOrders = o.Value<int>("open_orders");
            this.ServerTime = o.Value<int>("server_time");
            this.TransactionCount = o.Value<int>("transaction_count");
            return;
        }
        #endregion
        #endregion
    }
}
