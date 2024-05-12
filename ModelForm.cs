using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using CefSharp;
using CefSharp.WinForms;
using CefSharp.SchemeHandler;
//using Microsoft.Msagl.Drawing;

namespace University_Diploma
{
    public partial class ModelForm : Form
    {

        /*[DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AllocConsole();
        public static void AllocateConsole()
        {
            AllocConsole();
            Console.OutputEncoding = Encoding.UTF8;
        }*/

        public ModelForm()
        {
            //AllocateConsole();
            InitializeComponent();
            InitializeBrowser();
            //deleteEdge.Visible = false;
            //KeyDown += new KeyEventHandler(OnKeyDown);
            //KeyUp += new KeyEventHandler(OnKeyUp);
            //Browser.FrameLoadEnd
        }

        private bool IsLoading = true;

        /*public static uint ColorToUInt(Color color)
        {
            return (uint)((color.A << 24) | (color.R << 16) | (color.G << 8) | (color.B << 0));
        }*/

        private void InitializeBrowser()
        {
            string Domain = "modelling";
            string Scheme = "resources";
            //string HTML;
            CefSettings Settings = new ();
            //Settings.BackgroundColor = ColorToUInt(Color.FromArgb(0, 0, 16));
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
            //Browser.JavascriptObjectRepository.Register("cefCustomObject", new CefCustomObject<Address>(chromium), options: BindingOptions.DefaultBinder);
            Browser.LoadingStateChanged += (sender, args) =>
            {
                IsLoading = args.IsLoading;
            };
            //HTML = File.ReadAllText(@"./FrontEnd/Front.html");
            /*Browser.ConsoleMessage += (sender, e) => {
                Console.WriteLine($"{e.Source}: {e.Message}");
            };*/
            Browser.Load($"{Scheme}://{Domain}/");
            //Browser.
            //Browser.LoadHtml(HTML);
            //Browser.Dock = DockStyle.Fill;
            //Browser.LoadHtml(HTML, )
            //Browser.EvaluateScriptAsync(File.ReadAllText(@"./FrontEnd/graph.js"));
        }

        private void Loaded(object sender, LoadingStateChangedEventArgs e)
        {
            Browser.ShowDevTools();
        }
    }

