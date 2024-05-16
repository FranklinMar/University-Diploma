using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace University_Diploma
{
    [Serializable]
    public class Node: IComparable
    {
        [XmlAttribute("id")]
        public string ID { get; private set; }
        [XmlAttribute("label")]
        public string Label { get; set; }
        //public bool Pole { get; private set; }// = false;

        public Node(string id, string label/*, bool pole = false*/)
        {
            ID = id;
            Label = label;
            //Pole = pole;
        }

        public int CompareTo(object obj)
        {
            if (!(obj is Node))
            {
                return -1;
            }
            return 0;
            /*Node Node = obj as Node;
            int Result = ID.CompareTo(Node.ID);
            if (Result != 0)
            {
                return Result;
            }
            return Label.CompareTo(Node.Label);*/
        }
    }
}
