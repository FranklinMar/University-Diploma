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
            Handler = new(Proxy.Graph);
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
            var NodePaths = Handler.NodePaths;
            Console.WriteLine("Edge paths");
            foreach (List<Edge<Node>> Path in Paths)
            {
                Path.ForEach(Edge => Console.Write($"{Edge.Source.Label}->{Edge.Target.Label}|"));
                Console.Write("\n");
            }
            /*Console.WriteLine("Node Paths");
            foreach(List<Node> Path in NodePaths)
            {
                Path.ForEach(Node => Console.Write($"{Node.Label}|"));
                Console.Write("\n");
            }*/
            var Cuts = Handler.AllMinCuts(Source, Target);
            Console.WriteLine("\nMin cuts");
            foreach(List<Edge<Node>> Cut in Cuts)
            {
                Cut.ForEach(Edge => Console.Write($"{Edge.Source.Label}->{Edge.Target.Label}|"));
                Console.Write("\n");
            }
            //MessageBox.Show(Paths.ToString());
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
