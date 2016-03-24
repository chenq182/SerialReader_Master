namespace Zigbee
{
    partial class DbFormatForm
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
        protected void InitializeComponent()
        {
            this.gridView = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MTemp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MHum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MPhoto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MVoltage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MCurrent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MPower = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MEnergy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            this.SuspendLayout();
            // 
            // gridView
            // 
            this.gridView.AllowUserToAddRows = false;
            this.gridView.AllowUserToDeleteRows = false;
            this.gridView.AllowUserToOrderColumns = true;
            this.gridView.AllowUserToResizeRows = false;
            this.gridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.MTime,
            this.MVoltage,
            this.MCurrent,
            this.MPower,
            this.MEnergy,
            this.MTemp,
            this.MHum,
            this.MPhoto,
            this.MCount});
            this.gridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridView.Location = new System.Drawing.Point(0, 0);
            this.gridView.Name = "gridView";
            this.gridView.ReadOnly = true;
            this.gridView.RowHeadersVisible = false;
            this.gridView.RowTemplate.Height = 23;
            this.gridView.Size = new System.Drawing.Size(550, 350);
            this.gridView.TabIndex = 0;
            // 
            // ID
            // 
            this.ID.Frozen = true;
            this.ID.HeaderText = "节点";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            // 
            // MTime
            // 
            this.MTime.Frozen = true;
            this.MTime.HeaderText = "时间";
            this.MTime.Name = "MTime";
            this.MTime.ReadOnly = true;
            // 
            // MTemp
            // 
            this.MTemp.Frozen = true;
            this.MTemp.HeaderText = "温度/℃";
            this.MTemp.Name = "MTemp";
            this.MTemp.ReadOnly = true;
            // 
            // MHum
            // 
            this.MHum.Frozen = true;
            this.MHum.HeaderText = "湿度/%";
            this.MHum.Name = "MHum";
            this.MHum.ReadOnly = true;
            // 
            // MPhoto
            // 
            this.MPhoto.Frozen = true;
            this.MPhoto.HeaderText = "光照/kLux";
            this.MPhoto.Name = "MPhoto";
            this.MPhoto.ReadOnly = true;
            // 
            // MVoltage
            // 
            this.MVoltage.Frozen = true;
            this.MVoltage.HeaderText = "电压/V";
            this.MVoltage.Name = "MVoltage";
            this.MVoltage.ReadOnly = true;
            // 
            // MCurrent
            // 
            this.MCurrent.Frozen = true;
            this.MCurrent.HeaderText = "电流/mA";
            this.MCurrent.Name = "MCurrent";
            this.MCurrent.ReadOnly = true;
            // 
            // MPower
            // 
            this.MPower.Frozen = true;
            this.MPower.HeaderText = "功率/W";
            this.MPower.Name = "MPower";
            this.MPower.ReadOnly = true;
            // 
            // MEnergy
            // 
            this.MEnergy.Frozen = true;
            this.MEnergy.HeaderText = "电量/kWh";
            this.MEnergy.Name = "MEnergy";
            this.MEnergy.ReadOnly = true;
            // 
            // MCount
            // 
            this.MCount.Frozen = true;
            this.MCount.HeaderText = "计数";
            this.MCount.Name = "MCount";
            this.MCount.ReadOnly = true;
            // 
            // ComForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(550, 350);
            this.Controls.Add(this.gridView);
            this.Name = "DbFormat";
            this.ShowIcon = false;
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.DataGridView gridView;
        protected System.Windows.Forms.DataGridViewTextBoxColumn ID;
        protected System.Windows.Forms.DataGridViewTextBoxColumn MTime;
        protected System.Windows.Forms.DataGridViewTextBoxColumn MTemp;
        protected System.Windows.Forms.DataGridViewTextBoxColumn MHum;
        protected System.Windows.Forms.DataGridViewTextBoxColumn MPhoto;
        protected System.Windows.Forms.DataGridViewTextBoxColumn MVoltage;
        protected System.Windows.Forms.DataGridViewTextBoxColumn MCurrent;
        protected System.Windows.Forms.DataGridViewTextBoxColumn MPower;
        protected System.Windows.Forms.DataGridViewTextBoxColumn MEnergy;
        protected System.Windows.Forms.DataGridViewTextBoxColumn MCount;
    }
}
