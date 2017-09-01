using System;
using System.Text;
using System.Management;
using System.Runtime.InteropServices;

class Changes
{
    [DllImport("user32.dll", SetLastError = true)]
    private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);
    public static void HotKey(char key)
    {
        keybd_event(0x11, 0, 0, 0);
        keybd_event((byte)key, 0, 0, 0);
        keybd_event((byte)key, 0, 0x2, 0);
        keybd_event(0x11, 0, 0x2, 0);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SystemTime
    {
        public short Year;
        public short Month;
        public short DayOfWeek;
        public short Day;
        public short Hour;
        public short Minute;
        public short Second;
        public short Millisecond;
    };

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool SetSystemTime(ref SystemTime time);
    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern void GetSystemTime(ref SystemTime time);
}