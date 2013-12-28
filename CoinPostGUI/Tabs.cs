using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CoinPostGUI
{
    public partial class Tabs : TabControl
    {
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
            int counter=0;
            foreach (TabPage page in this.TabPages)
            {
                Rectangle rect=this.GetTabRect(counter);
                e.Graphics.FillRectangle(new SolidBrush(counter == this.SelectedIndex ? Color.FromArgb(34,34,34) : Color.Black), rect);
                e.Graphics.DrawRectangle(new Pen(counter==this.SelectedIndex?Color.White:Color.DarkGray), rect);
                e.Graphics.DrawString(page.Text, this.Font, new SolidBrush(counter==this.SelectedIndex?CoinPostGUI.Properties.Settings.Default.BrightText:Color.Green), new Point(rect.Left+5, rect.Top+5));
                counter++;
            }
        }
    }
}
