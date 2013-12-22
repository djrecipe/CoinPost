using System;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

namespace CoinPost
{
    public partial class formCredentials : Form
    {
        public string APIKey { get; private set; }
        public string APISecret { get; private set; }
        public string APIPassword { get; private set; }
        private bool validate_password;
        public formCredentials(bool initial_login=true)
        {
            InitializeComponent();
            this.APIKey = null;
            this.APISecret = null;
            this.APIPassword = null;
            this.validate_password = initial_login;
            if (!initial_login)
            {
                this.txtAPIKey.Enabled = false;
                this.txtSecret.Enabled = false;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.validate_password && this.txtPassword.Text.Length < 12)
            {
                MessageBox.Show("Password must contain at least 12 characters.");
                return;
            }
            this.APIKey = this.txtAPIKey.Text;
            this.APISecret = this.txtSecret.Text;
            this.APIPassword = this.txtPassword.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            TextBox caller = (TextBox)sender;
            caller.UseSystemPasswordChar = true;
            this.btnOK.Enabled = true;
        }

        private void txtAPIKey_Click(object sender, EventArgs e)
        {
            TextBox caller = (TextBox)sender;
            if (caller.Text == "ENTER YOUR API KEY HERE")
            {
                caller.SelectionStart = 0;
                caller.SelectionLength = caller.Text.Length;
            }

        }
        private void txtSecret_Click(object sender, EventArgs e)
        {
            TextBox caller = (TextBox)sender;
            if (caller.Text == "ENTER YOUR API SECRET HERE")
            {
                caller.SelectionStart = 0;
                caller.SelectionLength = caller.Text.Length;
            }
        }

        private void txtPassword_Click(object sender, EventArgs e)
        {
            TextBox caller = (TextBox)sender;
            if (caller.Text == "ENTER YOUR -COINPOST- PASSWORD HERE")
            {
                caller.Text = "";
            }
        }


    }
}
