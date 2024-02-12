using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace crazy_buttons
{
    class ButtonComparable: Button, IComparable
    {
        public int CompareTo(object obj) 
        {
            ButtonComparable tmp = (ButtonComparable)obj;
            if(this.Location.X > tmp.Location.X) 
            {
                return -1;
            }
            else 
            { 
                if(this.Location.X <  tmp.Location.X) { return 1; }
                else {  return 0; }
            }
        }
    }
}
