namespace CoinPost
{
    partial class formGrid
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.dgviewBuys = new System.Windows.Forms.DataGridView();
            this.splitTrades = new System.Windows.Forms.SplitContainer();
            this.dgviewSells = new System.Windows.Forms.DataGridView();
            this.lblBid = new System.Windows.Forms.Label();
            this.lblAsk = new System.Windows.Forms.Label();
            this.colDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVolume = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgviewBuys)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitTrades)).BeginInit();
            this.splitTrades.Panel1.SuspendLayout();
            this.splitTrades.Panel2.SuspendLayout();
            this.splitTrades.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgviewSells)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 344);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(473, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // dgviewBuys
            // 
            this.dgviewBuys.AllowUserToAddRows = false;
            this.dgviewBuys.AllowUserToDeleteRows = false;
            this.dgviewBuys.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgviewBuys.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgviewBuys.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgviewBuys.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgviewBuys.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgviewBuys.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgviewBuys.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgviewBuys.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDate,
            this.colPrice,
            this.colVolume,
            this.colID});
            this.dgviewBuys.Location = new System.Drawing.Point(0, 27);
            this.dgviewBuys.Name = "dgviewBuys";
            this.dgviewBuys.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgviewBuys.RowHeadersVisible = false;
            this.dgviewBuys.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgviewBuys.ShowEditingIcon = false;
            this.dgviewBuys.Size = new System.Drawing.Size(241, 317);
            this.dgviewBuys.TabIndex = 1;
            // 
            // splitTrades
            // 
            this.splitTrades.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitTrades.Location = new System.Drawing.Point(0, 0);
            this.splitTrades.Name = "splitTrades";
            // 
            // splitTrades.Panel1
            // 
            this.splitTrades.Panel1.Controls.Add(this.lblBid);
            this.splitTrades.Panel1.Controls.Add(this.dgviewBuys);
            // 
            // splitTrades.Panel2
            // 
            this.splitTrades.Panel2.Controls.Add(this.lblAsk);
            this.splitTrades.Panel2.Controls.Add(this.dgviewSells);
            this.splitTrades.Size = new System.Drawing.Size(473, 344);
            this.splitTrades.SplitterDistance = 244;
            this.splitTrades.TabIndex = 2;
            // 
            // dgviewSells
            // 
            this.dgviewSells.AllowUserToAddRows = false;
            this.dgviewSells.AllowUserToDeleteRows = false;
            this.dgviewSells.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgviewSells.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgviewSells.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgviewSells.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgviewSells.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgviewSells.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgviewSells.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgviewSells.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.dgviewSells.Location = new System.Drawing.Point(-1, 27);
            this.dgviewSells.Name = "dgviewSells";
            this.dgviewSells.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgviewSells.RowHeadersVisible = false;
            this.dgviewSells.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgviewSells.ShowEditingIcon = false;
            this.dgviewSells.Size = new System.Drawing.Size(226, 317);
            this.dgviewSells.TabIndex = 2;
            // 
            // lblBid
            // 
            this.lblBid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBid.Location = new System.Drawing.Point(3, 0);
            this.lblBid.Name = "lblBid";
            this.lblBid.Size = new System.Drawing.Size(238, 24);
            this.lblBid.TabIndex = 2;
            this.lblBid.Text = "BID";
            this.lblBid.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAsk
            // 
            this.lblAsk.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAsk.Location = new System.Drawing.Point(-1, 0);
            this.lblAsk.Name = "lblAsk";
            this.lblAsk.Size = new System.Drawing.Size(226, 24);
            this.lblAsk.TabIndex = 3;
            this.lblAsk.Text = "ASK";
            this.lblAsk.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // colDate
            // 
            this.colDate.HeaderText = "Date";
            this.colDate.Name = "colDate";
            this.colDate.ReadOnly = true;
            // 
            // colPrice
            // 
            this.colPrice.HeaderText = "Price";
            this.colPrice.Name = "colPrice";
            this.colPrice.ReadOnly = true;
            // 
            // colVolume
            // 
            this.colVolume.HeaderText = "Volume";
            this.colVolume.Name = "colVolume";
            this.colVolume.ReadOnly = true;
            // 
            // colID
            // 
            this.colID.HeaderText = "ID";
            this.colID.Name = "colID";
            this.colID.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Date";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Price";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Volume";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "ID";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // formGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 366);
            this.Controls.Add(this.splitTrades);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "formGrid";
            this.Text = "CoinPost - Trades";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.formGrid_FormClosing);
            this.Load += new System.EventHandler(this.formGrid_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgviewBuys)).EndInit();
            this.splitTrades.Panel1.ResumeLayout(false);
            this.splitTrades.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitTrades)).EndInit();
            this.splitTrades.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgviewSells)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.DataGridView dgviewBuys;
        private System.Windows.Forms.SplitContainer splitTrades;
        private System.Windows.Forms.DataGridView dgviewSells;
        private System.Windows.Forms.Label lblBid;
        private System.Windows.Forms.Label lblAsk;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVolume;
        private System.Windows.Forms.DataGridViewTextBoxColumn colID;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    }
}