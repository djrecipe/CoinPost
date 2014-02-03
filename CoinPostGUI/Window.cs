using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoinPostGUI
{
    public partial class Window : Form
    {
        private bool x_hover = false;
        private bool north = false, south = false, east = false, west = false;
        Point old_point = new Point(0,0);
        Point old_location = new Point(0, 0);
        Size old_size = new Size(0, 0);
        [Browsable(false)]
        new public bool ControlBox
        {
            get { return base.ControlBox; }
            private set
            {
                base.ControlBox = value;
                return;
            }
        }
        [Browsable(false)]
        new public FormBorderStyle FormBorderStyle
        {
            get
            {
                return base.FormBorderStyle;
            }
            private set
            {
                base.FormBorderStyle = value;
                return;
            }
        }
        private Color _BorderColor = Color.FromArgb(104, 104, 104);
        [Browsable(true)]
        [DisplayName("Border Color")]
        [Description("If 'Border Enabled' is set to true, this determines the border color.")]
        public Color BorderColor
        {
            get
            {
                return this._BorderColor;
            }
            set
            {
                if (value == this._BorderColor)
                    return;
                this._BorderColor = value;
                if (this.BorderEnabled)
                    this.Invalidate();
                return;
            }
        }
        private bool _BorderEnabled = true;
        [Browsable(true)]
        [DisplayName("Border Enabled")]
        [Description("If true, a border is drawn around the edge of the form.")]
        [DefaultValue(true)]
        public bool BorderEnabled
        {
            get
            {
                return this._BorderEnabled;
            }
            set
            {
                if (value == this._BorderEnabled)
                    return;
                this._BorderEnabled = value;
                this.Invalidate();
                return;
            }
        }
        private float _GradientAngle = 90.0f;
        [Browsable(true)]
        [DisplayName("Gradient Angle")]
        [Description("If 'Gradient Enabled' is set to true, this angle determines the gradient direction.")]
        [DefaultValue(90.0f)]
        public float GradientAngle
        {
            get { return this._GradientAngle; }
            set
            {
                if (value == this._GradientAngle)
                    return;
                this._GradientAngle = value;
                if(this.GradientEnabled)
                    this.Invalidate();
                return;
            }
        }
        private bool _GradientEnabled = true;
        [Browsable(true)]
        [DisplayName("Gradient Enabled")]
        [Description("If true, a background gradient is used instead of a solid color.")]
        [DefaultValue(true)]
        public bool GradientEnabled
        {
            get { return this._GradientEnabled; }
            set
            {
                if (value == this._GradientEnabled)
                    return;
                this._GradientEnabled = value;
                this.Invalidate();
                return;
            }
        }
        private Color _GradientEnd = Color.FromArgb(26,26,26);
        [Browsable(true)]
        [DisplayName("Gradient End Color")]
        [Description("If 'Gradient Enabled' is set to true, this is the second color of the gradient.")]
        public Color GradientEnd
        {
            get { return this._GradientEnd; }
            set
            {
                if (value == this._GradientEnd)
                    return;
                this._GradientEnd = value;
                if (this.GradientEnabled)
                    this.Invalidate();
                return;
            }
        }
        private Color _GradientStart = Color.FromArgb(66, 66, 66);
        [Browsable(true)]
        [DisplayName("Gradient Start Color")]
        [Description("If 'Gradient Enabled' is set to true, this is the first color of the gradient.")]
        public Color GradientStart
        {
            get { return this._GradientStart; }
            set
            {
                if (value == this._GradientStart)
                    return;
                this._GradientStart = value;
                if (this.GradientEnabled)
                    this.Invalidate();
                return;
            }
        }
        private bool _Resizable = true;
        public bool Resizable
        {
            get { return this._Resizable; }
            set
            {
                this._Resizable = value;
                return;
            }
        }
        private bool _ResizeOnDoubleClick = true;
        [Browsable(true)]
        [DisplayName("Resize On Double Click")]
        [Description("If true, the window state will change between 'normal' and 'maximized' when the form is double clicked.")]
        [DefaultValue(true)]
        public bool ResizeOnDoubleClick
        {
            get { return this._ResizeOnDoubleClick; }
            set
            {
                if (value == this.ResizeOnDoubleClick)
                    return;
                if(value)
                    this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Window_MouseDoubleClick);
                else
                    this.MouseDoubleClick -= new System.Windows.Forms.MouseEventHandler(this.Window_MouseDoubleClick);
                this._ResizeOnDoubleClick = value;
                return;
            }
        }
        private bool _XEnabled = true;
        [Browsable(true)]
        [DisplayName("X Enabled")]
        [Description("If true, displays a close button ('X') in the top right of the form.")]
        [DefaultValue(true)]
        public bool XEnabled
        {
            get { return this._XEnabled; }
            set
            {
                if (value == this._XEnabled)
                    return;
                this._XEnabled = value;
                this.Invalidate();
                return;
            }
        }
        public Window()
        {
            this.InitializeComponent();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            return;
        }  
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (this.GradientEnabled)
            {
                e.Graphics.FillRectangle(new LinearGradientBrush(this.DisplayRectangle,this.GradientStart,this.GradientEnd,this.GradientAngle),this.DisplayRectangle);
            }
            else
            {
                e.Graphics.FillRectangle(new SolidBrush(this.BackColor),this.DisplayRectangle);
            }
            return;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if(this.XEnabled)
            {
                int left = this.Width-20, right = this.Width-10, top = 10, bottom = 20;
                e.Graphics.DrawLine(new Pen(new SolidBrush(this.x_hover ? Color.Black : Color.FromArgb(120, 120, 120)), 2.0f), new Point(left + 2, top), new Point(right + 2, bottom));
                e.Graphics.DrawLine(new Pen(new SolidBrush(this.x_hover ? Color.Black : Color.FromArgb(120, 120, 120)), 2.0f), new Point(right + 2, top), new Point(left + 2, bottom));
                e.Graphics.DrawLine(new Pen(new SolidBrush(this.x_hover ? Color.FromArgb(120, 120, 120) : Color.Black), 2.0f), new Point(left, top), new Point(right, bottom));
                e.Graphics.DrawLine(new Pen(new SolidBrush(this.x_hover ? Color.FromArgb(120, 120, 120) : Color.Black), 2.0f), new Point(right, top), new Point(left, bottom));
            }
            if(this.BorderEnabled)
                e.Graphics.DrawRectangle(new Pen(new SolidBrush(this.BorderColor)), new Rectangle(this.DisplayRectangle.X,this.DisplayRectangle.Y,this.DisplayRectangle.Width-1,this.DisplayRectangle.Height-1));
            return;
        }
        private void Window_Activated(object sender, EventArgs e)
        {
            this.ResumeLayout(true);
            return;
        }
        private void Window_Leave(object sender, EventArgs e)
        {
            this.ResumeLayout(true);
            return;
        }
        private void Window_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.X > this.Width - 20 && e.X < this.Width - 10 && e.Y > 10 && e.Y < 20)
                this.BeginInvoke((MethodInvoker) delegate{this.Close();});
            return;
        }
        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && this.WindowState == FormWindowState.Normal)
            {
                Point new_point = this.PointToScreen(e.Location);
                Point delta = new Point(new_point.X - old_point.X, new_point.Y - old_point.Y);
                bool not_resizing = true;
                if (this.north)
                {
                    this.Location = new Point(this.Location.X, this.old_location.Y + delta.Y);
                    this.Size = new Size(this.Size.Width, this.old_size.Height - delta.Y);
                    not_resizing = false;
                }
                else if (this.south)
                {
                    this.Size = new Size(this.Size.Width, this.old_size.Height + delta.Y);
                    not_resizing = false;
                }
                if (this.west)
                {
                    this.Location = new Point(this.old_location.X + delta.X, this.Location.Y);
                    this.Size = new Size(this.old_size.Width - delta.X, this.Size.Height);
                    not_resizing = false;
                }
                else if (this.east)
                {
                    this.Size = new Size(this.old_size.Width + delta.X, this.Size.Height);
                    not_resizing = false;
                }
                if (not_resizing)
                {
                    this.Location = new Point(this.old_location.X + delta.X, this.old_location.Y + delta.Y);
                    if (this.Cursor != Cursors.SizeAll)
                        this.Cursor = Cursors.SizeAll;
                }
            }
            else if (e.Button == MouseButtons.None && this.GetChildAtPoint(e.Location) == null)
            {
                if (e.X > this.Width - 20 && e.X < this.Width - 10 && e.Y > 10 && e.Y < 20)
                {
                    if (this.Cursor != Cursors.Hand)
                        this.Cursor = Cursors.Hand;
                    if (!this.x_hover)
                    {
                        this.x_hover = true;
                        this.Invalidate();
                    }
                }
                else if (this.Resizable && this.WindowState == FormWindowState.Normal)
                {
                    this.north = e.Y < 5; this.south = e.Y > this.Height - 5; this.east = e.X > this.Width - 5; this.west = e.X < 5;
                    if ((north && east) || (south && west))
                        this.Cursor = Cursors.SizeNESW;
                    else if ((north && west) || (south && east))
                        this.Cursor = Cursors.SizeNWSE;
                    else if (north || south)
                        this.Cursor = Cursors.SizeNS;
                    else if (east || west)
                        this.Cursor = Cursors.SizeWE;
                    else if (this.Cursor != Cursors.Arrow)
                    {
                        if (this.x_hover)
                        {
                            this.x_hover = false;
                            this.Invalidate();
                        }
                        this.Cursor = Cursors.Arrow;
                    }
                }
                else if (this.Cursor != Cursors.Arrow)
                {
                    if (this.x_hover)
                    {
                        this.x_hover = false;
                        this.Invalidate();
                    }
                    this.Cursor = Cursors.Arrow;
                }

            }
            else if (this.Cursor == Cursors.SizeAll)
                this.Cursor = Cursors.Arrow;
            return;
        }    
        private void Window_MouseDown(object sender, MouseEventArgs e)
        {
            this.old_location = this.Location;
            this.old_point = this.PointToScreen(e.Location);
            this.old_size = this.Size;
            if (this.north || this.south || this.west || this.east)
                this.SuspendLayout();
            return;
        }

        private void Window_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = this.WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;
            return;
        }

        private void Window_MouseUp(object sender, MouseEventArgs e)
        {
            this.ResumeLayout(true);
            return;
        }


    }
}
