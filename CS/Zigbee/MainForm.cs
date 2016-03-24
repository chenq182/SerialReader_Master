using System;
using System.Data;
using System.Text;
using System.Windows.Forms;

using MySql.Data.MySqlClient;
using remoteMysql;

namespace Zigbee
{
    public partial class MainForm : Form
    {
        public static Mysql GuideClient = new Mysql();
        public static Mysql WriteClient = new Mysql();
        public static Mysql ReadClient = new Mysql();
        public static Boolean WriteDB = false;
        public MainForm()
        {
            InitializeComponent();
        }
        #region 用户
        private void 登陆ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoginForm dlg = new LoginForm();
            dlg.MdiParent = this;
            dlg.Show();
        }
        private void 注销ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm.GuideClient.Close();
            MainForm.ReadClient.Close();
            MainForm.WriteClient.Close();
            MainForm.GuideClient.User = "guest";//
            MainForm.GuideClient.Password = "";
            MainForm.ReadClient.User = "guest";//
            MainForm.ReadClient.Password = "";
            MainForm.WriteClient.User = "guest";//
            MainForm.WriteClient.Password = "";
            WriteDB = false;
            登陆ToolStripMenuItem.Text = "登陆";
            登陆ToolStripMenuItem.Enabled = true;
            注销ToolStripMenuItem.Enabled = false;
            数据库选择ToolStripMenuItem.Enabled = false;
            读数据库ToolStripMenuItem.Enabled = false;
            读串口ToolStripMenuItem.Enabled = false;
            写数据库ToolStripMenuItem.Enabled = false;
            写数据库ToolStripMenuItem.Checked = false;
            写数据库ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            实时数据ToolStripMenuItem.Enabled = false;
            传输状态ToolStripMenuItem.Enabled = false;
            坐标设置ToolStripMenuItem.Enabled = false;
        }
        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm.GuideClient.Close();
            MainForm.ReadClient.Close();
            MainForm.WriteClient.Close();
            Application.Exit();
        }
        #endregion
        #region 数据读取
        private void 数据库选择ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MainForm.GuideClient.Active())
            {
                ClusterView newChild = new ClusterView("http://" + MainForm.GuideClient.Server + "/cgi-bin/load.py");
                newChild.MdiParent = this;
                newChild.Show();
            }
            else
            {
                MessageBox.Show("数据库登陆失败！");
            }
        }
        private void 读数据库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MainForm.ReadClient.Active())
            {
                DBListForm newChild = new DBListForm();
                newChild.MdiParent = this;
                newChild.Show();
            }
            else
            {
                MessageBox.Show("数据库登陆失败！");
            }
        }
        #endregion
        #region 数据采集
        #region 串口动态菜单
        private System.Windows.Forms.ToolStripMenuItem[] 串口ToolStripMenuItem;
        private void 数据采集ToolStripMenu_Click(object sender, EventArgs e)
        {
            string[] COMs = ComPort.Ports();
            this.串口ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem[COMs.Length];
            for (int i = 0; i < COMs.Length; i++)
            {
                this.串口ToolStripMenuItem[i] = new System.Windows.Forms.ToolStripMenuItem();
                this.串口ToolStripMenuItem[i].Name = COMs[i];
                this.串口ToolStripMenuItem[i].Size = new System.Drawing.Size(152, 22);
                this.串口ToolStripMenuItem[i].Text = COMs[i];
                this.串口ToolStripMenuItem[i].Click += new System.EventHandler(this.串口ToolStripMenuItem_Click);
            }
            读串口ToolStripMenuItem.DropDownItems.Clear();
            读串口ToolStripMenuItem.DropDownItems.AddRange(
                this.串口ToolStripMenuItem);
        }
        #endregion
        private void 串口ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolStripMenuItem s = (System.Windows.Forms.ToolStripMenuItem)sender;
            ComForm newChild = new ComForm(s.Name.ToString());
            newChild.MdiParent = this;
            newChild.Show();
        }
        private void 写数据库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (写数据库ToolStripMenuItem.Checked)
            {
                case true:
                    WriteDB = false;
                    写数据库ToolStripMenuItem.Checked = false;
                    break;
                case false:
                    WriteDB = true;
                    写数据库ToolStripMenuItem.Checked = true;
                    break;
            }
        }
        #endregion
        #region GIS图形
        private void 实时数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HeatMap newChild = new HeatMap("http://" + MainForm.GuideClient.Server + "/Web/nodeHeat.html");
            newChild.MdiParent = this;
            newChild.Show();
        }
        private void 传输状态ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NetMap newChild = new NetMap("http://" + MainForm.GuideClient.Server + "/Web/nodeNet.html");
            newChild.MdiParent = this;
            newChild.Show();
        }
        private void 坐标设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LocateMap newChild = new LocateMap("http://" + MainForm.GuideClient.Server + "/Web/nodeLocate.html");
            newChild.MdiParent = this;
            newChild.Show();
        }
        #endregion
    }
}
