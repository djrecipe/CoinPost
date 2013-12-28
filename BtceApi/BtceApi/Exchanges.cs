using System.Collections.Generic;

namespace BtcE
{
	public static class Exchanges
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
            Exchanges.non_wisdom_pairs = new List<string>();
            Exchanges.non_usd_pairs = new List<string>();
            Exchanges.usd_pairs = new List<string>();
            Exchanges.target_pairs = new List<string>();
            foreach (string s in System.IO.File.ReadAllText(file_path).Split('\n'))
            {
                string new_string=s.Replace("\r","");
                int index = Exchanges.Headers.IndexOf(new_string);
                if (index!=-1)
                {
                    stage = (FileStage)index;
                    continue;
                }
                switch (stage)
                {
                    case FileStage.USDPairs:
                        Exchanges.usd_pairs.Add(new_string);
                        break;
                    case FileStage.NonUSDPairs:
                        Exchanges.non_usd_pairs.Add(new_string);
                        break;
                    case FileStage.TargetPairs:
                        Exchanges.target_pairs.Add(new_string);
                        break;
                    case FileStage.NonWisdomPairs:
                        Exchanges.non_wisdom_pairs.Add(new_string);
                        break;
                    default:
                        break;
                }
            }
            return Exchanges.target_pairs.Count>0;
        }
        public static List<string> non_wisdom_pairs { get; private set; }
        public static List<string> non_usd_pairs { get; private set; }
        public static List<string> usd_pairs { get; private set; }
        public static List<string> target_pairs { get; private set; }
    }
}
