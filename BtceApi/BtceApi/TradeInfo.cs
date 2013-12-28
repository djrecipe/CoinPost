namespace BtcE
{
    public class TradeInfo
    {
        #region Instance Properties
        #region Numeric Properties
        public decimal Amount { get; private set; }
        public decimal Price { get; private set; }
        public uint Tid { get; private set; }
        #endregion
        #region Object Properties
        public System.DateTime Date { get; private set; }
        public TradeInfoType Type { get; private set; }
        #endregion
        #region String Properties
        public string Item { get; private set; }
        public string PriceCurrency { get; private set; }
        #endregion
        #endregion
        #region Static Methods
        public static TradeInfo CreateInstance(Newtonsoft.Json.Linq.JObject o)
        {
            return new TradeInfo(o);
        }
        #endregion
        #region Instance Methods
        public TradeInfo(Newtonsoft.Json.Linq.JObject o)
        {
            this.Amount = o.Value<decimal>("amount");
            this.Price = o.Value<decimal>("price");
            this.Date = UnixTime.ConvertToDateTime(o.Value<uint>("date"));
            this.Item = o.Value<string>("item");
            this.PriceCurrency = o.Value<string>("price_currency");
            this.Tid = o.Value<uint>("tid");
            this.Type = TradeInfoTypeHelper.FromString(o.Value<string>("trade_type"));
            return;
        }
        #endregion
    }
}
