﻿using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using CefSharp;
using CefSharp.WinForms;
using CefSharp.SchemeHandler;
using QuikGraph;
using QuikGraph.Serialization;
using Newtonsoft.Json.Linq;

namespace University_Diploma
{
    public partial class NetworkForm : Form
    {
        private readonly ProxyController Proxy;
        private readonly Dictionary<TabPage, UndirectedGraph<Node, GraphEdge>> Pages = new();
        private readonly string SessionPath = "./session/";
        private readonly string GraphFileType = "*.graphml";
        private Rectangle CloseX = Rectangle.Empty;
        
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
            Proxy = new(OnGraphRecieved, OnNodeRecieved);
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

        private void PageLoaded(object sender, FrameLoadEndEventArgs e)
        {
            //Browser.ShowDevTools();
            try
            {
                string[] Files = Directory.GetFiles(SessionPath, GraphFileType, SearchOption.TopDirectoryOnly);
                if (Files.Length != 0)
                {
                    if (TabControl.InvokeRequired)
                    {
                        TabControl.Invoke(new MethodInvoker(delegate {
                            foreach (string File in Files)
                            {
                                var Page = LoadGraph(File);
                                TabControl.TabPages.Add(Page);
                                TabControl.SelectedTab = Page;
                                Browser.RecalculateLayout();
                            }
                            TabControl.TabPages.Remove(Tab);
                        }));
                    }
                }
                else
                {
                    throw new DirectoryNotFoundException("Empty Exception for working");
                }
            } catch (DirectoryNotFoundException)
            {
                Pages.Add(Tab, new UndirectedGraph<Node, GraphEdge>());
                if (TabControl.InvokeRequired)
                {
                    TabControl.Invoke(new MethodInvoker(delegate {
                        TabControl.SelectedTab = Tab;
                    }));
                }
            }
        }

        private void Loaded(object sender, EventArgs e)
        {
            Controls.Add(AddPageButton);
            AddPageButton.Top = TabControl.Top;
            AddPageButton.Left = TabControl.Right - AddPageButton.Width - 5;
            AddPageButton.BringToFront();
        }

        private void CloseForm(object sender, FormClosedEventArgs e)
        {
            Directory.CreateDirectory(SessionPath);
            var DirInfo = new DirectoryInfo(SessionPath);
            try
            {
                foreach (var File in DirInfo.EnumerateFiles())
                {
                    File.Delete();
                }
                foreach (var Pair in Pages)
                {
                    if (Pair.Value.VertexCount == 0 && Pair.Value.EdgeCount == 0)
                    {
                        continue;
                    }
                    ExportGraph(Pair.Value, SessionPath + Pair.Key.Text.Trim() + GraphFileType[1..]);
                }
            } catch(DirectoryNotFoundException) { }
        }
        private void OnGraphRecieved(UndirectedGraph<Node, GraphEdge> Graph)
        {
            if (TabControl.InvokeRequired)
            {
                TabControl.Invoke(new MethodInvoker(delegate {
                    Pages[TabControl.SelectedTab] = Graph;
                }));
            }
            if (CalcButton.InvokeRequired)
            {
                CalcButton.Invoke(new MethodInvoker(delegate
                {
                    CalcButton.Enabled = (Graph.Edges.Any());
                }));
            }
            /*var Edges = Graph.Edges;
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
                    EdgeBar This = sender as EdgeBar;
                    This.ChangeValue();
                    //ChangeFrontEndProbability(This.Edge);
                    Browser.ChangeFrontEndProbability(This.Edge);
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
            }*/
        }

        private string GenerateTitle()
        {
            int Index = 0;
            string Title = "";
            bool Loop = true;
            while (Loop)
            {
                ++Index;
                Loop = false;
                Title = $"Graph {Index}   ";
                foreach (TabPage Page in TabControl.TabPages)
                {
                    if (Page.Text.Trim().Equals(Title.Trim()))
                    {
                        Loop = true;
                    }
                }
            }
            if (TabControl.InvokeRequired)
            {
                TabControl.Invoke(new MethodInvoker(delegate {
                    TabControl.SelectedTab = Tab;
                }));
            }
            return Title;
        }

        private void OnNodeRecieved(Node Node)
        {
            if (TabControl.InvokeRequired)
            {
                TabControl.Invoke(new MethodInvoker(delegate {
                    var Graph = Pages[TabControl.SelectedTab];
                    Node GraphNode = Graph.Vertices.Where(node => node.ID.Equals(Node.ID)).First();
                    GraphNode.Label = Node.Label;
                    GraphNode.Position = Node.Position;
                }));
            }
        }

        private TabPage LoadGraph(string FileName)
        {
            UndirectedGraph<Node, GraphEdge> Graph = new();
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
                Graph.AddEdge(new GraphEdge(source, target, double.Parse(Edge.Element(NS + "data").Value.Replace('.', ','))));
            }
            TabPage NewTab = new(GenerateTitle());
            Pages.Add(NewTab, Graph);
            return NewTab;
        }

