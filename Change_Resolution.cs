using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
#region DEVMODE_STRUCT
[StructLayout(LayoutKind.Sequential)]
public struct DEVMODE
{
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
    public string dmDeviceName;
    public short dmSpecVersion;
    public short dmDriverVersion;
    public short dmSize;
    public short dmDriverExtra;
    public int dmFields;
    public short dmOrientation;
    public short dmPaperSize;
    public short dmPaperLength;
    public short dmPaperWidth;
    public short dmScale;
    public short dmCopies;
    public short dmDefaultSource;
    public short dmPrintQuality;
    public short dmColor;
    public short dmDuplex;
    public short dmYResolution;
    public short dmTTOption;
    public short dmCollate;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
    public string dmFormName;
    public short dmLogPixels;
    public short dmBitsPerPel;
    public int dmPelsWidth;
    public int dmPelsHeight;
    public int dmDisplayFlags;
    public int dmDisplayFrequency;
    public int dmICMMethod;
    public int dmICMIntent;
    public int dmMediaType;
    public int dmDitherType;
    public int dmReserved1;
    public int dmReserved2;
    public int dmPanningWidth;
    public int dmPanningHeight;
};
#endregion DEVMODE_STRUCT
#region PINVOKEDEF
class User32
{
    [DllImport("user32.dll")]
    public static extern int EnumDisplaySettings(string deviceName, int modeNum, ref DEVMODE devMode);
    [DllImport("user32.dll")]
    public static extern int ChangeDisplaySettings(ref DEVMODE devMode, int flags);
    public const int ENUM_CURRENT_SETTINGS = -1;
    public const int CDS_UPDATEREGISTRY = 0x01;
    public const int CDS_TEST = 0x02;
    public const int DISP_CHANGE_SUCCESSFUL = 0;
    public const int DISP_CHANGE_RESTART = 1;
    public const int DISP_CHANGE_FAILED = -1;
}
#endregion PINVOKEDEF
    public class Resolution
    {
        public static void ReadScreenRes(out int width, out int height)
        {
            width = Screen.PrimaryScreen.Bounds.Width;
            height = Screen.PrimaryScreen.Bounds.Height;
        }

        public static string ChangeScreenRes(int width, int height)
        {
            string res = "";
            DEVMODE dm = new DEVMODE();
            dm.dmDeviceName = new String(new char[32]);
            dm.dmFormName = new String(new char[32]);
            dm.dmSize = (short)Marshal.SizeOf(dm);
            if (0 != User32.EnumDisplaySettings(null, User32.ENUM_CURRENT_SETTINGS, ref dm))
            {
                #region ChangeDisplaySettings
                dm.dmPelsWidth = width;
                dm.dmPelsHeight = height;
                int iRet = User32.ChangeDisplaySettings(ref dm, User32.CDS_TEST);
                if (iRet == User32.DISP_CHANGE_FAILED)
                {
                    Console.WriteLine("Change failed");
                }
                else
                {
                    iRet = User32.ChangeDisplaySettings(ref dm, User32.CDS_UPDATEREGISTRY);
                    switch (iRet)
                    {
                        case User32.DISP_CHANGE_SUCCESSFUL:
                            {
                                res="Changed the resolution.";
                                break;
                            }
                        case User32.DISP_CHANGE_RESTART:
                            {
                                res="You will need to reboot for the change to happen.";
                                break;
                            }
                        default:
                            {
                                res="Failed to change the resolution.";
                                break;
                            }
                    }
                }
                #endregion ChangeDisplaySettings
            }
            return res;
        }
    }