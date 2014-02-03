using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CoinPostGUI
{
    public partial class Tabs : TabControl
    {
        private bool clicking = false;
        private int old_count = 0;
        private List<Rectangle> tab_rects = new List<Rectangle>();
        public Tabs() : base()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            InitializeComponent();
            this.Appearance = TabAppearance.FlatButtons;
            this.Font = new Font("Arial",10.0f);
            this.ControlAdded += Tabs_ControlAdded;
            this.MouseClick += Tabs_MouseClick;
        }

        void Tabs_MouseClick(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < this.tab_rects.Count; i++)
            {
                if (this.tab_rects[i].Contains(e.Location))
                {
                    this.clicking = true;
                    this.TabIndex = i;
                    this.clicking = false;
                    break;
                }
            }
            return;
        }
        void Tabs_ControlAdded(object sender, ControlEventArgs e)
        {
            TabPage page = e.Control as TabPage;
            if (page != null)
            {
                page.Paint += Tabs_Paint;
            }
        }
        void Tabs_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.FromArgb(10, 10, 10));
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(Color.FromArgb(10, 10, 10));
            bool new_rects = (this.TabPages.Count != this.old_count);
            if (new_rects)
                this.tab_rects.Clear();
            this.old_count = this.TabPages.Count;
            for(int i=0; i<this.TabPages.Count; i++)
            {
                if (new_rects)
                {
                    Rectangle rect = this.GetTabRect(i);
                    rect = new Rectangle(rect.X + 5, rect.Y + 5, rect.Width, rect.Height);
                    this.tab_rects.Add(rect);
                }
                e.Graphics.FillRectangle(new SolidBrush(i == this.SelectedIndex ? Color.FromArgb(34,34,34) : Color.Black), this.tab_rects[i]);
                e.Graphics.DrawRectangle(new Pen(i == this.SelectedIndex ? Color.FromArgb(160,160,160) : Color.DarkGray), this.tab_rects[i]);
                SizeF string_size = e.Graphics.MeasureString(this.TabPages[i].Text, this.Font);
                e.Graphics.DrawString(this.TabPages[i].Text, this.Font, new SolidBrush(i == this.SelectedIndex ? CoinPostGUI.Properties.Settings.Default.BrightText : Color.Green), new Point(this.tab_rects[i].Left + this.tab_rects[i].Width / 2 - (int)(string_size.Width / 2.0), this.tab_rects[i].Top + this.tab_rects[i].Height / 2 - (int)(string_size.Height / 2.0)));
            }
        }
        protected override void OnTabIndexChanged(System.EventArgs e)
        {
            if(this.clicking)
                base.OnTabIndexChanged(e);
            return;
        }
    }
}
