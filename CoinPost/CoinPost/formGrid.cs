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
    public partial class formGrid : Form
    {
        private formMain caller;
        private delEmpty onClose;
        private double? last_bid;
        private double? last_ask;
        public formGrid(formMain caller_in, delEmpty callback_in)
        {
            this.onClose = callback_in;
            this.caller = caller_in;
            InitializeComponent();
        }

        private void formGrid_FormClosing(object sender, FormClosingEventArgs e)
        {
            caller.BeginInvoke(this.onClose);
        }
        public void UpdateHistory(List<List<string>> history_in)
        {
            if (history_in.Count < 1)
                return;
            while (this.dgviewSells.Rows.Count > 200) { this.dgviewSells.Rows.RemoveAt(200); }
            while (this.dgviewBuys.Rows.Count > 200) { this.dgviewBuys.Rows.RemoveAt(200); }
            foreach (DataGridViewRow row in this.dgviewSells.Rows)
            {
                row.DefaultCellStyle.BackColor = Color.White;
            }
            foreach (DataGridViewRow row in this.dgviewBuys.Rows)
            {
                row.DefaultCellStyle.BackColor = Color.White;
            }
            bool scroll_to_top = this.dgviewBuys.RowCount == 0 && this.dgviewSells.RowCount == 0;
            foreach (List<string> list in history_in)
            {
                if (list[(int)TradeStat.Action] == "bid")
                {
                    this.dgviewBuys.Rows.Insert(0, list.Take(4).ToArray());
                    double new_price = Convert.ToDouble(list[(int)TradeStat.Price]);
                    if (!last_bid.HasValue)
                        this.dgviewBuys.Rows[0].DefaultCellStyle.BackColor = Color.Yellow;
                    else if (new_price > this.last_bid.Value)
                        this.dgviewBuys.Rows[0].DefaultCellStyle.BackColor = Color.Green;
                    else if (new_price < this.last_bid.Value)
                        this.dgviewBuys.Rows[0].DefaultCellStyle.BackColor = Color.Red;
                    else
                        this.dgviewBuys.Rows[0].DefaultCellStyle.BackColor = Color.Yellow;
                }
                else
                {
                    this.dgviewSells.Rows.Insert(0, list.Take(4).ToArray());
                    double new_price = Convert.ToDouble(list[(int)TradeStat.Price]);
                    if (!last_ask.HasValue)
                        this.dgviewSells.Rows[0].DefaultCellStyle.BackColor = Color.Yellow;
                    else if (new_price > this.last_ask.Value)
                        this.dgviewSells.Rows[0].DefaultCellStyle.BackColor = Color.Green;
                    else if (new_price < this.last_ask.Value)
                        this.dgviewSells.Rows[0].DefaultCellStyle.BackColor = Color.Red;
                    else
                        this.dgviewSells.Rows[0].DefaultCellStyle.BackColor = Color.Yellow;
                }

            }
            //
            double new_buy_price=Convert.ToDouble(this.dgviewBuys.Rows[0].Cells[(int)TradeStat.Price].Value);
            double new_sell_price=Convert.ToDouble(this.dgviewSells.Rows[0].Cells[(int)TradeStat.Price].Value); 
            //
            this.lblBid.Text = "BID: " + new_buy_price.ToString();
            this.lblAsk.Text = "ASK: " + new_sell_price.ToString();
            //
            if (!last_bid.HasValue)
                this.lblBid.BackColor = Color.Yellow;
            else if (new_buy_price > this.last_bid.Value)
                this.lblBid.BackColor = Color.Green;
            else if (new_buy_price < this.last_bid.Value)
                this.lblBid.BackColor = Color.Red;
            else if (this.dgviewBuys.Rows[0].DefaultCellStyle.BackColor == Color.Yellow)
                this.lblBid.BackColor = Color.Yellow;
            else
                this.lblBid.BackColor = Color.White;
            this.last_bid = new_buy_price;
            //
            if (!last_ask.HasValue)
                this.lblAsk.BackColor = Color.Yellow;
            else if (new_sell_price > this.last_ask.Value)
                this.lblAsk.BackColor = Color.Green;
            else if (new_sell_price < this.last_ask.Value)
                this.lblAsk.BackColor = Color.Red;
            else if (this.dgviewSells.Rows[0].DefaultCellStyle.BackColor == Color.Yellow)
                this.lblAsk.BackColor = Color.Yellow;
            else
                this.lblAsk.BackColor = Color.White;
            this.last_ask = new_sell_price;

            if (scroll_to_top)
            {
                if (this.dgviewBuys.Rows.Count > 0)
                    this.dgviewBuys.FirstDisplayedScrollingRowIndex = 0;
                if (this.dgviewSells.Rows.Count > 0)
                    this.dgviewSells.FirstDisplayedScrollingRowIndex = 0;
            }
        }

        private void formGrid_Load(object sender, EventArgs e)
        {
            if (this.dgviewBuys.Rows.Count > 0)
                this.dgviewBuys.FirstDisplayedScrollingRowIndex = 0;
            if (this.dgviewSells.Rows.Count > 0)
                this.dgviewSells.FirstDisplayedScrollingRowIndex = 0;
        }
    }
}
