using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace MoneyTransTuto
{
    public partial class AdminLogin : Form
    {
        public AdminLogin()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=LAPTOP-N2GB72JE;Initial Catalog=MoneyTransDB;Integrated Security=True");

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            if(UPasswordTb.Text == "")
            {
                MBox.Alert("Enter the Password");
            }
            else
            {

                try
                {
                    baglanti.Open();
                    SqlDataAdapter sda = new SqlDataAdapter("select count(*) from AdminTbl where AdPass = '" + UPasswordTb.Text + "'", baglanti);
                    DataTable table = new DataTable();
                    sda.Fill(table);
                    if (table.Rows[0][0].ToString() == "1")
                    {
                        Agents Obj = new Agents();
                        Obj.Show();
                        this.Hide();
                        baglanti.Close();
                    }
                    else
                    {
                        MessageBox.Show("Wrong Admin Password");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata : " + ex.Message);
                    
                }
               
                baglanti.Close(); 
            }
           
        }
    }
}
