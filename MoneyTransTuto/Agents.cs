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
    public partial class Agents : Form
    {
        public Agents()
        {
            InitializeComponent();
            DisplayAgents();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=LAPTOP-N2GB72JE;Initial Catalog=MoneyTransDB;Integrated Security=True");
        private void Agents_Load(object sender, EventArgs e)
        {

        }

        private void Reset()
        {
            ANameTxt.Text = "";
            APhoneTxt.Text = "";
            ACityCmb.SelectedItem = -1;
            APasswordTxt.Text = "";
            key = 0;
        }
        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (ANameTxt.Text == "" || APhoneTxt.Text == "" || ACityCmb.SelectedIndex == -1 || APasswordTxt.Text == "")
            {
                MBox.Alert("Missing Information");
            }
            else
            {
                baglanti.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select count(*) from AgentTbl where AName ='" + ANameTxt.Text + "'and APass = '" + APasswordTxt.Text + "'and APhone ='"+APhoneTxt.Text+"'", baglanti);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if(dt.Rows[0][0].ToString() == "1")
                {
                    MBox.Alert("Kayıtlı Temsilci");
                }
                else
                {
                    try
                    {
                        //baglanti.Open();
                        SqlCommand komut = new SqlCommand("insert into AgentTbl(AName, APhone, ACity, APass) values('" + ANameTxt.Text + "','" + APhoneTxt.Text + "','" + ACityCmb.SelectedItem.ToString() + "','" + APasswordTxt.Text + "')", baglanti);
                        komut.ExecuteNonQuery();
                        MBox.Alert("Agent Saved Successfully");
                        baglanti.Close();
                        DisplayAgents();
                        Reset();
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("Hata : " + ex.Message);
                    }
                }

               
            }


            //if (ANameTxt.Text == "" || APhoneTxt.Text == "" || AAdressTxt.Text == "" || APasswordTxt.Text == "")
            //{
            //    MBox.Alert("Missing Information");
            //}
            //else
            //{
            //    try
            //    {
            //        baglanti.Open();
            //        SqlCommand komut = new SqlCommand("insert into AgentTbl(AName, APhone, ACity, APass) values(@An, @Ap, @Ac, @Apass)", baglanti);
            //        komut.Parameters.AddWithValue("@An", ANameTxt.Text);
            //        komut.Parameters.AddWithValue("@Ap", APhoneTxt.Text);
            //        komut.Parameters.AddWithValue("@Ac", AAdressTxt.Text);
            //        komut.Parameters.AddWithValue("@Apass", APasswordTxt.Text);
            //        komut.ExecuteNonQuery();
            //        MBox.Alert("Agent Saved Successfully");
            //        baglanti.Close();
            //    }
            //    catch (Exception ex)
            //    {

            //        MessageBox.Show("Hata : " + ex.Message);
            //    }
            //}





        }

        private void DisplayAgents()
        {
            baglanti.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select * from AgentTbl", baglanti);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            AgentsDGV.DataSource = ds.Tables[0];
            baglanti.Close();

        }

        int key = 0;
        private void AgentsDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ANameTxt.Text = AgentsDGV.SelectedRows[0].Cells[1].Value.ToString();
            APhoneTxt.Text = AgentsDGV.SelectedRows[0].Cells[2].Value.ToString();
            ACityCmb.SelectedItem = AgentsDGV.SelectedRows[0].Cells[3].Value.ToString();
            APasswordTxt.Text = AgentsDGV.SelectedRows[0].Cells[4].Value.ToString();

            if (ANameTxt.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(AgentsDGV.SelectedRows[0].Cells[0].Value.ToString());
            }

        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MBox.Alert("Select an Agent To Delete");
            }
            else
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("delete from AgentTbl where AId = @AgKey", baglanti);
                komut.Parameters.AddWithValue("AgKey", key);
                komut.ExecuteNonQuery();
                MBox.Alert("Agent Deleted");
                baglanti.Close();
                DisplayAgents();
                Reset();
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {


            if (ANameTxt.Text == "" || APhoneTxt.Text == "" || ACityCmb.SelectedIndex == -1 || APasswordTxt.Text == "")
            {
                MBox.Alert("Missing Information");
            }
            else
            {
                try
                {
                    baglanti.Open();
                    SqlCommand komut2 = new SqlCommand("update AgentTbl set AName = '" + ANameTxt.Text + "',APhone ='" + APhoneTxt.Text + "',ACity ='" + ACityCmb.SelectedItem.ToString() + "',APass ='" + APasswordTxt.Text + "'where AId = '" + key + "'", baglanti);
                    komut2.ExecuteNonQuery();
                    MBox.Alert("Agent Edit Successfully");
                    baglanti.Close();
                    DisplayAgents();
                    Reset();
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Hata : " + ex.Message);
                }
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {
            UpdatePass Obj = new UpdatePass();
            Obj.Show();
            this.Hide();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
