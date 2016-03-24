using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Security.Permissions;

namespace Zigbee
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public class HeatMap : LocateMap
    {
        private List<string> liveIdArr;
        private List<string> valueArr;
        public HeatMap(string str)
            : base(str)
        {
            this.Text = "实时数据";
            //this.webBrowser.ScriptErrorsSuppressed = true;
            liveIdArr = new List<string>();
            valueArr = new List<string>();
            for (int i = 0; i < idArr.Count; i++)
            {
                liveIdArr.Add(idArr[i]);
                valueArr.Add("");
            }
        }
        public Boolean removeNode(string htmlID){
            int id = -1;
            if (int.TryParse(htmlID, out id) == false)
                return false;
            for (int i = 0; i < liveIdArr.Count; i++)
            {
                if (liveIdArr[i] == htmlID)
                {
                    liveIdArr.RemoveAt(i);
                    valueArr.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }
        public int initNodeCount(string status)
        {
            string sql = "SELECT ID,"+status+" FROM MaxTime;";
            MySql.Data.MySqlClient.MySqlDataReader reader = MainForm.ReadClient.Reader(sql);
            while (reader.Read())
            {
                string id = reader["ID"].ToString();
                string value = reader[status].ToString();
                for (int i = 0; i < liveIdArr.Count; i++)
                {
                    if (liveIdArr[i] == id)
                    {
                        valueArr[i] = value;
                        break;
                    }
                }
            }
            reader.Close();
            return liveIdArr.Count;
        }
        public int initHisNodeCount(string status, string time)
        {
            string sql = "SELECT MAX(MTime) AS MTime,ID,"
                + status + " FROM Msg WHERE MTime < '"
                + time + "' GROUP BY ID ORDER BY ID;";
            MySql.Data.MySqlClient.MySqlDataReader reader = MainForm.ReadClient.Reader(sql);
            while (reader.Read())
            {
                string id = reader["ID"].ToString();
                string value = reader[status].ToString();
                for (int i = 0; i < liveIdArr.Count; i++)
                {
                    if (liveIdArr[i] == id)
                    {
                        valueArr[i] = value;
                        break;
                    }
                }
            }
            reader.Close();
            return liveIdArr.Count;
        }
        public string getID(int i)
        {
            return liveIdArr[i];
        }
        public string getValue(int i)
        {
            return valueArr[i];
        }
    }
}
