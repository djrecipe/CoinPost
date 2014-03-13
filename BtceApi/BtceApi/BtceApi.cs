#region Using
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Web;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
#endregion
namespace BtcE
{
	public class BtceApi
    {
        #region Static Members
        private static Mutex mutQuery = new Mutex();
        private static uint nonce = 0;
        #endregion
        #region Instance Members
        string key="";
        System.Security.Cryptography.HMACSHA512 hashMaker=null;
        #endregion
        #region Static Methods
        public static bool HandleError(JObject o)
        {
            if (o == null)
                return false;
            if (o.Value<int>("success") == 0)
            {
                string error_str = o.Value<string>("error");
                if (error_str.Contains("nonce"))
                {
                    int first_colon = error_str.IndexOf(':');
                    BtceApi.nonce = Convert.ToUInt32(error_str.Substring(first_colon + 1, error_str.IndexOf(',', first_colon) - first_colon - 1)) + 1;
                }
                else if (error_str.Contains("bad status"))
                    MessageBox.Show("Could not modify the order. The order likely no longer exists.");
                else if (error_str.Contains("incorrectly entered"))
                    MessageBox.Show("You entered a field incorrectly (likely too many digits after the decimal point).");
                else if (!error_str.Contains("no orders"))
                    MessageBox.Show(error_str);
                return false;
            }
            return true;
        }
        #endregion
        public BtceApi(string key, string secret)
        {
            this.key = key;
            this.hashMaker = new System.Security.Cryptography.HMACSHA512(Encoding.ASCII.GetBytes(secret));
            return;
        }
        public Info GetInfo()
        {
            //
            string query = this.Query(new Dictionary<string, string>()
            {
                { "method", "getInfo" }
            });
            if (query == null)
                return null;
            JObject result = JObject.Parse(query);
			return BtceApi.HandleError(result)?new Info(result["return"] as JObject):null;
		}

		public TransHistory GetTransHistory(int? from = null,int? count = null,int? fromId = null,int? endId = null,bool? orderAsc = null,
                                            DateTime? since = null,DateTime? end = null)
        {
            Dictionary<string, string> args = new Dictionary<string, string>(){{ "method", "TransHistory" }};
			if ( from != null ) args.Add("from", from.Value.ToString());
			if ( count != null ) args.Add("count", count.Value.ToString());
			if ( fromId != null ) args.Add("from_id", fromId.Value.ToString());
			if ( endId != null ) args.Add("end_id", endId.Value.ToString());
			if ( orderAsc != null ) args.Add("order", orderAsc.Value ? "ASC" : "DESC");
			if ( since != null ) args.Add("since", UnixTime.GetFromDateTime(since.Value).ToString());
			if ( end != null ) args.Add("end", UnixTime.GetFromDateTime(end.Value).ToString());
            //
            string query = this.Query(args);
            if (query == null)
                return null;
            JObject result = JObject.Parse(query);
			return BtceApi.HandleError(result)?new TransHistory(result["return"] as JObject):null;
		}

		public TradeHistory GetTradeHistory(int? from = null,int? count = null,int? fromId = null,int? endId = null,bool? orderAsc = null,
                                            DateTime? since = null,DateTime? end = null)
        {
            Dictionary<string, string> args = new Dictionary<string, string>(){{ "method", "TradeHistory" }};

            if ( from != null ) args.Add("from", from.Value.ToString());
            if ( count != null ) args.Add("count", count.Value.ToString());
            if ( fromId != null ) args.Add("from_id", fromId.Value.ToString());
            if ( endId != null ) args.Add("end_id", endId.Value.ToString());
            if ( orderAsc != null ) args.Add("order", orderAsc.Value ? "ASC" : "DESC");
            if ( since != null ) args.Add("since", UnixTime.GetFromDateTime(since.Value).ToString());
            if ( end != null ) args.Add("end", UnixTime.GetFromDateTime(end.Value).ToString());
            //
            string query = this.Query(args);
            if (query == null)
                return null;
            JObject result = JObject.Parse(query);
            return BtceApi.HandleError(result)?new TradeHistory(result["return"] as JObject):null;
		}

        public OrderList GetOrderList(bool? active = null,int? from = null,int? count = null,int? fromId = null,int? endId = null,bool? orderAsc = null,
                                        DateTime? since = null,DateTime? end = null,string pair = null)
        {
            Dictionary<string, string> args = new Dictionary<string, string>()
            {
                { "method", "OrderList" }
            };
            if ( from != null) args.Add("from", from.Value.ToString());
            if ( count!=null) args.Add("count", count.Value.ToString());
            if ( fromId!=null) args.Add("from_id", fromId.Value.ToString());
            if ( endId != null ) args.Add("end_id", endId.Value.ToString());
            if ( orderAsc!=null) args.Add("order", orderAsc.Value ?  "ASC" : "DESC" );
            if ( since != null) args.Add("since", UnixTime.GetFromDateTime(since.Value).ToString());
            if ( end != null ) args.Add("end", UnixTime.GetFromDateTime(end.Value).ToString());
            if ( pair != null ) args.Add("pair", pair);
            if ( active!=null) args.Add("active", active.Value ? "1" : "0");
            //
            string query = this.Query(args);
            if (query == null)
                return null;
            JObject result = JObject.Parse(query);
			return BtceApi.HandleError(result)?new OrderList(result["return"] as JObject):null;
		}

