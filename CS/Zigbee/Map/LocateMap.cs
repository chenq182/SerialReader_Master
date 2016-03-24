using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Security.Permissions;

namespace Zigbee
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class LocateMap : Form
    {
        protected string urlStr;
        protected int nodeCount;
        protected List<string> lngArr;
        protected List<string> latArr;
        protected List<string> idArr;
        public LocateMap(string str)
        {
            urlStr = str;//Application.StartupPath + "\\nodeLocate.html";
            InitializeComponent();
            string sql = "SELECT * FROM NodeGIS;";
            MySql.Data.MySqlClient.MySqlDataReader reader = MainForm.GuideClient.Reader(sql);
            nodeCount = 0;
            lngArr = new List<string>();
            latArr = new List<string>();
            idArr = new List<string>();
            while (reader.Read())
            {
                if (!reader.HasRows)
                    break;
                if (String.IsNullOrEmpty(reader["NLon"].ToString()))
                    continue;
                if (String.IsNullOrEmpty(reader["NLat"].ToString()))
                    continue;
                nodeCount++;
                idArr.Add(reader["NId"].ToString());
                lngArr.Add(reader["NLon"].ToString());
                latArr.Add(reader["NLat"].ToString());
            }
            reader.Close();
            Uri url = new Uri(urlStr);
            webBrowser.Url = url;
            webBrowser.ObjectForScripting = this;
            this.ResumeLayout(false);
        }
        public int leftNodesNum() {
            return nodeCount;
        }
        public string popID() {
            if (idArr.Count == 0)
                return "";
            else
                return idArr[idArr.Count - nodeCount--];
        }
        public string popLng()
        {
            if (idArr.Count == 0)
                return "";
            else
                return lngArr[idArr.Count - nodeCount - 1];
        }
        public string popLat()
        {
            if (idArr.Count == 0)
                return "";
            else
                return latArr[idArr.Count - nodeCount - 1];
        }
        public Boolean validID(string nodeID)
        {
            int i = -1;
            if (int.TryParse(nodeID, out i) == false)
                return false;
            if (i >= 0)
                return true;
            else
                return false;
        }
        public Boolean updateLocation(string id, string lng, string lat) {
            string[] sqls = new string[2];
            sqls[0] = "DELETE FROM NodeGIS WHERE NId=" + id + ";";
            sqls[1] = "INSERT INTO NodeGIS(NId, NLon, NLat) VALUES (" + id + "," + lng + "," + lat + ")";
            MainForm.GuideClient.Trans(sqls);
            return true;
        }
    }
}
