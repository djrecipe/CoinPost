using Microsoft.Win32;
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
    public partial class formSettings : CoinPostGUI.Window
    {
        public formSettings()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\CoinPost\Initialization", "SavePassword", this.chkboxRememberPassword.Checked);
            this.DialogResult = DialogResult.OK;
            this.Close();
            return;
        }

        private void formSettings_Load(object sender, EventArgs e)
        {
            this.chkboxRememberPassword.Checked = Convert.ToBoolean((string)Registry.GetValue(@"HKEY_CURRENT_USER\Software\CoinPost\Initialization", "SavePassword", "False"));
            return;
        }
    }
}
