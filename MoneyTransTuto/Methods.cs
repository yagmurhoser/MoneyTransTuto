using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTransTuto
{
    internal class Methods
    {
        public void Alert(string Msg)
        {
            MBox Obj = new MBox();            
            Obj.Show();
            Obj.TopMost = true;

        }



    }
}
