﻿
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using BtcE;
using Microsoft.Win32;
using Gecko;
using System.Net;

namespace CoinPost
{
    public partial class formMain : Form
    {
        #region formMain Members
        #region Constants
        private static readonly double MinimumSellThreshold=1.004012032080192;
        private static readonly double MinimumBuyThreshold = 0.996004;
        private static readonly string replace_jscript = "<script src=\"/js/main_b.js";//?1387478040\" type=\"text/javascript\"></script>";
        private static readonly string end_jscript="</script>";
        #endregion
        #region Mutices
        private static Mutex mutExchangeString = new Mutex();           // to access current currency exchange string (i.e. "ltc_btc")
        private static Mutex mutTradeHistory = new Mutex();             // determines whether or not trade history should be shown
        private static Mutex mutValid = new Mutex();                    // for halting information retrieval thread
        #endregion
        #region Callback Delegates
        private delegate void delDecimal(Decimal? decimal_in);
        public delegate void delEmpty();
        private delegate void delOrderList(OrderList info_in);
        private delegate void delTradeHistory(TradeHistory history_in);
        private delegate void delUserInfo(UserInfo info_in);

        private formMain.delDecimal cbUpdateLastPrice;                   // callback for updating current price
        private formMain.delOrderList cbUpdateOrderList;                 // callback for updating active order list    
        private formMain.delTradeHistory cbUpdateTradeHistory;           // callback for updating trade history
        private formMain.delUserInfo cbUpdateUserInfo;                   // callback for updating balance info
        #endregion
        #region Threading
        private Thread threadInfo;                                      // primary information retrieval thread
        #endregion
        #region BtceApi
        private BtceApi btceApi;                                // primary object for api interaction
        private List<PendingTrade> pendingTrades;               // list of trades to-be-made (used for cancel & reorder)
        private Dictionary<int, Trade> recentPurchases;
        private string current_exchange;                        // current exchange string (see mutExchangeString)
        #endregion
        #region Flags
        private bool exchange_changed;                                  // indicated whether or not our exchange currency has changed
        private bool get_trade_history;
        private bool navigating;
        private bool valid;                                             // halts information retrieval thread
        public bool is_valid { get; private set; }
        #endregion
        #region Forms
        formTradeHistory fTradeHistory;
        #endregion
        #region Strings
        private string old_price_text="0.0";
        private string old_quantity_text = "0.0";
        private string new_javascript = "";
        #endregion
        #region Numbers
        private Point tabPoint = new Point(0,0);
        #endregion
        #endregion
        #region formMain Methods
        #region Initialization Methods
        private void InitializeBrowser()
        {
            
        }
        private bool InitializeBtceApi()
        {
            string curr_api_key = null, curr_api_secret = null;
            if (File.Exists("CoinPost.key"))
            {
                //formCredentials fLogin = new formCredentials(false);
                //if (fLogin.ShowDialog() != DialogResult.OK || fLogin.APIPassword == null)
                //    return false;
                string encrypted_text = Convert.ToBase64String(File.ReadAllBytes("CoinPost.key"));
                string decrypted_text = Crypto.SimpleDecryptWithPassword(encrypted_text, "0Mn1c1a17!!!");//fLogin.APIPassword);
                if(decrypted_text==null || !decrypted_text.Contains("encrypted"))
                    return false;
                string[] text_elements = decrypted_text.Split('|');
                curr_api_key = text_elements[1];
                curr_api_secret = text_elements[2];
            }
            else
            {
                formCredentials fLogin = new formCredentials();
                if (fLogin.ShowDialog()==DialogResult.OK && fLogin.APIKey!=null && fLogin.APISecret!=null)
                {
                    FileStream fstream = File.Create("CoinPost.key");
                    string output = "encrypted|"+fLogin.APIKey + "|" + fLogin.APISecret;
                    curr_api_key = fLogin.APIKey;
                    curr_api_secret = fLogin.APISecret;
                    output = Crypto.SimpleEncryptWithPassword(output, fLogin.APIPassword);
                    if (output == null)
                    {
                        fstream.Close();
                        File.Delete("CoinPost.key");
                        return false;
                    }
                    byte[] bytes = Convert.FromBase64String(output);
                    fstream.Write(bytes, 0, bytes.Length);
                    fstream.Close();
                }
                else
                    return false;
            }
            this.btceApi = new BtceApi(curr_api_key, curr_api_secret);
            this.pendingTrades = new List<PendingTrade>();
            this.recentPurchases = new Dictionary<int, Trade>();
            return true;
        }
        public formMain()
        {
            this.InitializeComponent();
            Xpcom.Initialize(@".\xulrunner");
            
            #region Child Form Initialization
            this.fTradeHistory = new formTradeHistory(new delEmpty(this.SafeToggleTradeHistory));
            #endregion
            #region Callback Delegate Initialization
            this.cbUpdateUserInfo = new delUserInfo(this.UpdateUserInfo);
            this.cbUpdateOrderList = new delOrderList(this.UpdateOrderList);
            this.cbUpdateLastPrice = new delDecimal(this.UpdateLastPrice);
            this.cbUpdateTradeHistory = new delTradeHistory(this.UpdateTradeHistory);
            #endregion
            #region Flag Initialization
            this.exchange_changed = true;
            this.get_trade_history = false;
            this.navigating = false;
            this.valid = true;
            #endregion
            #region Combobox Initialization
            this.comboSourceCurrency.SelectedIndex = 0;
            this.comboTargetCurrency.SelectedIndex = 1;
            #endregion
            #region Balance Grid Initialization
            this.gridBalances.Rows.Clear();
            this.gridBalances.Rows.Add(8);
            this.gridBalances.Rows[0].Cells[0].Value = "BTC";
            this.gridBalances.Rows[1].Cells[0].Value = "FTC";
            this.gridBalances.Rows[2].Cells[0].Value = "LTC";
            this.gridBalances.Rows[3].Cells[0].Value = "NMC";
            this.gridBalances.Rows[4].Cells[0].Value = "NVC";
            this.gridBalances.Rows[5].Cells[0].Value = "PPC";
            this.gridBalances.Rows[6].Cells[0].Value = "TRC";
            this.gridBalances.Rows[7].Cells[0].Value = "USD";
            #endregion
            #region Current Orders Grid Initialization
            this.gridSell.Rows.Clear();
            #endregion
            #region BtceApi Initialization
            if (this.InitializeBtceApi())
            {
                #region Threading Initialization
                this.threadInfo = new Thread(new ThreadStart(GetInfo));
                this.threadInfo.Name = "CoinPost_GetInfo";
                #endregion
                this.is_valid = true;
            }
            else
                this.is_valid = false;
            #endregion
            this.InitializeBrowser();
        }
        #endregion
        #region GridView Methods
        private int gridBalances_FindIndexOfPair(string source_in)
        {
            foreach (DataGridViewRow row in this.gridBalances.Rows)
            {
                if (row.Cells[0].Value.ToString().ToLower() == source_in.ToLower())
                    return row.Index;
            }
            return -1;

        }
        #endregion
        #region Information Retrieval / Multithreading
        private string SafeRetrieveExchangeString()
        {
            mutExchangeString.WaitOne();
            string retval = this.current_exchange;
            mutExchangeString.ReleaseMutex();
            return retval;
        }
        private void SafeUpdateExchangeString()
        {
            if (this.comboSourceCurrency.SelectedIndex > -1 && this.comboTargetCurrency.SelectedIndex > -1)
            {
                mutExchangeString.WaitOne();
                this.current_exchange = this.comboSourceCurrency.SelectedItem.ToString() + "_" + this.comboTargetCurrency.SelectedItem.ToString();
                this.exchange_changed = true;
                mutExchangeString.ReleaseMutex();
                if (this.webBrowser.Created && (this.comboSourceCurrency.SelectedItem.ToString() == "PPC" || this.comboSourceCurrency.SelectedItem.ToString() == "NVC" || this.comboSourceCurrency.SelectedItem.ToString() == "TRC" || this.comboSourceCurrency.SelectedItem.ToString() == "FTC"))
                {
                    this.navigating = true;
                    this.webBrowser.Navigate("https://btc-e.com/exchange/" + this.comboSourceCurrency.SelectedItem.ToString().ToLower() + "_" + this.comboTargetCurrency.SelectedItem.ToString().ToLower());
                }
                else if (this.webBrowser.Created)
                {
                    this.navigating = true;
                    this.webBrowser.Navigate("bitcoinwisdom.com/markets/btce/" + this.comboSourceCurrency.SelectedItem.ToString().ToLower() + this.comboTargetCurrency.SelectedItem.ToString().ToLower());
                }

            }
        }
        private void SafeToggleTradeHistory()
        {
            bool new_val = this.get_trade_history = !this.get_trade_history;
            mutTradeHistory.WaitOne();
            if (new_val)
                this.fTradeHistory.Show();
            else
                this.fTradeHistory.Hide();
            mutTradeHistory.ReleaseMutex();
            this.lklblShowAllHistory.Text = new_val ? "HIDE Trade History" : "SHOW Trade History";
            return;
        }
        private void GetInfo()
        {
            while (mutValid.WaitOne() && this.valid)
            {
               
                this.BeginInvoke(this.cbUpdateUserInfo,this.btceApi.GetInfo());
                mutTradeHistory.WaitOne();
                this.BeginInvoke(this.cbUpdateTradeHistory, this.btceApi.GetTradeHistory());
                mutTradeHistory.ReleaseMutex();
                this.BeginInvoke(this.cbUpdateOrderList, this.btceApi.GetOrderList());
                this.BeginInvoke(this.cbUpdateLastPrice, BtceApi.GetTicker(this.SafeRetrieveExchangeString()).Last);
                mutValid.ReleaseMutex();
                Thread.Sleep(1200);
            }
            mutValid.ReleaseMutex();
            return;
        }
        #endregion
        #region Update Callbacks
        private void UpdateOrderList(OrderList orders_in)
        {
            this.gridSell.Rows.Clear();
            this.gridBuy.Rows.Clear();
            if (orders_in == null)
                return;
            foreach (KeyValuePair<int, Order> pair in orders_in.List)
            {
                string[] units = pair.Value.Pair.ToString().Split('_');
                ((pair.Value.Type.ToString() == "Sell")?this.gridSell:this.gridBuy).Rows.Add(new object[]
                { 
                    pair.Key, pair.Value.Amount.ToString() + " " + units[0].ToUpper(), pair.Value.Rate.ToString() + " " + units[1].ToUpper(),
                    (pair.Value.Amount * pair.Value.Rate).ToString() + " " + units[1].ToUpper(),
                    "X",
                    "M"
                });
            }
        }
        private void UpdateUserInfo(UserInfo info_in)
        {
            if (info_in == null)
                return;
            this.gridBalances.Rows[this.gridBalances_FindIndexOfPair("btc")].Cells[1].Value = info_in.Funds.Btc;
            this.gridBalances.Rows[this.gridBalances_FindIndexOfPair("ftc")].Cells[1].Value = info_in.Funds.Ftc;
            this.gridBalances.Rows[this.gridBalances_FindIndexOfPair("ltc")].Cells[1].Value = info_in.Funds.Ltc;
            this.gridBalances.Rows[this.gridBalances_FindIndexOfPair("nmc")].Cells[1].Value = info_in.Funds.Nmc;
            this.gridBalances.Rows[this.gridBalances_FindIndexOfPair("nvc")].Cells[1].Value = info_in.Funds.Nvc;
            this.gridBalances.Rows[this.gridBalances_FindIndexOfPair("ppc")].Cells[1].Value = info_in.Funds.Ppc;
            this.gridBalances.Rows[this.gridBalances_FindIndexOfPair("trc")].Cells[1].Value = info_in.Funds.Trc;
            this.gridBalances.Rows[this.gridBalances_FindIndexOfPair("usd")].Cells[1].Value = info_in.Funds.Usd;
        }
        private void UpdateLastPrice(Decimal? price_in)
        {
            if (!price_in.HasValue)
                return;
            this.lklblLastPrice.Text = price_in.Value.ToString();
            mutExchangeString.WaitOne();
            if (Convert.ToDecimal(this.txtPrice.Text) == 0 || this.exchange_changed)
                this.txtPrice.Text = this.lklblLastPrice.Text;
            this.exchange_changed = false;
            mutExchangeString.ReleaseMutex();
        }
        private void UpdateTradeHistory(TradeHistory history_in)
        {
            foreach(KeyValuePair<int,Trade> pair in history_in.List)
            { // look for recent buys and sells of current exchange so-as to provide "most recent YOU bought/sold" prices on bottom of window
            }
            if(history_in!=null)
                this.fTradeHistory.UpdateTradeHistory(history_in);
        }
        #endregion
        #endregion
        #region formMain Events
        #region Button Events
        private void btnBuy_Click(object sender, EventArgs e)
        {
            double price_out = 0.0, quantity_out = 0.0;
            if (Double.TryParse(txtPrice.Text, out price_out) && Double.TryParse(txtQuantity.Text, out quantity_out) && price_out <= Convert.ToDouble(this.lklblLastPrice.Text)*1.1 && quantity_out != 0.0)
            {
                TradeAnswer answer = this.btceApi.Trade(this.SafeRetrieveExchangeString(), TradeType.Buy, price_out, quantity_out);
            }
        }

