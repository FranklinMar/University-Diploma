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

namespace University_Diploma
{
    public partial class NetworkForm : Form
    {

        //int Index = 0;
        //ToolStripItem deleteNodeItem = MenuStrip.Items.Find("deleteNode", true).First();
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
            Graph.AddNode("FrontPole");
            Graph.AddNode("BackPole");
            //Graph.Attr.MinimalHeight = GraphViewer.Height;
            //Graph.Attr.MinimalWidth = GraphViewer.Width;

            GraphViewer.Graph = Graph;
            //GraphViewer.GraphWithLayout = Graph;
            //GraphViewer.FitGraphBoundingBox();
            //GraphViewer.Graph.GeometryGraph.BoundingBox = new Rectangle(0, 0, GraphViewer.Width, GraphViewer.Height);
            GraphViewer.Click += ViewerClick;
            //GraphViewer.ZoomF
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

                    if (GraphViewer.SelectedObject is Node)
                    {
                        //MessageBox.Show(GraphViewer.SelectedObject.GetType().ToString());
                        Node Node = (Node)GraphViewer.SelectedObject;
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
            /*Node Node = */
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
            //gViewer1.Graph. = 10; new Size(gViewer1.Width, gViewer1.Height);
            //Item.
            /*Point Canvas = GraphViewer.Graph.BoundingBox.LeftTop;
            Node Node = GraphViewer.Graph.FindNode($"{Index}");
            Node.GeometryNode.Center = new Point(MousePosition.X + Canvas.X, MousePosition.Y + Canvas.Y);*/
            //++Index;
            //gViewer1.NeedToCalculateLayout = true;
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
