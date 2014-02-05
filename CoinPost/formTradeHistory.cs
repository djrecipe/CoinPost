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
    public partial class formTradeHistory : CoinPostGUI.Window
    {
        private System.Timers.Timer timerHighlight = new System.Timers.Timer(300);
        private formMain.delEmpty cbToggle;
        private int row_index = -1;
        private int recent_order_index = -1;
        private bool is_sell_grid = false;
        public formTradeHistory(formMain.delEmpty callback_in)
        {
            this.timerHighlight.Elapsed += timerHighlight_Elapsed;
            this.timerHighlight.Enabled = false;
            this.InitializeComponent();
            this.cbToggle = callback_in;
            this.gridBuy.ClearSelection();
            this.gridSell.ClearSelection();
            return;
        }

        void timerHighlight_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.timerHighlight.Enabled = false;
            DataGridViewRow row = (this.is_sell_grid?this.gridSell:this.gridBuy).Rows[this.row_index];
            string pair0 = row.Cells[2].Value.ToString().Split(' ')[1], pair1 = row.Cells[3].Value.ToString().Split(' ')[1];
            double price = Convert.ToDouble(row.Cells[3].Value.ToString().Split(' ')[0]);
            int order_id = Convert.ToInt32(row.Cells[0].Value);
            //Graphics.Suspend(this.is_sell_grid ? this.gridBuy : this.gridSell);
            foreach (DataGridViewRow r in (this.is_sell_grid?this.gridBuy.Rows:this.gridSell.Rows))
            {
                if (Convert.ToInt32(r.Cells[0].Value) > order_id && pair0 == r.Cells[2].Value.ToString().Split(' ')[1] && pair1 == r.Cells[3].Value.ToString().Split(' ')[1])
                {
                    double new_price = Convert.ToDouble(r.Cells[3].Value.ToString().Split(' ')[0]);
                    foreach (DataGridViewCell cell in r.Cells)
                    {
                        bool good_price = ((price * 0.998 >= new_price && this.is_sell_grid) || (price * 0.998 < new_price && !this.is_sell_grid));
                        cell.Style.BackColor = Color.FromArgb(!good_price ? 15 : 0, good_price ? 15 : 0, 0);
                        cell.Style.ForeColor = cell.ColumnIndex == 3 ? Color.FromArgb(122, 224, 122) : Color.FromArgb(0, 174, 0);
                    }
                }
                else if (r.Cells[0].Style.BackColor.B != 10)
                {
                    foreach (DataGridViewCell cell in r.Cells)
                    {
                        cell.Style.BackColor = Color.FromArgb(10, 10, 10);
                        cell.Style.ForeColor = cell.ColumnIndex == 3 ? Color.FromArgb(102, 204, 102) : Color.DarkGreen;
                    }
                }
            }
            //Graphics.Resume(this.is_sell_grid ? this.gridBuy : this.gridSell);
            return;
        }
        private void HighlightOppositeRows(bool target_sell)
        {

        }
        public void UpdateTradeHistory(BtcE.TradeHistory orders_in)
        {
            if (orders_in == null)
                return;
            if(orders_in.List.Keys.First()>this.recent_order_index)
            {
                this.recent_order_index = orders_in.List.Keys.First();
                DataGridViewSelectedRowCollection rows = this.gridSell.SelectedRows;
                string order_id = null;
                bool select_sell = true;
                if (rows == null || rows.Count < 1)
                {
                    select_sell = false;
                    rows = this.gridBuy.SelectedRows;
                }
                if(rows!=null && rows.Count>0)
                    order_id = rows[0].Cells[0].Value.ToString();
                this.gridSell.Rows.Clear();
                this.gridBuy.Rows.Clear();
                foreach (KeyValuePair<int, BtcE.Trade> pair in orders_in.List)
                {
                    DateTime dt = new DateTime(1970,1,1,0,0,0).AddSeconds(pair.Value.Timestamp).ToLocalTime();
                    string[] units = pair.Value.Pair.ToString().Split('_');
                    (pair.Value.Type.ToString().ToLower().Contains("sell") ? this.gridSell : this.gridBuy).Rows.Add(new object[]
                { 
                    pair.Key, dt.ToString(),pair.Value.Amount.ToString() + " " + units[0].ToUpper(),
                    pair.Value.Rate.ToString() + " " + units[1].ToUpper(),Math.Round(pair.Value.Amount * pair.Value.Rate * Convert.ToDecimal(0.998),3).ToString() + " " + units[1].ToUpper()
                });
                }
                this.gridSell.ClearSelection();
                this.gridBuy.ClearSelection();
                if(order_id!=null)
                {
                    DataGridViewRowCollection r = (select_sell ? this.gridSell : this.gridBuy).Rows;
                    for(int i=0; i<r.Count; i++)
                    {
                        if(r[i].Cells[0].Value.ToString()==order_id)
                        {
                            r[i].Selected=true;
                            break;
                        }
                    }
                }
            }
            return;
        }

        private void formTradeHistory_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Invoke(this.cbToggle);
            e.Cancel = true;
        }

        private void gridBuy_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;
            if (e.RowIndex != this.row_index || this.is_sell_grid)
            {
                this.timerHighlight.Stop();
                this.timerHighlight.Start();
                this.row_index = e.RowIndex;
                this.is_sell_grid = false;
            }
            return;
        }

        private void gridSell_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;
            if (e.RowIndex != this.row_index || !this.is_sell_grid)
            {
                this.timerHighlight.Stop();
                this.timerHighlight.Start();
                this.row_index = e.RowIndex;
                this.is_sell_grid = true;
            }
            return;
        }

        private void gridSell_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.gridBuy.ClearSelection();
            return;
        }

        private void gridBuy_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.gridSell.ClearSelection();
            return;
        }
        private void gridSell_MouseEnter(object sender, EventArgs e)
        {
            DataGridView caller = (DataGridView)sender;
            foreach (DataGridViewRow r in caller.Rows)
            {
                foreach (DataGridViewCell cell in r.Cells)
                {
                    cell.Style.BackColor = Color.FromArgb(10, 10, 10);
                    cell.Style.ForeColor = cell.ColumnIndex == 3 ? Color.FromArgb(102, 204, 102) : Color.DarkGreen;
                }
            }
            caller.Focus();
            return;
        }

        private void gridBuy_MouseEnter(object sender, EventArgs e)
        {
            DataGridView caller = (DataGridView)sender;
            foreach (DataGridViewRow r in caller.Rows)
            {
                foreach (DataGridViewCell cell in r.Cells)
                {
                    cell.Style.BackColor = Color.FromArgb(10, 10, 10);
                    cell.Style.ForeColor = cell.ColumnIndex == 3 ? Color.FromArgb(102, 204, 102) : Color.DarkGreen;
                }
            }
            caller.Focus();
            return;
        }

    }
}
