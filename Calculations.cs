using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MathNet.Symbolics;
using Expression = MathNet.Symbolics.SymbolicExpression;
using ScottPlot;
using ScottPlot.WinForms;
using System.Text.RegularExpressions;

namespace University_Diploma
{
    public partial class Calculations : Form
    {
        private FormsPlot FormPlot;
        private bool OldPaths = false;
        private bool OldCuts = false;
        public GraphHandler GraphHandler { get; private set; }
        private List<List<GraphEdge>> _MinPaths;
        private List<List<GraphEdge>> MinPaths {
            get {
                if (OldPaths)
                {
                    _MinPaths = GraphHandler.AllMinPaths(Source, Target);
                    OldPaths = false;
                }
                return _MinPaths;
            }
            set
            {
                _MinPaths = value;
            }
        }
        private List<List<GraphEdge>> _MinCuts;
        private List<List<GraphEdge>> MinCuts {
            get {
                if (OldCuts)
                {
                    _MinCuts = GraphHandler.AllMinCuts(Source, Target);
                    OldCuts = false;
                }
                return _MinCuts;
            }
            set
            {
                _MinCuts = value;
            }
        }
        private Node _source;
        public Node Source
        {
            get
            {
                return _source;
            }
            set
            {
                if (!GraphHandler.Graph.Vertices.Contains(value))
                {
                    throw new ArgumentException("Graph must contain this Node", nameof(Source));
                }
                OldPaths = true;
                OldCuts = true;
                _source = value;
            }
        }
        private Node _target;
        public Node Target
        {
            get
            {
                return _target;
            }
            set
            {
                if (!GraphHandler.Graph.Vertices.Contains(value))
                {
                    throw new ArgumentException("Graph must contain this Node", nameof(Target));
                }
                OldPaths = true;
                OldCuts = true;
                _target = value;
            }
        }
        public Calculations(GraphHandler Handler/*, Node Source = null, Node Target = null*/)
        {
            GraphHandler = Handler;
            //this.Source = Source;
            //this.Target = Target;
            InitializeComponent();
        }
        private void SetUpPlot()
        {
            Plot Plot = FormPlot.Plot;
            Plot.XLabel("Edges Success Probability");
            Plot.YLabel("Network connectivity");
            Plot.Title("Network Connectivity assesment");
            Plot.ShowLegend();
            Plot.FigureBackground.Color = Color.FromHex("#181818");
            Plot.DataBackground.Color = Color.FromHex("#1f1f1f");
            Plot.Axes.Color(Color.FromHex("#d7d7d7"));
            Plot.Grid.MajorLineColor = Color.FromHex("#404040");
            Plot.Legend.BackgroundColor = Color.FromHex("#404040");
            Plot.Legend.FontColor = Color.FromHex("#d7d7d7");
            Plot.Legend.OutlineColor = Color.FromHex("#d7d7d7");
            Plot.Axes.SetLimits(0, 1, 0, 1);
            //Plot.
            //var Function = new Func<double, double>((x) => x);
            //Plot.Add.Function(Function);
        }

        private void RefreshData()
        {
            PrintMinPaths();
            PrintMinCuts();
            double UpperLimit = EzariProshanUpperLimit(out string Upper);
            double LowerLimit = EzariProshanLowerLimit(out string Lower);
            Upper = Regex.Replace(Upper, @"P\[.*?\]", "x");
            Lower = Regex.Replace(Lower, @"P\[.*?\]", "x");
            ConstructPlot(Lower, Upper);
            LowerResult.Text = LowerLimit.ToString();
            UpperResult.Text = UpperLimit.ToString();
        }