    #region old
    /*void ViewerLoaded(object sender, EventArgs e)
    {
        Graph Graph = new("Graph");
        Node Node = Graph.AddNode("FrontPole");
        Node.Attr.Color = Color.Green;
        Node = Graph.AddNode("BackPole");
        Node.Attr.Color = Color.Green;
        //Graph.Attr.MinimalHeight = GViewer.Height;
        //Graph.Attr.MinimalWidth = GViewer.Width;
        //GViewer.Edge
        GViewer.Graph = Graph;
        //GViewer.GraphWithLayout = Graph;
        //GViewer.FitGraphBoundingBox();
        //GViewer.Graph.GeometryGraph.BoundingBox = new Rectangle(0, 0, GViewer.Width, GViewer.Height);
        GViewer.Click += new EventHandler(ViewerClick);
        //GViewer.EdgeAdded += ViewerEdgeAdded;
        //GViewer.GraphChanged += GraphChange;
        GViewer.EdgeAdded += new EventHandler(GraphChange);
        //edgeRouter_ = new InteractiveEdgeRouter(GViewer.Nodes.Select(n => n.BoundaryCurve), 3, 0.65 * 3, 0);
        //GViewer.ZoomF
        GViewer.AsyncLayout = true;
        //GViewer.NeedToCalculateLayout = false;
    }

    void ViewerClick(object sender, EventArgs e)
    {
        MouseEventArgs me = (MouseEventArgs)e;

        if (me.Button == System.Windows.Forms.MouseButtons.Right)
        {
            *//*if (GViewer.SelectedObject != null)
            {
                deleteNode.Visible = false;
                //MenuStrip.Items.Find("deleteNode", true).First().Visible = false;
            }*//*
            if (GViewer.SelectedObject == null)
            {
                deleteNode.Visible = false;
            }
            else
            {

                if (GViewer.SelectedObject is Node)
                {
                    //MessageBox.Show(GViewer.SelectedObject.GetType().ToString());
                    Node Node = (Node)GViewer.SelectedObject;
                    if (Node.Id == "FrontPole" || Node.Id == "BackPole")
                    {
                        deleteNode.Visible = false;
                    }
                    else
                    {
                        deleteNode.Visible = true;
                    }
                }
                //deleteEdge.Visible = (GViewer.SelectedObject is Edge) ? true : false;
            }
            MenuStrip.Show(MousePosition);
        }
    }

    private void AddNodeClick(object sender, EventArgs e)
    {
        string PromptResult = Prompt.ShowDialog("Please insert a name for the node", "Name");
        //GViewer.NeedToCalculateLayout = false;
        if (string.IsNullOrWhiteSpace(PromptResult))
        {
            return;
        }
        Graph Graph = GViewer.GraphWithLayout;
        if (GViewer.ObjectUnderMouseCursor != null)
        {
            GViewer.Graph = null;

            *//*Node Node = *//*
            if (Graph.NodeMap[PromptResult.Trim()] != null)
            {
                MessageBox.Show("Node already exists");
            }
            Graph.AddNode(PromptResult); //gViewer1.Graph.FindNode($"{Index}");
                                         //ToolStripMenuItem Item = (ToolStripMenuItem)sender;
                                         //MessageBox.Show(sender.GetType().ToString());
                                         //MessageBox.Show(e.GetType().ToString());
                                         //Node.GeometryNode = new Geomr();
                                         //GViewer.NeedToCalculateLayout = false;
                                         //GViewer.NeedToCalculateLayout = true;
            GViewer.Graph = Graph;
            //GViewer.NeedToCalculateLayout = true;
            //gViewer1.Graph.GeometryGraph = Graph.GeometryGraph;
            //GViewer.Update();
            GViewer.Refresh();
            //GViewer.NeedToCalculateLayout = false;
            //gViewer1.Graph. = 10; new Size(gViewer1.Width, gViewer1.Height);
            //Item.
            *//*Point Canvas = GViewer.Graph.BoundingBox.LeftTop;
            Node Node = GViewer.Graph.FindNode($"{Index}");
            Node.GeometryNode.Center = new Point(MousePosition.X + Canvas.X, MousePosition.Y + Canvas.Y);*//*
            //++Index;
            //gViewer1.NeedToCalculateLayout = true;
        }
    }

    private void DeleteNodeClick(object sender, EventArgs e)
    {
        Graph Graph = GViewer.GraphWithLayout;
        Node Node = (Node)GViewer.SelectedObject;
        Graph.RemoveNode(Node);
        GViewer.Graph = Graph;
        //GViewer.RemoveNode((IViewerNode) GViewer.SelectedObject, true);
    }

    private void DeleteEdgeClick(object sender, EventArgs e)
    {
        Graph Graph = GViewer.GraphWithLayout;
        Edge Edge = (Edge)GViewer.SelectedObject;
        Graph.RemoveEdge(Edge);
        GViewer.Graph = Graph;
    }

    private void ViewerEdgeAdded(object sender, EventArgs e)
    {
        //MessageBox.Show(sender.GetType().FullName);
        MessageBox.Show(GViewer.SelectedObject.GetType().FullName);
        Graph Graph = GViewer.GraphWithLayout;
        Edge Edge = (Edge)sender;
        // If Edge already exists
        foreach (Edge edge in (from edge in Graph.Edges
                               where
         (edge.Source == Edge.Source && edge.Target == Edge.Target) ||
         (edge.Source == edge.Target && edge.Target == edge.Source)
                               select edge))
        {
            MessageBox.Show($"{edge.Source}|{edge.Target}");
        }
        if ((from edge in Graph.Edges
             where
(edge.Source == Edge.Source && edge.Target == Edge.Target) ||
(edge.Source == edge.Target && edge.Target == edge.Source)
             select edge).Count() > 1)
        {
            //GViewer.RemoveEdge(GViewer.Edge);
            MessageBox.Show($"Edge already exists", "Error");
        }
        else
        {
            Graph.AddEdge(Edge.Target, Edge.Source);
        }
        GViewer.Graph = Graph;
        GViewer.Refresh();
    }

    private void GraphChange(object sender, EventArgs e)
    {
        //MessageBox.Show(sender.GetType().FullName);
        //var EdgeGroups = //GViewer.Graph.Edges.GroupBy(Edge => (Edge.Source, Edge.Target));
        //EdgeGroups.Select(Pair => Pair.)
        Graph Graph = GViewer.Graph;
        var EdgeNonDistinct = from Edge in Graph.Edges
                              group (Edge.Source, Edge.Target) by (Edge.Source, Edge.Target)
                              into Grouped
                              where Grouped.Count() > 1
                              select Grouped.Key;
        //foreach ()
        if (GViewer.ObjectUnderMouseCursor != null)
        {
            GViewer.Graph = null;

            foreach (var Edge in EdgeNonDistinct)
            {
                MessageBox.Show($"Edge (\"{Edge.Source}\" -> \"{Edge.Target}\") already exists.");
                Graph.RemoveEdge(Graph.Edges.First(edge => (edge.Source == Edge.Source && edge.Target == Edge.Target)));
            }
            GViewer.Graph = Graph;
        }
        //from Edge in GViewer.Graph.Edgs group Edge by Edge
    }*/
    #endregion
}
