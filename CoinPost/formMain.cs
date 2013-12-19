
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using BtcE;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace CoinPost
{
    public partial class formMain : Form
    {
        #region formMain Members
        #region Constants
        private static readonly double MinimumSellThreshold=1.004012032080192;
        private static readonly double MinimumBuyThreshold = 0.996004;
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
        private bool valid;                                             // halts information retrieval thread
        public bool is_valid { get; private set; }
        #endregion
        #region Forms
        formTradeHistory fTradeHistory;
        #endregion
        #endregion
        #region formMain Methods
        #region Initialization Methods
        private bool InitBtceApi()
        {
            string curr_api_key = null, curr_api_secret = null;
            if (File.Exists("CoinPost.key"))
            {
                File.Decrypt("CoinPost.key");
                string[] encrypted_text = File.ReadAllText("CoinPost.key").Split('|');
                File.Encrypt("CoinPost.key");
                Regex rgx = new Regex("[^a-zA-Z0-9 -]");
                curr_api_key = rgx.Replace(encrypted_text[0], "");
                curr_api_secret = rgx.Replace(encrypted_text[1], "");
            }
            else
            {
                formCredentials fLogin = new formCredentials();
                fLogin.ShowDialog();
                if (fLogin.APIKey!=null && fLogin.APISecret!=null)
                {
                    FileStream fstream = File.Create("CoinPost.key");
                    string output = fLogin.APIKey + "|" + fLogin.APISecret;
                    curr_api_key = fLogin.APIKey;
                    curr_api_secret = fLogin.APISecret;
                    byte[] bytes = new byte[output.Length * sizeof(char)];
                    System.Buffer.BlockCopy(output.ToCharArray(), 0, bytes, 0, bytes.Length);
                    fstream.Write(bytes, 0, bytes.Length);
                    fstream.Close();
                    File.Encrypt("CoinPost.key");
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
            Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Internet Explorer\\Main\\Feature Control\\FEATURE_BROWSER_EMULATION", "CoinPost.exe", 0x2af9, RegistryValueKind.DWord);
            Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\FEATURE_BROWSER_EMULATION", "CoinPost.vshost.exe", 0x2af9, RegistryValueKind.DWord);
            this.InitializeComponent();
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
            if (this.InitBtceApi())
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
                if (this.comboSourceCurrency.SelectedItem.ToString() == "PPC" || this.comboSourceCurrency.SelectedItem.ToString() == "NVC" || this.comboSourceCurrency.SelectedItem.ToString() == "TRC" || this.comboSourceCurrency.SelectedItem.ToString() == "FTC")
                    this.webBrowser.Url = new System.Uri("https://btc-e.com/exchange/" + this.comboSourceCurrency.SelectedItem.ToString().ToLower() + "_" + this.comboTargetCurrency.SelectedItem.ToString().ToLower());
                else
                    this.webBrowser.Url = new System.Uri("http://bitcoinwisdom.com/markets/btce/" + this.comboSourceCurrency.SelectedItem.ToString() + this.comboTargetCurrency.SelectedItem.ToString() + ":8080");
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
            {
                this.InitBtceApi();
                return;
            }
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
            {
                this.InitBtceApi();
                return;
            }
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
            return;
        }
        #endregion
        #region Form Events
        private void formMain_Load(object sender, EventArgs e)
        {
            if (!this.threadInfo.IsAlive)
                this.threadInfo.Start();
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
                formModifyOrder fModify = new formModifyOrder(initial_price_out, initial_quantity_out, exchange_string_out, caller == this.gridBuy);
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
        #region TextBox Events
        private void txtTotal_Update(object sender, EventArgs e)
        {
            TextBox caller = (TextBox)sender;
            decimal result = 0;
            if (!decimal.TryParse(caller.Text, out result))
            {
                caller.Text = "0.0";
                this.btnBuy.Enabled = false;
                this.btnMaxBuy.Enabled = false;
                this.btnSell.Enabled = false;
                this.btnMaxSell.Enabled = false;
            }
            else
            {
                result = Decimal.Truncate(result * 100000000) / 100000000;
                this.btnBuy.Enabled = true;
                this.btnMaxBuy.Enabled = true;
                this.btnSell.Enabled = true;
                this.btnMaxSell.Enabled = true;
            }
            double? new_total = null;
            if (this.txtPrice.Text == "" || this.txtQuantity.Text == "")
                this.txtTotal.Text = "";
            else
            {
                new_total = Convert.ToDouble(this.txtPrice.Text) * Convert.ToDouble(this.txtQuantity.Text)*0.998;
                this.txtTotal.Text = new_total.ToString() + " " + comboTargetCurrency.SelectedItem.ToString();
            }
            if (new_total!=null)
            {
                this.ttipOrderAssist.SetToolTip(this.btnBuy, "0.0%: " + Math.Round(Convert.ToDouble(this.txtPrice.Text) * formMain.MinimumSellThreshold,6).ToString() + " " + this.comboTargetCurrency.SelectedItem.ToString() +
                                                "\n0.5%: " + Math.Round(Convert.ToDouble(this.txtPrice.Text) * formMain.MinimumSellThreshold * 1.005, 6).ToString() + " " + this.comboTargetCurrency.SelectedItem.ToString() +
                                                "\n1.0%: " + Math.Round(Convert.ToDouble(this.txtPrice.Text) * formMain.MinimumSellThreshold * 1.01, 6).ToString() + " " + this.comboTargetCurrency.SelectedItem.ToString() +
                                                "\n2.0%: " + Math.Round(Convert.ToDouble(this.txtPrice.Text) * formMain.MinimumSellThreshold * 1.02, 6).ToString() + " " + this.comboTargetCurrency.SelectedItem.ToString());
                this.ttipOrderAssist.SetToolTip(this.btnSell, "0.0%: " + Math.Round(Convert.ToDouble(this.txtPrice.Text) * formMain.MinimumBuyThreshold).ToString() + " " + this.comboTargetCurrency.SelectedItem.ToString() +
                                                "\n0.5%: " + Math.Round(Convert.ToDouble(this.txtPrice.Text) * formMain.MinimumBuyThreshold * 1/1.005, 6).ToString() + " " + this.comboTargetCurrency.SelectedItem.ToString() +
                                                "\n1.0%: " + Math.Round(Convert.ToDouble(this.txtPrice.Text) * formMain.MinimumBuyThreshold * 1/1.01, 6).ToString() + " " + this.comboTargetCurrency.SelectedItem.ToString() +
                                                "\n2.0%: " + Math.Round(Convert.ToDouble(this.txtPrice.Text) * formMain.MinimumBuyThreshold * 1/1.02, 6).ToString() + " " + this.comboTargetCurrency.SelectedItem.ToString());
            }
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
        private void webBrowser_NewWindow(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            return;
        }
        #endregion

        #endregion

    }
}