        private void ConstructPlot(params string[] Funcs)
        {
            Func<double, double> Function;
            foreach (var Item in Funcs)
            {
                Function = new(x =>
                {
                    if (x <= 0)
                    {
                        return 0;
                    }
                    if (x >= 1)
                    {
                        return 1;
                    }
                    string Func = Item.Replace("x", x.ToString()).Replace(',', '.');
                    //MessageBox.Show(Func);
                    //MessageBox.Show(new DataTable().Compute(Func, null).GetType().ToString());
                    return Convert.ToDouble(new DataTable().Compute(Func, null));
                });
                /*Function = (x) =>
                {
                    if (x <= 0)
                        return 0;
                    if (x >= 1)
                        return 1;
                    return Expression.Parse(Item).Compile("x")(x);
                };*/
                FormPlot.Plot.Add.Function(Function);
            }
        }

        private void OnGraphChanged()
        {
            string[] Nodes = GraphHandler.Graph.Vertices.Select(Node => Node.Label).ToArray();
            //if (SourceBox.Items.Count != 0)
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
                }
                else
                {
                    SourceBox.Text = "";
                }
            }
            else
            {
                SourceBox.Enabled = false;
                SourceBox.Text = "";
            }
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
            }
            else
            {
                TargetBox.Enabled = false;
                TargetBox.Text = "";
            }
            /*if (SourceBox.InvokeRequired)
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
                        }
                        else
                        {
                            SourceBox.Text = "";
                        }
                    }
                    else
                    {
                        SourceBox.Enabled = false;
                        SourceBox.Text = "";
                    }
                }));
            }*/
            //SourceBox.SelectedIndex = null;

            //if (TargetBox.Items.Count != 0)
            /*if (TargetBox.InvokeRequired)
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
                    }
                    else
                    {
                        TargetBox.Enabled = false;
                        TargetBox.Text = "";
                    }
                }));
            }*/
        }

        private void SelectionChanged(object sender, EventArgs e)
        {
            CalcButton.Enabled = (SourceBox.SelectedItem != TargetBox.SelectedItem &&
                SourceBox.SelectedItem != null && TargetBox.SelectedItem != null);
            if (sender == SourceBox)
            {
                Source = GraphHandler.Graph.Vertices.Where(Node => Node.Label.Equals(SourceBox.SelectedItem)).First();
            }
            if (sender == TargetBox)
            {
                Target = GraphHandler.Graph.Vertices.Where(Node => Node.Label.Equals(TargetBox.SelectedItem)).First();
            }
        }

        /*private static string Superscripts = "⁰¹²³⁴⁵⁶⁷⁸⁹⁻";
        private static char GetSuperscript(int number)
        {
            if (number < 0)
                return Superscripts[^1];
            return Superscripts.ElementAt(number);
        }
        private static string Subscripts = "₀₁₂₃₄₅₆₇₈₉₋";
        private static char GetSubscript(int number)
        {
            if (number < 0)
                return Subscripts[^1];
            return Subscripts.ElementAt(number);
        }*/

        public void PrintMinPaths()
        {
            TextPaths.Text = "";
            StringBuilder Builder = new();
            //GraphEdge Edge;
            Node Node;
            foreach(List<Node> Path in GraphHandler.NodePaths)
            {
                for(int i = 0; i < Path.Count; i++) 
                {
                    Node = Path[i];
                    Builder.Append($"P[{Node.Label}]");
                    if (i < Path.Count - 1)
                    {
                        Builder.Append(" ➤ ");
                    }
                }
                Builder.Append(Environment.NewLine);
            }
            TextPaths.Text = Builder.ToString();
        }

        public void PrintMinCuts()
        {
            TextCuts.Text = "";
            StringBuilder Builder = new();
            GraphEdge Edge;
            foreach (List<GraphEdge> Cut in MinCuts)
            {
                for (int i = 0; i < Cut.Count; i++)
                {
                    Edge = Cut[i];
                    Builder.Append($"P[{Edge.Source.Label}] — P[{Edge.Target.Label}]");
                    if (i < Cut.Count - 1)
                    {
                        Builder.Append(" | ");
                    }
                }
                Builder.Append(Environment.NewLine);
            }
            TextCuts.Text = Builder.ToString();
        }

        public double EzariProshanUpperLimit(out string expression)
        {
            Dictionary<GraphEdge, Expression> Variables = new();
            var Edges = GraphHandler.Graph.Edges;
            foreach (GraphEdge Edge in Edges)
            {
                Variables.Add(Edge, Expression.Variable($"P[{Edge.Source.Label}—{Edge.Target.Label}]"));
            }
            Expression Product;
            Expression Result = 1;
            // 1 - П_(1<=j<=M) (1 - П_(i in A) (p_i))
            foreach (var Path in MinPaths)
            {
                Product = 1;
                foreach (GraphEdge Edge in Path)
                {
                    Product *= Variables[Edge];
                }
                Result *= (1 - Product);
            }
            Result = 1 - Result;
            expression = Result.ToString();
            TextUpper.Text = Result.ToString().Replace("*", " × ").Replace("-", " — ");
            //StringBuilder Builder = new();
            Dictionary<string, FloatingPoint> Values = new();
            foreach (KeyValuePair<GraphEdge, Expression> Pair in Variables)
            {
                Values.Add(Pair.Value.VariableName, Pair.Key.Probability);
            }
            return Result.Evaluate(Values).RealValue;
        }

        public double EzariProshanLowerLimit(out string expression)
        {
            Dictionary<GraphEdge, Expression> Variables = new();
            var Edges = GraphHandler.Graph.Edges;
            foreach (GraphEdge Edge in Edges)
            {
                Variables.Add(Edge, Expression.Variable($"P[{Edge.Source.Label}—{Edge.Target.Label}]"));
            }
            Expression Product;
            Expression Result = 1;
            // П_(1<=k<=N) (1 - П_(i in B) (q_i))
            foreach (var Cut in MinCuts)
            {
                Product = 1;
                foreach (GraphEdge Edge in Cut)
                {
                    Product *= (1 - Variables[Edge]);
                }
                Result *= (1 - Product);
            }
            expression = Result.ToString();
            TextLower.Text = Result.ToString().Replace("*", " × ").Replace("-", " — ");
            //StringBuilder Builder = new();
            Dictionary<string, FloatingPoint> Values = new();
            foreach(KeyValuePair<GraphEdge, Expression> Pair in Variables)
            {
                Values.Add(Pair.Value.VariableName, Pair.Key.Probability);
            }
            return Result.Evaluate(Values).RealValue;
        }

        private void Loaded(object sender, EventArgs e)
        {
            //RefreshData();
            OnGraphChanged();
            FormPlot = new();
            FormPlot.Dock = DockStyle.Fill;
            PlotPanel.Controls.Add(FormPlot);
            SetUpPlot();
        }

        private void CalcClick(object sender, EventArgs e)
        {
            RefreshData();
        }

        /*double LowerLimit = 1;
        double UpperLimit = 1;
        double MultiplyProbabilities;
        // П_(1<=k<=N) (1 - П_(i in B) (q_i))
        foreach (var Cut in MinCuts)
        {
            MultiplyProbabilities = 1;
            foreach (GraphEdge Edge in Cut)
            {
                MultiplyProbabilities *= (1 - Edge.Probability*//*Probabilities[Edge]*//*);
            }
            LowerLimit *= (1 - MultiplyProbabilities);
        }
        // 1 - П_(1<=j<=M) (1 - П_(i in A) (p_i))
        foreach (var Path in MinPaths)
        {
            MultiplyProbabilities = 1;
            foreach (GraphEdge Edge in Path)
            {
                MultiplyProbabilities *= Edge.Probability;//Probabilities[Edge];
            }
            UpperLimit *= (1 - MultiplyProbabilities);
        }
        UpperLimit = 1 - UpperLimit;
        MessageBox.Show($"{LowerLimit} <= M Ф (X) <= {UpperLimit}", "Calculations complete!");*/
    }
}
