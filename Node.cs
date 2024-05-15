using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University_Diploma
{
    public class Node
    {
        public string ID { get; private set; }
        public string Label { get; set; } = null;
        //public bool Pole { get; private set; }// = false;

        public Node(string id, string label/*, bool pole = false*/)
        {
            ID = id;
            Label = label;
            //Pole = pole;
        }
    }
}
