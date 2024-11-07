using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODAL
{
    public class No
    {
        public string value;
        public No next;
        public No(string value)
        {
            this.value = value;
            this.next = null;
        }
    }
}
