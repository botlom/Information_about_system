using System;
using System.Text;
using System.Collections.Generic;
using System.Drawing;

class Monitor
{ 
    public static string resolution()
    {
        string s="";
        Int32 xmax = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width;
        Int32 ymax = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height;
        s = "SystemInformation.PrimaryMonitorSize\n Width =" + xmax + " Height =" + ymax + "\n";
        xmax = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
        ymax = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
        s += "\nSystem.Windows.Forms.Screen.PrimaryScreen.Bounds\n Width =" + xmax + " Height =" + ymax + "\n";
        xmax = System.Windows.Forms.SystemInformation.PrimaryMonitorMaximizedWindowSize.Width;
        ymax = System.Windows.Forms.SystemInformation.PrimaryMonitorMaximizedWindowSize.Height;
        s += "\nSystemInformation.PrimaryMonitorMaximizedWindowSize\n Width =" + xmax + " Height =" + ymax;
        return s;        
    }
}