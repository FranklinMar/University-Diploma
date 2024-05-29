using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using QuikGraph;

namespace University_Diploma
{
    public delegate void NotifyGraph(UndirectedGraph<Node, GraphEdge> graph);
    public delegate void NotifyNode(Node node);
    public class ProxyController
    {
        public UndirectedGraph<Node, GraphEdge> Graph { get; private set; }
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
                Node Source = Graph.Vertices.First(node => node.ID.Equals(Edge.Value<string>("source")));
                Node Target = Graph.Vertices.First(node => node.ID.Equals(Edge.Value<string>("target")));
                GraphEdge edge;
                if (Edge.Value<string>("label") != null)
                {
                    edge = new(Source, Target, double.Parse(Edge.Value<string>("label").Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture));
                    Graph.AddEdge(edge);
                }
            }
            GraphChanged?.Invoke(Graph);
        }
        public void RecieveNode(string JSON)
        {
            JObject JNode = JObject.Parse(JSON);
            var Position = JNode.Value<JToken>("position");
            Point Point = new (Position.Value<double>("x"), Position.Value<double>("y"));
            Node Node = new (JNode.Value<string>("id"), JNode.Value<string>("label"), Point);
            NodeChanged?.Invoke(Node);
        }
    }
}
