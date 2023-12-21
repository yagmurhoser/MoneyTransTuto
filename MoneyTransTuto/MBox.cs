using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MoneyTransTuto
{
    public partial class MBox : Form
    {
        public MBox()
        {
            InitializeComponent();
            MessageLbl.Text = AMessage;
        }
        static string AMessage;

        public static void Alert(string Msg)
        {
            AMessage = Msg; 
            MBox Obj = new MBox();
            Obj.Show();

        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