		public TradeAnswer Trade(string pair, string type, double rate, double amount)
        {
            pair = pair.ToLower();
            Dictionary<string, string> args = new Dictionary<string, string>()
            {
                { "method", "Trade" },
                { "pair", pair },
                { "type", type },
                { "rate", rate.ToString() },
                { "amount", amount.ToString() }
            };
            string query = this.Query(args);
            if (query == null)
                return null;
            JObject result = JObject.Parse(query);
			return BtceApi.HandleError(result)?new TradeAnswer(result["return"] as JObject):null;
		}

		public CancelOrderAnswer CancelOrder(int orderId)
        {
            Dictionary<string, string> args = new Dictionary<string, string>()
            {
                { "method", "CancelOrder" },
                { "order_id", orderId.ToString() }
            };
            string query = this.Query(args);
            if (query == null)
                return null;
            JObject result = JObject.Parse(query);
			return BtceApi.HandleError(result)?new CancelOrderAnswer(result["return"] as JObject):null;
		}

		string Query(Dictionary<string, string> args)
        {
            mutQuery.WaitOne();
            args.Add("nonce", GetNonce().ToString());

            byte[] data = Encoding.ASCII.GetBytes(BuildPostData(args));

            WebRequest request = null;
            try
            {
                request = WebRequest.Create(new Uri("https://btc-e.com/tapi")) as HttpWebRequest;
            }
            catch(Exception e)
            {
                request = null;
            }
            string retval = null, exception_msg = null;
            if (request != null)
            {
                request.Method = "POST";
                request.Timeout = 15000;
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                request.Headers.Add("Key", key);
                request.Headers.Add("Sign", ByteArrayToString(hashMaker.ComputeHash(data)).ToLower());
                Stream reqStream = null;
                try
                {
                    reqStream = request.GetRequestStream();
                    reqStream.Write(data, 0, data.Length);
                    reqStream.Close();
                }
                catch(System.Net.WebException e)
                {
                    exception_msg = "Please make sure you are connected to the internet. Exception message: \n\n'" + e.Message + "'";
                    retval = null;
                    reqStream = null;
                }
                try
                {
                    retval = new StreamReader(request.GetResponse().GetResponseStream()).ReadToEnd();
                }
                catch (Exception e)
                {
                    exception_msg = "Please make sure you are connected to the internet. Exception message: \n\n'" + e.Message + "'";
                    retval = null;
                }
            }
            mutQuery.ReleaseMutex();
            if(exception_msg!=null)
                MessageBox.Show(exception_msg);
            return retval;
        }
        static string ByteArrayToString(byte[] ba)
        {
            return BitConverter.ToString(ba).Replace("-", "");
        }
		static string BuildPostData(Dictionary<string, string> d)
        {
			StringBuilder s = new StringBuilder();
			foreach ( var item in d )
            {
				s.AppendFormat("{0}={1}", item.Key, HttpUtility.UrlEncode(item.Value));
				s.Append("&");
			}
			if ( s.Length > 0 ) s.Remove(s.Length - 1, 1);
			return s.ToString();
		}

		uint GetNonce()
        {
            return BtceApi.nonce++;
		}
		static string DecimalToString(decimal d)
        {
			return d.ToString(System.Globalization.CultureInfo.InvariantCulture);
		}
		public static Depth GetDepth(string pair)
        {
            string queryStr = string.Format("https://btc-e.com/api/2/{0}/depth", pair);
            queryStr = Query(queryStr);
			return queryStr == null ? null : new Depth(JObject.Parse(Query(queryStr)));
		}
		public static Ticker GetTicker(string pair)
        {
			string queryStr = string.Format("https://btc-e.com/api/2/{0}/ticker", pair.ToLower());
            queryStr = Query(queryStr);
            return queryStr == null ? null : new Ticker(JObject.Parse(queryStr)["ticker"] as JObject);
		}
		public static List<TradeInfo> GetTrades(string pair)
        {
			string queryStr = string.Format("https://btc-e.com/api/2/{0}/trades", pair);
            queryStr = Query(queryStr);
            return queryStr == null ? null : JArray.Parse(queryStr).OfType<JObject>().Select(TradeInfo.CreateInstance).ToList();
		}
		public static decimal GetFee(string pair)
        {
            string queryStr = string.Format("https://btc-e.com/api/2/{0}/fee", pair);
            queryStr = Query(queryStr);
            return queryStr == null ? new decimal(0.0) : JObject.Parse(Query(queryStr)).Value<decimal>("trade");
		}
		static string Query(string url)
        {
            WebRequest request = null;
            try
            {
                request = WebRequest.Create(url);
            }
            catch(Exception e)
            {
                request = null;
            }
            if (request == null)
                return null;
			request.Proxy = WebRequest.DefaultWebProxy;
			request.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
            try
            {
                return new StreamReader(request.GetResponse().GetResponseStream()).ReadToEnd();
            }
            catch(System.Net.WebException e)
            {
                MessageBox.Show("Please make sure you are connected to the internet. Exception message: \n\n'"+e.Message+"'");
                return null;
            }
		}
	}
    public static class UnixTime
    {
        #region Static Members
        private static DateTime unixEpoch = new DateTime(1970, 1, 1);
        #endregion
        #region Static Properties
        public static uint Now
        {
            get
            {
                return UnixTime.GetFromDateTime(DateTime.UtcNow);
            }
        }
        #endregion
        #region Static Methods
        public static uint GetFromDateTime(DateTime d)
        {
            return (uint)(d - unixEpoch).TotalSeconds;
        }
        public static DateTime ConvertToDateTime(uint unixtime)
        {
            return unixEpoch.AddSeconds(unixtime);
        }
        #endregion
    }
}
