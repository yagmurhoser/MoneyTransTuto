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
    public partial class Receives : Form
    {
        public Receives()
        {
            InitializeComponent();
            DisplayRec();
            CityLbl.Text = Transactions.UserCity;
        }
        SqlConnection baglanti = new SqlConnection("Data Source=LAPTOP-N2GB72JE;Initial Catalog=MoneyTransDB;Integrated Security=True");

        int Amt;
        private void CheckCode()
        {

            if (SCodeTxt.Text == "")
            {
                MessageBox.Show("Enter The Code");
            }
            else
            {
                baglanti.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select count(*) from SendTbl where SCode = '" + SCodeTxt.Text + "' and Collected = '"+"No"+"' and RCity = '"+CityLbl.Text+"'", baglanti);
                DataTable table = new DataTable();
                sda.Fill(table);
                if (table.Rows[0][0].ToString() == "1")
                {
                    try
                    {
                       // baglanti.Open();
                        SqlCommand komut = new SqlCommand("select * from SendTbl where SCode = '" + SCodeTxt.Text + "' ", baglanti);
                        SqlDataAdapter sda1 = new SqlDataAdapter(komut);
                        DataTable table1 = new DataTable();
                        sda1.Fill(table1);
                        foreach (DataRow dr in table1.Rows) //table ın her satırını döngüye aldık(1 satır var)
                        {
                            SNameLbl.Text = dr["SenderName"].ToString(); // her sütununu bir label a atadık.
                            RNameLbl.Text = dr["ReceiverName"].ToString();

                            Amt = Convert.ToInt32(dr["SAmt"].ToString());

                            AmtLbl.Text = "RS :" + dr["SAmt"].ToString();
                            SentDateLbl.Text = dr["SDate"].ToString();
                            SCityLbl.Text = dr["SCity"].ToString();
                            CityLbl.Text = dr["RCity"].ToString();
                        }
                        baglanti.Close();
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("Hata : " + ex.Message);
                    }

                }
                else
                {
                    MessageBox.Show("No Transaction this Code");
                    baglanti.Close();
                }





            }
        }



        private void DisplayRec()
        {
            baglanti.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select * from ReceiveTbl", baglanti);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            ReceiveDGV.DataSource = ds.Tables[0];
            baglanti.Close();

        }



        private void button5_Click(object sender, EventArgs e)
        {
            CheckCode();
        }

        private void Reset()
        {
            SNameLbl.Text = "";
            RNameLbl.Text = "";
            AmtLbl.Text = "";
            SentDateLbl.Text = "";
            SCodeTxt.Text = "";
            SCityLbl.Text = "";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void Receives_Load(object sender, EventArgs e)
        {
            TodayLbl.Text = TodayDate.Value.Date.ToString("yyyy-MM-dd");
        }

        private void UpdateSend()
        {
                       
                try
                {
                    baglanti.Open();
                    SqlCommand komut2 = new SqlCommand("update SendTbl set Collected = '"+"Yes"+"' where SCode = '"+SCodeTxt.Text+"'", baglanti);
                    komut2.ExecuteNonQuery();
                    //MBox.Alert("Agent Edit Successfully");
                    baglanti.Close();
                    //DisplayRec();
                    Reset();
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Hata : " + ex.Message);
                }            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (SCodeTxt.Text == "" || SNameLbl.Text == "" || RNameLbl.Text == "" || AmtLbl.Text == "" || SentDateLbl.Text =="" || SCityLbl.Text =="")
            {
                MBox.Alert("Missing Information");
            }
            else
            {
                try
                {
                    //int total;
                    //double Rate = Convert.ToDouble(RateTxt.Text) / 100;
                    //double Fess = Convert.ToDouble(AmtTxt.Text) * Rate;
                    //total = Convert.ToInt32(AmtTxt.Text) + Convert.ToInt32(Fess);
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("insert into ReceiveTbl(SCode, SName, RName, STotal, SDate, RDate, RCity, SCity) values('" + SCodeTxt.Text + "','" + SNameLbl.Text + "','" + RNameLbl.Text + "','" + Amt + "','" + SentDateLbl.Text + "', '"+TodayLbl.Text+"','" + CityLbl.Text + "','" + SCityLbl.Text + "')", baglanti);
                    komut.ExecuteNonQuery();
                    MBox.Alert("Money Received");
                    baglanti.Close();
                    DisplayRec();
                    UpdateSend();
                    Reset();
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Hata : " + ex.Message);
                }
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {
            Transactions Obj = new Transactions();
            Obj.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }
        private void Search()
        {

            if (textBox5.Text == "")
            {
                MBox.Alert("Enter a Sender Name");
            }
            else
            {
                baglanti.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select * from ReceiveTbl where RName = '" + textBox5.Text + "'", baglanti);
                SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                ReceiveDGV.DataSource = ds.Tables[0];
                baglanti.Close();

            }



        }

        private void button4_Click(object sender, EventArgs e)
        {
            Search();
            textBox5.Text = "";
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            DisplayRec();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
