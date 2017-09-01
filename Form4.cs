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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        SqlDataAdapter Adapter;
        DataSet ds = new DataSet();
        SqlConnection myConnection = new SqlConnection("server=.;" +
                               "Trusted_Connection=yes;" +
                               "database=Trade; " +
                               "connection timeout=30");

        private void Form4_Load(object sender, EventArgs e)
        {
            try
            {
                 myConnection.Open();
                 Adapter = new SqlDataAdapter("SELECT * FROM dbo.Сотрудник", myConnection);
                 Adapter.Fill(ds, "Sotrudnik");
                 Adapter = new SqlDataAdapter("SELECT * FROM dbo.Клиент", myConnection);
                 Adapter.Fill(ds, "Klient");
                 Adapter = new SqlDataAdapter("SELECT id from dbo.Заказ", myConnection);
                 Adapter.Fill(ds, "Zakaz");
            }
            catch (Exception)
            {
                MessageBox.Show("exception");
            }

            comboBox1.DataSource = ds.Tables[0];
            comboBox1.ValueMember = "ФИО";
            comboBox2.DataSource = ds.Tables[1];
            comboBox2.ValueMember = "ФИО";
            AutoCompleteStringCollection col = new AutoCompleteStringCollection();
            for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                col.Add(ds.Tables[0].Rows[i]["ФИО"].ToString());

            }
            comboBox1.AutoCompleteCustomSource = col;
            col.Clear();
            for (int i = 0; i <= ds.Tables[1].Rows.Count - 1; i++)
            {
                col.Add(ds.Tables[1].Rows[i]["ФИО"].ToString());

            }
            comboBox2.AutoCompleteCustomSource = col;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {

        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            myConnection.Close();
            myConnection.Dispose();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = ds.Tables[0].Rows[comboBox1.SelectedIndex]["Фамилия"].ToString();
            textBox2.Text = ds.Tables[0].Rows[comboBox1.SelectedIndex]["Имя"].ToString();
            textBox3.Text = ds.Tables[0].Rows[comboBox1.SelectedIndex]["Отчество"].ToString();
            textBox4.Text = ds.Tables[0].Rows[comboBox1.SelectedIndex]["Должность"].ToString();
            textBox5.Text = ds.Tables[0].Rows[comboBox1.SelectedIndex]["Дата_рождения"].ToString();
            textBox6.Text = ds.Tables[0].Rows[comboBox1.SelectedIndex]["Адрес"].ToString();
            textBox7.Text = ds.Tables[0].Rows[comboBox1.SelectedIndex]["Телефон"].ToString();
            ds.Tables["Zakaz"].Clear();
            Adapter = new SqlDataAdapter("SELECT Заказ.id, Сотрудник.ФИО, Клиент.ФИО,"+
                                        "Заказ.Дата_заказа, Заказ.Тип_заказа, Заказ.Сумма,"+
                                        "Заказ.Комплектующие FROM Заказ INNER JOIN Клиент "+
                                        "ON Заказ.Клиент = Клиент.id INNER JOIN Сотрудник "+
                                        "ON Заказ.Сотрудник = Сотрудник.id WHERE Заказ.Сотрудник = '" + (comboBox1.SelectedIndex + 1) + "'", myConnection);
            Adapter.Fill(ds, "Zakaz");
            dataGridView1.DataSource = ds.Tables["Zakaz"];
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox8.Text = ds.Tables[1].Rows[comboBox2.SelectedIndex]["Фамилия"].ToString();
            textBox9.Text = ds.Tables[1].Rows[comboBox2.SelectedIndex]["Имя"].ToString();
            textBox10.Text = ds.Tables[1].Rows[comboBox2.SelectedIndex]["Отчество"].ToString();
            textBox11.Text = ds.Tables[1].Rows[comboBox2.SelectedIndex]["Телефон"].ToString();
            ds.Tables["Zakaz"].Clear();
            Adapter = new SqlDataAdapter("SELECT Заказ.id, Клиент.ФИО, Сотрудник.ФИО, " +
                                        "Заказ.Дата_заказа, Заказ.Тип_заказа, Заказ.Сумма," +
                                        "Заказ.Комплектующие FROM Заказ INNER JOIN Клиент " +
                                        "ON Заказ.Клиент = Клиент.id INNER JOIN Сотрудник " +
                                        "ON Заказ.Сотрудник = Сотрудник.id WHERE Заказ.Клиент = '" + (comboBox2.SelectedIndex + 1) + "'", myConnection);
            Adapter.Fill(ds, "Zakaz");
            dataGridView1.DataSource = ds.Tables["Zakaz"];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new_sotr a = new new_sotr();
            a.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            New_kient a = new New_kient();
            a.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection("server=.;" +
                              "Trusted_Connection=yes;" +
                              "database=Trade; " +
                              "connection timeout=30"))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = "UPDATE Сотрудник SET ФИО=@ФИО, Фамилия=@Фамилия, Имя=@Имя, Отчество=@Отчество, Должность=@Должность, Дата_рождения=@Дата_рождения, Адрес=@Адрес, Телефон=@Телефон WHERE id = '" + (comboBox1.SelectedIndex + 1) + "'";
                    command.Parameters.AddWithValue("@ФИО", textBox1.Text + " " + textBox2.Text + " " + textBox3.Text);
                    command.Parameters.AddWithValue("@Фамилия", textBox1.Text);
                    command.Parameters.AddWithValue("@Имя", textBox2.Text);
                    command.Parameters.AddWithValue("@Отчество", textBox3.Text);
                    command.Parameters.AddWithValue("@Должность", textBox4.Text);
                    command.Parameters.AddWithValue("@Дата_рождения", textBox5.Text);
                    command.Parameters.AddWithValue("@Адрес", textBox6.Text);
                    command.Parameters.AddWithValue("@Телефон", textBox7.Text);
                    try
                    {
                        connection.Open();
                        int recordsAffected = command.ExecuteNonQuery();
                        MessageBox.Show("Обновление успешно");
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection("server=.;" +
                              "Trusted_Connection=yes;" +
                              "database=Trade; " +
                              "connection timeout=30"))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = "DELETE FROM Сотрудник WHERE id = '" + (comboBox1.SelectedIndex + 1) + "'";
                    try
                    {
                        connection.Open();
                        int recordsAffected = command.ExecuteNonQuery();
                        MessageBox.Show("Удаление успешно");
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection("server=.;" +
                  "Trusted_Connection=yes;" +
                  "database=Trade; " +
                  "connection timeout=30"))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = "UPDATE Клиент SET ФИО=@ФИО, Фамилия=@Фамилия, Имя=@Имя, Отчество=@Отчество, Телефон=@Телефон WHERE id = '" + (comboBox2.SelectedIndex + 1) + "'";
                    command.Parameters.AddWithValue("@ФИО", textBox8.Text + " " + textBox9.Text + " " + textBox10.Text);
                    command.Parameters.AddWithValue("@Фамилия", textBox8.Text);
                    command.Parameters.AddWithValue("@Имя", textBox9.Text);
                    command.Parameters.AddWithValue("@Отчество", textBox10.Text);
                    command.Parameters.AddWithValue("@Телефон", textBox11.Text);
                    try
                    {
                        connection.Open();
                        int recordsAffected = command.ExecuteNonQuery();
                        MessageBox.Show("Обновление успешно");
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection("server=.;" +
                  "Trusted_Connection=yes;" +
                  "database=Trade; " +
                  "connection timeout=30"))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = "DELETE FROM Клиент WHERE id = '" + (comboBox2.SelectedIndex + 1) + "'";
                    try
                    {
                        connection.Open();
                        int recordsAffected = command.ExecuteNonQuery();
                        MessageBox.Show("Удаление успешно");
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

    }
}
