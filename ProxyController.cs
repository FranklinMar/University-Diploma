using Newtonsoft.Json.Linq;
using QuikGraph;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Forms;

namespace University_Diploma
{
    public delegate void NotifyGraph(UndirectedGraph<Node, GraphEdge> graph);
    public delegate void NotifyNode(Node node);
    public class ProxyController
    {
        public UndirectedGraph<Node, GraphEdge> Graph { get; private set; }// = new();
        public event NotifyGraph GraphChanged;
        public event NotifyNode NodeChanged;

        public ProxyController(NotifyGraph graphChanged, NotifyNode nodeChanged)
        {
            GraphChanged += graphChanged;
            NodeChanged += nodeChanged;
        }

        public void DisplayError(string message)
        {
            MessageBox.Show(message, "Error");
        }

        public void RecieveGraph(string JSON)
        {
            JObject JSONObject = JObject.Parse(JSON);
            //Graph.Clear();
            Graph = new();
            JToken[] edges = JSONObject.GetValue("edges").ToArray();
            JToken[] nodes = JSONObject.GetValue("nodes").ToArray();
            foreach (JToken Node in nodes)
            {
                if (Node.Value<string>("label") != "")
                {
                    var Position = Node.Value<JToken>("position");
                    Point Point = new (Position.Value<double>("x"), Position.Value<double>("y"));
                    Graph.AddVertex(new Node(Node.Value<string>("id"), Node.Value<string>("label"), Point));
                }
            }
            foreach (JToken Edge in edges)
            {
                //try
                //{
                    Node Source = Graph.Vertices.First(node => node.ID.Equals(Edge.Value<string>("source")));
                    Node Target = Graph.Vertices.First(node => node.ID.Equals(Edge.Value<string>("target")));
                    GraphEdge edge;
                    //try
                    //{
                if (Edge.Value<string>("label") != null)
                {
                    //MessageBox.Show(.ToString());
                    edge = new(Source, Target, double.Parse(Edge.Value<string>("label").Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture));
                    Graph.AddEdge(edge);
                }
                    //}
                    //catch (ArgumentException)
                    //{
                    //}
                //}
                //catch (InvalidOperationException)
                //{
                //    continue;
                //}
            }
            GraphChanged?.Invoke(Graph);
        }
        public void RecieveNode(string JSON)
        {
            //MessageBox.Show(JSON);
            JObject JNode = JObject.Parse(JSON);
            var Position = JNode.Value<JToken>("position");
            Point Point = new (Position.Value<double>("x"), Position.Value<double>("y"));
            Node Node = new (JNode.Value<string>("id"), JNode.Value<string>("label"), Point);
            NodeChanged?.Invoke(Node);
        }
    }
}
