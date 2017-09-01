using System;
using System.Text;
using System.Collections.Generic;

class Laptop
{
    public static List<string> Battery()
    {
        List<string> s = new List<string>();
        try
        {
            s.Add(System.Windows.Forms.SystemInformation.PowerStatus.BatteryChargeStatus.ToString());
            s.Add(System.Windows.Forms.SystemInformation.PowerStatus.BatteryFullLifetime.ToString());
            s.Add((System.Windows.Forms.SystemInformation.PowerStatus.BatteryLifePercent*100).ToString() + " %");
            s.Add((System.Windows.Forms.SystemInformation.PowerStatus.BatteryLifeRemaining/60).ToString());
            s.Add(System.Windows.Forms.SystemInformation.PowerStatus.PowerLineStatus.ToString());
        }
        catch (Exception ex)
        {
            s.Add(ex.Message);
        }
        return s;
    }

}