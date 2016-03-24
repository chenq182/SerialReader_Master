namespace Zigbee
{
    partial class MainForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.userToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            登陆ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            注销ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.数据库ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            数据库选择ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            读数据库ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.数据采集ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            读串口ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            写数据库ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gIS图形ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            实时数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            传输状态ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            坐标设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.userToolStripMenuItem,
            this.数据库ToolStripMenuItem,
            this.数据采集ToolStripMenuItem,
            this.gIS图形ToolStripMenuItem,
            this.帮助ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // userToolStripMenuItem
            // 
            this.userToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            登陆ToolStripMenuItem,
            注销ToolStripMenuItem,
            this.toolStripSeparator1,
            this.退出ToolStripMenuItem});
            this.userToolStripMenuItem.Name = "userToolStripMenuItem";
            this.userToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.userToolStripMenuItem.Text = "用户";
            // 
            // 登陆ToolStripMenuItem
            // 
            登陆ToolStripMenuItem.Name = "登陆ToolStripMenuItem";
            登陆ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            登陆ToolStripMenuItem.Text = "登陆";
            登陆ToolStripMenuItem.Click += new System.EventHandler(登陆ToolStripMenuItem_Click);
            // 
            // 注销ToolStripMenuItem
            // 
            注销ToolStripMenuItem.Enabled = false;
            注销ToolStripMenuItem.Name = "注销ToolStripMenuItem";
            注销ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            注销ToolStripMenuItem.Text = "注销";
            注销ToolStripMenuItem.Click += new System.EventHandler(注销ToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(97, 6);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // 数据库ToolStripMenuItem
            // 
            this.数据库ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            数据库选择ToolStripMenuItem,
            读数据库ToolStripMenuItem});
            this.数据库ToolStripMenuItem.Name = "数据库ToolStripMenuItem";
            this.数据库ToolStripMenuItem.Size = new System.Drawing.Size(56, 21);
            this.数据库ToolStripMenuItem.Text = "数据库";
            // 
            // 数据库选择ToolStripMenuItem
            // 
            数据库选择ToolStripMenuItem.Enabled = false;
            数据库选择ToolStripMenuItem.Name = "数据库选择ToolStripMenuItem";
            数据库选择ToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            数据库选择ToolStripMenuItem.Text = "数据库选择";
            数据库选择ToolStripMenuItem.Click += new System.EventHandler(数据库选择ToolStripMenuItem_Click);
            // 
            // 读数据库ToolStripMenuItem
            //
            读数据库ToolStripMenuItem.Enabled = false;
            读数据库ToolStripMenuItem.Name = "读数据库ToolStripMenuItem";
            读数据库ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            读数据库ToolStripMenuItem.Text = "读数据库";
            读数据库ToolStripMenuItem.Click += new System.EventHandler(读数据库ToolStripMenuItem_Click);
            // 
            // 数据采集ToolStripMenuItem
            // 
            this.数据采集ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            读串口ToolStripMenuItem,
            写数据库ToolStripMenuItem});
            this.数据采集ToolStripMenuItem.Name = "数据采集ToolStripMenuItem";
            this.数据采集ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.数据采集ToolStripMenuItem.Text = "数据采集";
            this.数据采集ToolStripMenuItem.Click += new System.EventHandler(this.数据采集ToolStripMenu_Click);
            // 
            // 读串口ToolStripMenuItem
            // 
            读串口ToolStripMenuItem.Enabled = true;
            读串口ToolStripMenuItem.Name = "读串口ToolStripMenuItem";
            读串口ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            读串口ToolStripMenuItem.Text = "读串口";
            // 
            // 写数据库ToolStripMenuItem
            // 
            写数据库ToolStripMenuItem.Checked = true;
            写数据库ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            写数据库ToolStripMenuItem.Enabled = false;
            写数据库ToolStripMenuItem.Name = "写数据库ToolStripMenuItem";
            写数据库ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            写数据库ToolStripMenuItem.Text = "写数据库";
            写数据库ToolStripMenuItem.Click += new System.EventHandler(写数据库ToolStripMenuItem_Click);
            // 
            // gIS图形ToolStripMenuItem
            // 
            this.gIS图形ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            实时数据ToolStripMenuItem,
            传输状态ToolStripMenuItem,
            this.toolStripSeparator4,
            坐标设置ToolStripMenuItem});
            this.gIS图形ToolStripMenuItem.Name = "gIS图形ToolStripMenuItem";
            this.gIS图形ToolStripMenuItem.Size = new System.Drawing.Size(64, 21);
            this.gIS图形ToolStripMenuItem.Text = "GIS图形";
            // 
            // 实时数据ToolStripMenuItem
            // 
            实时数据ToolStripMenuItem.Enabled = false;
            实时数据ToolStripMenuItem.Name = "实时数据ToolStripMenuItem";
            实时数据ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            实时数据ToolStripMenuItem.Text = "实时数据";
            实时数据ToolStripMenuItem.Click += new System.EventHandler(实时数据ToolStripMenuItem_Click);
            // 
            // 传输状态ToolStripMenuItem
            // 
            传输状态ToolStripMenuItem.Enabled = false;
            传输状态ToolStripMenuItem.Name = "传输状态ToolStripMenuItem";
            传输状态ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            传输状态ToolStripMenuItem.Text = "传输状态";
            传输状态ToolStripMenuItem.Click += new System.EventHandler(传输状态ToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(121, 6);
            // 
            // 坐标设置ToolStripMenuItem
            // 
            坐标设置ToolStripMenuItem.Enabled = false;
            坐标设置ToolStripMenuItem.Name = "坐标设置ToolStripMenuItem";
            坐标设置ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            坐标设置ToolStripMenuItem.Text = "坐标设置";
            坐标设置ToolStripMenuItem.Click += new System.EventHandler(坐标设置ToolStripMenuItem_Click);
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.关于ToolStripMenuItem});
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.帮助ToolStripMenuItem.Text = "帮助";
            // 
            // 关于ToolStripMenuItem
            // 
            this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            this.关于ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.关于ToolStripMenuItem.Text = "关于";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Zigbee";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.MenuStrip menuStrip1;
        public System.Windows.Forms.ToolStripMenuItem userToolStripMenuItem;
        public static System.Windows.Forms.ToolStripMenuItem 登陆ToolStripMenuItem;
        public static System.Windows.Forms.ToolStripMenuItem 注销ToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem 数据库ToolStripMenuItem;
        public static System.Windows.Forms.ToolStripMenuItem 数据库选择ToolStripMenuItem;
        public static System.Windows.Forms.ToolStripMenuItem 读数据库ToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem 数据采集ToolStripMenuItem;
        public static System.Windows.Forms.ToolStripMenuItem 读串口ToolStripMenuItem;
        public static System.Windows.Forms.ToolStripMenuItem 写数据库ToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem gIS图形ToolStripMenuItem;
        public static System.Windows.Forms.ToolStripMenuItem 实时数据ToolStripMenuItem;
        public static System.Windows.Forms.ToolStripMenuItem 传输状态ToolStripMenuItem;
        public static System.Windows.Forms.ToolStripMenuItem 坐标设置ToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem;
        public System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        public System.Windows.Forms.ToolStripSeparator toolStripSeparator4;

    }
}
