namespace CoinPost
{
    partial class formMain
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnBuy = new System.Windows.Forms.Button();
            this.lblBid = new System.Windows.Forms.Label();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.groupTrade = new System.Windows.Forms.GroupBox();
            this.lklblLastPrice = new System.Windows.Forms.LinkLabel();
            this.lblCurrentPrice = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnMaxBuy = new System.Windows.Forms.Button();
            this.btnMaxSell = new System.Windows.Forms.Button();
            this.btnSell = new System.Windows.Forms.Button();
            this.comboTargetCurrency = new System.Windows.Forms.ComboBox();
            this.comboSourceCurrency = new System.Windows.Forms.ComboBox();
            this.lblAsk = new System.Windows.Forms.Label();
            this.timerModifyOrder = new System.Windows.Forms.Timer(this.components);
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.webBrowser = new Gecko.GeckoWebBrowser();
            this.gridBalances = new CoinPost.Grid();
            this.colCurrency = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBalance = new System.Windows.Forms.DataGridViewLinkColumn();
            this.splitActiveOrders = new System.Windows.Forms.SplitContainer();
            this.gridBuy = new CoinPost.Grid();
            this.colBuyID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBuying = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBuyPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBuyTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCancelBuy = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colModifyBuy = new System.Windows.Forms.DataGridViewButtonColumn();
            this.gridSell = new CoinPost.Grid();
            this.colSellID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSelling = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSellPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSellTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCancelSell = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colModifySell = new System.Windows.Forms.DataGridViewButtonColumn();
            this.lklblShowAllHistory = new System.Windows.Forms.ToolStripStatusLabel();
            this.stripMain = new System.Windows.Forms.StatusStrip();
            this.lblBlank = new System.Windows.Forms.ToolStripStatusLabel();
            this.ttipOrderAssist = new System.Windows.Forms.ToolTip(this.components);
            this.groupTrade.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridBalances)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitActiveOrders)).BeginInit();
            this.splitActiveOrders.Panel1.SuspendLayout();
            this.splitActiveOrders.Panel2.SuspendLayout();
            this.splitActiveOrders.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridBuy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSell)).BeginInit();
            this.stripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnBuy
            // 
            this.btnBuy.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnBuy.AutoEllipsis = true;
            this.btnBuy.Enabled = false;
            this.btnBuy.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnBuy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuy.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(204)))), ((int)(((byte)(102)))));
            this.btnBuy.Location = new System.Drawing.Point(224, 126);
            this.btnBuy.Name = "btnBuy";
            this.btnBuy.Size = new System.Drawing.Size(69, 29);
            this.btnBuy.TabIndex = 5;
            this.btnBuy.Text = "Buy";
            this.btnBuy.UseVisualStyleBackColor = true;
            this.btnBuy.Click += new System.EventHandler(this.btnBuy_Click);
            // 
            // lblBid
            // 
            this.lblBid.AutoSize = true;
            this.lblBid.Font = new System.Drawing.Font("Arial", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBid.Location = new System.Drawing.Point(7, 47);
            this.lblBid.Name = "lblBid";
            this.lblBid.Size = new System.Drawing.Size(64, 16);
            this.lblBid.TabIndex = 6;
            this.lblBid.Text = "Quantity:";
            // 
            // txtQuantity
            // 
            this.txtQuantity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(125)))), ((int)(((byte)(125)))));
            this.txtQuantity.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtQuantity.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtQuantity.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuantity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(40)))), ((int)(((byte)(0)))));
            this.txtQuantity.Location = new System.Drawing.Point(77, 49);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(140, 15);
            this.txtQuantity.TabIndex = 8;
            this.txtQuantity.Text = "0.0";
            this.txtQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtQuantity.TextChanged += new System.EventHandler(this.txtQuantity_Update);
            // 
            // txtPrice
            // 
            this.txtPrice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(125)))), ((int)(((byte)(125)))));
            this.txtPrice.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPrice.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(40)))), ((int)(((byte)(0)))));
            this.txtPrice.Location = new System.Drawing.Point(77, 76);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(140, 15);
            this.txtPrice.TabIndex = 9;
            this.txtPrice.Text = "0.0";
            this.txtPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPrice.TextChanged += new System.EventHandler(this.txtPrice_Update);
            // 
            // groupTrade
            // 
            this.groupTrade.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupTrade.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.groupTrade.Controls.Add(this.lklblLastPrice);
            this.groupTrade.Controls.Add(this.lblCurrentPrice);
            this.groupTrade.Controls.Add(this.txtTotal);
            this.groupTrade.Controls.Add(this.lblTotal);
            this.groupTrade.Controls.Add(this.btnMaxBuy);
            this.groupTrade.Controls.Add(this.btnMaxSell);
            this.groupTrade.Controls.Add(this.btnSell);
            this.groupTrade.Controls.Add(this.comboTargetCurrency);
            this.groupTrade.Controls.Add(this.btnBuy);
            this.groupTrade.Controls.Add(this.comboSourceCurrency);
            this.groupTrade.Controls.Add(this.lblBid);
            this.groupTrade.Controls.Add(this.txtPrice);
            this.groupTrade.Controls.Add(this.lblAsk);
            this.groupTrade.Controls.Add(this.txtQuantity);
            this.groupTrade.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupTrade.Font = new System.Drawing.Font("Arial", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupTrade.ForeColor = System.Drawing.Color.Green;
            this.groupTrade.Location = new System.Drawing.Point(11, 3);
            this.groupTrade.MaximumSize = new System.Drawing.Size(474, 9000);
            this.groupTrade.MinimumSize = new System.Drawing.Size(305, 182);
            this.groupTrade.Name = "groupTrade";
            this.groupTrade.Size = new System.Drawing.Size(305, 182);
            this.groupTrade.TabIndex = 11;
            this.groupTrade.TabStop = false;
            this.groupTrade.Text = "Trade";
            // 
            // lklblLastPrice
            // 
            this.lklblLastPrice.Font = new System.Drawing.Font("Arial Black", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lklblLastPrice.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(182)))), ((int)(((byte)(255)))));
            this.lklblLastPrice.Location = new System.Drawing.Point(111, 21);
            this.lklblLastPrice.Name = "lklblLastPrice";
            this.lklblLastPrice.Size = new System.Drawing.Size(93, 21);
            this.lklblLastPrice.TabIndex = 19;
            this.lklblLastPrice.TabStop = true;
            this.lklblLastPrice.Text = "0.115";
            this.lklblLastPrice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lklblLastPrice.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lklblLastPrice_LinkClicked);
            // 
            // lblCurrentPrice
            // 
            this.lblCurrentPrice.Font = new System.Drawing.Font("Arial", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentPrice.Location = new System.Drawing.Point(6, 22);
            this.lblCurrentPrice.Name = "lblCurrentPrice";
            this.lblCurrentPrice.Size = new System.Drawing.Size(110, 18);
            this.lblCurrentPrice.TabIndex = 18;
            this.lblCurrentPrice.Text = "Current Price:";
            this.lblCurrentPrice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtTotal
            // 
            this.txtTotal.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.txtTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.txtTotal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTotal.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(204)))), ((int)(((byte)(102)))));
            this.txtTotal.Location = new System.Drawing.Point(77, 97);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(140, 15);
            this.txtTotal.TabIndex = 16;
            this.txtTotal.Text = "0.0";
            this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Arial", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(27, 96);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(43, 16);
            this.lblTotal.TabIndex = 15;
            this.lblTotal.Text = "Total:";
            // 
            // btnMaxBuy
            // 
            this.btnMaxBuy.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnMaxBuy.Enabled = false;
            this.btnMaxBuy.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnMaxBuy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMaxBuy.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMaxBuy.ForeColor = System.Drawing.Color.DarkGreen;
            this.btnMaxBuy.Location = new System.Drawing.Point(176, 126);
            this.btnMaxBuy.Name = "btnMaxBuy";
            this.btnMaxBuy.Size = new System.Drawing.Size(41, 29);
            this.btnMaxBuy.TabIndex = 14;
            this.btnMaxBuy.Text = "Max";
            this.btnMaxBuy.UseVisualStyleBackColor = true;
            this.btnMaxBuy.Click += new System.EventHandler(this.btnMaxBuy_Click);
            // 
            // btnMaxSell
            // 
            this.btnMaxSell.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnMaxSell.Enabled = false;
            this.btnMaxSell.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnMaxSell.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMaxSell.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMaxSell.ForeColor = System.Drawing.Color.DarkGreen;
            this.btnMaxSell.Location = new System.Drawing.Point(30, 126);
            this.btnMaxSell.Name = "btnMaxSell";
            this.btnMaxSell.Size = new System.Drawing.Size(40, 29);
            this.btnMaxSell.TabIndex = 13;
            this.btnMaxSell.Text = "Max";
            this.btnMaxSell.UseVisualStyleBackColor = true;
            this.btnMaxSell.Click += new System.EventHandler(this.btnMaxSell_Click);
            // 
            // btnSell
            // 
            this.btnSell.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSell.AutoEllipsis = true;
            this.btnSell.Enabled = false;
            this.btnSell.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSell.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSell.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSell.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(204)))), ((int)(((byte)(102)))));
            this.btnSell.Location = new System.Drawing.Point(77, 126);
            this.btnSell.Name = "btnSell";
            this.btnSell.Size = new System.Drawing.Size(69, 29);
            this.btnSell.TabIndex = 12;
            this.btnSell.Text = "Sell";
            this.ttipOrderAssist.SetToolTip(this.btnSell, "Recent Purchases (Button)");
            this.btnSell.UseVisualStyleBackColor = true;
            this.btnSell.Click += new System.EventHandler(this.btnSell_Click);
            // 
            // comboTargetCurrency
            // 
            this.comboTargetCurrency.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.comboTargetCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboTargetCurrency.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboTargetCurrency.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboTargetCurrency.ForeColor = System.Drawing.Color.DarkGreen;
            this.comboTargetCurrency.FormattingEnabled = true;
            this.comboTargetCurrency.Items.AddRange(new object[] {
            "BTC",
            "USD"});
            this.comboTargetCurrency.Location = new System.Drawing.Point(226, 74);
            this.comboTargetCurrency.Name = "comboTargetCurrency";
            this.comboTargetCurrency.Size = new System.Drawing.Size(67, 21);
            this.comboTargetCurrency.Sorted = true;
            this.comboTargetCurrency.TabIndex = 11;
            this.comboTargetCurrency.SelectedIndexChanged += new System.EventHandler(this.comboTargetCurrency_SelectedIndexChanged);
            // 
            // comboSourceCurrency
            // 
            this.comboSourceCurrency.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.comboSourceCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboSourceCurrency.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboSourceCurrency.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboSourceCurrency.ForeColor = System.Drawing.Color.DarkGreen;
            this.comboSourceCurrency.FormattingEnabled = true;
            this.comboSourceCurrency.Items.AddRange(new object[] {
            "BTC",
            "FTC",
            "LTC",
            "NMC",
            "NVC",
            "PPC",
            "TRC"});
            this.comboSourceCurrency.Location = new System.Drawing.Point(226, 47);
            this.comboSourceCurrency.Name = "comboSourceCurrency";
            this.comboSourceCurrency.Size = new System.Drawing.Size(67, 21);
            this.comboSourceCurrency.Sorted = true;
            this.comboSourceCurrency.TabIndex = 10;
            this.comboSourceCurrency.SelectedIndexChanged += new System.EventHandler(this.comboBidCurrency_SelectedIndexChanged);
            // 
            // lblAsk
            // 
            this.lblAsk.AutoSize = true;
            this.lblAsk.Font = new System.Drawing.Font("Arial", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAsk.Location = new System.Drawing.Point(28, 75);
            this.lblAsk.Name = "lblAsk";
            this.lblAsk.Size = new System.Drawing.Size(43, 16);
            this.lblAsk.TabIndex = 7;
            this.lblAsk.Text = "Price:";
            // 
            // timerModifyOrder
            // 
            this.timerModifyOrder.Interval = 2000;
            this.timerModifyOrder.Tick += new System.EventHandler(this.timerModifyOrder_Tick);
            // 
            // splitMain
            // 
            this.splitMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.splitMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitMain.Location = new System.Drawing.Point(0, 0);
            this.splitMain.Margin = new System.Windows.Forms.Padding(0);
            this.splitMain.Name = "splitMain";
            this.splitMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.splitMain.Panel1.Controls.Add(this.webBrowser);
            this.splitMain.Panel1MinSize = 300;
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.splitMain.Panel2.Controls.Add(this.gridBalances);
            this.splitMain.Panel2.Controls.Add(this.splitActiveOrders);
            this.splitMain.Panel2.Controls.Add(this.groupTrade);
            this.splitMain.Panel2MinSize = 200;
            this.splitMain.Size = new System.Drawing.Size(953, 650);
            this.splitMain.SplitterDistance = 438;
            this.splitMain.SplitterWidth = 8;
            this.splitMain.TabIndex = 13;
            // 
            // webBrowser
            // 
            this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser.Location = new System.Drawing.Point(0, 0);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(951, 436);
            this.webBrowser.TabIndex = 0;
            this.webBrowser.UseHttpActivityObserver = false;
            // 
            // gridBalances
            // 
            this.gridBalances.AllowUserToAddRows = false;
            this.gridBalances.AllowUserToDeleteRows = false;
            this.gridBalances.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gridBalances.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridBalances.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridBalances.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.gridBalances.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gridBalances.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.gridBalances.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(204)))), ((int)(((byte)(102)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridBalances.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridBalances.ColumnHeadersHeight = 20;
            this.gridBalances.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gridBalances.ColumnHeadersVisible = false;
            this.gridBalances.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCurrency,
            this.colBalance});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.DarkGreen;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Chartreuse;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridBalances.DefaultCellStyle = dataGridViewCellStyle2;
            this.gridBalances.EnableHeadersVisualStyles = false;
            this.gridBalances.Location = new System.Drawing.Point(322, 10);
            this.gridBalances.MultiSelect = false;
            this.gridBalances.Name = "gridBalances";
            this.gridBalances.ReadOnly = true;
            this.gridBalances.RowHeadersVisible = false;
            this.gridBalances.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gridBalances.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridBalances.ShowCellErrors = false;
            this.gridBalances.ShowCellToolTips = false;
            this.gridBalances.ShowEditingIcon = false;
            this.gridBalances.ShowRowErrors = false;
            this.gridBalances.Size = new System.Drawing.Size(144, 175);
            this.gridBalances.TabIndex = 0;
            this.gridBalances.TabStop = false;
            this.gridBalances.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridBalances_CellContentClick);
            this.gridBalances.SelectionChanged += new System.EventHandler(this.gridBalances_SelectionChanged);
            // 
            // colCurrency
            // 
            this.colCurrency.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.colCurrency.HeaderText = "Currency";
            this.colCurrency.Name = "colCurrency";
            this.colCurrency.ReadOnly = true;
            this.colCurrency.Width = 5;
            // 
            // colBalance
            // 
            this.colBalance.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colBalance.HeaderText = "Balance";
            this.colBalance.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(182)))), ((int)(((byte)(255)))));
            this.colBalance.Name = "colBalance";
            this.colBalance.ReadOnly = true;
            // 
            // splitActiveOrders
            // 
            this.splitActiveOrders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitActiveOrders.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitActiveOrders.Location = new System.Drawing.Point(471, 9);
            this.splitActiveOrders.Name = "splitActiveOrders";
            // 
            // splitActiveOrders.Panel1
            // 
            this.splitActiveOrders.Panel1.Controls.Add(this.gridBuy);
            this.splitActiveOrders.Panel1MinSize = 100;
            // 
            // splitActiveOrders.Panel2
            // 
            this.splitActiveOrders.Panel2.Controls.Add(this.gridSell);
            this.splitActiveOrders.Panel2MinSize = 100;
            this.splitActiveOrders.Size = new System.Drawing.Size(469, 176);
            this.splitActiveOrders.SplitterDistance = 222;
            this.splitActiveOrders.TabIndex = 13;
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
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(204)))), ((int)(((byte)(102)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridBuy.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gridBuy.ColumnHeadersHeight = 20;
            this.gridBuy.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gridBuy.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colBuyID,
            this.colBuying,
            this.colBuyPrice,
            this.colBuyTotal,
            this.colCancelBuy,
            this.colModifyBuy});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.DarkGreen;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Chartreuse;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridBuy.DefaultCellStyle = dataGridViewCellStyle8;
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
            this.gridBuy.Size = new System.Drawing.Size(220, 174);
            this.gridBuy.TabIndex = 0;
            this.gridBuy.TabStop = false;
            this.gridBuy.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridBuySell_CellContentClick);
            // 
            // colBuyID
            // 
            this.colBuyID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colBuyID.DefaultCellStyle = dataGridViewCellStyle4;
            this.colBuyID.HeaderText = "#";
            this.colBuyID.Name = "colBuyID";
            this.colBuyID.ReadOnly = true;
            this.colBuyID.Width = 37;
            // 
            // colBuying
            // 
            this.colBuying.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colBuying.DefaultCellStyle = dataGridViewCellStyle5;
            this.colBuying.HeaderText = "Buy";
            this.colBuying.Name = "colBuying";
            this.colBuying.ReadOnly = true;
            // 
            // colBuyPrice
            // 
            this.colBuyPrice.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colBuyPrice.DefaultCellStyle = dataGridViewCellStyle6;
            this.colBuyPrice.HeaderText = "Price";
            this.colBuyPrice.Name = "colBuyPrice";
            this.colBuyPrice.ReadOnly = true;
            // 
            // colBuyTotal
            // 
            this.colBuyTotal.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colBuyTotal.DefaultCellStyle = dataGridViewCellStyle7;
            this.colBuyTotal.HeaderText = "Total";
            this.colBuyTotal.Name = "colBuyTotal";
            this.colBuyTotal.ReadOnly = true;
            // 
            // colCancelBuy
            // 
            this.colCancelBuy.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colCancelBuy.HeaderText = "";
            this.colCancelBuy.Name = "colCancelBuy";
            this.colCancelBuy.ReadOnly = true;
            this.colCancelBuy.Width = 5;
            // 
            // colModifyBuy
            // 
            this.colModifyBuy.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colModifyBuy.HeaderText = "";
            this.colModifyBuy.Name = "colModifyBuy";
            this.colModifyBuy.ReadOnly = true;
            this.colModifyBuy.Width = 5;
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
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(204)))), ((int)(((byte)(102)))));
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridSell.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.gridSell.ColumnHeadersHeight = 20;
            this.gridSell.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gridSell.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSellID,
            this.colSelling,
            this.colSellPrice,
            this.colSellTotal,
            this.colCancelSell,
            this.colModifySell});
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.gridSell.Size = new System.Drawing.Size(241, 174);
            this.gridSell.TabIndex = 0;
            this.gridSell.TabStop = false;
            this.gridSell.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridBuySell_CellContentClick);
            // 
            // colSellID
            // 
            this.colSellID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colSellID.DefaultCellStyle = dataGridViewCellStyle10;
            this.colSellID.HeaderText = "#";
            this.colSellID.Name = "colSellID";
            this.colSellID.ReadOnly = true;
            this.colSellID.Width = 37;
            // 
            // colSelling
            // 
            this.colSelling.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colSelling.DefaultCellStyle = dataGridViewCellStyle11;
            this.colSelling.HeaderText = "Sell";
            this.colSelling.Name = "colSelling";
            this.colSelling.ReadOnly = true;
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
            this.colSellTotal.DefaultCellStyle = dataGridViewCellStyle13;
            this.colSellTotal.HeaderText = "Total";
            this.colSellTotal.Name = "colSellTotal";
            this.colSellTotal.ReadOnly = true;
            // 
            // colCancelSell
            // 
            this.colCancelSell.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colCancelSell.HeaderText = "";
            this.colCancelSell.Name = "colCancelSell";
            this.colCancelSell.ReadOnly = true;
            this.colCancelSell.Width = 5;
            // 
            // colModifySell
            // 
            this.colModifySell.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colModifySell.HeaderText = "";
            this.colModifySell.Name = "colModifySell";
            this.colModifySell.ReadOnly = true;
            this.colModifySell.Width = 5;
            // 
            // lklblShowAllHistory
            // 
            this.lklblShowAllHistory.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.lklblShowAllHistory.IsLink = true;
            this.lklblShowAllHistory.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(182)))), ((int)(((byte)(255)))));
            this.lklblShowAllHistory.Name = "lklblShowAllHistory";
            this.lklblShowAllHistory.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lklblShowAllHistory.Size = new System.Drawing.Size(116, 17);
            this.lklblShowAllHistory.Text = "SHOW Trade History";
            this.lklblShowAllHistory.Click += new System.EventHandler(this.lklblShowAllHistory_Click);
            // 
            // stripMain
            // 
            this.stripMain.BackColor = System.Drawing.Color.Transparent;
            this.stripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblBlank,
            this.lklblShowAllHistory});
            this.stripMain.Location = new System.Drawing.Point(0, 650);
            this.stripMain.Name = "stripMain";
            this.stripMain.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.stripMain.Size = new System.Drawing.Size(953, 22);
            this.stripMain.SizingGrip = false;
            this.stripMain.TabIndex = 14;
            this.stripMain.Text = "statusStrip1";
            // 
            // lblBlank
            // 
            this.lblBlank.Name = "lblBlank";
            this.lblBlank.Size = new System.Drawing.Size(19, 17);
            this.lblBlank.Text = "    ";
            // 
            // ttipOrderAssist
            // 
            this.ttipOrderAssist.AutoPopDelay = 15000;
            this.ttipOrderAssist.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.ttipOrderAssist.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(204)))), ((int)(((byte)(102)))));
            this.ttipOrderAssist.InitialDelay = 500;
            this.ttipOrderAssist.ReshowDelay = 100;
            this.ttipOrderAssist.Tag = "";
            // 
            // formMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.ClientSize = new System.Drawing.Size(953, 672);
            this.Controls.Add(this.stripMain);
            this.Controls.Add(this.splitMain);
            this.MinimumSize = new System.Drawing.Size(900, 710);
            this.Name = "formMain";
            this.Text = "CoinPost";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.formMain_FormClosing);
            this.Load += new System.EventHandler(this.formMain_Load);
            this.groupTrade.ResumeLayout(false);
            this.groupTrade.PerformLayout();
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridBalances)).EndInit();
            this.splitActiveOrders.Panel1.ResumeLayout(false);
            this.splitActiveOrders.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitActiveOrders)).EndInit();
            this.splitActiveOrders.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridBuy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSell)).EndInit();
            this.stripMain.ResumeLayout(false);
            this.stripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBuy;
        private System.Windows.Forms.Label lblBid;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.GroupBox groupTrade;
        private System.Windows.Forms.ComboBox comboTargetCurrency;
        private System.Windows.Forms.ComboBox comboSourceCurrency;
        private System.Windows.Forms.Label lblAsk;
        private System.Windows.Forms.Button btnSell;
        private System.Windows.Forms.Button btnMaxSell;
        private System.Windows.Forms.Button btnMaxBuy;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Timer timerModifyOrder;
        private System.Windows.Forms.Label lblCurrentPrice;
        private System.Windows.Forms.LinkLabel lklblLastPrice;
        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.ToolStripStatusLabel lklblShowAllHistory;
        private System.Windows.Forms.StatusStrip stripMain;
        private System.Windows.Forms.SplitContainer splitActiveOrders;
        private System.Windows.Forms.ToolStripStatusLabel lblBlank;
        private Grid gridBalances;
        private Grid gridBuy;
        private Grid gridSell;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCurrency;
        private System.Windows.Forms.DataGridViewLinkColumn colBalance;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSellID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSelling;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSellPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSellTotal;
        private System.Windows.Forms.DataGridViewButtonColumn colCancelSell;
        private System.Windows.Forms.DataGridViewButtonColumn colModifySell;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBuyID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBuying;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBuyPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBuyTotal;
        private System.Windows.Forms.DataGridViewButtonColumn colCancelBuy;
        private System.Windows.Forms.DataGridViewButtonColumn colModifyBuy;
        private System.Windows.Forms.ToolTip ttipOrderAssist;
        private Gecko.GeckoWebBrowser webBrowser;
    }
}

