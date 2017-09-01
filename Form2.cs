using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Диплом
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        SqlDataAdapter Adapter;
        DataSet ds = new DataSet();
        bool show = false;
        SqlConnection myConnection = new SqlConnection("server=.;" +
                                       "Trusted_Connection=yes;" +
                                       "database=PC; " +
                                       "connection timeout=30");
        private void Form2_Load(object sender, EventArgs e)
        {
            try
            {
                myConnection.Open();
                Adapter = new SqlDataAdapter("SELECT * FROM dbo.MotherBoard", myConnection);
                Adapter.Fill(ds, "Mother");
                Adapter = new SqlDataAdapter("SELECT * FROM dbo.DDR", myConnection);
                Adapter.Fill(ds, "DDR");
                Adapter = new SqlDataAdapter("SELECT * FROM dbo.CPU", myConnection);
                Adapter.Fill(ds, "CPU");
                Adapter = new SqlDataAdapter("SELECT * FROM dbo.GDDR", myConnection);
                Adapter.Fill(ds, "GDDR");
                Adapter = new SqlDataAdapter("SELECT * FROM dbo.BP", myConnection);
                Adapter.Fill(ds, "BP");
                Adapter = new SqlDataAdapter("SELECT * FROM dbo.SATA", myConnection);
                Adapter.Fill(ds, "SATA");

            }
            catch (Exception)
            {
                MessageBox.Show("exception");
            }

            if (show == false)
            {
                //combobox

                comboBox1.DataSource = ds.Tables[0];
                comboBox1.ValueMember = "Name";

                comboBox2.DataSource = ds.Tables[1];
                comboBox2.ValueMember = "Model";

                comboBox3.DataSource = ds.Tables[2];
                comboBox3.ValueMember = "Модель";

                comboBox4.DataSource = ds.Tables[3];
                comboBox4.ValueMember = "Название";

                comboBox5.DataSource = ds.Tables[4];
                comboBox5.ValueMember = "Модель";

                comboBox5.DataSource = ds.Tables[4];
                comboBox5.ValueMember = "Модель";

                comboBox6.DataSource = ds.Tables[5];
                comboBox6.ValueMember = "Модель";
                //check
                show = true;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["PCI"]);
            checkBox2.Checked = Convert.ToBoolean(ds.Tables[0].Rows[comboBox1.SelectedIndex]["PCIEx1"]);
            checkBox3.Checked = Convert.ToBoolean(ds.Tables[0].Rows[comboBox1.SelectedIndex]["PCIEx4"]);
            checkBox4.Checked = Convert.ToBoolean(ds.Tables[0].Rows[comboBox1.SelectedIndex]["PCIEx16"]);
            textBox1.Text = ds.Tables[0].Rows[comboBox1.SelectedIndex]["DDR"].ToString();
            textBox2.Text = ds.Tables[0].Rows[comboBox1.SelectedIndex]["Socket"].ToString();
            textBox3.Text = ds.Tables[0].Rows[comboBox1.SelectedIndex]["SATA"].ToString();
            textBox4.Text = ds.Tables[0].Rows[comboBox1.SelectedIndex]["LAN"].ToString();
            textBox5.Text = ds.Tables[0].Rows[comboBox1.SelectedIndex]["Processors"].ToString();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkBox5.Checked = Convert.ToBoolean(ds.Tables[1].Rows[comboBox2.SelectedIndex]["DDR"]);
            checkBox6.Checked = Convert.ToBoolean(ds.Tables[1].Rows[comboBox2.SelectedIndex]["DDR2"]);
            checkBox7.Checked = Convert.ToBoolean(ds.Tables[1].Rows[comboBox2.SelectedIndex]["DDR3"]);
            textBox6.Text = ds.Tables[1].Rows[comboBox2.SelectedIndex]["Gz"].ToString();
            textBox7.Text = ds.Tables[1].Rows[comboBox2.SelectedIndex]["Capacity"].ToString();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox8.Text = ds.Tables[2].Rows[comboBox3.SelectedIndex]["Socket"].ToString();
            textBox9.Text = ds.Tables[2].Rows[comboBox3.SelectedIndex]["Тактовая_частота"].ToString();
            textBox10.Text = ds.Tables[2].Rows[comboBox3.SelectedIndex]["Число_ядер"].ToString();
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox11.Text = ds.Tables[3].Rows[comboBox4.SelectedIndex]["Тип_памяти"].ToString();
            textBox12.Text = ds.Tables[3].Rows[comboBox4.SelectedIndex]["Размер_оперативной_памяти"].ToString();
            textBox13.Text = ds.Tables[3].Rows[comboBox4.SelectedIndex]["Дополнительное_питание"].ToString();
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox14.Text = ds.Tables[4].Rows[comboBox5.SelectedIndex]["Мощность"].ToString();
            textBox15.Text = ds.Tables[4].Rows[comboBox5.SelectedIndex]["Питание_видеокарты"].ToString();
            textBox16.Text = ds.Tables[4].Rows[comboBox5.SelectedIndex]["Размер_вентилятора"].ToString();
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox17.Text = ds.Tables[5].Rows[comboBox6.SelectedIndex]["Тип"].ToString();
            textBox18.Text = ds.Tables[5].Rows[comboBox6.SelectedIndex]["Разъем"].ToString();
            textBox19.Text = ds.Tables[5].Rows[comboBox6.SelectedIndex]["Емкость"].ToString();
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            if (sender is CheckBox)
                ((CheckBox)sender).Checked = !((CheckBox)sender).Checked;
        }

        private void checkBox2_Click(object sender, EventArgs e)
        {
            if (sender is CheckBox)
                ((CheckBox)sender).Checked = !((CheckBox)sender).Checked;
        }

        private void checkBox3_Click(object sender, EventArgs e)
        {
            if (sender is CheckBox)
                ((CheckBox)sender).Checked = !((CheckBox)sender).Checked;
        }

        private void checkBox4_Click(object sender, EventArgs e)
        {
            if (sender is CheckBox)
                ((CheckBox)sender).Checked = !((CheckBox)sender).Checked;
        }

        private void checkBox5_Click(object sender, EventArgs e)
        {
            if (sender is CheckBox)
                ((CheckBox)sender).Checked = !((CheckBox)sender).Checked;
        }

        private void checkBox6_Click(object sender, EventArgs e)
        {
            if (sender is CheckBox)
                ((CheckBox)sender).Checked = !((CheckBox)sender).Checked;
        }

        private void checkBox7_Click(object sender, EventArgs e)
        {
            if (sender is CheckBox)
                ((CheckBox)sender).Checked = !((CheckBox)sender).Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string result = "";
            string DDR = textBox1.Text.Substring(2, 4);
            string HDD = textBox3.Text.Substring(2, 7);
            if (textBox2.Text == textBox8.Text) result += "Процессор подходит\n\n"; else result += "Процессор не подходит\n\n";
            if ((checkBox5.Checked && checkBox5.Text == DDR) ||
                (checkBox6.Checked && checkBox6.Text == DDR) ||
                (checkBox7.Checked && checkBox7.Text == DDR)) result += "Оперативная память подходит\n\n";
            else result += "Оперативная память не подходит\n\n";
            if ((checkBox1.Checked && checkBox1.Text == ds.Tables[3].Rows[comboBox4.SelectedIndex]["Разъем"].ToString()) ||
                (checkBox2.Checked && checkBox2.Text == ds.Tables[3].Rows[comboBox4.SelectedIndex]["Разъем"].ToString()) ||
                (checkBox3.Checked && checkBox3.Text == ds.Tables[3].Rows[comboBox4.SelectedIndex]["Разъем"].ToString()) ||
                (checkBox4.Checked && checkBox4.Text == ds.Tables[3].Rows[comboBox4.SelectedIndex]["Разъем"].ToString())) result += "Видеокарта подходит\n\n";
            else result += "Видеокарта не подходит\n\n";
            if (HDD.Contains(textBox18.Text) || textBox18.Text.Contains(HDD)) result += "Жесткий диск подходит\n\n"; else result += "Жесткий диск не подходит\n\n";
            if (textBox13.Text == textBox15.Text) result += "Блок питания подходит\n\n"; else result += "Блок питания не подходит к видеокарте\n\n";
            label1.Text = result;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Zapolnenie a = new Zapolnenie();
            a.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
