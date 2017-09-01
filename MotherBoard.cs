using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;

    class MotherBoard
    {
        public static List<string> BaseBoard()
        {
            List<string> Mother = new List<string>();
            ManagementObjectSearcher searcher = new ManagementObjectSearcher
           ("SELECT Product, Caption, SerialNumber, Manufacturer,CreationClassName FROM Win32_BaseBoard");

            ManagementObjectCollection information = searcher.Get();
            foreach (ManagementObject obj in information)
            {
                foreach (PropertyData data in obj.Properties)
                {
                    Mother.Add(data.Name +" - "+ data.Value.ToString());
                }
            }
            return Mother;
        }
    }