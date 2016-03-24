using System;
using System.Collections.Generic;

namespace Zigbee
{
    public class ZigbeeNode
    {
        private readonly static int poolSize = 20;
        private readonly static int waitSeconds = 30;////////////////////config
        public readonly static int MaxId = 100;//////////////////////////config
        private List<PPPMsg> pool;
        private int size;
        public ZigbeeNode()
        {
            pool = new List<PPPMsg>();
            size = 0;
        }
        public static int PoolSize { get { return poolSize; } }
        public int Size { get { return size; } }
        public void NewMsg(PPPMsg msg)
        {
            pool.Add(msg);
            size++;
            if (size > poolSize)
            {
                pool.RemoveAt(0);
                size--;
            }
        }
        public PPPMsg RecentMsg()
        {
            if (size == 0)
                return null;
            else
                return pool[size-1];
        }
        public Boolean Offline()
        {
            if (size < 1)
                return true;
            TimeSpan offTime = DateTime.Now - pool[size-1].MTime;
            int sec = Convert.ToInt32(offTime.Seconds);
            if (sec >= waitSeconds)
                return true;
            else
                return false;
        }
        public void PairList(string type, ref DateTime[] time, ref double[] value)
        {
            time = null;
            value = null;
            if (size != 0)
            {
                time = new DateTime[size];
                value = new double[size];
                for (int i = 0; i < size; i++)
                {
                    switch (type)
                    {
                        case "温度":
                            value[i] = pool[i].MTemp;
                            break;
                        case "湿度":
                            value[i] = pool[i].MHum;
                            break;
                        case "光照":
                            value[i] = pool[i].MPhoto;
                            break;
                        case "电压":
                            value[i] = pool[i].MVoltage;
                            break;
                        case "电流":
                            value[i] = pool[i].MCurrent;
                            break;
                        case "功率":
                            value[i] = pool[i].MPower;
                            break;
                        case "电量":
                            value[i] = pool[i].MEnergy;
                            break;
                        case "计数":
                            value[i] = pool[i].MCount;
                            break;
                    }
                    time[i] = pool[i].MTime;
                }
            }
        }
    }
}
