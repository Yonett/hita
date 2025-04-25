using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hita.Geometric
{
    public class Element
    {
        public int[] node_numbers = new int[4];
        public Element() { }
        public Element(int a, int b, int c, int d)
        {
            node_numbers[0] = a;
            node_numbers[1] = b;
            node_numbers[2] = c;
            node_numbers[3] = d;
        }
    }
}
