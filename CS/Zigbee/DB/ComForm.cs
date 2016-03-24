using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Zigbee
{
    public sealed class ComForm : DbFormatForm
    {
        private string comStr;
        private ComPort comPort;
        public static ZigbeeNode[] Node;
        private System.Timers.Timer t;
        static ComForm()
        {
            Node = new ZigbeeNode[ZigbeeNode.MaxId + 1];
            for (int i = 0; i <= ZigbeeNode.MaxId; i++)
                Node[i] = new ZigbeeNode();
        }
        public ComForm(string str)
        {
            gridView.CellClick += new DataGridViewCellEventHandler(this.gridView_Click);
            comStr = str;
            comPort = new ComPort(comStr);
            this.Text = comStr;

            t = new System.Timers.Timer(1000);
            t.Elapsed += new System.Timers.ElapsedEventHandler(refresh);
            t.AutoReset = true;
            t.Enabled = true;
            for (int i = 0; i <= ZigbeeNode.MaxId; i++)
            {
                int index = this.gridView.Rows.Add();
                for (int j = 0; j < 10; j++)
                {
                    gridView.Rows[index].Cells[j].Value = i.ToString();
                    gridView.Rows[index].Visible = false;
                }
            }
        }
        private void refresh(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                for (int i = 0; i <= ZigbeeNode.MaxId; i++)
                {
                    if (!ComForm.Node[i].Offline())
                    {
                        PPPMsg m = ComForm.Node[i].RecentMsg();
                        this.Invoke((EventHandler)
                            (delegate { gridView.Rows[i].Visible = true; }));
                        gridView.Rows[i].Cells[0].Value = m.ID.ToString("G");
                        gridView.Rows[i].Cells[1].Value = m.MTime.ToString("HH:mm:ss");
                        gridView.Rows[i].Cells[2].Value = m.MVoltage.ToString("F2");
                        gridView.Rows[i].Cells[3].Value = m.MCurrent.ToString("F2");
                        gridView.Rows[i].Cells[4].Value = m.MPower.ToString("F2");
                        gridView.Rows[i].Cells[5].Value = m.MEnergy.ToString("F4");
                        gridView.Rows[i].Cells[6].Value = m.MTemp.ToString("F2");
                        gridView.Rows[i].Cells[7].Value = m.MHum.ToString("F2");
                        gridView.Rows[i].Cells[8].Value = m.MPhoto.ToString("F2");
                        gridView.Rows[i].Cells[9].Value = m.MCount.ToString("G");
                    }
                    else if (gridView.Rows[i].Visible == true)
                        this.Invoke((EventHandler)
                            (delegate { gridView.Rows[i].Visible = false; }));
                }
            }
            catch
            {
                t.Enabled = false;
            }
        }
        private void ComForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            t.Enabled = false;
            comPort.Close();
        }
        #region Zed图
        private void gridView_Click(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > 1 && e.RowIndex > -1)
            {
                DataGridViewTextBoxCell vCell = (DataGridViewTextBoxCell)gridView.CurrentCell;
                string type = "";
                switch (vCell.OwningColumn.Name)
                {
                    case "MTemp": type = "温度"; break;
                    case "MHum": type = "湿度"; break;
                    case "MPhoto": type = "光照"; break;
                    case "MVoltage": type = "电压"; break;
                    case "MCurrent": type = "电流"; break;
                    case "MPower": type = "功率"; break;
                    case "MEnergy": type = "电量"; break;
                    case "MCount": type = "计数"; break;
                }
                ZedGraphForm graph = new ZedGraphForm
                    (vCell.OwningRow.Index, type);
                graph.MdiParent = this.Owner;
                graph.Show();
            }
        }
        #endregion
    }
}
