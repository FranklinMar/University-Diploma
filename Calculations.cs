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
using ScottPlot.Plottables;
using System.Text.RegularExpressions;
using System.Drawing;

namespace University_Diploma
{
    public delegate void Calculation();
    public partial class Calculations : Form
    {
        public GraphHandler GraphHandler { get; private set; }
        private FormsPlot FormPlot;
        private bool OldPaths = false;
        private bool OldCuts = false;
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

        private readonly Dictionary<string, bool> Modes = new(){{ "Ezari-Proshan", false }, { "Precise", false }};
        //private string LastMode = "";
        private Calculation Function;
        private readonly string PEdgeRegex = @"P\[.*?\]";
        private readonly string EdgeRegex = @"\[.*?\]";

        public Calculations(GraphHandler Handler)
        {
            GraphHandler = Handler;
            InitializeComponent();
            ModeBox.Items.AddRange(Modes.Keys.ToArray());
        }

        private void SetUpPlot()
        {
            Plot Plot = FormPlot.Plot;
            Plot.XLabel("Edges Success Probability");
            Plot.YLabel("Network connectivity");
            Plot.Title("Network Connectivity assesment");
            Plot.ShowLegend();
            Plot.FigureBackground.Color = ScottPlot.Color.FromHex("#181818");
            Plot.DataBackground.Color = ScottPlot.Color.FromHex("#1f1f1f");
            Plot.Axes.Color(ScottPlot.Color.FromHex("#d7d7d7"));
            Plot.Grid.MajorLineColor = ScottPlot.Color.FromHex("#404040");
            Plot.Legend.BackgroundColor = ScottPlot.Color.FromHex("#404040");
            Plot.Legend.FontColor = ScottPlot.Color.FromHex("#d7d7d7");
            Plot.Legend.OutlineColor = ScottPlot.Color.FromHex("#d7d7d7");
            Plot.Axes.SetLimits(-0.2, 1.2, -0.2, 1.2);
            Plot.ZoomRectangle.VerticalSpan = false;
            Plot.ZoomRectangle.HorizontalSpan = false;
        }

        private void CalcClick(object sender, EventArgs e)
        {
            PrintMinPaths();
            PrintMinCuts();
            Function();
            Modes[ModeBox.SelectedItem.ToString()] = true;
            //LastMode = Mode;
        }

        private void ModeChanged(object sender, EventArgs e)
        {
            string Mode = (string)ModeBox.SelectedItem;
            if (Mode == "Ezari-Proshan")
            {
                LowerResult.Show();
                CalcLabel2.Show();
                UpperResult.Show();
                PreciseResult.Hide();
                TextLower.Show();
                LowerLabel.Show();
                TextUpper.Show();
                UpperLabel.Show();
                TextPrecise.Hide();
                PreciseLabel.Hide();
                Function = EzariProshanData;
            }
            else if (Mode == "Precise")
            {
                LowerResult.Hide();
                CalcLabel2.Hide();
                UpperResult.Hide();
                PreciseResult.Show();
                TextLower.Hide();
                LowerLabel.Hide();
                TextUpper.Hide();
                UpperLabel.Hide();
                TextPrecise.Show();
                PreciseLabel.Show();
                Function = PreciseData;
            }
        }

        private void EzariProshanData()
        {
            //if (LastMode.Equals(ModeBox.SelectedItem))
            CheckPlot();
            double UpperLimit = EzariProshanUpperLimit(out string Upper);
            double LowerLimit = EzariProshanLowerLimit(out string Lower);
            Upper = Regex.Replace(Upper, PEdgeRegex, "x");
            Lower = Regex.Replace(Lower, PEdgeRegex, "x");
            var Funcs = new[] { Lower, Upper };
            var Legends = new[] { "Lower Ezari-Proshan", "Upper Ezari-Proshan" };
            ConstructPlot(Funcs, Legends);
            LowerResult.Text = LowerLimit.ToString();
            UpperResult.Text = UpperLimit.ToString();
            FormPlot.Refresh();
            //FormPlot.Plot.Legend.ManualItems.Add();
        }