        private void ExportGraph(UndirectedGraph <Node, GraphEdge> Graph, string FileName)
        {
            using XmlWriter Writer = XmlWriter.Create(FileName, new XmlWriterSettings { Indent = true, WriteEndDocumentOnClose = false });
            Graph.SerializeToGraphML<Node, GraphEdge, UndirectedGraph<Node, GraphEdge>>(Writer);
        }

        private void CalcClick(object sender, EventArgs e)
        {
            var Graph = Pages[TabControl.SelectedTab];
            Calculations Form = new(new(Graph));
            Form.Show();
        }

        private void ImportClick(object sender, EventArgs e)
        {
            OpenFileDialog Dialog = new();
            Dialog.Filter = "GraphML files | *.graphml"; // file types, that will be allowed to upload
            Dialog.Multiselect = false; // allow/deny user to upload more than one file at a time
            if (Dialog.ShowDialog() == DialogResult.Cancel)
                return;
            string FileName = Dialog.FileName; // get name of file
            TabPage Page = LoadGraph(FileName);
            TabControl.TabPages.Add(Page);
            TabControl.SelectedTab = Page;
        }

        private void ExportClick(object sender, EventArgs e)
        {
            SaveFileDialog Dialog = new();
            Dialog.Filter = "GraphML files | *.graphml"; // file types, that will be allowed to upload
            if (Dialog.ShowDialog() == DialogResult.Cancel)
                return;
            string Filename = Dialog.FileName; // get selected file
            var Graph = Pages[TabControl.SelectedTab];
            ExportGraph(Graph, Filename);
        }

        private void SelectedChanged(object sender, EventArgs e)
        {
            TabControl.SelectedTab.Controls.Add(Browser);
            Browser.ChangeFrontEndGraph(Pages[TabControl.SelectedTab]);
            CalcButton.Enabled = Pages[TabControl.SelectedTab].Edges.Any();
        }

        private void TabDraw(object sender, DrawItemEventArgs e)
        {
            Size NewSize = new(13, 13);
            TabPage Page = TabControl.TabPages[e.Index];
            e.DrawBackground();
            using (SolidBrush Brush = new (e.ForeColor))
                e.Graphics.DrawString(Page.Text + "   ", e.Font, Brush, e.Bounds.X + 3, e.Bounds.Y + 4);
            if (e.State == DrawItemState.Selected)
            {
                CloseX = new Rectangle(e.Bounds.Right - NewSize.Width - 3,
                                       e.Bounds.Top + 5, NewSize.Width, NewSize.Height);
                e.Graphics.DrawImage(ImagesList.Images[0], CloseX,
                                     new Rectangle(0, 0, 16, 16), GraphicsUnit.Pixel);
            }
        }

        private void TabClose(object sender, MouseEventArgs e)
        {
            if (CloseX.Contains(e.Location))
            {
                Pages.Remove(TabControl.SelectedTab);
                TabControl.TabPages.Remove(TabControl.SelectedTab);
            }
            if (TabControl.TabPages.Count == 0)
            {
                Tab.Controls.Add(Browser);
                TabControl.TabPages.Add(Tab);
            }
        }

        private void AddPageClick(object sender, EventArgs e)
        {
            TabPage NewTabPage = new(GenerateTitle());
            Pages.Add(NewTabPage, new UndirectedGraph<Node, GraphEdge>());
            TabControl.TabPages.Add(NewTabPage);
            TabControl.SelectedTab = NewTabPage;
        }
    }

    public static class BrowserExtensions
    {
        public static void ChangeFrontEndGraph(this ChromiumWebBrowser Browser, UndirectedGraph<Node, GraphEdge> Graph)
        {
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
                if (Node.Position != null)
                {
                    JObject Position = new();
                    Position.Add("x", Node.Position.X);
                    Position.Add("y", Node.Position.Y);
                    JNode.Add("position", Position);
                }
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
            JSONObject.Add("nodes", JNodes);
            JSONObject.Add("edges", JEdges);
            string Script = $"ChangeGraphBool = false; RefreshGraph({JSONObject}); ChangeGraphBool = true;";
            lock (Browser)
            {
                Browser.ExecuteScriptAsync(Script);
            }
        }

        public static void ChangeFrontEndProbability(this ChromiumWebBrowser Browser, GraphEdge Edge)
        {
            JObject JEdge = new();
            JEdge.Add("source", Edge.Source.ID);
            JEdge.Add("target", Edge.Target.ID);
            JEdge.Add("label", Edge.Probability.ToString());
            string Script = $"ChangeGraphBool = false; RefreshEdge({JEdge}); ChangeGraphBool = true;";
            lock (Browser)
            {
                Browser.ExecuteScriptAsync(Script);
            }
        }

        public static void RecalculateLayout(this ChromiumWebBrowser Browser)
        {
            string Script = $"RecalculateLayout()";
            lock (Browser)
            {
                Browser.ExecuteScriptAsync(Script);
            }
        }
    }
}
