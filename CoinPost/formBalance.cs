using BtcE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace CoinPost
{
    public partial class formBalance : CoinPostGUI.Window
    {
        private BtceDatabase database = null;
        public formBalance(BtceDatabase db)
        {
            this.database = db;
            this.InitializeComponent();
            this.graphMain.GraphPane.YAxis.Title.Text = "USD";
            this.graphMain.GraphPane.XAxis.Title.IsVisible = false;
            this.graphMain.GraphPane.XAxis.Type = AxisType.Date;
            this.graphMain.GraphPane.XAxis.Scale.MajorUnit = DateUnit.Day;
            this.graphMain.GraphPane.XAxis.Scale.FontSpec.Angle = 65;
            this.graphMain.GraphPane.XAxis.Scale.MajorStep = 1;
            this.graphMain.GraphPane.XAxis.Scale.MinorUnit = DateUnit.Hour;
            this.graphMain.GraphPane.XAxis.Scale.MinorStep = 6;
            this.graphMain.GraphPane.XAxis.Scale.Format = "d MMM";
            this.graphMain.GraphPane.Chart.Fill.Color = Color.FromArgb(46, 46, 46);
            this.graphMain.GraphPane.Chart.Fill.SecondaryValueGradientColor = Color.FromArgb(46, 46, 46);
            this.graphMain.GraphPane.Chart.Fill.Brush = new SolidBrush(Color.FromArgb(46, 46, 46));
            this.graphMain.GraphPane.Fill.Color = Color.FromArgb(46, 46, 46);
            this.graphMain.GraphPane.Legend.IsVisible = false;
            this.graphMain.GraphPane.Title.Text = "Balance";
            this.graphMain.GraphPane.XAxis.Scale.FormatAuto = true;
            return;
        }

        private void formBalance_Load(object sender, EventArgs e)
        {
            BtceData data = this.database.Query("select * from Balance");
            double[] x_out = new double[data.Data.Rows.Count], y_out = new double[data.Data.Rows.Count];
            for (int i = 0; i < data.Data.Rows.Count; i++)
            {
                DateTime dt = UnixTime.ConvertToDateTime(Convert.ToUInt32(data.Data.Rows[i][0]));
                x_out[i] = new XDate(dt.Year,dt.Month,dt.Day,dt.Hour,dt.Minute,dt.Second,dt.Millisecond);
                y_out[i] = Convert.ToDouble(data.Data.Rows[i][1]);
            }
            this.AddSeries(x_out, y_out);
            return;
        }
        public void AddSeries(double[] x, double[] y)
        {
            LineItem curve = this.graphMain.GraphPane.AddCurve("Balance", new PointPairList(x, y), Color.FromArgb(170,170,170));
            curve.Line.Width = 3.0f;
            this.graphMain.AxisChange();
            this.graphMain.Invalidate();
            return;
        }

        private void graphMain_DoubleClick(object sender, EventArgs e)
        {
            this.graphMain.ZoomOutAll(this.graphMain.GraphPane) ;
            return;
        }
    }
}
