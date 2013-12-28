#region Using
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
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
            foreach (string s in Exchanges.usd_pairs)
                this.funds.Add(s, o.Value<decimal>(s.ToLower()));
            foreach (string s in Exchanges.non_usd_pairs)
                this.funds.Add(s, o.Value<decimal>(s.ToLower()));
            this.funds.Add(Exchanges.target_pairs[1], o.Value<decimal>(Exchanges.target_pairs[1].ToLower()));
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
}
