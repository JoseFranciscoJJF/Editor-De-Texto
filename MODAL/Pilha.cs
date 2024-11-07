using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MODAL
{
    public class Pilha
    {
        No top;
        No actual;
        List <No> Delected;

        public Pilha()
        {
            this.top = null;
            this.actual = null;
        }
        public No RetornaTopo()
        {
            return this.top;
        }
        public No RetornaActual()
        {
            return this.actual;
        }
        public void Add(No Element)
        {
            if (this.actual == null)
            {
                this.actual = Element;
                Delected = new List <No>();
            }
            else
            {
                //Element.next = this.top;
                //this.top = Element;
                Element.next = this.actual;
                this.actual = Element;
                Delected.Clear();
            }
        }
        public bool Next()
        {
            //if (this.actual != this.actual.next)
            if (Delected.Count()!=0)
            {
                Delected[Delected.Count()-1].next = this.actual;
                this.actual = Delected[Delected.Count()-1];
                Delected.RemoveAt(Delected.Count()-1);
                //actual.next = this.actual;
                return true;
            }
            else
                return false;
        }
        public void Remember(No Element)
        {
            if (this.actual == null)
            {
                this.top = Element;
                this.actual = top;
            }
            else
            {
                Element.next = this.actual;
                this.actual = Element;
                this.top = this.actual;
            }
        }
        public bool Antecedent()
        {
            if (this.actual == null)
                return false;
            else
            {
                //top = top.next;
                if (Delected == null)
                {
                    No item = null;
                    item = new No(actual.value);
                    Delected.Add(item);
                }
                else
                {
                    No item = null;
                    item = new No(actual.value);
                    Delected.Add(item);
                }
                actual = actual.next;
                return true;
            }
        }
    }
}
