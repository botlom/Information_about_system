using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Management;
using System.Security.Principal;
using Microsoft.Win32;

class check
{
    public static bool Ping(string Target)
    {
        SelectQuery query =
              new SelectQuery("Win32_PingStatus",
                   string.Format("Address='{0}'", Target));
        ManagementObjectSearcher searcher =
              new ManagementObjectSearcher(query);

        foreach (ManagementObject result in searcher.Get())
        {
            return (result["StatusCode"] != null && (0 == (UInt32)result["StatusCode"]));
        }
        return false;
    }
    public static string SiteToIp(string Target)
    {
        try
        {
            string ip = System.Net.Dns.GetHostByName(Target).AddressList[0].ToString();
            return ip;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
    public static string isAdmin()
    {
        bool isAdmin;
        try
        {
            WindowsIdentity user = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(user);
            isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
        catch (UnauthorizedAccessException ex)
        {
            isAdmin = false;
            return ex.Message;
        }
        catch (Exception ex)
        {
            isAdmin = false;
            return ex.Message;
        }
        return "Является администратором";
    }
    public static List<string> GetAllRegisteredFile()
    {
        RegistryKey root = Registry.ClassesRoot;
        List<string> subKeys = new List<string>();
        IEnumerator enums = root.GetSubKeyNames().GetEnumerator();
        while (enums.MoveNext())
        {
            if (enums.Current.ToString().StartsWith("."))
                subKeys.Add(enums.Current.ToString());
        }
        return subKeys;
    }
    public static List<string> Antivirus()
    {
        List<string> Avz = new List<string>();
        string computer = Environment.MachineName;
        string wmipath = @"\\" + computer + @"\root\SecurityCenter2";
        try
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(wmipath,
              "SELECT * FROM AntivirusProduct");
            ManagementObjectCollection information = searcher.Get();
            foreach (ManagementObject obj in information)
            {
                foreach (PropertyData data in obj.Properties)
                {
                    Avz.Add(data.Name + " - " + data.Value.ToString());
                }
            }
        }
        catch (Exception e)
        {
            Avz.Add(e.Message);
        }
        return Avz;
    }
    public static string GetOSFriendlyName()
    {
        string result = string.Empty;
        ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem");
        foreach (ManagementObject os in searcher.Get())
        {
            result = os["Caption"].ToString();
            break;
        }
        return result;
    }
}

