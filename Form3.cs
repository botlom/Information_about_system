using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Диплом
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        bool off = false;
        private void button1_Click(object sender, EventArgs e)
        {
            Form a = new Form();
            if (textBox1.Text == Диплом.Properties.Settings.Default.Password && Диплом.Properties.Settings.Default.boolean==0)
            {
                MessageBox.Show("Верно");
                Диплом.Properties.Settings.Default.boolean++;
                Диплом.Properties.Settings.Default.Save();
                off = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Не верно");
                Application.Exit();
            }
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (off == false) Application.Exit();
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Application.Exit();
        }
    }
}
