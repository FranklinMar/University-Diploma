using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.Core.Geometry;
using Microsoft.Msagl.Routing;

namespace University_Diploma
{
    public partial class NetworkForm : Form
    {

        //int Index = 0;
        //ToolStripItem deleteNodeItem = MenuStrip.Items.Find("deleteNode", true).First();
        private InteractiveEdgeRouter edgeRouter_;
        public NetworkForm()
        {
            InitializeComponent();
            //deleteEdge.Visible = false;
            //KeyDown += new KeyEventHandler(OnKeyDown);
            //KeyUp += new KeyEventHandler(OnKeyUp);
        }

        private void ViewerLoaded(object sender, EventArgs e)
        {
            Graph Graph = new("Graph");
            Node Node = Graph.AddNode("FrontPole");
            Node.Attr.Color = Color.Green;
            Node = Graph.AddNode("BackPole");
            Node.Attr.Color = Color.Green;
            //Graph.Attr.MinimalHeight = GraphViewer.Height;
            //Graph.Attr.MinimalWidth = GraphViewer.Width;
            //GraphViewer.Edge
            GraphViewer.Graph = Graph;
            //GraphViewer.GraphWithLayout = Graph;
            //GraphViewer.FitGraphBoundingBox();
            //GraphViewer.Graph.GeometryGraph.BoundingBox = new Rectangle(0, 0, GraphViewer.Width, GraphViewer.Height);
            GraphViewer.Click += new EventHandler(ViewerClick);
            //GraphViewer.EdgeAdded += ViewerEdgeAdded;
            //GraphViewer.GraphChanged += GraphChange;
            GraphViewer.EdgeAdded += new EventHandler(GraphChange);
            //edgeRouter_ = new InteractiveEdgeRouter(GraphViewer.Nodes.Select(n => n.BoundaryCurve), 3, 0.65 * 3, 0);
            //GraphViewer.ZoomF
            GraphViewer.AsyncLayout = true;
            //GraphViewer.NeedToCalculateLayout = false;
        }

        private void ViewerClick(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;

            if (me.Button == System.Windows.Forms.MouseButtons.Right)
            {
                /*if (GraphViewer.SelectedObject != null)
                {
                    deleteNode.Visible = false;
                    //MenuStrip.Items.Find("deleteNode", true).First().Visible = false;
                }*/
                if (GraphViewer.SelectedObject == null)
                {
                    deleteNode.Visible = false;
                } else
                {
                    //MessageBox.Show(GraphViewer.SelectedObject.GetType().ToString());

                    if (GraphViewer.SelectedObject is Node Node)
                    {
                        if (Node.Id == "FrontPole" || Node.Id == "BackPole")
                        {
                            deleteNode.Visible = false;
                        }
                        else
                        {
                            deleteNode.Visible = true;
                        }
                    }
                    //deleteEdge.Visible = (GraphViewer.SelectedObject is Edge) ? true : false;
                }
                MenuStrip.Show(MousePosition);
            }
        }

        private void AddNodeClick(object sender, EventArgs e)
        {
            string PromptResult = Prompt.ShowDialog("Please insert a name for the node", "Name");
            //GraphViewer.NeedToCalculateLayout = false;
            if (string.IsNullOrWhiteSpace(PromptResult))
            {
                return;
            }
            Graph Graph = GraphViewer.GraphWithLayout;
            if (GraphViewer.ObjectUnderMouseCursor != null)
            {
                GraphViewer.Graph = null;

                /*Node Node = */
                if (Graph.NodeMap[PromptResult.Trim()] != null)
                {
                    MessageBox.Show("Node already exists");
                }
                Graph.AddNode(PromptResult); //gViewer1.Graph.FindNode($"{Index}");
                //ToolStripMenuItem Item = (ToolStripMenuItem)sender;
                //MessageBox.Show(sender.GetType().ToString());
                //MessageBox.Show(e.GetType().ToString());
                //Node.GeometryNode = new Geomr();
                //GraphViewer.NeedToCalculateLayout = false;
                //GraphViewer.NeedToCalculateLayout = true;
                GraphViewer.Graph = Graph;
                //GraphViewer.NeedToCalculateLayout = true;
                //gViewer1.Graph.GeometryGraph = Graph.GeometryGraph;
                //GraphViewer.Update();
                GraphViewer.Refresh();
                //GraphViewer.NeedToCalculateLayout = false;
                //gViewer1.Graph. = 10; new Size(gViewer1.Width, gViewer1.Height);
                //Item.
                /*Point Canvas = GraphViewer.Graph.BoundingBox.LeftTop;
                Node Node = GraphViewer.Graph.FindNode($"{Index}");
                Node.GeometryNode.Center = new Point(MousePosition.X + Canvas.X, MousePosition.Y + Canvas.Y);*/
                //++Index;
                //gViewer1.NeedToCalculateLayout = true;
            }
        }

