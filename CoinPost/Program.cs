using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using BtcE;

namespace CoinPost
{
    public class PendingTrade
    {
        public string exchange { get; private set; }
        public TradeType type { get; private set; }
        public double price { get; private set; }
        public double quantity { get; private set; }
        public PendingTrade(string exchange_in, TradeType type_in, double price_in, double quantity_in)
        {
            this.exchange = exchange_in;
            this.type = type_in;
            this.price = price_in;
            this.quantity = quantity_in;
        }
    }
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            formMain fMain=new formMain();
            if(fMain.is_valid)
                Application.Run(fMain);
        }
    }
}
