namespace CoinPost
{
    partial class formBalance
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
            this.graphMain = new ZedGraph.ZedGraphControl();
            this.SuspendLayout();
            // 
            // graphMain
            // 
            this.graphMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.graphMain.BackColor = System.Drawing.Color.Transparent;
            this.graphMain.Location = new System.Drawing.Point(1, 30);
            this.graphMain.Margin = new System.Windows.Forms.Padding(0);
            this.graphMain.Name = "graphMain";
            this.graphMain.ScrollGrace = 0D;
            this.graphMain.ScrollMaxX = 0D;
            this.graphMain.ScrollMaxY = 0D;
            this.graphMain.ScrollMaxY2 = 0D;
            this.graphMain.ScrollMinX = 0D;
            this.graphMain.ScrollMinY = 0D;
            this.graphMain.ScrollMinY2 = 0D;
            this.graphMain.Size = new System.Drawing.Size(283, 232);
            this.graphMain.TabIndex = 1;
            this.graphMain.DoubleClick += new System.EventHandler(this.graphMain_DoubleClick);
            // 
            // formBalance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.graphMain);
            this.Name = "formBalance";
            this.Text = "formBalance";
            this.Load += new System.EventHandler(this.formBalance_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ZedGraph.ZedGraphControl graphMain;

    }
}