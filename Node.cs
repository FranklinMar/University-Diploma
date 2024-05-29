using System;
using System.Drawing;
using System.Xml.Serialization;

namespace University_Diploma
{
    public class Point
    {
        public double X { get; private set; }
        public double Y { get; private set; }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }
    }

    [Serializable]
    public class Node: IComparable
    {
        [XmlAttribute("id")]
        public string ID { get; private set; }
        [XmlAttribute("label")]
        public string Label { get; set; }
        public Point Position;

        public Node(string id, string label)
        {
            ID = id;
            Label = label;
        }

        public Node(string id, string label, Point position)
        {
            ID = id;
            Label = label;
            Position = position;
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
