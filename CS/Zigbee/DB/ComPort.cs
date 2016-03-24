using System;
using System.Collections;
using System.IO.Ports;

namespace Zigbee
{
    public class ComPort
    {

        private SerialPort port;
        public static string[] Ports()
        {
            string[] ports = SerialPort.GetPortNames();
            Array.Sort(ports);
            return ports;
        }
        #region 构造与析构
        public ComPort(string portName)
        {
            port = new SerialPort();
            port.PortName = portName;
            port.BaudRate = int.Parse("115200");
            port.DataBits = 8;
            port.StopBits = StopBits.One;
            port.Parity = Parity.None;
            port.NewLine = "\r\n";
            port.RtsEnable = true;
            port.DataReceived += OnDataReceived;
            try
            {
                port.Open();
                port.BreakState = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Close()
        {
            if (port.IsOpen)
                port.Close();
        }
        #endregion
        private void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int n = port.BytesToRead;
            byte[] buf = new byte[n];
            port.Read(buf, 0, n);
            PPPMsg[] msgs = PPP.Parse(buf);
            if (MainForm.WriteDB == true)
            {
                string[] sqls = new string[msgs.Length];
                for (int i = 0; i < msgs.Length; i++)
                    sqls[i] = msgs[i].SqlInsert();
                try
                {
                    MainForm.WriteClient.Trans(sqls);
                }
                catch
                {
                    /////////////
                }
            }
            for (int i = 0; i < msgs.Length; i++)
            {
                int j = msgs[i].ID;
                ComForm.Node[j].NewMsg(msgs[i]);
            }
        }
    }
}
