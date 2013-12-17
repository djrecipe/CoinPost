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
        public formModifyOrder(double initial_price, double initial_quantity, string unit_string, bool buy_order)
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
            //
            if (!buy_order)
                this.lblOrderType.Text = "This is a SELL order.";
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
            this.txtQuantity.Text = this.lklblQuantity.Text.Split(' ')[0];
        }

        private void lklblPrice_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.txtPrice.Text = this.lklblPrice.Text.Split(' ')[0];
        }
    }
}
