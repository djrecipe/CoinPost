using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace CoinPost
{
    public class BtceData
    {
        #region Instance Properties
        public DataTable Data {get; private set;}
        #endregion
        #region Instance Methods
        public BtceData(IDataReader data_in)
        {
            if(data_in!=null)
            {
                this.Data=new DataTable();
                this.Data.Load(data_in);
            }
            else
                this.Data=null;
            data_in.Close();
            return;
        }
        public DateTime GetLastTime()
        {
            DateTime retval = new DateTime();
            if (this.Data != null && this.Data.Rows.Count>0)
            {
                try
                {
                    return BtcE.UnixTime.ConvertToDateTime(Convert.ToUInt32(this.Data.Rows[0].ItemArray[0]));
                }
                catch (Exception e)
                {
                }
            }
            return retval;
        }
        #endregion
    }
    public class BtceDatabase
    {
        #region Instance Members
        private string file_name = null;
        private SQLiteConnection connection = null;
        private bool valid = false;
        #endregion
        #region Instance Methods
        public BtceDatabase(string file_name_in)
        {
            this.file_name = file_name_in;
            try
            {
                this.connection = new SQLiteConnection("Data source=" + this.file_name);
            }
            catch (Exception e)
            {
                this.connection = null;
                System.Windows.Forms.MessageBox.Show("Could not open SQLite database! Exception:\n\n"+e.Message);
                return;
            }
            this.valid = true;
            return;
        }
        public bool AddBalanceEntry(uint unix_time_in, double balance_in)
        {
            if (this.SafeOpenConnection())
            {
                try
                {
                    SQLiteCommand cmd = new SQLiteCommand("insert into Balance values (" + unix_time_in.ToString() + ", " + balance_in.ToString() + ")", this.connection);
                    int rows_updated = cmd.ExecuteNonQuery();
                    if (rows_updated != 1)
                        throw new Exception("Number of rows updated (" + rows_updated.ToString() + ") does not match expected (1).");
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show("Could not add SQLite entry! Exception:\n\n" + e.Message);
                    this.SafeCloseConnection();
                    return false;
                }
                this.SafeCloseConnection();
                return true;
            }
            return false;
        }
        public BtceData Query(string query_in)
        {
            
            if (this.SafeOpenConnection())
            {
                SQLiteDataReader reader = null;
                try
                {
                    SQLiteCommand cmd = new SQLiteCommand(query_in, this.connection);
                    reader = cmd.ExecuteReader();
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show("Could not perform SQLite query! Exception:\n\n" + e.Message);
                    this.SafeCloseConnection();
                    return null;
                }
                BtceData data = new BtceData(reader);
                this.SafeCloseConnection();
                return data;
            }
            else
                return null;
        }
        private bool SafeCloseConnection()
        {
            try
            {
                this.connection.Close();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        private bool SafeOpenConnection()
        {
            if (!this.valid)
                return false;
            try
            {
                this.connection.Open();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}
