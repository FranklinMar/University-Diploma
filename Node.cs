using System;
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

        public Node(string id, string label)
        {
            ID = id;
            Label = label;
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
