using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using NAudio.CoreAudioApi;
using System.Data.OleDb;

namespace Сведения_о_системе
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text += " " + Environment.OSVersion.ToString() +" "+ check.GetOSFriendlyName();
            label2.Text += " " + Environment.SystemDirectory.ToString();
            label3.Text += " " + Environment.MachineName.ToString();
            label4.Text += " " + Environment.UserName.ToString();
            label5.Text += " " + Environment.ProcessorCount.ToString();
            string host = System.Net.Dns.GetHostName();
            label6.Text += " " + System.Net.Dns.GetHostByName(host).AddressList[0];

            DriveInfo[] alldrivers = DriveInfo.GetDrives();
            int i = 0;
            string[] dr_name = Environment.GetLogicalDrives();
            int a = dr_name.Length;
            string[] dr_type = new string[a];
            string[] dr_label = new string[a];
            string[] dr_format = new string[a];
            string[] dr_sp1 = new string[a];
            string[] dr_sp2 = new string[a];


            foreach (DriveInfo d in alldrivers)
            {
                dr_type[i] = d.DriveType.ToString();

                if (d.IsReady == true)
                {
                    dr_label[i] = d.VolumeLabel;
                    dr_format[i] = d.DriveFormat;
                    dr_sp1[i] = (d.AvailableFreeSpace/1024/1024/1024).ToString();
                    dr_sp2[i] = (d.TotalSize / 1024/1024/1024).ToString();
                }
                else
                {
                    dr_label[i] = "Unknown";
                    dr_format[i] = "Unknown";
                    dr_sp1[i] = "Unknown";
                    dr_sp2[i] = "Unknown";
                }
                i++;
            }
            richTextBox1.Lines = dr_name;
            richTextBox2.Lines = dr_type;
            richTextBox3.Lines = dr_label;
            richTextBox4.Lines = dr_format;
            richTextBox5.Lines = dr_sp1;
            richTextBox6.Lines = dr_sp2;
            Диплом.Properties.Settings.Default.Check++;
            Диплом.Properties.Settings.Default.Save();
            label16.Text += Диплом.Properties.Settings.Default.Check + " раз";
            listBox1.DataSource = check.GetAllRegisteredFile();
            listBox2.DataSource = MotherBoard.BaseBoard();
            CPULoad();
            label29.Text += Laptop.Battery()[0];
            label30.Text += Laptop.Battery()[1];
            label31.Text += Laptop.Battery()[2];
            label32.Text += Laptop.Battery()[3];
            label33.Text += Laptop.Battery()[4];
            toolTip1.SetToolTip(label17, DateTime.Now.ToString("dd MMMM yyyy"));
            if (!check.GetOSFriendlyName().Contains("Windows XP"))
            {
                MMDeviceEnumerator devEnum = new MMDeviceEnumerator();
                MMDevice defaultDevice = devEnum.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            }
            uint volume = 0, hW0 = 0; float k = 0;
            waveOutGetVolume(hW0, ref volume);
            uint volume2 = ((uint)volume & 0x0000ffff);
            int z=Convert.ToInt32(volume2);
            k=(float)z/655.26f;
            //Диплом.Form3 form3 = new Диплом.Form3();
            Form1 form1 = new Form1();
            /*if (Диплом.Properties.Settings.Default.Check > 3 && Диплом.Properties.Settings.Default.boolean==0)
            {
                //form3.ShowDialog();
                //form1.Visible = false;
            }*/
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            PerformanceCounter ram = new PerformanceCounter("Memory", "Available MBytes");
            label8.Text = "Количество свободной памяти: " + ram.NextValue().ToString() + " Mb";
            label24.Text = "Компьютер работает: " + CPU.getUptime();
            label17.Text = DateTime.Now.ToString("HH:mm:ss");
            //label28.Text = CPU.GetLastInputTime().ToString();
        }
        #region Check
        private void button1_Click(object sender, EventArgs e) //Ping
        {
            if (check.Ping(textBox1.Text))
            {
                label15.Text = "Компьютер доступен";
            }
            else
            {
                label15.Text = "Не удалось обнаружить узел";
            }
        }

        private void button2_Click(object sender, EventArgs e) //SiteToIp
        {
            textBox5.Text=check.SiteToIp(textBox2.Text);
        }

        private void button3_Click(object sender, EventArgs e) //isAdmin
        {
            label18.Text=check.isAdmin();
        }
        #endregion
        #region CPU
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e) //LoadPercentage
        {
            string constLabel = label21.Text;
            System.Management.ManagementObjectSearcher searcher = new System.Management.ManagementObjectSearcher("SELECT * FROM Win32_Processor");
            foreach (System.Management.ManagementObject queryObj in searcher.Get())
            {
                LoadCpu(constLabel + queryObj["LoadPercentage"].ToString() + "%");
                LoadCpuProgressBar(Convert.ToInt16(queryObj["LoadPercentage"]));
            }
            //backgroundWorker1.RunWorkerAsync();
        }
        public void CPULoad() //CPULoad
        {
            string constLabel;
            int coreCount = 0;
            foreach (var item in new System.Management.ManagementObjectSearcher("Select * from Win32_Processor").Get())
            {
                label19.Text = item["Name"].ToString();
                coreCount += int.Parse(item["NumberOfCores"].ToString());
            }

            label20.Text = label20.Text + coreCount.ToString();
            constLabel = label21.Text;

            backgroundWorker1.RunWorkerAsync();
        }
        public void LoadCpu(string loadcpu) //Delegate_CPU
        {
            try
            {
                if (this.InvokeRequired)
                    BeginInvoke(new MethodInvoker(delegate
                    { label21.Text = loadcpu; }));
                else
                    label21.Text = loadcpu;
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
        public void LoadCpuProgressBar(int loadcpu) //CPUBar
        {
            try
            {
                if (this.InvokeRequired)
                    BeginInvoke(new MethodInvoker(delegate
                    { progressBar1.Value = loadcpu; }));
                else
                    progressBar1.Value = loadcpu;
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
        #endregion 
        #region Volume
        [DllImport("winmm.dll")]
        public static extern int waveOutSetVolume(IntPtr hwo, uint dwVolume);
        [DllImport("winmm.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern uint waveOutGetVolume(uint hwo, ref uint dwVolume);

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (!check.GetOSFriendlyName().Contains("Windows XP")) //Не работает на Windows XP
            {
                MMDeviceEnumerator devEnum = new MMDeviceEnumerator();  //Windows 7
                MMDevice defaultDevice = devEnum.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
                defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar = (float)trackBar1.Value / 100.0f;
            }
            
            double newVolume = ushort.MaxValue * trackBar1.Value / 10.0; //Windows XP
            uint v = ((uint)newVolume) & 0xffff;
            uint vAll = v | (v << 16);
            int retVal = waveOutSetVolume(IntPtr.Zero, vAll);
        }
        #endregion

        private void tabPage6_Enter(object sender, EventArgs e) //ShowResolution
        {
            label25.Text = Monitor.resolution();
        }

        private void button7_Click(object sender, EventArgs e) //Resolution
        {
            label27.Text+=Resolution.ChangeScreenRes(Convert.ToInt32(textBox3.Text), Convert.ToInt32(textBox4.Text));
            label25.Text = Monitor.resolution();
        }

        private void pictureBox1_Click(object sender, EventArgs e) //Copy
        {
            textBox5.Select();
            textBox5.SelectAll();
            Changes.HotKey('C');
        }

        private void pictureBox2_Click(object sender, EventArgs e) //Paste
        {
            Changes.HotKey('V');
        }

        private void tabPage2_Enter(object sender, EventArgs e) 
        {
            toolTip1.SetToolTip(pictureBox1, "Копировать");
            toolTip1.SetToolTip(pictureBox2, "Вставить");
        }

        private void pictureBox3_Click(object sender, EventArgs e) //Sleep
        {
            if (MessageBox.Show("Вы действительно хотите перейти в спящий режим?", "Подтвердить", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.SetSuspendState(PowerState.Suspend, true, true);
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e) //Hibernate
        {
            if (MessageBox.Show("Вы действительно хотите перейти в режим гибернации?", "Подтвердить", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.SetSuspendState(PowerState.Hibernate, true, true);
            }
        }

        private void pictureBox5_Click_1(object sender, EventArgs e) //Shutdown
        {
            if (MessageBox.Show("Вы действительно хотите выключить компьютер?", "Подтвердить", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start("shutdown.exe", "/s /f /t 0");
            }
        }

        private void tabPage3_Enter(object sender, EventArgs e)  //CPUTerm
        {
            listBox3.DataSource = CPU.CPUTerm();
        }

        private void tabPage5_Enter(object sender, EventArgs e)  //Antivirus
        {
            listBox5.DataSource = check.Antivirus();
        }

        #region Notify
        private void Form1_Resize(object sender, EventArgs e) //Notify
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon1.Visible = true;
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e) //Notify
        {
            this.Show();
            notifyIcon1.Visible = false;
            WindowState = FormWindowState.Normal;
        }
        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            notifyIcon1.Visible = false;
            WindowState = FormWindowState.Normal;
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Диплом.AboutBox1 a = new Диплом.AboutBox1();
            a.Show();
        }
        #endregion

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Process.Start("Helper.htm");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Диплом.Form2 a = new Диплом.Form2();
            a.Show();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Диплом.Form4 a = new Диплом.Form4();
            a.Show();
        }

    }
}