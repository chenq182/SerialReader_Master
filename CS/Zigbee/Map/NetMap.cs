using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Security.Permissions;

namespace Zigbee
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public class NetMap : LocateMap
    {
        private List<int> showIdArr;
        private List<int> pidArr;
        private List<string> timeArr;
        //protected List<string> idArr;
        //protected List<string> lngArr;
        //protected List<string> latArr;
        private int[] hash;
        public NetMap(string str)
            : base(str)
        {
            this.Text = "传输状态";
            //this.webBrowser.ScriptErrorsSuppressed = true;
            showIdArr = new List<int>();
            pidArr = new List<int>();
            timeArr = new List<string>();
            hash = new int[ZigbeeNode.MaxId + 1];
            for (int i = 0; i <= ZigbeeNode.MaxId; i++)
                hash[i] = -1;
            for (int i = 0; i < idArr.Count; i++)
                hash[Convert.ToInt32(idArr[i])] = i;
        }
        public int initHisNodeCount(string time)
        {
            string sql = "SELECT MAX(MTime) AS MTime,ID,pId FROM Msg WHERE MTime < '"
                + time + "' GROUP BY ID ORDER BY ID;";
            MySql.Data.MySqlClient.MySqlDataReader reader = MainForm.ReadClient.Reader(sql);
            int count = 0;
            showIdArr.Clear();
            pidArr.Clear();
            timeArr.Clear();
            while (reader.Read())
            {
                int id = Convert.ToInt32(reader["ID"].ToString());
                int pid = Convert.ToInt32(reader["pID"].ToString());
                if (hash[id] == -1 || hash[pid] == -1)
                    continue;
                count++;
                showIdArr.Add(id);
                pidArr.Add(pid);
                timeArr.Add(reader["MTime"].ToString());
            }
            reader.Close();
            return count;
        }
        public int lid(int i) { return Convert.ToInt32(showIdArr[i]); }
        public int lpid(int i) { return pidArr[i]; }
        public string ltime(int i) { return timeArr[i]; }
        public string lngA(int i) { return lngArr[hash[showIdArr[i]]]; }
        public string latA(int i) { return latArr[hash[showIdArr[i]]]; }
        public string lngB(int i) { return lngArr[hash[pidArr[i]]]; }
        public string latB(int i) { return latArr[hash[pidArr[i]]]; }
        public int pNext(int i) {
            int j = pidArr[i];
            for (int k = 0; k < showIdArr.Count; k++)
                if (showIdArr[k] == j)
                    return k;
            return -1;
        }
    }
}
