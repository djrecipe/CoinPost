namespace CoinPost
{
    partial class formModifyOrder
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lklblPrice = new System.Windows.Forms.LinkLabel();
            this.lklblQuantity = new System.Windows.Forms.LinkLabel();
            this.lblPrice = new System.Windows.Forms.Label();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.lblOld = new System.Windows.Forms.Label();
            this.lblNew = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAccept = new System.Windows.Forms.Button();
            this.lblOrderType = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lklblPrice
            // 
            this.lklblPrice.BackColor = System.Drawing.Color.Transparent;
            this.lklblPrice.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(182)))), ((int)(((byte)(255)))));
            this.lklblPrice.Location = new System.Drawing.Point(76, 56);
            this.lklblPrice.Name = "lklblPrice";
            this.lklblPrice.Size = new System.Drawing.Size(110, 20);
            this.lklblPrice.TabIndex = 0;
            this.lklblPrice.TabStop = true;
            this.lklblPrice.Text = "0.1255 BTC";
            this.lklblPrice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lklblPrice.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lklblPrice_LinkClicked);
            // 
            // lklblQuantity
            // 
            this.lklblQuantity.BackColor = System.Drawing.Color.Transparent;
            this.lklblQuantity.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(182)))), ((int)(((byte)(255)))));
            this.lklblQuantity.Location = new System.Drawing.Point(76, 81);
            this.lklblQuantity.Name = "lklblQuantity";
            this.lklblQuantity.Size = new System.Drawing.Size(110, 20);
            this.lklblQuantity.TabIndex = 1;
            this.lklblQuantity.TabStop = true;
            this.lklblQuantity.Text = "5.1152 PPC";
            this.lklblQuantity.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lklblQuantity.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lklblQuantity_LinkClicked);
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.BackColor = System.Drawing.Color.Transparent;
            this.lblPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(204)))), ((int)(((byte)(102)))));
            this.lblPrice.Location = new System.Drawing.Point(36, 60);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(34, 13);
            this.lblPrice.TabIndex = 2;
            this.lblPrice.Text = "Price:";
            // 
            // lblQuantity
            // 
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.BackColor = System.Drawing.Color.Transparent;
            this.lblQuantity.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuantity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(204)))), ((int)(((byte)(102)))));
            this.lblQuantity.Location = new System.Drawing.Point(21, 85);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(49, 13);
            this.lblQuantity.TabIndex = 3;
            this.lblQuantity.Text = "Quantity:";
            // 
            // txtPrice
            // 
            this.txtPrice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(125)))), ((int)(((byte)(125)))));
            this.txtPrice.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPrice.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(40)))), ((int)(((byte)(0)))));
            this.txtPrice.Location = new System.Drawing.Point(194, 58);
            this.txtPrice.Margin = new System.Windows.Forms.Padding(5);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(111, 15);
            this.txtPrice.TabIndex = 4;
            this.txtPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPrice.TextChanged += new System.EventHandler(this.txtPrice_TextChanged);
            // 
            // txtQuantity
            // 
            this.txtQuantity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(125)))), ((int)(((byte)(125)))));
            this.txtQuantity.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtQuantity.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuantity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(40)))), ((int)(((byte)(0)))));
            this.txtQuantity.Location = new System.Drawing.Point(194, 83);
            this.txtQuantity.Margin = new System.Windows.Forms.Padding(5, 5, 5, 10);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(111, 15);
            this.txtQuantity.TabIndex = 5;
            this.txtQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtQuantity.TextChanged += new System.EventHandler(this.txtQuantity_TextChanged);
            // 
            // lblOld
            // 
            this.lblOld.AutoSize = true;
            this.lblOld.BackColor = System.Drawing.Color.Transparent;
            this.lblOld.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOld.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(204)))), ((int)(((byte)(102)))));
            this.lblOld.Location = new System.Drawing.Point(107, 30);
            this.lblOld.Name = "lblOld";
            this.lblOld.Size = new System.Drawing.Size(41, 13);
            this.lblOld.TabIndex = 6;
            this.lblOld.Text = "Current";
            // 
            // lblNew
            // 
            this.lblNew.AutoSize = true;
            this.lblNew.BackColor = System.Drawing.Color.Transparent;
            this.lblNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNew.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(204)))), ((int)(((byte)(102)))));
            this.lblNew.Location = new System.Drawing.Point(235, 30);
            this.lblNew.Name = "lblNew";
            this.lblNew.Size = new System.Drawing.Size(29, 13);
            this.lblNew.TabIndex = 7;
            this.lblNew.Text = "New";
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(204)))), ((int)(((byte)(102)))));
            this.btnCancel.Location = new System.Drawing.Point(91, 143);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(92, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Abort Changes";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAccept
            // 
            this.btnAccept.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.btnAccept.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnAccept.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAccept.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(204)))), ((int)(((byte)(102)))));
            this.btnAccept.Location = new System.Drawing.Point(213, 143);
            this.btnAccept.Margin = new System.Windows.Forms.Padding(5, 5, 20, 5);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(92, 23);
            this.btnAccept.TabIndex = 9;
            this.btnAccept.Text = "Commit";
            this.btnAccept.UseVisualStyleBackColor = false;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // lblOrderType
            // 
            this.lblOrderType.BackColor = System.Drawing.Color.Transparent;
            this.lblOrderType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(204)))), ((int)(((byte)(102)))));
            this.lblOrderType.Location = new System.Drawing.Point(114, 177);
            this.lblOrderType.Name = "lblOrderType";
            this.lblOrderType.Size = new System.Drawing.Size(150, 18);
            this.lblOrderType.TabIndex = 10;
            this.lblOrderType.Text = "This is a BUY order.";
            this.lblOrderType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtTotal
            // 
            this.txtTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.txtTotal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTotal.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(204)))), ((int)(((byte)(102)))));
            this.txtTotal.Location = new System.Drawing.Point(125, 118);
            this.txtTotal.Margin = new System.Windows.Forms.Padding(5, 10, 5, 5);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(180, 15);
            this.txtTotal.TabIndex = 11;
            this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // formModifyOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 211);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.lblOrderType);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblNew);
            this.Controls.Add(this.lblOld);
            this.Controls.Add(this.txtQuantity);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.lblQuantity);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.lklblQuantity);
            this.Controls.Add(this.lklblPrice);
            this.Name = "formModifyOrder";
            this.Resizable = false;
            this.Text = "Modify an Existing Order";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel lklblPrice;
        private System.Windows.Forms.LinkLabel lklblQuantity;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Label lblOld;
        private System.Windows.Forms.Label lblNew;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Label lblOrderType;
        private System.Windows.Forms.TextBox txtTotal;
    }
}