        private void btnSell_Click(object sender, EventArgs e)
        {
            double price_out = 0.0, quantity_out = 0.0;
            if (Double.TryParse(txtPrice.Text, out price_out) && Double.TryParse(txtQuantity.Text, out quantity_out) && price_out >= Convert.ToDouble(this.lklblLastPrice.Text)*0.9 && quantity_out != 0.0)
            {
                TradeAnswer answer = this.btceApi.Trade(this.SafeRetrieveExchangeString(), TradeType.Sell, price_out, quantity_out);
            }
        }
        private void btnMaxBuy_Click(object sender, EventArgs e)
        {
            decimal new_value = Decimal.Truncate((Convert.ToDecimal(this.gridBalances.Rows[this.gridBalances_FindIndexOfPair(this.comboTargetCurrency.SelectedItem.ToString())].Cells[1].Value) / Convert.ToDecimal(this.txtPrice.Text)) * 100000000) / 100000000;
            this.txtQuantity.Text = new_value.ToString();
        }
        private void btnMaxSell_Click(object sender, EventArgs e)
        {
            this.txtQuantity.Text = (Decimal.Truncate(Convert.ToDecimal(this.gridBalances.Rows[this.gridBalances_FindIndexOfPair(this.comboSourceCurrency.SelectedItem.ToString())].Cells[1].Value.ToString()) * 100000000) / 100000000).ToString();
        }
        #endregion
        #region Context Menu Events
        private void conitemRemoveTab_Click(object sender, EventArgs e)
        {
            if (this.tabsMain.SelectedIndex == 0)
                return;
            TabPage delete_me = this.tabsMain.SelectedTab;
            this.tabsMain.SelectedIndex = 0;
            this.tabsMain.TabPages.Remove(delete_me);
        }
        #endregion
        #region ComboBox Events
        private void comboBidCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox caller = (ComboBox)sender;
            if (caller.SelectedIndex != -1 && (caller.SelectedItem.ToString() == "FTC" || caller.SelectedItem.ToString() == "TRC"))
            {
                this.comboTargetCurrency.SelectedIndex = 0;
                if(this.comboTargetCurrency.Items.Contains("USD"))
                    this.comboTargetCurrency.Items.Remove("USD");
            }
            else if(!this.comboTargetCurrency.Items.Contains("USD"))
                this.comboTargetCurrency.Items.Add("USD");
            if (caller.SelectedIndex == 0 && this.comboTargetCurrency.SelectedIndex == 0)
                this.comboTargetCurrency.SelectedIndex = 1;
            this.SafeUpdateExchangeString();
            this.webBrowser.Focus();
            return;
        }
        private void comboTargetCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox caller = (ComboBox)sender;
            if (caller.SelectedIndex == 0 && this.comboSourceCurrency.SelectedIndex == 0)
                this.comboSourceCurrency.SelectedIndex = 1;
            else if (caller.SelectedIndex == 1 && this.comboSourceCurrency.SelectedIndex != -1 && (this.comboSourceCurrency.SelectedItem.ToString() == "FTC" || this.comboSourceCurrency.SelectedItem.ToString() == "TRC"))
                caller.SelectedIndex = 0;
            this.SafeUpdateExchangeString();
            this.webBrowser.Focus();
            return;
        }
        #endregion
        #region Form Events
        private void formMain_Load(object sender, EventArgs e)
        {
            if (!this.threadInfo.IsAlive)
                this.threadInfo.Start();
            if (this.comboSourceCurrency.SelectedItem.ToString() == "PPC" || this.comboSourceCurrency.SelectedItem.ToString() == "NVC" || this.comboSourceCurrency.SelectedItem.ToString() == "TRC" || this.comboSourceCurrency.SelectedItem.ToString() == "FTC")
            {
                this.navigating = true;
                this.webBrowser.Navigate("https://btc-e.com/exchange/" + this.comboSourceCurrency.SelectedItem.ToString().ToLower() + "_" + this.comboTargetCurrency.SelectedItem.ToString().ToLower());
            }
            else
            {
                this.navigating = true;
                this.webBrowser.Navigate("bitcoinwisdom.com/markets/btce/" + this.comboSourceCurrency.SelectedItem.ToString().ToLower() + this.comboTargetCurrency.SelectedItem.ToString().ToLower());
            }
        }

        private void formMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            mutValid.WaitOne();
            this.valid = false;
            mutValid.ReleaseMutex();
        }
        #endregion
        #region GridView Events
        private void gridBalances_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                if (this.comboTargetCurrency.SelectedItem.ToString() == this.gridBalances.Rows[e.RowIndex].Cells[0].Value.ToString())
                {
                    decimal new_value = Decimal.Truncate((Convert.ToDecimal(this.gridBalances.Rows[this.gridBalances_FindIndexOfPair(this.comboTargetCurrency.SelectedItem.ToString())].Cells[1].Value) / Convert.ToDecimal(this.txtPrice.Text)) * 100000000) / 100000000;
                    this.txtQuantity.Text = new_value.ToString();
                }
                else
                {
                    this.comboSourceCurrency.SelectedItem = this.gridBalances.Rows[e.RowIndex].Cells[0].Value.ToString();
                    this.txtQuantity.Text = (Decimal.Truncate(Convert.ToDecimal(this.gridBalances.Rows[e.RowIndex].Cells[1].Value) * 100000000) / 100000000).ToString();
                }
            }
        }
        private void gridBalances_SelectionChanged(object sender, EventArgs e)
        {
            this.gridBalances.ClearSelection();
        }
        private void gridBuySell_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView caller = (DataGridView)sender;
            if (e.ColumnIndex == 4)
            {
                CancelOrderAnswer answer = this.btceApi.CancelOrder(Convert.ToInt32(caller.Rows[e.RowIndex].Cells[0].Value));
                caller.Rows.RemoveAt(e.RowIndex);
            }
            if (e.ColumnIndex == 5)
            {
                double initial_price_out = Convert.ToDouble(caller.Rows[e.RowIndex].Cells[2].Value.ToString().Split(' ')[0]);
                double initial_quantity_out = Convert.ToDouble(caller.Rows[e.RowIndex].Cells[1].Value.ToString().Split(' ')[0]);
                string exchange_string_out = caller.Rows[e.RowIndex].Cells[1].Value.ToString().Split(' ')[1] + "_" + caller.Rows[e.RowIndex].Cells[2].Value.ToString().Split(' ')[1];
                formModifyOrder fModify = new formModifyOrder(initial_price_out, initial_quantity_out, Convert.ToDouble(this.gridBalances.Rows[this.gridBalances_FindIndexOfPair(caller.Rows[e.RowIndex].Cells[2].Value.ToString().Split(' ')[1])].Cells[1].Value), exchange_string_out, caller == this.gridBuy);
                fModify.ShowDialog();
                if (fModify.Price.HasValue && fModify.Quantity.HasValue)
                {
                    if (fModify.Price.Value != initial_price_out || fModify.Quantity.Value != initial_quantity_out)
                    {
                        this.btceApi.CancelOrder(Convert.ToInt32(caller.Rows[e.RowIndex].Cells[0].Value));
                        this.pendingTrades.Add(new PendingTrade(exchange_string_out, caller == this.gridBuy ? TradeType.Buy : TradeType.Sell, fModify.Price.Value, fModify.Quantity.Value));
                        this.timerModifyOrder.Enabled = true;
                    }
                }
            }
        }
        #endregion
        #region Link-Label Event Methods
        private void lklblLastPrice_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.txtPrice.Text = ((LinkLabel)sender).Text;
        }
        private void lklblShowAllHistory_Click(object sender, EventArgs e)
        {
            this.SafeToggleTradeHistory();
        }
        #endregion
        #region TabControl Events
        private void tabsMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl caller = (TabControl)sender;
            if (caller.SelectedIndex == 0)
            {
                caller.ContextMenuStrip = null;
            }
            else if (caller.ContextMenuStrip == null)
                caller.ContextMenuStrip = this.conmenTabs;
        }
        #endregion
        #region TextBox Events
        private void txtQuantity_Update(object sender, EventArgs e)
        {
            TextBox caller = (TextBox)sender;
            string temp = caller.Text;
            if (temp.Length == 0)
                return;
            if (temp.Last() == '.')
                temp += "0";
            decimal value = 0;
            if (decimal.TryParse(temp, out value))
            {
                if (caller.Text.Last() != '.')
                    caller.Text = (Decimal.Truncate(value * 1000000) / 1000000).ToString();
            }
            else
                caller.Text = old_quantity_text;
            old_quantity_text = caller.Text;
            caller.SelectionStart = caller.Text.Length;
            this.UpdateTotal();
        }
        private void txtPrice_Update(object sender, EventArgs e)
        {
            TextBox caller = (TextBox)sender;
            string temp = caller.Text;
            if (temp.Length == 0)
                return;
            if (temp.Last() == '.')
                temp += "0";
            decimal value = 0;
            if (decimal.TryParse(temp, out value))
            {
                if (caller.Text.Last() != '.')
                    caller.Text = (Decimal.Truncate(value * 1000000) / 1000000).ToString();
            }
            else
                caller.Text = old_price_text;
            old_price_text = caller.Text;
            caller.SelectionStart = caller.Text.Length;
            this.UpdateTotal();
        }
        private void UpdateTotal()
        {
            double? new_total = Convert.ToDouble(this.txtPrice.Text) * Convert.ToDouble(this.txtQuantity.Text) * 0.998;
            this.txtTotal.Text = new_total.ToString() + " " + comboTargetCurrency.SelectedItem.ToString();
            this.ttipOrderAssist.SetToolTip(this.btnBuy, "0.0%: " + Math.Round(Convert.ToDouble(this.txtPrice.Text) * formMain.MinimumSellThreshold,6).ToString() + " " + this.comboTargetCurrency.SelectedItem.ToString() +
                                            "\n0.5%: " + Math.Round(Convert.ToDouble(this.txtPrice.Text) * formMain.MinimumSellThreshold * 1.005, 6).ToString() + " " + this.comboTargetCurrency.SelectedItem.ToString() +
                                            "\n1.0%: " + Math.Round(Convert.ToDouble(this.txtPrice.Text) * formMain.MinimumSellThreshold * 1.01, 6).ToString() + " " + this.comboTargetCurrency.SelectedItem.ToString() +
                                            "\n2.0%: " + Math.Round(Convert.ToDouble(this.txtPrice.Text) * formMain.MinimumSellThreshold * 1.02, 6).ToString() + " " + this.comboTargetCurrency.SelectedItem.ToString());
            this.ttipOrderAssist.SetToolTip(this.btnSell, "0.0%: " + Math.Round(Convert.ToDouble(this.txtPrice.Text) * formMain.MinimumBuyThreshold).ToString() + " " + this.comboTargetCurrency.SelectedItem.ToString() +
                                            "\n0.5%: " + Math.Round(Convert.ToDouble(this.txtPrice.Text) * formMain.MinimumBuyThreshold * 1/1.005, 6).ToString() + " " + this.comboTargetCurrency.SelectedItem.ToString() +
                                            "\n1.0%: " + Math.Round(Convert.ToDouble(this.txtPrice.Text) * formMain.MinimumBuyThreshold * 1/1.01, 6).ToString() + " " + this.comboTargetCurrency.SelectedItem.ToString() +
                                            "\n2.0%: " + Math.Round(Convert.ToDouble(this.txtPrice.Text) * formMain.MinimumBuyThreshold * 1/1.02, 6).ToString() + " " + this.comboTargetCurrency.SelectedItem.ToString());
        }
        #endregion
        #region Timer Events
        private void timerModifyOrder_Tick(object sender, EventArgs e)
        {
            TradeAnswer answer = this.btceApi.Trade(this.pendingTrades[0].exchange, this.pendingTrades[0].type, this.pendingTrades[0].price, this.pendingTrades[0].quantity);
            this.pendingTrades.RemoveAt(0);
            if (this.pendingTrades.Count < 1)
                this.timerModifyOrder.Enabled = false;
        }
        #endregion
        #region Web Browser Events
        void webBrowser_Navigating(object sender, Gecko.Events.GeckoNavigatingEventArgs e)
        {
            string[] split_string = e.Uri.AbsoluteUri.Split('/');
            int split_string_len = split_string.Length;
            if (e.Uri.AbsoluteUri == this.webBrowser.Url.AbsoluteUri || split_string_len<2)
            {
                e.Cancel = true;
                this.navigating = false;
                return;
            }
            if (!this.navigating && !e.Uri.AbsoluteUri.Contains("about:blank"))
            {
                string tab_name = split_string[split_string_len - 2] + "/" + split_string[split_string_len - 1];
                bool tab_exists=false;
                foreach (TabPage t in this.tabsMain.TabPages)
                {
                    if (t.Text == tab_name)
                    {
                        this.tabsMain.SelectedTab = t;
                        tab_exists = true;
                        break;
                    }
                }
                if (!tab_exists)
                {
                    this.tabsMain.TabPages.Add("???");
                    Gecko.GeckoWebBrowser newBrowser = new GeckoWebBrowser();
                    newBrowser.Dock = DockStyle.Fill;
                    newBrowser.Navigate(e.Uri.AbsoluteUri);
                    newBrowser.Navigating += this.otherBrowsers_Navigating;
                    this.tabsMain.TabPages[this.tabsMain.TabPages.Count - 1].Text = tab_name;
                    this.tabsMain.TabPages[this.tabsMain.TabPages.Count - 1].Controls.Add(newBrowser);
                    this.tabsMain.SelectedIndex = this.tabsMain.TabPages.Count - 1;
                }
                e.Cancel = true;
            }
            this.navigating = false;
            return;
        }
        void otherBrowsers_Navigating(object sender, Gecko.Events.GeckoNavigatingEventArgs e)
        {
            GeckoWebBrowser caller = (GeckoWebBrowser)sender;
            string[] split_string = e.Uri.AbsoluteUri.Split('/');
            int split_string_len = split_string.Length;
            e.Cancel = true;
            if (e.Uri.AbsoluteUri == caller.Url.AbsoluteUri  || e.Uri.AbsoluteUri.Contains("about:blank") || split_string_len < 2)
                return;
            string source_currency = split_string[split_string_len - 1].Substring(0, 3).ToUpper();
            string target_currency = split_string[split_string_len - 1].Substring(3, 3).ToUpper();
            if (split_string[split_string_len - 2]=="btce" && source_currency == this.comboSourceCurrency.SelectedItem.ToString() && target_currency == this.comboTargetCurrency.SelectedItem.ToString())
            {
                this.tabsMain.SelectedIndex = 0;
                return;
            }
            string tab_name = split_string[split_string_len - 2] + "/" + split_string[split_string_len - 1];
            foreach (TabPage t in this.tabsMain.TabPages)
            {
                if (t.Text == tab_name)
                {
                    this.tabsMain.SelectedTab = t;
                    return;
                }
            }
            this.tabsMain.TabPages.Add("???");
            Gecko.GeckoWebBrowser newBrowser = new GeckoWebBrowser();
            newBrowser.Dock = DockStyle.Fill;
            newBrowser.Navigate(e.Uri.AbsoluteUri);
            newBrowser.Navigating += this.otherBrowsers_Navigating;
            this.tabsMain.TabPages[this.tabsMain.TabPages.Count - 1].Text = tab_name;
            this.tabsMain.TabPages[this.tabsMain.TabPages.Count - 1].Controls.Add(newBrowser);
            this.tabsMain.SelectedIndex = this.tabsMain.TabPages.Count - 1;
        }
        private void webBrowser_CreateWindow(object sender, GeckoCreateWindowEventArgs e)
        {

        }
        #endregion
        #endregion

    }
}
