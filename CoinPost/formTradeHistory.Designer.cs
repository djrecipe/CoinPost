namespace CoinPost
{
    partial class formTradeHistory
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.gridBuy = new CoinPost.Grid();
            this.colBuyID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBuyTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBuyQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBuyPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBuyTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridSell = new CoinPost.Grid();
            this.colSellID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSellTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSellQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSellPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSellTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridBuy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSell)).BeginInit();
            this.SuspendLayout();
            // 
            // splitMain
            // 
            this.splitMain.BackColor = System.Drawing.Color.Transparent;
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Location = new System.Drawing.Point(0, 0);
            this.splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.gridBuy);
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.gridSell);
            this.splitMain.Size = new System.Drawing.Size(745, 219);
            this.splitMain.SplitterDistance = 382;
            this.splitMain.SplitterWidth = 8;
            this.splitMain.TabIndex = 1;
            // 
            // gridBuy
            // 
            this.gridBuy.AllowUserToAddRows = false;
            this.gridBuy.AllowUserToDeleteRows = false;
            this.gridBuy.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridBuy.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridBuy.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.gridBuy.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gridBuy.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.gridBuy.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(204)))), ((int)(((byte)(102)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridBuy.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridBuy.ColumnHeadersHeight = 20;
            this.gridBuy.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gridBuy.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colBuyID,
            this.colBuyTime,
            this.colBuyQuantity,
            this.colBuyPrice,
            this.colBuyTotal});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.DarkGreen;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Chartreuse;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridBuy.DefaultCellStyle = dataGridViewCellStyle7;
            this.gridBuy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridBuy.EnableHeadersVisualStyles = false;
            this.gridBuy.Location = new System.Drawing.Point(0, 0);
            this.gridBuy.MultiSelect = false;
            this.gridBuy.Name = "gridBuy";
            this.gridBuy.ReadOnly = true;
            this.gridBuy.RowHeadersVisible = false;
            this.gridBuy.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gridBuy.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridBuy.ShowCellErrors = false;
            this.gridBuy.ShowCellToolTips = false;
            this.gridBuy.ShowEditingIcon = false;
            this.gridBuy.ShowRowErrors = false;
            this.gridBuy.Size = new System.Drawing.Size(382, 219);
            this.gridBuy.TabIndex = 0;
            this.gridBuy.TabStop = false;
            // 
            // colBuyID
            // 
            this.colBuyID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colBuyID.DefaultCellStyle = dataGridViewCellStyle2;
            this.colBuyID.HeaderText = "#";
            this.colBuyID.Name = "colBuyID";
            this.colBuyID.ReadOnly = true;
            this.colBuyID.Width = 41;
            // 
            // colBuyTime
            // 
            this.colBuyTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle3.Format = "G";
            dataGridViewCellStyle3.NullValue = null;
            this.colBuyTime.DefaultCellStyle = dataGridViewCellStyle3;
            this.colBuyTime.HeaderText = "Time";
            this.colBuyTime.Name = "colBuyTime";
            this.colBuyTime.ReadOnly = true;
            this.colBuyTime.Width = 66;
            // 
            // colBuyQuantity
            // 
            this.colBuyQuantity.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colBuyQuantity.DefaultCellStyle = dataGridViewCellStyle4;
            this.colBuyQuantity.HeaderText = "Buy";
            this.colBuyQuantity.Name = "colBuyQuantity";
            this.colBuyQuantity.ReadOnly = true;
            // 
            // colBuyPrice
            // 
            this.colBuyPrice.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colBuyPrice.DefaultCellStyle = dataGridViewCellStyle5;
            this.colBuyPrice.HeaderText = "Price";
            this.colBuyPrice.Name = "colBuyPrice";
            this.colBuyPrice.ReadOnly = true;
            // 
            // colBuyTotal
            // 
            this.colBuyTotal.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colBuyTotal.DefaultCellStyle = dataGridViewCellStyle6;
            this.colBuyTotal.HeaderText = "Total";
            this.colBuyTotal.Name = "colBuyTotal";
            this.colBuyTotal.ReadOnly = true;
            // 
            // gridSell
            // 
            this.gridSell.AllowUserToAddRows = false;
            this.gridSell.AllowUserToDeleteRows = false;
            this.gridSell.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridSell.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridSell.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.gridSell.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gridSell.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.gridSell.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(204)))), ((int)(((byte)(102)))));
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridSell.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.gridSell.ColumnHeadersHeight = 20;
            this.gridSell.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gridSell.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSellID,
            this.colSellTime,
            this.colSellQuantity,
            this.colSellPrice,
            this.colSellTotal});
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.Color.DarkGreen;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.Color.Chartreuse;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridSell.DefaultCellStyle = dataGridViewCellStyle14;
            this.gridSell.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridSell.EnableHeadersVisualStyles = false;
            this.gridSell.Location = new System.Drawing.Point(0, 0);
            this.gridSell.MultiSelect = false;
            this.gridSell.Name = "gridSell";
            this.gridSell.ReadOnly = true;
            this.gridSell.RowHeadersVisible = false;
            this.gridSell.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gridSell.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridSell.ShowCellErrors = false;
            this.gridSell.ShowCellToolTips = false;
            this.gridSell.ShowEditingIcon = false;
            this.gridSell.ShowRowErrors = false;
            this.gridSell.Size = new System.Drawing.Size(355, 219);
            this.gridSell.TabIndex = 1;
            this.gridSell.TabStop = false;
            // 
            // colSellID
            // 
            this.colSellID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colSellID.DefaultCellStyle = dataGridViewCellStyle9;
            this.colSellID.HeaderText = "#";
            this.colSellID.Name = "colSellID";
            this.colSellID.ReadOnly = true;
            this.colSellID.Width = 41;
            // 
            // colSellTime
            // 
            this.colSellTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle10.Format = "G";
            dataGridViewCellStyle10.NullValue = null;
            this.colSellTime.DefaultCellStyle = dataGridViewCellStyle10;
            this.colSellTime.HeaderText = "Time";
            this.colSellTime.Name = "colSellTime";
            this.colSellTime.ReadOnly = true;
            this.colSellTime.Width = 66;
            // 
            // colSellQuantity
            // 
            this.colSellQuantity.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colSellQuantity.DefaultCellStyle = dataGridViewCellStyle11;
            this.colSellQuantity.HeaderText = "Sell";
            this.colSellQuantity.Name = "colSellQuantity";
            this.colSellQuantity.ReadOnly = true;
            // 
            // colSellPrice
            // 
            this.colSellPrice.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colSellPrice.DefaultCellStyle = dataGridViewCellStyle12;
            this.colSellPrice.HeaderText = "Price";
            this.colSellPrice.Name = "colSellPrice";
            this.colSellPrice.ReadOnly = true;
            // 
            // colSellTotal
            // 
            this.colSellTotal.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle13.NullValue = null;
            this.colSellTotal.DefaultCellStyle = dataGridViewCellStyle13;
            this.colSellTotal.HeaderText = "Total";
            this.colSellTotal.Name = "colSellTotal";
            this.colSellTotal.ReadOnly = true;
            // 
            // formTradeHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.ClientSize = new System.Drawing.Size(745, 219);
            this.Controls.Add(this.splitMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "formTradeHistory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Trade History";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.formTradeHistory_FormClosing);
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridBuy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSell)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Grid gridBuy;
        private System.Windows.Forms.SplitContainer splitMain;
        private Grid gridSell;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBuyID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBuyTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBuyQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBuyPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBuyTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSellID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSellTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSellQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSellPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSellTotal;

    }
}