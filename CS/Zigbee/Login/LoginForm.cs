using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using remoteMysql;

namespace Zigbee
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            this.User = "guest";//
        }
        public string User
        {
            set { textBox1.Text = value; }
            get { return textBox1.Text; }
        }
        public string Password
        {
            set { textBox2.Text = value; }
            get { return textBox2.Text; }
        }
        private void confirm_Click(object sender, EventArgs e)
        {
            this.button1.Text = "登陆中...";
            MainForm.GuideClient.User = this.User;
            MainForm.GuideClient.Password = this.Password;
            MainForm.ReadClient.User = this.User;
            MainForm.ReadClient.Password = this.Password;
            MainForm.WriteClient.User = this.User;
            MainForm.WriteClient.Password = this.Password;
            if (MainForm.GuideClient.Active())
            {
                MainForm.WriteDB = false;
                MainForm.登陆ToolStripMenuItem.Enabled = false;
                MainForm.登陆ToolStripMenuItem.Text = this.User;
                MainForm.注销ToolStripMenuItem.Enabled = true;
                MainForm.数据库选择ToolStripMenuItem.Enabled = true;
                MainForm.读数据库ToolStripMenuItem.Enabled = true;
                MainForm.读串口ToolStripMenuItem.Enabled = true;
                MainForm.写数据库ToolStripMenuItem.Enabled = true;
                MainForm.实时数据ToolStripMenuItem.Enabled = true;
                MainForm.传输状态ToolStripMenuItem.Enabled = true;
                MainForm.坐标设置ToolStripMenuItem.Enabled = true;
            }
            else
            {
                MessageBox.Show("用户名或密码错误！");
            }
            MainForm.GuideClient.Close();
            Dispose();
        }
        private void cancel_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