        private void DeleteNodeClick(object sender, EventArgs e)
        {
            Graph Graph = GraphViewer.GraphWithLayout;
            Node Node = (Node)GraphViewer.SelectedObject;
            Graph.RemoveNode(Node);
            GraphViewer.Graph = Graph;
            //GraphViewer.RemoveNode((IViewerNode) GraphViewer.SelectedObject, true);
        }

        private void DeleteEdgeClick(object sender, EventArgs e)
        {
            Graph Graph = GraphViewer.GraphWithLayout;
            Edge Edge = (Edge) GraphViewer.SelectedObject;
            Graph.RemoveEdge(Edge);
            GraphViewer.Graph = Graph;
        }

        private void ViewerEdgeAdded(object sender, EventArgs e)
        {
            //MessageBox.Show(sender.GetType().FullName);
            MessageBox.Show(GraphViewer.SelectedObject.GetType().FullName);
            Graph Graph = GraphViewer.GraphWithLayout;
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
            if ((from edge in Graph.Edges where
                (edge.Source == Edge.Source && edge.Target == Edge.Target) ||
                (edge.Source == edge.Target && edge.Target == edge.Source)
                    select edge).Count() > 1)
            {
                //GraphViewer.RemoveEdge(GraphViewer.Edge);
                MessageBox.Show($"Edge already exists", "Error");
            }
            else
            {
                Graph.AddEdge(Edge.Target, Edge.Source);
            }
            GraphViewer.Graph = Graph;
            GraphViewer.Refresh();
        }

        private void GraphChange(object sender, EventArgs e)
        {
            //MessageBox.Show(sender.GetType().FullName);
            //var EdgeGroups = //GraphViewer.Graph.Edges.GroupBy(Edge => (Edge.Source, Edge.Target));
            //EdgeGroups.Select(Pair => Pair.)
            Graph Graph = GraphViewer.Graph;
            var EdgeNonDistinct = from Edge in Graph.Edges
                                  group (Edge.Source, Edge.Target) by (Edge.Source, Edge.Target)
                                  into Grouped where Grouped.Count() > 1
                                  select Grouped.Key;
            //foreach ()
            if (GraphViewer.ObjectUnderMouseCursor != null)
            {
                GraphViewer.Graph = null;

                foreach (var (Source, Target) in EdgeNonDistinct)
                {
                    MessageBox.Show($"Edge (\"{Source}\" -> \"{Target}\") already exists.");
                    Graph.RemoveEdge(Graph.Edges.First(edge => (edge.Source == Source && edge.Target == Target)));
                }
                GraphViewer.Graph = Graph;
            }
            //from Edge in GraphViewer.Graph.Edgs group Edge by Edge
        }

        /*private void RouteMissingEdges()
        {
            foreach (var edge in GraphViewer.Graph.Edges)
            {
                if (edge.EdgeCurve == null)
                {
                    SmoothedPolyline ignore;

                    edge.EdgeCurve = edgeRouter_.RouteSplineFromPortToPortWhenTheWholeGraphIsReady(
                        new FloatingPort(edge.Source.BoundaryCurve, edge.Source.Center),
                        new FloatingPort(edge.Target.BoundaryCurve, edge.Target.Center),
                        true, out ignore);

                    Arrowheads.TrimSplineAndCalculateArrowheads(edge.EdgeGeometry,
                                                    edge.Source.BoundaryCurve,
                                                    edge.Target.BoundaryCurve,
                                                    edge.Curve, true,
                                                    false);

                }
            }
        }*/

        /*private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == ((byte)Keys.Space))
            {
                GraphViewer.PanButtonPressed = true;
                e.Handled = true;
            }
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == ((byte)Keys.Space))
            {
                GraphViewer.PanButtonPressed = false ;
                e.Handled = true;
            }
        }*/
    }
}