        private void PreciseData()
        {
            CheckPlot();
            double Result = PreciseCalculate(out string Func);
            Func = Regex.Replace(Func, PEdgeRegex, "x");
            Func <double, double> Function = new(x => {
                if (x <= 0)
                {
                    return 0;
                }
                if (x >= 1)
                {
                    return 1;
                }
                string Funct = Func.Replace("x", x.ToString()).Replace(',', '.');
                return Convert.ToDouble(new DataTable().Compute(Funct, null));
            });
            FunctionPlot FP = FormPlot.Plot.Add.Function(Function);
            FP.LegendText = "Precise Function";
            FormPlot.Refresh();
        }
        private void CheckPlot()
        {
            if (Modes[ModeBox.SelectedItem.ToString()])
            {
                FormPlot.Plot.Clear();
                foreach (var Key in Modes)
                {
                    Modes[Key.Key] = false;
                }
            }
        }

        private void ConstructPlot(string[] Funcs, string[] Legends = null)
        {
            Func<double, double> Function;
            int i = 0;
            if (Legends != null)
            {
                if (Legends.Length != Funcs.Length)
                {
                    throw new ArgumentException("Arrays of Functions and Legends must be equal in size", nameof(Legends));
                }
            }
            for (; i < Funcs.Length; i++)
            {
                //MessageBox.Show(i.ToString());
                Function = new(x => {
                    if (x <= 0)
                    {
                        return 0;
                    }
                    if (x >= 1)
                    {
                        return 1;
                    }
                    string Func = Funcs[i].Replace("x", x.ToString()).Replace(',', '.');
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
                FunctionPlot FP = FormPlot.Plot.Add.Function(Function);
                if (Legends != null)
                {
                    FP.LegendText = Legends[i];
                }
            }
            i = 0;
        }

        private double PreciseCalculate(out string expression)
        {
            Dictionary<GraphEdge, Expression> Variables = new();
            var Edges = GraphHandler.Graph.Edges.ToArray();
            foreach (GraphEdge Edge in Edges)
            {
                Variables.Add(Edge, Expression.Variable($"P[{Edge.Source.Label}—{Edge.Target.Label}]"));
            }
            //Code for calculating precise equation of graph structure
            HashSet<string> BinaryEdges = new();
            StringBuilder Builder;
            foreach (var Path in MinPaths)
            {
                Builder = new();
                // Code for interpreting every Edge in binary? with state of Failed(0) and Success(1)
                foreach (var Edge in Edges)
                {
                    // If Edge is in MinPaths - assign 2 as immutable
                    Builder.Append(Path.Contains(Edge) ? '2' : '0');
                }
                // Find every possible binary combination
                BinaryEdges.Add(Builder.ToString().Replace('2', '1'));
                while (Builder.ToString().Any(Ch => Ch == '0'))
                {
                    int i = 0;
                    for (; i < Builder.Length && (Builder[i] == '1' || Builder[i] == '2'); i++)
                    {
                        if (Builder[i] == '1')
                        {
                            Builder[i] = '0';
                        }
                    }
                    Builder[i] = '1';
                    BinaryEdges.Add(Builder.ToString().Replace('2', '1'));
                }
            }
            // Calculate the formula, where '1' == p, and '-' == q
            Expression Product;
            Expression Result = 0;
            foreach (string BinaryEdge in BinaryEdges)
            {
                Product = 1;
                for (int i = 0; i < BinaryEdge.Length; i++)
                {
                    //var Edge = Edges[i].Probability;
                    Product *= (BinaryEdge[i] == '1' ? Variables[Edges[i]] : (1 - Variables[Edges[i]]));
                }
                Result += Product;
            }
            expression = Result.ToString();
            PrintExpression(TextPrecise, expression);
            Dictionary<string, FloatingPoint> Values = new();
            foreach (KeyValuePair<GraphEdge, Expression> Pair in Variables)
            {
                Values.Add(Pair.Value.VariableName, Pair.Key.Probability);
            }
            return Result.Evaluate(Values).RealValue;
        }

        private void PrintExpression(RichTextBox TextBox, string Expression)
        {
            TextBox.Text = Expression.Replace("*", " × ").Replace("-", " — ")
                .Replace("[", "").Replace("]", "");
            var Match = Regex.Match(Expression, EdgeRegex);
            while (Match.Success)
            {
                int Index = TextBox.Text.IndexOf(Match.Value.Replace("[", "").Replace("]", ""));
                TextBox.Select(Index, Match.Length - 2);
                TextBox.SelectionFont = new(TextBox.Font.Name, 8);
                TextBox.SelectionCharOffset = -5;
                Match = Match.NextMatch();
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
            GraphHandler.AllMinPaths(Source, Target);
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
            PrintExpression(TextPaths, Builder.ToString());
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
            PrintExpression(TextCuts, Builder.ToString());
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
            //Result.Compile();
            expression = Result.ToString();
            PrintExpression(TextUpper, expression);
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
            PrintExpression(TextLower, expression);
            Dictionary<string, FloatingPoint> Values = new();
            foreach(KeyValuePair<GraphEdge, Expression> Pair in Variables)
            {
                Values.Add(Pair.Value.VariableName, Pair.Key.Probability);
            }
            return Result.Evaluate(Values).RealValue;
        }

        private void Loaded(object sender, EventArgs e)
        {
            OnGraphChanged();
            FormPlot = new();
            FormPlot.Dock = DockStyle.Fill;
            PlotPanel.Controls.Add(FormPlot);
            SetUpPlot();
            var Edges = GraphHandler.Graph.Edges;
            //List<FlowLayoutPanel> Panels = new();
            foreach (GraphEdge Edge in Edges)
            {
                FlowLayoutPanel Panel = new();
                Panel.AutoSize = true;
                Panel.FlowDirection = FlowDirection.TopDown;
                FlowLayoutPanel SmallerPanel = new();
                SmallerPanel.AutoSize = true;
                SmallerPanel.FlowDirection = FlowDirection.LeftToRight;
                SmallerPanel.BorderStyle = BorderStyle.FixedSingle;
                System.Windows.Forms.Label Label = new();
                Label.AutoSize = true;
                Label.TextAlign = ContentAlignment.TopCenter;
                Label.BackColor = System.Drawing.Color.White;
                Label.ForeColor = System.Drawing.Color.Black;
                Label.Font = new Font("Roboto", 11);
                Label.Text = $"P[{Edge.Source.Label}-{Edge.Target.Label}]";
                SmallerPanel.Controls.Add(Label);
                System.Windows.Forms.Label Prob = new();
                Prob.AutoSize = true;
                Prob.TextAlign = ContentAlignment.TopCenter;
                Prob.ForeColor = System.Drawing.Color.White;
                Prob.Font = new Font("Roboto", 13);
                //Prob.Text = string.Format("{0:N2}", Edge.Probability).Replace(',', '.');
                SmallerPanel.Controls.Add(Prob);
                EdgeBar Bar = new(Edge, Prob);
                Bar.AutoSize = true;
                Panel.Controls.Add(SmallerPanel);
                Panel.Controls.Add(Bar);
                var Event = new EventHandler((sender, args) =>
                {
                    EdgeBar This = sender as EdgeBar;
                    This.ChangeValue();
                    //ChangeFrontEndProbability(This.Edge);
                    //Browser.ChangeFrontEndProbability(This.Edge);
                });
                Bar.Scroll += Event;
                Bar.ValueChanged += Event;
                ProbPanel.Controls.Add(Panel);
                //Panels.Add(Panel);
            }
            ModeBox.SelectedIndex = 0;
            /*if (ProbPanel.InvokeRequired)
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
    }
}
