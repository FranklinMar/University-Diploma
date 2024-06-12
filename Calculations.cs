using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ScottPlot;
using ScottPlot.WinForms;
using ScottPlot.Plottables;
using MathNet.Symbolics;
using Expression = MathNet.Symbolics.SymbolicExpression;

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
            Plot.Title("Network Connectivity Assesment");
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
            Modes[$"{ModeBox.SelectedItem}"] = true;
        }

        private void ModeChanged(object sender, EventArgs e)
        {
            string Mode = (string)ModeBox.SelectedItem;
            if (Mode == "Ezari-Proshan")
            {
                LowerResult.Show();
                CalcLabel2.Show();
                UpperResult.Show();
                TextLower.Show();
                LowerLabel.Show();
                TextUpper.Show();
                UpperLabel.Show();
                PreciseResult.Hide();
                TextPrecise.Hide();
                PreciseLabel.Hide();
                Function = EzariProshanData;
            }
            else if (Mode == "Precise")
            {
                LowerResult.Hide();
                CalcLabel2.Hide();
                UpperResult.Hide();
                TextLower.Hide();
                LowerLabel.Hide();
                TextUpper.Hide();
                UpperLabel.Hide();
                PreciseResult.Show();
                TextPrecise.Show();
                PreciseLabel.Show();
                Function = PreciseData;
            }
        }

        private void EzariProshanData()
        {
            CheckPlot();
            double UpperLimit = EzariProshanUpperLimit(out string Upper);
            double LowerLimit = EzariProshanLowerLimit(out string Lower);
            Upper = Regex.Replace(Upper, PEdgeRegex, "x");
            Lower = Regex.Replace(Lower, PEdgeRegex, "x");
            Func<double, double> Function = new(x => {
                if (x <= 0)
                {
                    return 0;
                }
                if (x >= 1)
                {
                    return 1;
                }
                string Funct = Upper.Replace("x", $"{x}").Replace(',', '.');
                return Convert.ToDouble(new DataTable().Compute(Funct, null));
            });
            FunctionPlot FP = FormPlot.Plot.Add.Function(Function);
            FP.LegendText = "Upper Ezari-Proshan";
            Function = new(x => {
                if (x <= 0)
                {
                    return 0;
                }
                if (x >= 1)
                {
                    return 1;
                }
                string Funct = Lower.Replace("x", $"{x}").Replace(',', '.');
                return Convert.ToDouble(new DataTable().Compute(Funct, null));
            });
            FP = FormPlot.Plot.Add.Function(Function);
            FP.LegendText = "Lower Ezari-Proshan";
            LowerResult.Text = $"{LowerLimit}".Replace(',', '.');
            UpperResult.Text = $"{UpperLimit}".Replace(',', '.');
            FormPlot.Refresh();
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
                string Funct = Func.Replace("x", $"{x}").Replace(',', '.');
                return Convert.ToDouble(new DataTable().Compute(Funct, null));
            });
            FunctionPlot FP = FormPlot.Plot.Add.Function(Function);
            FP.LegendText = "Precise Function";
            PreciseResult.Text = $"{Result}".Replace(',', '.');
            FormPlot.Refresh();
        }

        private void CheckPlot()
        {
            if (Modes[$"{ModeBox.SelectedItem}"])
            {
                FormPlot.Plot.Clear();
                foreach (var Key in Modes)
                {
                    Modes[Key.Key] = false;
                }
            }
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
                    Product *= (BinaryEdge[i] == '1' ? Variables[Edges[i]] : (1 - Variables[Edges[i]]));
                }
                Result += Product;
            }
            expression = $"{Result}";
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
            int Index = -1;
            while (Match.Success)
            {
                do
                {
                    ++Index;
                    Index = TextBox.Text.IndexOf(Match.Value.Replace("[", "").Replace("]", ""), Index);
                    if (Index != -1)
                    {
                        TextBox.Select(Index, Match.Length - 2);
                        TextBox.SelectionFont = new(TextBox.Font.Name, 8);
                        TextBox.SelectionCharOffset = -5;
                    }
                } while (Index != -1);
                Match = Match.NextMatch();
            }
        }
        public void PrintMinPaths()
        {
            GraphHandler.AllMinPaths(Source, Target);
            TextPaths.Text = "";
            StringBuilder Builder = new();
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
            expression = $"{Result}";
            PrintExpression(TextUpper, expression);
            Dictionary<string, FloatingPoint> Values = new();
            foreach (KeyValuePair<GraphEdge, Expression> Pair in Variables)
            {
                Values.Add(Pair.Value.VariableName, Pair.Key.Probability);
            }
            double product;
            double result = 1;
            foreach (var Path in MinPaths)
            {
                product = 1;
                foreach (GraphEdge Edge in Path)
                {
                    product *= Edge.Probability;
                }
                result *= (1 - product);
            }
            result = 1 - result;
            return result;
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
            expression = $"{Result}";
            PrintExpression(TextLower, expression);
            Dictionary<string, FloatingPoint> Values = new();
            foreach(KeyValuePair<GraphEdge, Expression> Pair in Variables)
            {
                Values.Add(Pair.Value.VariableName, Pair.Key.Probability);
            }
            double product;
            double result = 1;
            foreach (var Path in MinPaths)
            {
                product = 1;
                foreach (GraphEdge Edge in Path)
                {
                    product *= (1 - Edge.Probability);
                }
                result *= (1 - product);
            }
            return result;
        }

        private void Loaded(object sender, EventArgs e)
        {
            OnGraphChanged();
            FormPlot = new();
            FormPlot.Dock = DockStyle.Fill;
            PlotPanel.Controls.Add(FormPlot);
            SetUpPlot();
            var Edges = GraphHandler.Graph.Edges;
            foreach (GraphEdge Edge in Edges)
            {
                FlowLayoutPanel Panel = new();
                Panel.AutoSize = true;
                Panel.FlowDirection = FlowDirection.TopDown;
                Panel.Dock = DockStyle.Top;
                Panel.AutoSizeMode = AutoSizeMode.GrowOnly;
                FlowLayoutPanel SmallerPanel = new();
                SmallerPanel.AutoSize = true;
                SmallerPanel.AutoSizeMode = AutoSizeMode.GrowOnly;
                SmallerPanel.Dock = DockStyle.Top;
                SmallerPanel.FlowDirection = FlowDirection.LeftToRight;
                SmallerPanel.BorderStyle = BorderStyle.FixedSingle;
                System.Windows.Forms.Label Label = new();
                Label.AutoSize = true;
                Label.TextAlign = ContentAlignment.MiddleLeft;
                Label.BackColor = System.Drawing.Color.White;
                Label.ForeColor = System.Drawing.Color.Black;
                Label.Font = new Font("Roboto", 13);
                Label.Text = $"P[{Edge.Source.Label}-{Edge.Target.Label}]";
                SmallerPanel.Controls.Add(Label);
                System.Windows.Forms.Label Prob = new();
                Prob.AutoSize = true;
                Prob.TextAlign = ContentAlignment.TopCenter;
                Prob.ForeColor = System.Drawing.Color.White;
                Prob.Font = new Font("Roboto", 13);
                SmallerPanel.Controls.Add(Prob);
                EdgeBar Bar = new(Edge, Prob);
                Bar.AutoSize = true;
                Panel.Controls.Add(SmallerPanel);
                Panel.Controls.Add(Bar);
                var Event = new EventHandler((sender, args) =>
                {
                    EdgeBar This = sender as EdgeBar;
                    This.ChangeValue();
                });
                Bar.Scroll += Event;
                Bar.ValueChanged += Event;
                ProbPanel.Controls.Add(Panel);
            }
            ModeBox.SelectedIndex = 0;
        }

        private void OnGraphChanged()
        {
            string[] Nodes = GraphHandler.Graph.Vertices.Select(Node => Node.Label).ToArray();
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
