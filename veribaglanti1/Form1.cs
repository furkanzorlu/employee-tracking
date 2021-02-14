using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;  
using System.Collections; 
using System.IO;

namespace veribaglanti1
{
    public partial class Form1 : Form
    {
        OleDbConnection con;
        OleDbDataAdapter da;
        OleDbCommand cmd;
        OleDbDataReader dr;
        DataSet ds;

        public Form1()
        {
            InitializeComponent();
            button6.Enabled = false;
        }
        void griddoldur()
        {
            con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=FoodFabricaDB.mdb");
            da = new OleDbDataAdapter("SElect *from Employee", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "Employee");
            dataGridView1.DataSource = ds.Tables["Employee"];
            con.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            griddoldur();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tbno.Text == "")
            {
                MessageBox.Show("id cannot be blank");
            }
            else
            {
                cmd = new OleDbCommand();
                con.Open();
                cmd.Connection = con;

                cmd.CommandText = "insert into Employee (ıd,ad,maas,tel) values ('" + tbno.Text + "','" + tbad.Text + "','" + tbsoyad.Text + "','" + tbtel.Text + "')";
                //cmd.ExecuteNonQuery();
                try
                {
                    var results = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ids cannot be the same");
                }
                con.Close();
                griddoldur();
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cmd = new OleDbCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "update Employee set ad='" + tbad.Text + "',maas='" + tbsoyad.Text + "',tel='" + tbtel.Text + "' where ıd=" + tbno.Text + "";
            cmd.ExecuteNonQuery();
            con.Close();
            griddoldur();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cmd = new OleDbCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "delete from Employee where ıd=" + tbno.Text + "";
            cmd.ExecuteNonQuery();
            con.Close();
            griddoldur();
        }


        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            tbno.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            tbad.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            tbsoyad.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            tbtel.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=FoodFabricaDB.mdb");
            da = new OleDbDataAdapter("SElect *from Employee where ad like '" + textBox5.Text + "%'", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "Employee");
            dataGridView1.DataSource = ds.Tables["Employee"];
            con.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button6.Enabled = true;
            cmd = new OleDbCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM Employee";
            dr = cmd.ExecuteReader();

            ArrayList Isimler = new ArrayList();


            while (dr.Read())
            {
                Isimler.Add(dr["ad"]); 
                
            }
            foreach (string eleman in Isimler)
            {
                listBox5.Items.Add(eleman);

            }

            dr.Close();
            con.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {

            int kayitSayisi;
            cmd = new OleDbCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "SELECT COUNT(*) FROM Employee";
            kayitSayisi = (int)cmd.ExecuteScalar();
            con.Close();
            MessageBox.Show(kayitSayisi.ToString());
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.SaveFileDialog dialog = new System.Windows.Forms.SaveFileDialog();
            dialog.Title = "Choose";
            dialog.OverwritePrompt = true;
            dialog.Filter = "Metin Belgeleri|*.txt|Bütün Dosyalar|*.*";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) using (System.IO.StreamWriter writer = new System.IO.StreamWriter(dialog.FileName, false)) foreach (object satir in listBox5.Items) writer.WriteLine(satir);
            
                MessageBox.Show("saved");
           
            


        }
    }
}
