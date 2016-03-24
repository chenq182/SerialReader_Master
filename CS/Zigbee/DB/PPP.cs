using System;
using System.Collections.Generic;

namespace Zigbee
{
    public static class PPP
    {
        private static List<byte> buffer = new List<byte>(16384);
        public static PPPMsg[] Parse(byte[] buf)
        {
            buffer.AddRange(buf);
            byte[] single;
            List<PPPMsg> tmp = new List<PPPMsg>();
            while ((single = Singleton()) != null)
                tmp.Add(new PPPMsg(single));
            PPPMsg[] result = tmp.ToArray();
            return result;
        }
        #region 私有方法
        private static byte[] Singleton()
        {
            while (buffer.Count > 0 && buffer[0] != 0x7E)
                buffer.RemoveAt(0);
            while (buffer.Count > 1 && buffer[1] == 0x7E)
                buffer.RemoveAt(0);
            int i = 1;
            while (i < buffer.Count && buffer[i] != 0x7E)
                i++;
            if (i >= buffer.Count || buffer[i] != 0x7E)
                return null;
            for (int j = 0; j <= i; j++)
            {
                if (buffer[j] == 0x7D && buffer[j + 1] == 0x5E)
                {
                    buffer[j] = 0x7E;
                    buffer.RemoveAt(j+1);
                    i--;
                }
                if (buffer[j] == 0x7D && buffer[j + 1] == 0x5D)
                {
                    buffer.RemoveAt(j+1);
                    i--;
                }
            }
            byte[] result = new byte[i+1];
            buffer.CopyTo(0, result, 0, i+1);
            buffer.RemoveRange(0, i);
            if (Valid(result) == false)
            {
                result = null;
                result = Singleton();
            }
            return result;
        }
        private static Boolean Valid(byte[] s)
        {
            if (s.Length != 55)
                return false;
            if (s[0] != 0x7E || s[54] != 0x7E)
                return false;
            if (s[1] != 0x45 || s[2] != 0x00)
                return false;
            if (s[26] != 0xCA || s[27] != 0xFE)
                return false;
            if (s[20] == 0x00 && s[21] == 0x00)
                return false;
            return true;
        }
        #endregion
    }

    public class PPPMsg
    {
        private int id;         //本ID ID
        private DateTime time;  //时间 MTime
        private int pid;        //父ID pID 
        private double temp;    //温度 MTemp
        private double hum;     //湿度 MHum
        private double photo;   //光照 MPhoto
        private double voltage; //电压 MVoltage
        private double current; //电流 MCurrent
        private double power;   //功率 MPower
        private double energy;  //电量 MEnergy
        private int count;      //计数 MCount
        public PPPMsg(byte[] m)
        {
            id = Convert.ToInt32(m[18] * 256 + m[19]);
            time = DateTime.Now;
            pid = Convert.ToInt32(m[22]*256+m[23]);
            temp = Convert.ToDouble((m[33] * 256 + m[34]) * 0.01 - 39.6);
            double tmp = Convert.ToDouble(m[37] * 256 + m[38]);
            hum = Convert.ToDouble(-4 + tmp * 0.0405 - tmp * tmp * 0.00000028);
            hum += (temp - 25) * (0.01 + 0.00008 * tmp);
            if (temp + 39.5 < 0) { temp = 50; hum = 100; }
            photo = Convert.ToDouble((m[35] * 256 + m[36]) * 0.085);
            voltage = Convert.ToDouble(m[39] * 65536 + m[40] * 256 + m[41]) * 220.1 / 1858384;
            current = Convert.ToDouble(m[42] * 65536 + m[43] * 256 + m[44]) * 1272 / 19570;
            power = Convert.ToDouble(m[45] * 16777216 + m[46] * 65536 + m[47] * 256 + m[48]) * 2.5258 / 10000;
            if (power < 0) { power = 0; }
            energy = Convert.ToDouble(m[49] * 65536 + m[50] * 256 + m[51]) / 3200;
            count = Convert.ToInt32(m[20]*256+m[21]);
        }
        public string SqlInsert()
        {
            string sql = "INSERT INTO Msg (ID,MTime,pID,MTemp,MHum,MPhoto,MVoltage,MCurrent,MPower,MEnergy,MCount) VALUES ('"
                + id.ToString("G") + "','"
                + time.ToString("yyyyMMddHHmmss") + "','"
                + pid.ToString("G") + "','"
                + temp.ToString("F2") + "','"
                + hum.ToString("F2") + "','"
                + photo.ToString("F2") + "','"
                + voltage.ToString("F2") + "','"
                + current.ToString("F2") + "','"
                + power.ToString("F2") + "','"
                + energy.ToString("F4") + "','"
                + count.ToString("G") + "');";
            return sql;
        }
        #region 属性
        public int ID { get { return id; } }
        public DateTime MTime { get { return time; } }
        public int pID { get { return pid; } }
        public double MTemp { get { return temp; } }
        public double MHum { get { return hum; } }
        public double MPhoto { get { return photo; } }
        public double MVoltage { get { return voltage; } }
        public double MCurrent { get { return current; } }
        public double MPower { get { return power; } }
        public double MEnergy { get { return energy; } }
        public int MCount { get { return count; } }
        #endregion
    }
}
