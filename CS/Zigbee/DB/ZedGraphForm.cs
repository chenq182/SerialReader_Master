using System;
using System.Windows.Forms;

using ZedGraph;

namespace Zigbee
{
    public partial class ZedGraphForm : Form
    {
        private readonly int nodeIndex;
        private readonly string type;
        System.Timers.Timer t;
        LineItem myCurve;
        PointPairList list;
        public ZedGraphForm(int nodeIndex, string type)
        {
            InitializeComponent();
            this.nodeIndex = nodeIndex;
            this.type = type;
        }

        private void ZedGraphForm_Load(object sender, EventArgs e)
        {
            this.Text = type + "图-节点" + nodeIndex.ToString();
            t = new System.Timers.Timer(1000);
            t.Elapsed += new System.Timers.ElapsedEventHandler(refresh);
            t.AutoReset = true;
            t.Enabled = true;
            this.zedGraphControl.GraphPane.Title.Text = type + "曲线图";
            this.zedGraphControl.GraphPane.XAxis.Title.Text = "时间";
            this.zedGraphControl.GraphPane.YAxis.Title.Text = type;
            this.zedGraphControl.GraphPane.XAxis.Type = ZedGraph.AxisType.DateAsOrdinal;

            initList();
            myCurve = zedGraphControl.GraphPane.AddCurve(type,
                this.list, System.Drawing.Color.DarkGreen, SymbolType.None);
            this.zedGraphControl.AxisChange();
            int min=0, max=100, minorstep=2, majorstep=10;
            switch (type)
            {
                case "温度":
                    min = -10;
                    max = 50;
                    minorstep = 1;
                    majorstep = 5;
                    break;
                case "湿度":
                    min = 0;
                    max = 100;
                    minorstep = 2;
                    majorstep = 10;
                    break;
                case "光照":
                    min = 0;
                    max = 160;
                    minorstep = 4;
                    majorstep = 20;
                    break;
            }
            this.zedGraphControl.GraphPane.YAxis.Scale.Min = min;
            this.zedGraphControl.GraphPane.YAxis.Scale.Max = max;
            this.zedGraphControl.GraphPane.YAxis.Scale.MinorStep = minorstep;
            this.zedGraphControl.GraphPane.YAxis.Scale.MajorStep = majorstep;
        }

        private void refresh(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                refreshList();
                this.Invoke((EventHandler)(delegate
                {
                    this.zedGraphControl.Refresh();
                }));
            }
            catch
            {
                t.Enabled = false;
            }
        }
        private void ZedGraphForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            t.Enabled = false;
        }

        private void initList()
        {
            list = new PointPairList();
            for (int i = 0; i < ZigbeeNode.PoolSize; i++)
            {
                double x = (double)new XDate(DateTime.Now.AddSeconds(-20 + i));
                double y = 0;
                if (type == "温度")
                    y = 20;
                if (type == "湿度")
                    y = 50;
                list.Add(x, y);
            }
        }
        private void refreshList()
        {
            DateTime[] x_ori = null;
            double[] y_ori = null;
            ComForm.Node[nodeIndex].PairList(type, ref x_ori, ref y_ori);
            list.RemoveRange(ZigbeeNode.PoolSize - x_ori.Length, x_ori.Length);
            for (int i = 0; i < x_ori.Length; i++)
            {
                list.Add((double)new XDate(x_ori[i]), y_ori[i]);
            }
        }
    }
}
