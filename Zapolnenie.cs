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
    public partial class Zapolnenie : Form
    {
        public Zapolnenie()
        {
            InitializeComponent();
        }
        SqlDataAdapter Adapter;
        DataSet ds = new DataSet();
        SqlConnection myConnection = new SqlConnection("user id=SQLSERVER;" +
                               "password=1234;server=.;" +
                               "Trusted_Connection=yes;" +
                               "database=PC; " +
                               "connection timeout=30");
        private void Zapolnenie_Load(object sender, EventArgs e)
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
            dataGridView1.DataSource = ds.Tables["Mother"];
            dataGridView2.DataSource = ds.Tables["DDR"];
            dataGridView3.DataSource = ds.Tables["CPU"];
            dataGridView4.DataSource = ds.Tables[3];
            dataGridView5.DataSource = ds.Tables[4];
            dataGridView6.DataSource = ds.Tables[5];
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
            Adapter = new SqlDataAdapter("SELECT * FROM dbo.MotherBoard", myConnection);
            new SqlCommandBuilder(Adapter);
            Adapter.Update(ds, "Mother");
            Adapter = new SqlDataAdapter("SELECT * FROM dbo.DDR", myConnection);
            new SqlCommandBuilder(Adapter);
            Adapter.Update(ds, "DDR");
            Adapter = new SqlDataAdapter("SELECT * FROM dbo.CPU", myConnection);
            new SqlCommandBuilder(Adapter);
            Adapter.Update(ds, "CPU");
            Adapter = new SqlDataAdapter("SELECT * FROM dbo.GDDR", myConnection);
            new SqlCommandBuilder(Adapter);
            Adapter.Update(ds, "GDDR");
            Adapter = new SqlDataAdapter("SELECT * FROM dbo.BP", myConnection);
            new SqlCommandBuilder(Adapter);
            Adapter.Update(ds, "BP");
            Adapter = new SqlDataAdapter("SELECT * FROM dbo.SATA", myConnection);
            new SqlCommandBuilder(Adapter);
            Adapter.Update(ds, "SATA");
            }
            catch (Exception)
            {
                MessageBox.Show("Изменение в базе данных выполнить не удалось!", "Результат", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
            dataGridView2.Rows.RemoveAt(dataGridView2.CurrentRow.Index);
            dataGridView3.Rows.RemoveAt(dataGridView3.CurrentRow.Index);
            dataGridView4.Rows.RemoveAt(dataGridView4.CurrentRow.Index);
            dataGridView5.Rows.RemoveAt(dataGridView5.CurrentRow.Index);
            dataGridView6.Rows.RemoveAt(dataGridView6.CurrentRow.Index);
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void Zapolnenie_FormClosing(object sender, FormClosingEventArgs e)
        {
            myConnection.Close();
            myConnection.Dispose();
        }
    }
}
