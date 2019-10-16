using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameAssistant.Movement
{
    class Mover
    {
        public static void randomove()
        {
            var rando = new Random().Next(1, 5);

            switch (rando)
            {
                case 1:
                    SendKeys.Send("w");
                    break;
                case 2:
                    SendKeys.Send("b");
                    break;
                case 3:
                    SendKeys.Send("c");
                    break;
                case 4:
                    SendKeys.Send("p");
                    break;
                case 5:
                    SendKeys.Send("s");
                    break;
            }     
                       
        }

    }
}
