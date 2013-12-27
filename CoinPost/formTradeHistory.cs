using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoinPost
{
    public partial class formTradeHistory : Form
    {
        private formMain.delEmpty cbToggle;
        private int most_recent_value;
        public formTradeHistory(formMain.delEmpty callback_in)
        {
            InitializeComponent();
            this.cbToggle = callback_in;
            this.most_recent_value = -1;
        }
        public void UpdateTradeHistory(BtcE.TradeHistory orders_in)
        {
            if (orders_in == null)
                return;
            if (orders_in.List.Keys.ToArray()[0] != this.most_recent_value)
            {
                this.most_recent_value = orders_in.List.Keys.ToArray()[0];
                this.gridSell.Rows.Clear();
                this.gridBuy.Rows.Clear();
                foreach (KeyValuePair<int, BtcE.Trade> pair in orders_in.List)
                {
                    DateTime dt = new DateTime(1970,1,1,0,0,0).AddSeconds(pair.Value.Timestamp).ToLocalTime();
                    string[] units = pair.Value.Pair.ToString().Split('_');
                    ((pair.Value.Type.ToString() == "Sell") ? this.gridSell : this.gridBuy).Rows.Add(new object[]
                { 
                    pair.Key, dt.ToLongTimeString(),pair.Value.Amount.ToString() + " " + units[0].ToUpper(),
                    pair.Value.Rate.ToString() + " " + units[1].ToUpper(),(pair.Value.Amount * pair.Value.Rate * Convert.ToDecimal(0.998)).ToString() + " " + units[1].ToUpper()
                });
                }
            }
        }

        private void formTradeHistory_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Invoke(this.cbToggle);
            e.Cancel = true;
        }
    }
}
