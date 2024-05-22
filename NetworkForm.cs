using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using CefSharp;
using CefSharp.WinForms;
using CefSharp.SchemeHandler;
using QuikGraph;
using QuikGraph.Serialization;
using System.Xml;
using System.Xml.Linq;
//using Microsoft.Msagl.Drawing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace University_Diploma
{
    public partial class NetworkForm : Form
    {
        // UndirectedGraph<Node, Edge<Node>> Graph;
        private readonly ProxyController Proxy;
        private readonly GraphHandler Handler;
        //private JObject CurrentJSON;
        //private readonly List<List<Node>> NodePaths;
        //private readonly List<List<Edge<Node>>> EdgePaths;

        //private bool IsLoading = true;

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool AllocConsole();
        private static void AllocateConsole()
        {
            AllocConsole();
            Console.OutputEncoding = Encoding.UTF8;
        }

        public NetworkForm()
        {
            //AllocateConsole();
            InitializeComponent();
            Proxy = new(OnGraphRecieved);
            Handler = new(Proxy.Graph/*, Proxy.Probabilities*/);
            string Domain = "modelling";
            string Scheme = "resources";
            CefSettings Settings = new();
            Settings.RegisterScheme(new CefCustomScheme
            {
                SchemeName = Scheme,
                DomainName = Domain,
                SchemeHandlerFactory = new FolderSchemeHandlerFactory(
                    rootFolder: @"./FrontEnd/",
                    hostName: Domain,
                    defaultPage: @"./Front.html")
            });
            Cef.Initialize(Settings);
            Browser.JavascriptObjectRepository.Settings.LegacyBindingEnabled = true;
            Browser.JavascriptObjectRepository.Register("Proxy", Proxy, options: BindingOptions.DefaultBinder);
            Browser.Load($"{Scheme}://{Domain}/");
        }

        private void PageLoaded(object sender, LoadingStateChangedEventArgs e)
        {
            Browser.ShowDevTools();
        }

        private void ChangeFrontEndProbability(GraphEdge Edge)
        {
            JObject JEdge = new();
            JEdge.Add("source", Edge.Source.ID);
            JEdge.Add("target", Edge.Target.ID);
            JEdge.Add("label", Edge.Probability.ToString());
            string Script = $"RefreshEdge({JEdge});";
            //Script += @"";
            Browser.ExecuteScriptAsync(Script);
        }
        private void ChangeFrontEndGraph()
        {
            var Graph = Handler.Graph;
            JObject JSONObject = new();
            var Edges = Graph.Edges;
            var Nodes = Graph.Vertices;
            JArray JEdges = new();
            JArray JNodes = new();
            foreach (Node Node in Nodes)
            {
                JObject JNode = new();
                JNode.Add("id", Node.ID);
                JNode.Add("label", Node.Label);
                JNodes.Add(JNode);
            }
            foreach (GraphEdge Edge in Edges)
            {
                JObject JEdge = new();
                JEdge.Add("source", Edge.Source.ID);
                JEdge.Add("target", Edge.Target.ID);
                JEdge.Add("label", Edge.Probability.ToString());
                JEdges.Add(JEdge);
            }
            //string output = JsonConvert.SerializeObject(Graph);
            JSONObject.Add("nodes", JNodes);
            JSONObject.Add("edges", JEdges);
            //MessageBox.Show(JSONObject.ToString());
            string Script = $"RefreshGraph({JSONObject});";
            //Script += @"";
            Browser.ExecuteScriptAsync(Script);

        }

        private void OnGraphRecieved(/*JObject JSON*/)
        {
            //CurrentJSON = JSON;
            var Edges = Handler.Graph.Edges;
            List<FlowLayoutPanel> Panels = new();
            foreach (GraphEdge Edge in Edges)
            {
                FlowLayoutPanel Panel = new();
                Panel.FlowDirection = FlowDirection.LeftToRight;
                Panel.AutoSize = true;
                FlowLayoutPanel SmallerPanel = new();
                SmallerPanel.FlowDirection = FlowDirection.TopDown;
                SmallerPanel.AutoSize = true;
                Label Label = new();
                Label.TextAlign = ContentAlignment.TopCenter;
                Label.BackColor = Color.White;
                Label.AutoSize = true;
                Label.ForeColor = Color.Black;
                Label.Font = new Font("Roboto", 11);
                Label.Text = $"P[{Edge.Source.Label}-{Edge.Target.Label}]";
                SmallerPanel.Controls.Add(Label);
                Label = new();
                Label.TextAlign = ContentAlignment.TopCenter;
                Label.AutoSize = true;
                Label.ForeColor = Color.White;
                Label.Font = new Font("Roboto", 13);
                Label.Text = string.Format("{0:N2}", Edge.Probability);
                EdgeBar Bar = new(Edge, Label);
                Bar.Width = (int)(Panel.Width * 0.6);
                SmallerPanel.Controls.Add(Label);
                Panel.Controls.Add(Bar);
                Panel.Controls.Add(SmallerPanel);
                var Handler = new EventHandler((sender, args) =>
                {
                    /*TrackBar This = sender as TrackBar;
                    Edge.Probability = This.Value / 100.0;
                    Label.Text = string.Format("{0:N2}", Edge.Probability);
                    ChangeFrontEndProbability(Edge);//ChangeGraph();*/
                    EdgeBar This = sender as EdgeBar;
                    //MessageBox.Show($"P[{This.Edge.Source.Label}-{This.Edge.Target.Label}]");
                    This.ChangeValue();
                    ChangeFrontEndProbability(This.Edge);
                });
                Bar.Scroll += Handler;
                Bar.ValueChanged += Handler;
                Panels.Add(Panel);
            }
            if (ProbPanel.InvokeRequired)
            {
                ProbPanel.Invoke(new MethodInvoker(delegate {
                    ProbPanel.Controls.Clear();
                    foreach (var Panel in Panels)
                    {
                        ProbPanel.Controls.Add(Panel);
                    }
                }));
            }
        }

        private void SelectionChanged(object sender, EventArgs e)
        {
            //CalcButton.Enabled = (SourceBox.SelectedItem != TargetBox.SelectedItem &&
            //    SourceBox.SelectedItem != null && TargetBox.SelectedItem != null);
        }

        private void CalcClick(object sender, EventArgs e)
        {
            Calculations Form = new(Handler/*, Source, Target*/);
            Form.Show();
            /*var Source = Handler.Graph.Vertices.Where(Node => Node.Label.Equals(SourceBox.SelectedItem)).First();
            var Target = Handler.Graph.Vertices.Where(Node => Node.Label.Equals(TargetBox.SelectedItem)).First();
            var Paths = Handler.AllMinPaths(Source, Target);//Handler.AllEdgePaths(Source, Target);
            var Cuts = Handler.AllMinCuts(Source, Target);*/
            //var NodePaths = Handler.NodePaths;
            /*Console.WriteLine("Edge paths");
            foreach (List<GraphEdge> Path in Paths)
            {
                Path.ForEach(Edge => Console.Write($"{Edge.Source.Label}---{Edge.Target.Label}|"));
                Console.Write("\n");
            }*/
            /*Console.WriteLine("Node Paths");
            foreach (List<Node> Path in NodePaths)
            {
                Path.ForEach(Node => Console.Write($"{Node.Label}|"));
                Console.Write("\n");
            }*/
            /*Console.WriteLine("\nMin cuts");
            foreach (List<GraphEdge> Cut in Cuts)
            {
                Cut.ForEach(Edge => Console.Write($"{Edge.Source.Label}---{Edge.Target.Label}|"));
                Console.Write("\n");
            }*/
            //Handler.EzariProshanAccount(Source, Target);
        }

        private void ImportClick(object sender, EventArgs e)
        {
            //ChangeFrontEndGraph();
            //return;
            OpenFileDialog Dialog = new();
            Dialog.Filter = "GraphML files | *.graphml"; // file types, that will be allowed to upload
            Dialog.Multiselect = false; // allow/deny user to upload more than one file at a time
            if (Dialog.ShowDialog() == DialogResult.Cancel)
                return;
            string FileName = Dialog.FileName; // get name of file
            /*UndirectedGraph<Node, GraphEdge> Deserialized = new();
            var VertexFactory = new IdentifiableVertexFactory<Node>((id, label) => new Node(id, label);
            var EdgeFactory = new IdentifiableEdgeFactory<Node, GraphEdge>();
            Deserialized = Deserialized.DeserializeFromGraphML(Path, (string id, string label) => new Node(id, label), (Node source, Node target, double probability) => new GraphEdge(source, target, probability));*/
            var Graph = Proxy.Graph;
            Graph.Clear();
            //var Namespace = XNamespace.Get("http://graphml.graphdrawing.org/xmlns");
            //XName XNgraph = XName.Get("graph", "http://graphml.graphdrawing.org/xmlns");
            var XML = XDocument.Load(FileName);
            XNamespace NS = "http://graphml.graphdrawing.org/xmlns";
            var graph = XML.Descendants(NS + "graph").First();
            var Nodes = graph.Elements(NS + "node").ToList();
            var Edges = graph.Elements(NS + "edge").ToList();
            foreach (var Node in Nodes)
            {
                string ID = Node.Elements(NS + "data").Where(Data => Data.Attribute("key").Value == "id").Select(Data => Data.Value).First();
                string Label = Node.Elements(NS + "data").Where(Data => Data.Attribute("key").Value == "label").Select(Data => Data.Value).First();
                Graph.AddVertex(new Node(ID, Label));
            }
            foreach (var Edge in Edges)
            {
                string Source = (from Node in Nodes
                                 where Node.Attribute("id").Value.Equals(Edge.Attribute("source").Value)
                                 select Node.Elements(NS + "data").Where(Data => Data.Attribute("key").Value == "id").First().Value).First();
                string Target = (from Node in Nodes
                                 where Node.Attribute("id").Value.Equals(Edge.Attribute("target").Value)
                                 select Node.Elements(NS + "data").Where(Data => Data.Attribute("key").Value == "id").First().Value).First();
                Node source = Graph.Vertices.Where(Node => Node.ID.Equals(Source)).First();
                Node target = Graph.Vertices.Where(Node => Node.ID.Equals(Target)).First();
                //MessageBox.Show(Edge.Element(NS + "data").Value);
                Graph.AddEdge(new GraphEdge(source, target, double.Parse(Edge.Element(NS + "data").Value.Replace('.', ','))));
            }
            ChangeFrontEndGraph();
            //var edges = xelement.Elements(XNgraph)
        }

        private void ExportClick(object sender, EventArgs e)
        {
            SaveFileDialog Dialog = new();
            Dialog.Filter = "GraphML files | *.graphml"; // file types, that will be allowed to upload
            if (Dialog.ShowDialog() == DialogResult.Cancel)
                return;
            // get selected file
            string Filename = Dialog.FileName;
            // save text into the file
            var Graph = Proxy.Graph;
            using XmlWriter Writer = XmlWriter.Create(Filename, new XmlWriterSettings { Indent = true, WriteEndDocumentOnClose = false });
            Graph.SerializeToGraphML<Node, GraphEdge, UndirectedGraph<Node, GraphEdge>>(Writer);
        }

        private void ModelForm_Load(object sender, EventArgs e)
        {

        }

        /*public static uint ColorToUInt(Color color)
        {
            return (uint)((color.A << 24) | (color.R << 16) | (color.G << 8) | (color.B << 0));
        }*/

        /*private void InitializeBrowser()
        {
            
        }*/
    }
}
