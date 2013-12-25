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
    public partial class formModifyOrder : Form
    {
        public double? Price { get; private set; }
        public double? Quantity { get; private set; }
        private string price_units;
        private string quantity_units;
        private string old_price_text;
        private string old_quantity_text;
        private double base_price;
        private double base_quantity;
        private double max_balance;
        private bool is_buy_order;
        public formModifyOrder(double initial_price, double initial_quantity,double balance, string unit_string, bool buy_order)
        {
            InitializeComponent();
            //
            string[] units = unit_string.ToUpper().Split('_');
            this.price_units = units[1];
            this.quantity_units = units[0];
            this.base_price = initial_price;
            this.base_quantity = initial_quantity;
            //
            this.lklblPrice.Text = initial_price.ToString() + " " + this.price_units;
            this.lklblQuantity.Text = initial_quantity.ToString() + " " + this.quantity_units;
            this.old_price_text = initial_price.ToString();
            this.old_quantity_text = initial_quantity.ToString();
            //
            this.is_buy_order = buy_order;
            this.max_balance = balance;
            if (!buy_order)
            {
                this.lblOrderType.Text = "This is a SELL order.";
                this.txtQuantity.Text = this.lklblQuantity.Text.Split(' ')[0];
            }
            else
                this.max_balance += initial_price * initial_quantity;
        }
        private void UpdateTotal()
        {
            double dbl_price = 0.0, dbl_quantity = 0.0;
            if (double.TryParse(this.txtPrice.Text, out dbl_price) && double.TryParse(this.txtQuantity.Text, out dbl_quantity))
                this.txtTotal.Text = (dbl_price * dbl_quantity).ToString() + " " + price_units ;
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            double new_price = 0.0, new_quantity = 0.0;
            if (this.is_buy_order && this.txtTotal.Text!="")
            {
                double total = Convert.ToDouble(this.txtTotal.Text.Split(' ')[0]);
                if (total > this.max_balance)
                    this.lklblQuantity_LinkClicked(null, null);
            }
            if (Double.TryParse(this.txtPrice.Text, out new_price) && Double.TryParse(this.txtQuantity.Text, out new_quantity))
            {
                this.Price = new_price;
                this.Quantity = new_quantity;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Price = null;
            this.Quantity = null;
            this.Close();
        }

        private void lklblQuantity_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            double dbl_price = 0.0;
            if (this.is_buy_order && Double.TryParse(this.txtPrice.Text, out dbl_price))
                this.txtQuantity.Text = (this.max_balance / dbl_price).ToString();
            else
                this.txtQuantity.Text = this.lklblQuantity.Text.Split(' ')[0];
        }

        private void lklblPrice_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.txtPrice.Text = this.lklblPrice.Text.Split(' ')[0];
        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {
            TextBox caller = (TextBox)sender;
            string temp = caller.Text;
            if (temp.Length == 0)
                return;
            if (temp.Last() == '.')
                temp += "0";
            decimal value = 0;
            decimal new_value = 0;
            if (decimal.TryParse(temp, out value))
            {
                new_value = Decimal.Truncate(value * 1000000) / 1000000;
                if (caller.Text.Last() != '.' && new_value != value && value != 0)
                    caller.Text = new_value.ToString();
            }
            else
                caller.Text = old_quantity_text;
            old_quantity_text = caller.Text;
            this.UpdateTotal();
        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            TextBox caller = (TextBox)sender;
            string temp = caller.Text;
            if (temp.Length == 0)
                return;
            if (temp.Last() == '.')
                temp += "0";
            decimal value = 0;
            decimal new_value = 0;
            if (decimal.TryParse(temp, out value))
            {
                new_value = Decimal.Truncate(value * 1000000) / 1000000;
                if (caller.Text.Last() != '.' && new_value != value && value != 0)
                    caller.Text = new_value.ToString();
            }
            else
                caller.Text = old_quantity_text;
            old_quantity_text = caller.Text;
            this.UpdateTotal();
        }
    }
}
