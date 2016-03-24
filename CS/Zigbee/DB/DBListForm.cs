using System;
using System.Windows.Forms;

namespace Zigbee
{
    public class DBListForm : DbFormatForm
    {
        #region 构造函数
        public DBListForm()
        {
            initAllNode();
        }
        public DBListForm(string nodeIndex)
        {
            initNode(nodeIndex);
        }
        #endregion
        private void initAllNode()
        {
            string sql = "SELECT * FROM MaxTime;";
            MySql.Data.MySqlClient.MySqlDataReader reader = MainForm.ReadClient.Reader(sql);
            this.Text = "数据库快照[" + DateTime.Now.ToString("HH:mm:ss") + "]";
            while (reader.Read())
            {
                int index = gridView.Rows.Add();
                gridView.Rows[index].Cells[0].Value = reader["ID"].ToString();
                DateTime t = DateTime.Parse(reader["MTime"].ToString());
                gridView.Rows[index].Cells[1].Value = t.ToString("HH:mm:ss");
                gridView.Rows[index].Cells[2].Value = reader["MVoltage"].ToString();
                gridView.Rows[index].Cells[3].Value = reader["MCurrent"].ToString();
                gridView.Rows[index].Cells[4].Value = reader["MPower"].ToString();
                gridView.Rows[index].Cells[5].Value = reader["MEnergy"].ToString();
                gridView.Rows[index].Cells[6].Value = reader["MTemp"].ToString();
                gridView.Rows[index].Cells[7].Value = reader["MHum"].ToString();
                gridView.Rows[index].Cells[8].Value = reader["MPhoto"].ToString();
                gridView.Rows[index].Cells[9].Value = reader["MCount"].ToString();
                gridView.Rows[index].Visible = true;
            }
            reader.Close();
            gridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridView_Click);
        }
        private void initNode(string nodeIndex)
        {
            string sql = "SELECT * FROM Msg WHERE ID=" + nodeIndex
                + " ORDER BY MTime DESC"
                + " LIMIT " + ZigbeeNode.PoolSize + ";";
            MySql.Data.MySqlClient.MySqlDataReader reader = MainForm.ReadClient.Reader(sql);
            this.Text = "节点" + nodeIndex + "快照[" + DateTime.Now.ToString("HH:mm:ss") + "]";
            while (reader.Read())
            {
                int index = gridView.Rows.Add();
                gridView.Rows[index].Cells[0].Value = reader["ID"].ToString();
                DateTime t = DateTime.Parse(reader["MTime"].ToString());
                gridView.Rows[index].Cells[1].Value = t.ToString("HH:mm:ss");
                gridView.Rows[index].Cells[2].Value = reader["MVoltage"].ToString();
                gridView.Rows[index].Cells[3].Value = reader["MCurrent"].ToString();
                gridView.Rows[index].Cells[4].Value = reader["MPower"].ToString();
                gridView.Rows[index].Cells[5].Value = reader["MEnergy"].ToString();
                gridView.Rows[index].Cells[6].Value = reader["MTemp"].ToString();
                gridView.Rows[index].Cells[7].Value = reader["MHum"].ToString();
                gridView.Rows[index].Cells[8].Value = reader["MPhoto"].ToString();
                gridView.Rows[index].Cells[9].Value = reader["MCount"].ToString();
                gridView.Rows[index].Visible = true;
            }
            reader.Close();
        }
        private void gridView_Click(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex > -1)
            {
                DataGridViewTextBoxCell vCell = (DataGridViewTextBoxCell)gridView.CurrentCell;
                DBListForm nodeList = new DBListForm(vCell.OwningRow.Cells[0].Value.ToString());
                nodeList.MdiParent = this.Owner;
                nodeList.Show();
            }
        }
    }
}
