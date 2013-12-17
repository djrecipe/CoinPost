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
            this.SuspendLayout();
            // 
            // lklblPrice
            // 
            this.lklblPrice.Location = new System.Drawing.Point(54, 28);
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
            this.lklblQuantity.Location = new System.Drawing.Point(54, 51);
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
            this.lblPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrice.Location = new System.Drawing.Point(18, 32);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(34, 13);
            this.lblPrice.TabIndex = 2;
            this.lblPrice.Text = "Price:";
            // 
            // lblQuantity
            // 
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuantity.Location = new System.Drawing.Point(3, 55);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(49, 13);
            this.lblQuantity.TabIndex = 3;
            this.lblQuantity.Text = "Quantity:";
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(170, 28);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(111, 20);
            this.txtPrice.TabIndex = 4;
            // 
            // txtQuantity
            // 
            this.txtQuantity.Location = new System.Drawing.Point(170, 51);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(111, 20);
            this.txtQuantity.TabIndex = 5;
            // 
            // lblOld
            // 
            this.lblOld.AutoSize = true;
            this.lblOld.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOld.Location = new System.Drawing.Point(87, 7);
            this.lblOld.Name = "lblOld";
            this.lblOld.Size = new System.Drawing.Size(41, 13);
            this.lblOld.TabIndex = 6;
            this.lblOld.Text = "Current";
            // 
            // lblNew
            // 
            this.lblNew.AutoSize = true;
            this.lblNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNew.Location = new System.Drawing.Point(210, 7);
            this.lblNew.Name = "lblNew";
            this.lblNew.Size = new System.Drawing.Size(29, 13);
            this.lblNew.TabIndex = 7;
            this.lblNew.Text = "New";
            // 
            // btnCancel
            // 
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(57, 85);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(92, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Abort Changes";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAccept
            // 
            this.btnAccept.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAccept.Location = new System.Drawing.Point(179, 85);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(92, 23);
            this.btnAccept.TabIndex = 9;
            this.btnAccept.Text = "Commit";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // lblOrderType
            // 
            this.lblOrderType.Location = new System.Drawing.Point(80, 119);
            this.lblOrderType.Name = "lblOrderType";
            this.lblOrderType.Size = new System.Drawing.Size(150, 18);
            this.lblOrderType.TabIndex = 10;
            this.lblOrderType.Text = "This is a BUY order.";
            this.lblOrderType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // formModifyOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(301, 148);
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
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "formModifyOrder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
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
    }
}