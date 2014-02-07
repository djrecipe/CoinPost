
#region Using
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


using BtcE;
using Gecko;
using System.Media;
#endregion

namespace CoinPost
{
    public partial class formMain : CoinPostGUI.Window
    {
        #region Type Definitions
        private delegate void delDecimal(Decimal? decimal_in);
        public delegate void delEmpty();
        private delegate void delOrderList(OrderList info_in);
        private delegate void delTradeHistory(TradeHistory history_in);
        private delegate void delUserInfo(Info info_in);
        #endregion
        #region Static Members
        #region Static Numeric Members
        private static readonly double MinimumSellThreshold = 1.004012032080192;
        private static readonly double MinimumBuyThreshold = 0.996004;
        #endregion
        #region Mutices
        private static Mutex mutExchangeString = new Mutex();           // to access current currency exchange string (i.e. "ltc_btc")
        private static Mutex mutTradeHistory = new Mutex();             // determines whether or not trade history should be shown
        private static Mutex mutValid = new Mutex();                    // for halting information retrieval thread
        #endregion
        #endregion
        #region Instance Members
        #region Control Members
        formTradeHistory fTradeHistory = null;
        #endregion
        #region Flag Members
        private bool browsers_enabled = false;
        private bool exchange_changed = false;                                  // indicated whether or not our exchange currency has changed
        private bool first_btce_page = true;
        private bool get_trade_history = false;
        private bool user_nav = false;
        private bool valid = false;                                             // halts information retrieval thread
        #endregion
        #region Numberic Members
        private List<int> canceledOrders = new List<int>();
        private Point tabPoint = new Point(0,0);
        private DateTime last_log_time=new DateTime();
        private int most_recent_trade = -1;
        private decimal recent_quantity = 0;
        private decimal recent_price = 0;
        #endregion
        #region Object Members
        private BtceApi btceApi;                                                        // primary object for api interaction
        private BtceDatabase btceDatabase;
        private List<PendingTrade> pendingTrades = new List<PendingTrade>();            // list of trades to-be-made (used for cancel & reorder)
        private OrderList recent_active_orders = null;
        private Dictionary<int, Trade> recentPurchases = new Dictionary<int,Trade>();
        #endregion
        #region String Members
        private string current_exchange = "";                                           // current exchange string (see mutExchangeString)      
        private string last_url = "";
        private string old_quantity_text = "0.0";
        private string old_price_text = "0.0";
        #endregion
        #region Threading Members
        private formMain.delDecimal cbUpdateLastPrice = null;                   // callback for updating current price
        private formMain.delOrderList cbUpdateOrderList = null;                 // callback for updating active order list    
        private formMain.delDecimal cbUpdateTotalBalance = null;
        private formMain.delTradeHistory cbUpdateTradeHistory = null;           // callback for updating trade history
        private formMain.delUserInfo cbUpdateUserInfo = null;                   // callback for updating balance info

