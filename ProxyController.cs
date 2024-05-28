using Newtonsoft.Json.Linq;
using QuikGraph;
using System;
using System.Linq;
using System.Windows.Forms;

namespace University_Diploma
{
    public delegate void Notify(UndirectedGraph<Node, GraphEdge> graph);
    public class ProxyController
    {
        public UndirectedGraph<Node, GraphEdge> Graph { get; private set; }// = new();
        public event Notify GraphChanged;

        public ProxyController(Notify graphChanged)
        {
            GraphChanged += graphChanged;
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
                    Graph.AddVertex(new Node(Node.Value<string>("id"), Node.Value<string>("label")));
                }
            }
            foreach (JToken Edge in edges)
            {
                try
                {
                    Node Source = Graph.Vertices.First(node => node.ID.Equals(Edge.Value<string>("source")));
                    Node Target = Graph.Vertices.First(node => node.ID.Equals(Edge.Value<string>("target")));
                    GraphEdge edge;
                    try
                    {
                        if (Edge.Value<string>("label") != "")
                        {
                            edge = new(Source, Target, Edge.Value<double>("label"));
                            Graph.AddEdge(edge);
                        }
                    }
                    catch (ArgumentException)
                    {
                    }
                }
                catch (InvalidOperationException)
                {
                    continue;
                }
            }
            GraphChanged?.Invoke(Graph);
        }
    }
}
