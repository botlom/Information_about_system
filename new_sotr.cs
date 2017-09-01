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
    public partial class new_sotr : Form
    {
        public new_sotr()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection("user id=SQLSERVER;" +
                               "password=1234;server=.;" +
                               "Trusted_Connection=yes;" +
                               "database=Trade; " +
                               "connection timeout=30"))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = "INSERT into Сотрудник (ФИО, Фамилия, Имя, Отчество, Должность, Дата_рождения, Адрес, Телефон) VALUES (@ФИО, @Фамилия, @Имя, @Отчество, @Должность, @Дата_рождения, @Адрес, @Телефон)";
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
                        MessageBox.Show("Добавление успешно");
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

        private void button2_Click(object sender, EventArgs e)
        {
            new_sotr.ActiveForm.Close();
        }
    }
}
