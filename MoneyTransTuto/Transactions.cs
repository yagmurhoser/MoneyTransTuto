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
    public partial class Transactions : Form
    {
        public Transactions()
        {
            InitializeComponent();
            CityLbl.Text = Login.UserCity;
            Sname = Login.UserName;
            DisplaySent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=LAPTOP-N2GB72JE;Initial Catalog=MoneyTransDB;Integrated Security=True");

        private void DisplaySent()
        {
            baglanti.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select * from SendTbl", baglanti);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            SendDGV.DataSource = ds.Tables[0];
            baglanti.Close();

        }


        private void Reset()
        {
            SCodeTxt.Text = "";
            SNameTxt.Text = "";
            RecNameTxt.Text = "";
            AmtTxt.Text = "";
            UCityCmb.Text = "";
            
        }


        private void Search()
        {

            if(SearchTxt.Text == "")
            {
                MBox.Alert("Enter a Sender Name");
            }
            else
            {
                baglanti.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select * from SendTbl where SenderName = '" + SearchTxt.Text + "'", baglanti);
                SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                SendDGV.DataSource = ds.Tables[0];
                baglanti.Close();

            }



        }

        string Sname;
        public static string UserCity = "";

        private void button1_Click(object sender, EventArgs e)
        {

            printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 650, 450);
            if(printPreviewDialog1.ShowDialog()== DialogResult.OK)
            {
                printDocument1.Print();
            }


            if (SCodeTxt.Text == "" || SNameTxt.Text == "" || RecNameTxt.Text == "" || AmtTxt.Text == "" || UCityCmb.SelectedIndex == -1)
            {
                MBox.Alert("Missing Information");
            }
            else
            {
                try
                {
                    int total;
                    double Rate = Convert.ToDouble(RateTxt.Text) / 100;
                    double Fess = Convert.ToDouble(AmtTxt.Text) * Rate;
                    total = Convert.ToInt32(AmtTxt.Text) + Convert.ToInt32(Fess);
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("insert into SendTbl(SCode, SenderName,ReceiverName, SAmt, STotal, SDate, RCity, SCity, Collected) values('" + SCodeTxt.Text + "','" + SNameTxt.Text + "','" + RecNameTxt.Text + "','" + AmtTxt.Text + "','" + total + "','" + SDate.Value.ToString("yyyy-MM-dd") + "','" + UCityCmb.SelectedItem.ToString() + "','" + CityLbl.Text + "','" + "No" + "')", baglanti);
                    komut.ExecuteNonQuery();
                    MBox.Alert("Money Sent");
                    baglanti.Close();
                    DisplaySent();
                    //Reset();
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Hata : " + ex.Message);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Search();
            SearchTxt.Text = "";

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            DisplaySent();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {
            UserCity = CityLbl.Text;
            Receives Obj = new Receives();
            Obj.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
