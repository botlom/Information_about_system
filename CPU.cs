using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;

class CPU
{
    public static List<string> CPUTerm()
    {
        List<string> Term = new List<string>();
        try
        {
            Double CPUtprt = 0;
            System.Management.ManagementObjectSearcher MOS = new System.Management.ManagementObjectSearcher("root\\WMI",
                    "SELECT * FROM MSAcpi_ThermalZoneTemperature");
            foreach (System.Management.ManagementObject Mo in MOS.Get())
            {
                CPUtprt = Convert.ToDouble(Convert.ToDouble(Mo.GetPropertyValue("CurrentTemperature".ToString())) - 2732) / 10.0;
                Term.Add(" CPU: " + CPUtprt.ToString() + " ° C");
            }
        }
        catch (Exception ex)
        {
            Term.Add("Ошибка получения данных " + ex.Message);
        }
        return Term;
    }
    public static string getUptime()
    {
        String strResult = String.Empty;
        strResult += Convert.ToString(Environment.TickCount / 86400000) + " дней, ";
        strResult += Convert.ToString(Environment.TickCount / 3600000 % 24) + " часов, ";
        strResult += Convert.ToString(Environment.TickCount / 120000 % 60) + " минут, ";
        strResult += Convert.ToString(Environment.TickCount / 1000 % 60) + " секунд.";
        return strResult;
    }
    [StructLayout(LayoutKind.Sequential)]
    struct LASTINPUTINFO
    {
        public static readonly int SizeOf = Marshal.SizeOf(typeof(LASTINPUTINFO));

        [MarshalAs(UnmanagedType.U4)]
        public UInt32 cbSize;
        [MarshalAs(UnmanagedType.U4)]
        public UInt32 dwTime;
    }

    [DllImport("user32.dll")]
    static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

    public static int GetLastInputTime()
    {
        int idleTime = 0;
        LASTINPUTINFO lastInputInfo = new LASTINPUTINFO();
        lastInputInfo.cbSize = (UInt32)Marshal.SizeOf(lastInputInfo);
        lastInputInfo.dwTime = 0;

        int envTicks = Environment.TickCount;

        if (GetLastInputInfo(ref lastInputInfo))
        {
            int lastInputTick = (Int32)lastInputInfo.dwTime;

            idleTime = envTicks - lastInputTick;
        }

        return ((idleTime > 0) ? (idleTime / 1000) : 0);
    }
}