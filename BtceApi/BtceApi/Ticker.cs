#region Using
using Newtonsoft.Json.Linq;
#endregion

namespace BtcE
{
	public class Ticker
    {
        #region Instance Members
        #region Numeric Members
        public decimal Average { get; private set; }
        public decimal Buy { get; private set; }
        public decimal High { get; private set; }
        public decimal Last { get; private set; }
        public decimal Low { get; private set; }
        public decimal Sell { get; private set; }
        public decimal Volume { get; private set; }
        public decimal VolumeCurrent { get; private set; }
        public uint ServerTime { get; private set; }
        #endregion
        #endregion
        #region Instance Methods
        public Ticker(JObject o)
        {
            if ( o == null )
                return;
            this.Average = o.Value<decimal>("avg");
            this.Buy = o.Value<decimal>("buy");
            this.High = o.Value<decimal>("high");
            this.Last = o.Value<decimal>("last");
            this.Low = o.Value<decimal>("low");
            this.Sell = o.Value<decimal>("sell");
            this.Volume = o.Value<decimal>("vol");
            this.VolumeCurrent = o.Value<decimal>("vol_cur");
            this.ServerTime = o.Value<uint>("server_time");
            return;
        }
        #endregion
    }
}
