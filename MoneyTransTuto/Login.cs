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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=LAPTOP-N2GB72JE;Initial Catalog=MoneyTransDB;Integrated Security=True");

        private void label5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public static string UserName = "",UserCity = "";

        private void label8_Click(object sender, EventArgs e)
        {
            AdminLogin Obj = new AdminLogin();
            Obj.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (UNameTb.Text == "" || UPasswordTb.Text == "" || UCityCmb.SelectedIndex == -1)
            {
                MBox.Alert("Enter Both UserName and Password");
            }
            else
            {
                baglanti.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select count(*) from AgentTbl where AName = '" + UNameTb.Text + "'and APass = '" + UPasswordTb.Text + "'", baglanti);
                DataTable table = new DataTable();
                sda.Fill(table);
                if (table.Rows[0][0].ToString() == "1")
                {
                    UserName = UNameTb.Text;
                    UserCity = UCityCmb.SelectedItem.ToString();
                    Transactions Obj = new Transactions();
                    Obj.Show();
                    this.Hide();
                    baglanti.Close();

                }
                else
                {
                    MessageBox.Show("Wrong UserName or Password");
                }
                baglanti.Close();
            }
        }
    }
}
