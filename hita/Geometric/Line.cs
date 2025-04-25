using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hita.Geometric
{
    public class Line
    {
        public Node M = new Node();
        public Node N = new Node();
        public Line(Node M, Node N)
        {
            this.M = M;
            this.N = N;
        }
        public Line() { }
    }
}
