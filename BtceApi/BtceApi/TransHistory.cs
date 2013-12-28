#region Using
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
#endregion

namespace BtcE
{
    public class Transaction
    {
        #region Instance Members
        #region Numeric Members
        public decimal Amount { get; private set; }
        public int Status { get; private set; }
        public UInt32 Timestamp { get; private set; }
        public int Type { get; private set; }
        #endregion
        #region String Members
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
            this.Timestamp = o.Value<UInt32>("timestamp");
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