        private Thread threadInfo = null;                                       // primary information retrieval thread
        #endregion
        #endregion
        #region Instance Properties
        #region Flag Properties
        public bool is_valid { get; private set; }
        #endregion
        #endregion
        #region Instance Methods
        #region Configuration Methods
        private void LoadWindowConfiguration()
        {
            //
            int? initial_height = (int?)Registry.GetValue(@"HKEY_CURRENT_USER\Software\CoinPost\Initialization", "WindowHeight", -1);
            int? initial_width = (int?)Registry.GetValue(@"HKEY_CURRENT_USER\Software\CoinPost\Initialization", "WindowWidth", -1);
            if (initial_height.HasValue && initial_width.HasValue && initial_height.Value > 0 && initial_width.Value > 0)
                this.Size = new Size(initial_width.Value, initial_height.Value);
            //
            int? initial_left = (int?)Registry.GetValue(@"HKEY_CURRENT_USER\Software\CoinPost\Initialization", "WindowLeft", -1);
            int? initial_top = (int?)Registry.GetValue(@"HKEY_CURRENT_USER\Software\CoinPost\Initialization", "WindowTop", -1);
            if (initial_left.HasValue && initial_top.HasValue && initial_left.Value != -1 && initial_top.Value != -1)
                this.Location = new Point(initial_left.Value, initial_top.Value);
            //
            bool maximized = Convert.ToBoolean((string)Registry.GetValue(@"HKEY_CURRENT_USER\Software\CoinPost\Initialization", "WindowMaximized", "False"));
            this.WindowState = maximized ? FormWindowState.Maximized : FormWindowState.Normal;
            this.formMain_ResizeEnd(null, null);
            return;
        }
        private void SaveWindowConfiguration()
        {
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\CoinPost\Initialization", "WindowHeight", this.Height);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\CoinPost\Initialization", "WindowWidth", this.Width);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\CoinPost\Initialization", "WindowLeft", this.Left);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\CoinPost\Initialization", "WindowTop", this.Top);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\CoinPost\Initialization", "WindowMaximized", this.WindowState==FormWindowState.Maximized?true:false);
            return;
        }
        #endregion
        #region Initialization Methods
        private void InitializeBrowser()
        {
        }
        private bool InitializeBtceApi()
        {
            string curr_api_key = null, curr_api_secret = null;
            if (File.Exists("CoinPost.key"))
            {
                string base_text = File.ReadAllText("CoinPost.key"),
                    encrypted_text =Convert.ToBase64String(File.ReadAllBytes("CoinPost.key"));
                bool already_decrypted = base_text.Contains("encrypted"),
                    save_password = Convert.ToBoolean((string)Registry.GetValue(@"HKEY_CURRENT_USER\Software\CoinPost\Initialization", "SavePassword", "False"));
                formCredentials fLogin = new formCredentials(false);
                if (!already_decrypted)
                {
                    DialogResult dlg_result = fLogin.ShowDialog();
                    if (dlg_result == DialogResult.Retry)
                    {
                        if (!this.PromptLogIn(out curr_api_key, out curr_api_secret))
                            return false;
                    }
                    else
                    {
                        if (dlg_result != DialogResult.OK || fLogin.APIPassword == null)
                            return false;
                        base_text = Crypto.SimpleDecryptWithPassword(encrypted_text, fLogin.APIPassword);
                        if (base_text == null || !base_text.Contains("encrypted"))
                            return false;
                    }
                }
                string[] text_elements = base_text.Split('|');
                curr_api_key = text_elements[1];
                curr_api_secret = text_elements[2];
                if(already_decrypted && !save_password)
                {
                    DialogResult dlg_result = fLogin.ShowDialog();
                    if (dlg_result == DialogResult.Retry)
                    {
                        if (!this.PromptLogIn(out curr_api_key, out curr_api_secret))
                            return false;
                    }
                    else
                    {
                        if (dlg_result != DialogResult.OK || fLogin.APIPassword == null)
                            return false;
                        int original_hash = (int)Registry.GetValue(@"HKEY_CURRENT_USER\Software\CoinPost\Initialization", "PasswordHash", -1);
                        if (original_hash == -1 || fLogin.APIPassword.GetHashCode() != original_hash)
                            return false;
                        FileStream fstream = File.Create("CoinPost.key");
                        encrypted_text = Crypto.SimpleEncryptWithPassword("encrypted|" + curr_api_key + "|" + curr_api_secret, fLogin.APIPassword);
                        if (encrypted_text == null)
                        {
                            fstream.Close();
                            File.Delete("CoinPost.key");
                            return false;
                        }
                        byte[] bytes = Convert.FromBase64String(encrypted_text);
                        fstream.Write(bytes, 0, bytes.Length);
                        fstream.Close();
                    }
                }
                else if(!already_decrypted && save_password)
                {
                    Registry.SetValue(@"HKEY_CURRENT_USER\Software\CoinPost\Initialization", "PasswordHash", fLogin.APIPassword.GetHashCode());
                    FileStream fstream = File.Create("CoinPost.key");
                    byte[] bytes = Encoding.ASCII.GetBytes(base_text);
                    fstream.Write(bytes, 0, bytes.Length);
                    fstream.Close();
                }
            }
            else if (!this.PromptLogIn(out curr_api_key, out curr_api_secret))
                return false;
            this.btceApi = new BtceApi(curr_api_key, curr_api_secret);
            return true;
        }
        public formMain()
        {
            this.is_valid = false;
            if (!Exchange.Initialize("Exchanges.ini"))
                return;
            try
            {
                Xpcom.Initialize(@".\xulrunner");
            }
            catch (Exception e)
            {
                MessageBox.Show("Could not initialized Xpcom.");
                return;
            }
            //
            this.InitializeComponent();
            
            #region Child Form Initialization
            this.fTradeHistory = new formTradeHistory(new delEmpty(this.SafeToggleTradeHistory));
            #endregion
            #region Callback Delegate Initialization
            this.cbUpdateUserInfo = new delUserInfo(this.UpdateUserInfo);
            this.cbUpdateOrderList = new delOrderList(this.UpdateOrderList);
            this.cbUpdateLastPrice = new delDecimal(this.UpdateLastPrice);
            this.cbUpdateTotalBalance = new delDecimal(this.UpdateTotalBalance);
            this.cbUpdateTradeHistory = new delTradeHistory(this.UpdateTradeHistory);
            #endregion
            #region Flag Initialization
            this.browsers_enabled = true;
            this.exchange_changed = true;
            this.get_trade_history = false;
            this.valid = true;
            #endregion
            #region Combobox Initialization
            this.comboSourceCurrency.Items.Clear();
            this.comboTargetCurrency.Items.Clear();
            foreach (string s in Exchange.usd_pairs)
                this.comboSourceCurrency.Items.Add(s);
            foreach (string s in Exchange.non_usd_pairs)
                this.comboSourceCurrency.Items.Add(s);
            foreach (string s in Exchange.target_pairs)
                this.comboTargetCurrency.Items.Add(s);
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
            this.gridBuy.Rows.Clear();
            #endregion
            #region BtceApi Initialization
            if (this.InitializeBtceApi())
            {
                #region Threading Initialization
                this.threadInfo = new Thread(new ThreadStart(GetInfo));
                this.threadInfo.Name = "CoinPost_GetInfo";
                #endregion
                this.LoadWindowConfiguration();
                this.is_valid = true;
            }
            #endregion
        }
        private bool PromptLogIn(out string api_key, out string api_Secret)
        {
            api_key = null;
            api_Secret = null;
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\CoinPost\Initialization", "SavePassword", false);
            formCredentials fLogin = new formCredentials(true);
            if (fLogin.ShowDialog() == DialogResult.OK && fLogin.APIKey != null && fLogin.APISecret != null)
            {
                FileStream fstream = File.Create("CoinPost.key");
                string output = "encrypted|" + fLogin.APIKey + "|" + fLogin.APISecret;
                api_key = fLogin.APIKey;
                api_Secret = fLogin.APISecret;
                output = Crypto.SimpleEncryptWithPassword(output, fLogin.APIPassword);
                if (output == null)
                {
                    Registry.SetValue(@"HKEY_CURRENT_USER\Software\CoinPost\Initialization", "PasswordHash", fLogin.APIPassword.GetHashCode());
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
            return true;
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
        #region Information Recording
        private void LogBalance(bool force_log=false)
        {
            DateTime now = DateTime.UtcNow;
            if (force_log || now.Subtract(this.last_log_time).TotalHours > 1.0)
            {
                double value_out = 0.0;
                if (Double.TryParse(this.lblTotalBalance.Text.Replace(" ","").Split('$')[1], out value_out))
                {
                    force_log = true;
                    this.btceDatabase.AddBalanceEntry(UnixTime.GetFromDateTime(now), value_out);
                    this.last_log_time = now;
                }
            }
            return;
        }
        #endregion
        #region Information Retrieval / Multithreading
        private void GetInfo()
        {
            while (mutValid.WaitOne(3000) && this.valid)
            {
                //
                Decimal? total_balance = 0; ;
                //
                Info userinfo = this.btceApi.GetInfo();
                if (userinfo != null && userinfo.rights.Info)
                {
                    this.BeginInvoke(this.cbUpdateUserInfo, userinfo);
                    //
                    Ticker[] usd_rates = new Ticker[Exchange.usd_pairs.Count];
                    Ticker[] non_usd_rates = new Ticker[Exchange.non_usd_pairs.Count];
                    for (int i = 0; i < Exchange.usd_pairs.Count; i++)
                        usd_rates[i] = BtceApi.GetTicker(Exchange.usd_pairs[i] + "_" + Exchange.target_pairs[1]);
                    for (int i = 0; i < Exchange.non_usd_pairs.Count; i++)
                        non_usd_rates[i] = BtceApi.GetTicker(Exchange.non_usd_pairs[i] + "_" + Exchange.target_pairs[0]);
                    //
                    total_balance += userinfo.funds.GetBalance(Exchange.target_pairs[1]);
                    for (int j = 0; j < Exchange.usd_pairs.Count; j++)
                        total_balance += userinfo.funds.GetBalance(Exchange.usd_pairs[j]) * usd_rates[j].Last * Convert.ToDecimal(0.998);
                    for (int j = 0; j < Exchange.non_usd_pairs.Count; j++)
                        total_balance += userinfo.funds.GetBalance(Exchange.non_usd_pairs[j]) * non_usd_rates[j].Last * usd_rates[Exchange.usd_pairs.IndexOf("BTC")].Last * Convert.ToDecimal(0.998) * Convert.ToDecimal(0.998);
                    //
                    OrderList orders = this.btceApi.GetOrderList();
                    this.BeginInvoke(this.cbUpdateOrderList, orders);
                    if (orders != null)
                    {
                        foreach (KeyValuePair<int, Order> order in orders.List)
                        {
                            string pair_str = order.Value.Pair.ToString().ToUpper();
                            if (order.Value.Type == "sell")
                            {
                                int index = Exchange.usd_pairs.IndexOf(pair_str.Substring(0, 3));
                                if (index != -1)
                                    total_balance += order.Value.Amount * usd_rates[index].Last * Convert.ToDecimal(0.998);
                                else
                                {
                                    index = Exchange.non_usd_pairs.IndexOf(pair_str.Take(3).ToString());
                                    if (index != -1)
                                        total_balance += order.Value.Amount * non_usd_rates[index].Last * usd_rates[Exchange.usd_pairs.IndexOf("BTC")].Last * Convert.ToDecimal(0.998);
                                }
                            }
                            else
                            {
                                if (pair_str.Length > 7)
                                {
                                    try
                                    {
                                        throw new Exception("Pair string (" + pair_str + ") was not of expected length (> 7).");
                                    }
                                    catch (Exception e)
                                    {
                                        MessageBox.Show("Exception in 'formMain.GetInfo()' with message: '"+e.Message+"'");
                                    }
                                }
                                else
                                {
                                    int index = Exchange.target_pairs.IndexOf(pair_str.Substring(4, 3));
                                    if (index != -1)
                                        total_balance += order.Value.Amount * order.Value.Rate * (Exchange.target_pairs[index].Contains("USD") ? 1 : usd_rates[Exchange.usd_pairs.IndexOf("BTC")].Last * Convert.ToDecimal(0.998));
                                }

                            }
                        }
                    }
                    this.SafeUpdateTradeHistory();
                    this.BeginInvoke(this.cbUpdateTotalBalance, total_balance);
                }
                this.BeginInvoke(this.cbUpdateLastPrice, BtceApi.GetTicker(this.SafeRetrieveExchangeString()).Last);
                mutValid.ReleaseMutex();
                Thread.Sleep(900);
            }
            mutValid.ReleaseMutex();
            return;
        }
        private string SafeRetrieveExchangeString()
        {
            mutExchangeString.WaitOne();
            string retval = this.current_exchange;
            mutExchangeString.ReleaseMutex();
            return retval;
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
        private void SafeUpdateExchangeString()
        {
            if (this.comboSourceCurrency.SelectedIndex > -1 && this.comboTargetCurrency.SelectedIndex > -1)
            {
                mutExchangeString.WaitOne();
                this.current_exchange = this.comboSourceCurrency.SelectedItem.ToString() + "_" + this.comboTargetCurrency.SelectedItem.ToString();
                this.exchange_changed = true;
                mutExchangeString.ReleaseMutex();
                if(this.browsers_enabled && this.webBrowser.Created)
                {
                    this.webBrowser.Navigate(Exchange.non_wisdom_pairs.Contains(this.comboSourceCurrency.SelectedItem.ToString()) ? "https://btc-e.com/exchange/" + this.comboSourceCurrency.SelectedItem.ToString().ToLower() + "_" + this.comboTargetCurrency.SelectedItem.ToString().ToLower() : "bitcoinwisdom.com/markets/btce/" + this.comboSourceCurrency.SelectedItem.ToString().ToLower() + this.comboTargetCurrency.SelectedItem.ToString().ToLower(), GeckoLoadFlags.AllowThirdPartyFixup | GeckoLoadFlags.FirstLoad | GeckoLoadFlags.BypassCache | GeckoLoadFlags.BypassHistory | GeckoLoadFlags.BypassProxy | GeckoLoadFlags.ReplaceHistory);
                    this.tabsMain.SelectedIndex = 0;
                }

            }
        }
        private void SafeUpdateTradeHistory()
        {
            mutTradeHistory.WaitOne();
            this.BeginInvoke(this.cbUpdateTradeHistory, this.btceApi.GetTradeHistory());
            mutTradeHistory.ReleaseMutex();
            return;
        }
        #endregion
        #region Update Callbacks
        private void PlaySound()
        {
            Stream str = Properties.Resources.trade;
            SoundPlayer snd = new SoundPlayer(str);
            snd.Play();
        }
        private void UpdateLastPrice(Decimal? price_in)
        {
            if (!price_in.HasValue)
                return;
            this.lklblLastPrice.Text = price_in.Value.ToString();
            mutExchangeString.WaitOne();
            if (this.exchange_changed)
                this.txtPrice.Text = this.lklblLastPrice.Text;
            this.exchange_changed = false;
            mutExchangeString.ReleaseMutex();
        }

        private void UpdateOrderList(OrderList orders_in)
        {
            bool change_occured = (this.recent_active_orders!=orders_in);
            if (!change_occured && this.recent_active_orders!=null && orders_in!=null)
            {
                for (int i = 0; i < orders_in.List.Count; i++)
                {
                    if (orders_in.List.Keys.ToArray()[i] != this.recent_active_orders.List.Keys.ToArray()[i] ||
                        orders_in.List[orders_in.List.Keys.ToArray()[i]].Amount != this.recent_active_orders.List[orders_in.List.Keys.ToArray()[i]].Amount)
                    {
                        change_occured = true;
                        break;
                    }
                }
            }
            if (change_occured)
            {
                this.recent_active_orders = orders_in;
                this.gridSell.Rows.Clear();
                this.gridBuy.Rows.Clear();
                List<int> new_canceledOrders = new List<int>();
                if(orders_in!=null)
                {
                    foreach (KeyValuePair<int, Order> pair in orders_in.List)
                    {
                        if (this.canceledOrders.Contains(pair.Key))
                        {
                            new_canceledOrders.Add(pair.Key);
                            continue;
                        }
                        string[] units = pair.Value.Pair.ToString().Split('_');
                        int index = (pair.Value.Type.ToString().ToLower().Contains("sell") ? this.gridSell : this.gridBuy).Rows.Add(new object[]
                        { 
                            pair.Key, pair.Value.Amount.ToString() + " " + units[0].ToUpper(), pair.Value.Rate.ToString() + " " + units[1].ToUpper(),
                            (pair.Value.Amount * pair.Value.Rate).ToString() + " " + units[1].ToUpper(),
                            "X","M"
                        });
                    }
                }
                this.canceledOrders = new_canceledOrders;
            }
            return;
        }
        private void UpdateTotalBalance(Decimal? balance_in)
        {
            if(balance_in.HasValue)
            {
                this.lblTotalBalance.Text = "Total Balance: $ " + Math.Round(balance_in.Value,2).ToString();
                this.LogBalance();
            }
            return;
        }
        private void UpdateTradeHistory(TradeHistory history_in)
        {
            if (history_in != null && history_in.List.Keys.ToArray()[0] != this.most_recent_trade)
            {
                foreach (KeyValuePair<int, Trade> pair in history_in.List)
                { // look for recent buys and sells of current exchange so-as to provide "most recent YOU bought/sold" prices on bottom of window
                }
                if (this.most_recent_trade != -1)
                {
                    this.PlaySound();
                    this.LogBalance(true);
                }
                this.most_recent_trade = history_in.List.Keys.ToArray()[0];
                this.fTradeHistory.UpdateTradeHistory(history_in);
            }
        }
        private void UpdateUserInfo(Info info_in)
        {
            if (info_in == null)
                return;
            foreach (string s in Exchange.usd_pairs)
                this.gridBalances.Rows[this.gridBalances_FindIndexOfPair(s)].Cells[1].Value = info_in.funds.GetBalance(s);
            foreach (string s in Exchange.non_usd_pairs)
                this.gridBalances.Rows[this.gridBalances_FindIndexOfPair(s)].Cells[1].Value = info_in.funds.GetBalance(s);
            this.gridBalances.Rows[this.gridBalances_FindIndexOfPair(Exchange.target_pairs[1])].Cells[1].Value = info_in.funds.GetBalance(Exchange.target_pairs[1]);
        }

        #endregion
        #endregion
        #region Event Methods
        #region Button Events
        private void btnBuy_Click(object sender, EventArgs e)
        {
            double price_out = 0.0, quantity_out = 0.0;
            if (Double.TryParse(txtPrice.Text, out price_out) && Double.TryParse(txtQuantity.Text, out quantity_out) && price_out <= Convert.ToDouble(this.lklblLastPrice.Text)*1.1 && quantity_out != 0.0)
            {
                TradeAnswer answer = this.btceApi.Trade(this.SafeRetrieveExchangeString(), "buy", price_out, quantity_out);
                if (answer!=null && answer.Received > 0)
                {
                    /*
                    Stream str = Properties.Resources.trade;
                    SoundPlayer snd = new SoundPlayer(str);
                    snd.Play(); */
                }
            }
        }

        private void btnSell_Click(object sender, EventArgs e)
        {
            double price_out = 0.0, quantity_out = 0.0;
            if (Double.TryParse(txtPrice.Text, out price_out) && Double.TryParse(txtQuantity.Text, out quantity_out) && price_out >= Convert.ToDouble(this.lklblLastPrice.Text)*0.9 && quantity_out != 0.0)
            {
                TradeAnswer answer = this.btceApi.Trade(this.SafeRetrieveExchangeString(), "sell", price_out, quantity_out);
                if (answer!=null && answer.Received > 0)
                {
                    /*
                    Stream str = Properties.Resources.trade;
                    SoundPlayer snd = new SoundPlayer(str);
                    snd.Play(); */
                }
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
            if (caller.SelectedIndex != -1 && Exchange.non_usd_pairs.Contains(caller.SelectedItem.ToString()))
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
            else if (caller.SelectedIndex == 1 && this.comboSourceCurrency.SelectedIndex != -1 && Exchange.non_usd_pairs.Contains(caller.SelectedItem.ToString()))
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
            if (!this.browsers_enabled)
                return;
            this.InitializeBrowser();
            if (this.comboSourceCurrency.SelectedIndex != -1 && this.comboTargetCurrency.SelectedIndex != -1)
            {
                if (Exchange.non_wisdom_pairs.Contains(this.comboSourceCurrency.SelectedItem.ToString()))
                    this.webBrowser.Navigate("https://btc-e.com/exchange/" + this.comboSourceCurrency.SelectedItem.ToString().ToLower() + "_" + this.comboTargetCurrency.SelectedItem.ToString().ToLower());
                else
                    this.webBrowser.Navigate("bitcoinwisdom.com/markets/btce/" + this.comboSourceCurrency.SelectedItem.ToString().ToLower() + this.comboTargetCurrency.SelectedItem.ToString().ToLower());
            }
            this.btceDatabase = new BtceDatabase("Main.db");
            BtceData data=this.btceDatabase.Query("select Time from Balance limit 1");
            this.last_log_time=data.GetLastTime();
            return;
        }

        private void formMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.SaveWindowConfiguration();
            mutValid.WaitOne();
            this.valid = false;
            mutValid.ReleaseMutex();
        }
        private void formMain_ResizeEnd(object sender, EventArgs e)
        {
            bool old_value = this.browsers_enabled;
            this.webBrowser.Visible = this.webBrowser.Enabled = this.browsers_enabled = (this.Height > this.MinimumSize.Height * 1.1);
            foreach (TabPage page in this.tabsMain.TabPages)
            {
                foreach (Control control in page.Controls)
                {
                    GeckoWebBrowser browser = control as GeckoWebBrowser;
                    if (browser != null)
                    {
                        browser.Visible = browser.Enabled = this.browsers_enabled;
                    }
                }
            }
            if (!old_value && this.browsers_enabled)
                this.SafeUpdateExchangeString();
        }
        private void splitMain_SplitterMoved(object sender, SplitterEventArgs e)
        {
            this.formMain_ResizeEnd(null, null);
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
            if (e.RowIndex < 0)
                return;
            DataGridView caller = (DataGridView)sender;
            int order_number = Convert.ToInt32(caller.Rows[e.RowIndex].Cells[0].Value);
            if (e.ColumnIndex == 4)
            {
                caller.Rows.RemoveAt(e.RowIndex);
                this.canceledOrders.Add(order_number);
                CancelOrderAnswer answer = this.btceApi.CancelOrder(order_number);
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
                        for (int i = 0; i < caller.Rows.Count; i++)
                        {
                            if (Convert.ToInt32(caller.Rows[i].Cells[0].Value)==order_number)
                            {
                                caller.Rows.RemoveAt(i);
                                this.canceledOrders.Add(order_number);
                                this.btceApi.CancelOrder(order_number);
                                this.pendingTrades.Add(new PendingTrade(exchange_string_out, caller == this.gridBuy ? "buy" : "sell", fModify.Price.Value, fModify.Quantity.Value));
                                this.timerModifyOrder.Enabled = true;
                                break;
                            }
                        }
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
            if (temp.First() == ' ')
                temp = "0" + temp;
            if (temp.Last() == '.')
                temp += "0";
            decimal value = 0;
            decimal new_value = 0;
            if (decimal.TryParse(temp, out value))
            {
                new_value = Decimal.Truncate(value * 1000000) / 1000000;
                this.recent_quantity = new_value;
                if (caller.Text.Last() != '.' && new_value != value && value != 0)
                {
                    int old_selection_start = caller.SelectionStart;
                    int old_selection_length = caller.SelectionLength;
                    caller.Text = new_value.ToString();
                    caller.SelectionStart = Math.Min(old_selection_start, caller.Text.Length - 1);
                    caller.SelectionLength = Math.Min(old_selection_length, caller.Text.Length - 1 - caller.SelectionStart);
                }
            }
            else
            {
                int old_selection_start = caller.SelectionStart;
                int old_selection_length = caller.SelectionLength;
                caller.Text = old_quantity_text;
                caller.SelectionStart = Math.Min(old_selection_start, caller.Text.Length - 1);
                caller.SelectionLength = Math.Min(old_selection_length, caller.Text.Length - 1 - caller.SelectionStart);
            }
            old_quantity_text = caller.Text;
            this.UpdateTotal();
        }
        private void txtPrice_Update(object sender, EventArgs e)
        {
            TextBox caller = (TextBox)sender;
            string temp = caller.Text;
            if (temp.Length == 0)
                return;
            if (temp.First() == ' ')
                temp = "0" + temp;
            if (temp.Last() == '.')
                temp += "0";
            decimal value = 0;
            decimal new_value = 0;
            if (decimal.TryParse(temp, out value))
            {
                new_value = Decimal.Truncate(value * 1000000) / 1000000;
                this.recent_price = new_value;
                if (caller.Text.Last() != '.' && new_value != value && value != 0)
                {
                    int old_selection_start = caller.SelectionStart;
                    int old_selection_length = caller.SelectionLength;
                    caller.Text = new_value.ToString();
                    caller.SelectionStart = Math.Min(old_selection_start,caller.Text.Length-1);
                    caller.SelectionLength = Math.Min(old_selection_length, caller.Text.Length - 1 - caller.SelectionStart);
                }
            }
            else
            {
                int old_selection_start = caller.SelectionStart;
                int old_selection_length = caller.SelectionLength;
                caller.Text = old_price_text;
                caller.SelectionStart = Math.Min(old_selection_start, caller.Text.Length - 1);
                caller.SelectionLength = Math.Min(old_selection_length, caller.Text.Length - 1 - caller.SelectionStart);
            }
            old_price_text = caller.Text;
            this.UpdateTotal();
        }
        private void UpdateTotal()
        {
            this.txtTotal.Text = (this.recent_price * this.recent_quantity * (decimal)0.998).ToString() + " " + comboTargetCurrency.SelectedItem.ToString();
            this.ttipOrderAssist.SetToolTip(this.btnBuy, "0.0%: " + Math.Round(Convert.ToDouble(this.recent_price) * formMain.MinimumSellThreshold, 6).ToString() + " " + this.comboTargetCurrency.SelectedItem.ToString() +
                                            "\n0.5%: " + Math.Round(Convert.ToDouble(this.recent_price) * formMain.MinimumSellThreshold * 1.005, 6).ToString() + " " + this.comboTargetCurrency.SelectedItem.ToString() +
                                            "\n1.0%: " + Math.Round(Convert.ToDouble(this.recent_price) * formMain.MinimumSellThreshold * 1.01, 6).ToString() + " " + this.comboTargetCurrency.SelectedItem.ToString() +
                                            "\n2.0%: " + Math.Round(Convert.ToDouble(this.recent_price) * formMain.MinimumSellThreshold * 1.02, 6).ToString() + " " + this.comboTargetCurrency.SelectedItem.ToString());
            this.ttipOrderAssist.SetToolTip(this.btnSell, "0.0%: " + Math.Round(Convert.ToDouble(this.recent_price) * formMain.MinimumBuyThreshold).ToString() + " " + this.comboTargetCurrency.SelectedItem.ToString() +
                                            "\n0.5%: " + Math.Round(Convert.ToDouble(this.recent_price) * formMain.MinimumBuyThreshold * 1 / 1.005, 6).ToString() + " " + this.comboTargetCurrency.SelectedItem.ToString() +
                                            "\n1.0%: " + Math.Round(Convert.ToDouble(this.recent_price) * formMain.MinimumBuyThreshold * 1 / 1.01, 6).ToString() + " " + this.comboTargetCurrency.SelectedItem.ToString() +
                                            "\n2.0%: " + Math.Round(Convert.ToDouble(this.recent_price) * formMain.MinimumBuyThreshold * 1 / 1.02, 6).ToString() + " " + this.comboTargetCurrency.SelectedItem.ToString());
        }
        #endregion
        #region Timer Events
        private void timerModifyOrder_Tick(object sender, EventArgs e)
        {
            if (this.pendingTrades.Count > 0)
            {
                TradeAnswer answer = this.btceApi.Trade(this.pendingTrades[0].exchange, this.pendingTrades[0].type, this.pendingTrades[0].price, this.pendingTrades[0].quantity);
                this.pendingTrades.RemoveAt(0);
            }
            if (this.pendingTrades.Count < 1)
                this.timerModifyOrder.Enabled = false;
        }
        #endregion
        #region Web Browser Events
        private void webBrowser_DocumentCompleted(object sender, EventArgs e)
        {
            GeckoWebBrowser browser = (GeckoWebBrowser)sender;
            if (this.first_btce_page && browser.DocumentTitle.Contains("loading"))
            {
                this.first_btce_page = false;
                this.SafeUpdateExchangeString();
            }
            else if (browser.Url.AbsoluteUri.Contains("bitcoinwisdom.com"))
            {
                using (AutoJSContext context = new AutoJSContext(browser.Window.JSContext))
                {
                    context.EvaluateScript("$( document ).ready(function(){$( \"#leftbar_outer\" ).hide();$( \"#leftbar\" ).hide();$( \"#footer\" ).hide();$( \"div.difficulty\" ).hide();$( \"#canvas_cross\" ).mousewheel();});");
                }
            }
            this.last_url = this.webBrowser.Url.AbsoluteUri;
            return;
        }
        void webBrowser_Navigating(object sender, Gecko.Events.GeckoNavigatingEventArgs e)
        {
            string[] split_string = e.Uri.AbsoluteUri.Split('/');
            int split_string_len = split_string.Length;
            if (!this.browsers_enabled || !(e.Uri.AbsoluteUri.Contains("btc-e.com/exchange/") || e.Uri.AbsoluteUri.Contains("bitcoinwisdom.com/markets/")) || split_string_len<2 || e.Uri.AbsoluteUri == this.last_url)
            {
                e.Cancel = true;
                return;
            }
            string source_currency = split_string[split_string_len - 1].Substring(0, 3).ToUpper();
            string target_currency = split_string[split_string_len - 1].Substring(3, 3).ToUpper();
            if (this.user_nav)
            {
                string tab_name = (e.Uri.AbsoluteUri.Contains("bitcoinwisdom.com/markets/") ? split_string[split_string_len - 2] : "btce") + " | " + source_currency+"/"+target_currency;
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
                    newBrowser.DocumentCompleted += this.webBrowser_DocumentCompleted;
                    this.tabsMain.TabPages[this.tabsMain.TabPages.Count - 1].Text = tab_name;
                    this.tabsMain.TabPages[this.tabsMain.TabPages.Count - 1].Controls.Add(newBrowser);
                    this.tabsMain.SelectedIndex = this.tabsMain.TabPages.Count - 1;
                }
                e.Cancel = true;
                this.user_nav = false;
            }
            return;
        }
        void otherBrowsers_Navigating(object sender, Gecko.Events.GeckoNavigatingEventArgs e)
        {
            if (!this.browsers_enabled)
            {
                e.Cancel = true;
                return;
            }
            GeckoWebBrowser caller = (GeckoWebBrowser)sender;
            string[] split_string = e.Uri.AbsoluteUri.Split('/');
            int split_string_len = split_string.Length;
            e.Cancel = true;
            if (!this.browsers_enabled || !(e.Uri.AbsoluteUri.Contains("btc-e.com/exchange/") || e.Uri.AbsoluteUri.Contains("bitcoinwisdom.com/markets/")) || split_string_len < 2)
                return;
            string source_currency = split_string[split_string_len - 1].Substring(0, 3).ToUpper();
            string target_currency = split_string[split_string_len - 1].Substring(3, 3).ToUpper();
            if (split_string[split_string_len - 2].Contains("btce") && source_currency == this.comboSourceCurrency.SelectedItem.ToString() && target_currency == this.comboTargetCurrency.SelectedItem.ToString())
            {
                this.tabsMain.SelectedIndex = 0;
                return;
            }
            string tab_name = (e.Uri.AbsoluteUri.Contains("bitcoinwisdom.com/markets/") ? split_string[split_string_len - 2] : "btce") + " | " + source_currency + "/" + target_currency;
            foreach (TabPage t in this.tabsMain.TabPages)
            {
                if (t.Text == tab_name)
                {
                    this.tabsMain.SelectedTab = t;
                    return;
                }
            }
            this.tabsMain.TabPages.Add("???");
            this.tabsMain.TabPages[this.tabsMain.TabPages.Count - 1].Disposed += formMain_TabDisposed;
            Gecko.GeckoWebBrowser newBrowser = new GeckoWebBrowser();
            newBrowser.Dock = DockStyle.Fill;
            newBrowser.Navigate(e.Uri.AbsoluteUri);
            newBrowser.Navigating += this.otherBrowsers_Navigating;
            newBrowser.DocumentCompleted += this.webBrowser_DocumentCompleted;
            this.tabsMain.TabPages[this.tabsMain.TabPages.Count - 1].Text = tab_name;
            this.tabsMain.TabPages[this.tabsMain.TabPages.Count - 1].Controls.Add(newBrowser);
            this.tabsMain.SelectedIndex = this.tabsMain.TabPages.Count - 1;
            return;
        }

        void formMain_TabDisposed(object sender, EventArgs e)
        {
            TabPage caller = (TabPage)sender;
            for (int i = 0; i < caller.Controls.Count; i++)
            {
                if (caller.Controls[i] as GeckoWebBrowser != null)
                {
                    caller.Controls[i].Dispose();
                    break;
                }
            }
            return;
        }
        private void webBrowser_CreateWindow(object sender, GeckoCreateWindowEventArgs e)
        {

        }
        #endregion
        private void imgSettings_Click(object sender, EventArgs e)
        {
            formSettings fSettings = new formSettings();
            fSettings.ShowDialog();
        }
        private void imgSettings_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
            return;
        }
        #endregion
        private void webBrowser_DomClick(object sender, DomEventArgs e)
        {
            this.user_nav = true;
            return;
        }

        private void lblTotalBalance_Click(object sender, EventArgs e)
        {
            formBalance fBalance = new formBalance(this.btceDatabase);
            fBalance.ShowDialog();
            return;
        }


    }
}
