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
    public partial class UpdatePass : Form
    {
        public UpdatePass()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=LAPTOP-N2GB72JE;Initial Catalog=MoneyTransDB;Integrated Security=True");

        private void label8_Click(object sender, EventArgs e)
        {
            Agents Obj = new Agents();
            Obj.Show();
            this.Hide();
        }

        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            if(UPasswordTb.Text == "")
            {
                MBox.Alert("Enter New Password");
            }
            else
            {
                try
                {
                    baglanti.Open();
                    SqlCommand komut2 = new SqlCommand("update AdminTbl set AdPass = '" + UPasswordTb.Text + "' where AdId = '" + 1 + "'", baglanti);
                    komut2.ExecuteNonQuery();
                    MessageBox.Show("Password Updated Successfully");
                    baglanti.Close();
                    AdminLogin Obj = new AdminLogin();
                    Obj.Show();
                    this.Hide();
                    
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Hata : " + ex.Message);
                }
            }
        }
    }
}
