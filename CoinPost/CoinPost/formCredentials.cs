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
    public partial class formCredentials : Form
    {
        public string APIKey { get; private set; }
        public string APISecret { get; private set; }
        public formCredentials()
        {
            this.APIKey = null;
            this.APISecret = null;
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.APIKey = this.txtAPIKey.Text;
            this.APISecret = this.txtSecret.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

    }
}
