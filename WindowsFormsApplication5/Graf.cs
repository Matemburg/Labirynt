using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Labirynt
{

    class Node
    {
        int level;
        public List<Node> Parent;
        public List<Node> Child;
        public char wartosc;

        public Node ()
        {
            Parent = new List<Node>();
            Child = new List<Node>();
        }
    }

    class Graf
    {
         
    }
}
