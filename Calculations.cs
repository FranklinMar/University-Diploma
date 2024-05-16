using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MathNet.Symbolics;
using Expression = MathNet.Symbolics.SymbolicExpression;

namespace University_Diploma
{
    public partial class Calculations : Form
    {
        private bool OldPaths = false;
        private bool OldCuts = false;
        private GraphHandler GraphHandler;
        private List<List<GraphEdge>> MinPaths;
        private List<List<GraphEdge>> MinCuts;
        private Node _source;
        private Node Source
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
        private Node Target
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
        public Calculations(GraphHandler Handler, Node Source, Node Target)
        {
            GraphHandler = Handler;
            this.Source = Source;
            this.Target = Target;
            InitializeComponent();
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
            if (OldPaths)
            {
                MinPaths = GraphHandler.AllMinPaths(Source, Target);
                OldPaths = false;
            }
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
            if (OldCuts)
            {
                MinCuts = GraphHandler.AllMinPaths(Source, Target);
                OldCuts = false;
            }
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

        public double EzariProshanUpperLimit()
        {
            if (OldPaths)
            {
                MinPaths = GraphHandler.AllMinPaths(Source, Target);
                OldPaths = false;
            }
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
            TextUpper.Text = Result.ToString().Replace("*", " × ").Replace("-", " — ");
            //StringBuilder Builder = new();
            Dictionary<string, FloatingPoint> Values = new();
            foreach (KeyValuePair<GraphEdge, Expression> Pair in Variables)
            {
                Values.Add(Pair.Value.VariableName, Pair.Key.Probability);
            }
            return Result.Evaluate(Values).RealValue;
        }

        public double EzariProshanLowerLimit()
        {
            if (OldCuts)
            {
                MinCuts = GraphHandler.AllMinCuts(Source, Target);
                OldCuts = false;
            }
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
            RefreshData();
        }

        private void RefreshData()
        {
            PrintMinPaths();
            PrintMinCuts();
            double UpperLimit = EzariProshanUpperLimit();
            double LowerLimit = EzariProshanLowerLimit();
            LowerResult.Text = LowerLimit.ToString();
            UpperResult.Text = UpperLimit.ToString();
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
