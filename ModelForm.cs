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

namespace University_Diploma
{
    public partial class ModelForm : Form
    {
        // UndirectedGraph<Node, Edge<Node>> Graph;
        private ProxyController Proxy;
        private GraphHandler Handler;
        //private readonly List<List<Node>> NodePaths;
        //private readonly List<List<Edge<Node>>> EdgePaths;

        private void SelectionChanged(object sender, EventArgs e)
        {
            CalcButton.Enabled = (SourceBox.SelectedItem != TargetBox.SelectedItem && 
                SourceBox.SelectedItem != null && TargetBox.SelectedItem != null);
        }

        //private bool IsLoading = true;

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool AllocConsole();
        private static void AllocateConsole()
        {
            AllocConsole();
            Console.OutputEncoding = Encoding.UTF8;
        }

        public ModelForm()
        {
            AllocateConsole();
            InitializeComponent();
            Proxy = new(OnGraphChanged/*out Graph*/);
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
            //Browser.JavascriptMessageReceived += ChromeBrowser_JavascriptMessageReceived;
            Browser.JavascriptObjectRepository.Settings.LegacyBindingEnabled = true;
            Browser.JavascriptObjectRepository.Register("Proxy", Proxy, options: BindingOptions.DefaultBinder);
            /*Browser.LoadingStateChanged += (sender, args) =>
            {
                IsLoading = args.IsLoading;
            };*/
            /*Browser.ConsoleMessage += (sender, e) => {
                Console.WriteLine($"{e.Source}: {e.Message}");
            };*/
            Browser.Load($"{Scheme}://{Domain}/");
            //Browser.Dock = DockStyle.Fill;
        }

        private void PageLoaded(object sender, LoadingStateChangedEventArgs e)
        {
            Browser.ShowDevTools();
            /*if (SourceBox.Items.Count != 0)
                SourceBox.Items.Clear();
            SourceBox.Items.AddRange(Graph.Vertices.ToArray());
            //SourceBox.SelectedIndex = null;

            if (TargetBox.Items.Count != 0)
                TargetBox.Items.Clear();
            TargetBox.Items.AddRange(Graph.Vertices.ToArray());*/
        }

        private void OnGraphChanged()
        {
            string[] Nodes = Handler.Graph.Vertices.Select(Node => Node.Label).ToArray();
            //if (SourceBox.Items.Count != 0)
            if (SourceBox.InvokeRequired)
            {
                SourceBox.Invoke(new MethodInvoker(delegate {
                    SourceBox.Items.Clear();
                    SourceBox.Items.AddRange(Nodes);
                    if (SourceBox.Items.Count != 0)
                    {
                        SourceBox.Enabled = true;
                        int Index = SourceBox.Items.IndexOf(SourceBox.Text);
                        if (Index != -1)
                        {
                            SourceBox.SelectedIndex = Index;
                            SourceBox.SelectedItem = SourceBox.Items[Index];
                        } else
                        {
                            SourceBox.Text = "";
                        }
                    } else 
                    {
                        SourceBox.Enabled = false;
                        SourceBox.Text = "";
                    }
                }));
            }
            //SourceBox.SelectedIndex = null;

            //if (TargetBox.Items.Count != 0)
            if (TargetBox.InvokeRequired)
            {
                TargetBox.Invoke(new MethodInvoker(delegate {
                    TargetBox.Items.Clear();
                    TargetBox.Items.AddRange(Nodes);
                    if (TargetBox.Items.Count != 0)
                    {
                        TargetBox.Enabled = true;
                        int Index = TargetBox.Items.IndexOf(TargetBox.Text);
                        if (Index != -1)
                        {
                            TargetBox.SelectedIndex = Index;
                            TargetBox.SelectedItem = TargetBox.Items[Index];
                        }
                        else
                        {
                            TargetBox.Text = "";
                        }
                    } else {
                        TargetBox.Enabled = false;
                        TargetBox.Text = "";
                    }
                }));
            }
        }

