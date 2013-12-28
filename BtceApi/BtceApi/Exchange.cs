#region Using
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
#endregion
namespace BtcE
{
    public static class Exchange
	{
        private enum FileStage : int { USDPairs, NonUSDPairs, TargetPairs, NonWisdomPairs, None };
        private static List<string> Headers = new List<string>(){"[USD]","[NON-USD]","[TARGET]","[BTC-E ONLY]"};
        public static bool Initialize(string file_path)
        {
            if (!System.IO.File.Exists(file_path))
            {
                System.Windows.Forms.MessageBox.Show("File not found: "+ file_path);
                return false;
            }
            FileStage stage = FileStage.None;
            Exchange.non_wisdom_pairs = new List<string>();
            Exchange.non_usd_pairs = new List<string>();
            Exchange.usd_pairs = new List<string>();
            Exchange.target_pairs = new List<string>();
            foreach (string s in System.IO.File.ReadAllText(file_path).Split('\n'))
            {
                string new_string=s.Replace("\r","");
                int index = Exchange.Headers.IndexOf(new_string);
                if (index!=-1)
                {
                    stage = (FileStage)index;
                    continue;
                }
                switch (stage)
                {
                    case FileStage.USDPairs:
                        Exchange.usd_pairs.Add(new_string);
                        break;
                    case FileStage.NonUSDPairs:
                        Exchange.non_usd_pairs.Add(new_string);
                        break;
                    case FileStage.TargetPairs:
                        Exchange.target_pairs.Add(new_string);
                        break;
                    case FileStage.NonWisdomPairs:
                        Exchange.non_wisdom_pairs.Add(new_string);
                        break;
                    default:
                        break;
                }
            }
            return Exchange.target_pairs.Count>0;
        }
        public static List<string> non_wisdom_pairs { get; private set; }
        public static List<string> non_usd_pairs { get; private set; }
        public static List<string> usd_pairs { get; private set; }
        public static List<string> target_pairs { get; private set; }
    }
    public class Ticker
    {
        #region Instance Properties
        #region Numeric Properties
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
            if (o == null)
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
