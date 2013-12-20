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
            this.max_balance = balance+initial_price*initial_quantity;
            Console.WriteLine(max_balance.ToString());
            //
            this.lklblPrice.Text = initial_price.ToString() + " " + this.price_units;
            this.lklblQuantity.Text = initial_quantity.ToString() + " " + this.quantity_units;
            //
            this.is_buy_order = buy_order;
            if (!buy_order)
            {
                this.lblOrderType.Text = "This is a SELL order.";
                this.txtQuantity.Text = this.lklblQuantity.Text.Split(' ')[0];
            }
        }
        private void UpdateTotal()
        {
            double dbl_price = 0.0, dbl_quantity = 0.0;
            if (double.TryParse(this.txtPrice.Text, out dbl_price) && double.TryParse(this.txtQuantity.Text, out dbl_quantity))
                this.txtTotal.Text = (dbl_price * dbl_quantity).ToString();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            double new_price = 0.0, new_quantity = 0.0;
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
            decimal value = 0;
            decimal.TryParse(caller.Text, out value);
            caller.Text = (Decimal.Truncate(value * 1000000) / 1000000).ToString();
            this.UpdateTotal();
        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            TextBox caller = (TextBox)sender;
            decimal value = 0;
            decimal.TryParse(caller.Text, out value);
            caller.Text = (Decimal.Truncate(value * 100000000) / 100000000).ToString();
            this.UpdateTotal();
        }
    }
}
