using System;
using System.Windows.Forms;
using System.Security.Permissions;

namespace Zigbee
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class ClusterView : Form
    {
        public ClusterView(string str)
        {
            InitializeComponent();
            Uri url = new Uri(str);
            webBrowser.Url = url;
            webBrowser.ObjectForScripting = this;
            this.ResumeLayout(false);
        }
        public string setIP(string ip)
        {
            MainForm.GuideClient.Server = ip;
            MainForm.WriteClient.Server = ip;
            MainForm.ReadClient.Server = ip;
            if (MainForm.GuideClient.Active())
            {
                return "Succeed!";
            }
            else
            {
                return "Failed!";
            }
        }
    }
}