        private void CalcClick(object sender, EventArgs e)
        {
            var Source = Handler.Graph.Vertices.Where(Node => Node.Label.Equals(SourceBox.SelectedItem)).First();
            var Target = Handler.Graph.Vertices.Where(Node => Node.Label.Equals(TargetBox.SelectedItem)).First();
            var Paths = Handler.AllMinPaths(Source, Target);//Handler.AllEdgePaths(Source, Target);
            var Cuts = Handler.AllMinCuts(Source, Target);
            //var NodePaths = Handler.NodePaths;
            Console.WriteLine("Edge paths");
            foreach (List<GraphEdge> Path in Paths)
            {
                Path.ForEach(Edge => Console.Write($"{Edge.Source.Label}---{Edge.Target.Label}|"));
                Console.Write("\n");
            }
            /*Console.WriteLine("Node Paths");
            foreach (List<Node> Path in NodePaths)
            {
                Path.ForEach(Node => Console.Write($"{Node.Label}|"));
                Console.Write("\n");
            }*/
            Console.WriteLine("\nMin cuts");
            foreach (List<GraphEdge> Cut in Cuts)
            {
                Cut.ForEach(Edge => Console.Write($"{Edge.Source.Label}---{Edge.Target.Label}|"));
                Console.Write("\n");
            }
            //MessageBox.Show(Paths.ToString());
            Calculations Form = new (Handler, Source, Target);
            Form.Show();
            //Handler.EzariProshanAccount(Source, Target);
        }

        private void ImportClick(object sender, EventArgs e)
        {
            OpenFileDialog Dialog = new();
            Dialog.Filter = "GraphML files | *.graphml"; // file types, that will be allowed to upload
            Dialog.Multiselect = false; // allow/deny user to upload more than one file at a time
            if (Dialog.ShowDialog() == DialogResult.Cancel)
                return;
            string FileName = Dialog.FileName; // get name of file
            //string Text;
            /*using (StreamReader reader = new(
                new FileStream(Path, FileMode.Open), new UTF8Encoding())) // do anything you want, e.g. read it
            {
                Text = reader.ReadToEnd();
            }*/
            /*UndirectedGraph<Node, GraphEdge> Deserialized = new();
            var VertexFactory = new IdentifiableVertexFactory<Node>((id, label) => new Node(id, label);
            var EdgeFactory = new IdentifiableEdgeFactory<Node, GraphEdge>();
            Deserialized = Deserialized.DeserializeFromGraphML(Path, (string id, string label) => new Node(id, label), (Node source, Node target, double probability) => new GraphEdge(source, target, probability));*/
            var Graph = Proxy.Graph;
            Graph.Clear();
            //var Namespace = XNamespace.Get("http://graphml.graphdrawing.org/xmlns");
            //XName XNgraph = XName.Get("graph", "http://graphml.graphdrawing.org/xmlns");
            var XML = XDocument.Load(FileName);
            var Nodes = XML.Root.Descendants("node");
            var Edges = XML.Root.Descendants("edge");
            foreach (var Node in Nodes)
            {
                string ID = Node.Elements("data").Where(Name => Name.Name == "id").Select(Element => Element.Value).First();
                string Label = Node.Elements("data").Where(Name => Name.Name == "label").Select(Element => Element.Value).First();
                Graph.AddVertex(new Node(ID, Label));
            }
            foreach (var Edge in Edges)
            {
                string Source = (from Node in Nodes where Node.Attribute("id").Value.Equals(Edge.Attribute("source").Value) 
                                select Node.Elements("data").Where(Element => Element.Attribute("Key").Value.Equals("id")).First().Value).First();
                string Target = (from Node in Nodes where Node.Attribute("id").Value.Equals(Edge.Attribute("target").Value)
                                 select Node.Elements("data").Where(Element => Element.Attribute("Key").Value.Equals("id")).First().Value).First();
                Node source = Graph.Vertices.Where(Node => Node.Label.Equals(Source)).First();
                Node target = Graph.Vertices.Where(Node => Node.Label.Equals(Target)).First();
                Graph.AddEdge(new GraphEdge(source, target, double.Parse(Edge.Element("data").Value)));
            }
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

        /*public static uint ColorToUInt(Color color)
        {
            return (uint)((color.A << 24) | (color.R << 16) | (color.G << 8) | (color.B << 0));
        }*/

        /*private void InitializeBrowser()
        {
            
        }*/
    }
}
