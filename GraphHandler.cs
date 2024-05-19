using QuikGraph;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuikGraph.Algorithms.Search;
using QuikGraph.Algorithms.Observers;
using System.Windows.Forms;
using System.Collections;

namespace University_Diploma
{
    public class GraphHandler
    {
        
        // UndirectedGraph
        // UndirectedDepthFirstSearchAlgorithm
        // UndirectedVertexPredecessorRecorderObserver
        public UndirectedGraph<Node, GraphEdge> Graph { get; private set; }
        //public Dictionary<GraphEdge, double> Probabilities { get; private set; }
        public readonly List<List<Node>> NodePaths = new();
        private readonly List<List<GraphEdge>> MinPaths = new();
        private readonly List<List<GraphEdge>> MinCuts = new();
        //private UndirectedDepthFirstSearchAlgorithm<Node, GraphEdge> Algorithm;
        //private UndirectedVertexPredecessorRecorderObserver<Node, GraphEdge> Observer;
        private readonly Random Generator = new ();

        public GraphHandler(UndirectedGraph<Node, GraphEdge> graph/*, Dictionary<GraphEdge, double> probabilities*/)
        {
            Graph = graph;
            //Probabilities = probabilities;
            //Graph.Edges.ToList()[0].
        }

        /*public List<List<Node>> AllNodePaths(Node Source, Node Target)
        {
            NodePaths.Clear();
            FindNodePaths(Source, Target, new List<Node>());
            return NodePaths;
        }

        private void FindNodePaths(Node Source, Node Target, List<Node> Path)
        {
            if (Source == Target)
            {
                Path.Add(Source);
                NodePaths.Add(new List<Node>(Path));
                Path.RemoveAt(Path.Count - 1);
                return;
            }
            if (Path.Contains(Source))
                return;

            Path.Add(Source);

            //var Edges = Graph.AdjacentEdges(Source);
            //foreach (Edge<Node> Edge in Edges)
            //{
            //    FindNodePaths(Edge.Target, Target, Path);
            //}
            var Nodes = Graph.AdjacentVertices(Source);
            foreach (var Node in Nodes)
            {
                FindNodePaths(Node, Target, Path);
            }
            //while (K.MoveNext())
            //{
            //    FindPath(K.Current.Target, E, Path);
            //}
            Path.RemoveAt(Path.Count - 1);
        }*/

        /*public List<List<Edge<Node>>> AllEdgePaths(Node Source, Node Target)
        {
            EdgePaths.Clear();
            FindEdgePaths(Source, null, Target, new List<Edge<Node>>());
            return EdgePaths;
        }

        private void FindEdgePaths(Node Source, Edge<Node> PreviousEdge, Node Target, List<Edge<Node>> Path)
        {
            if (Source == Target)
            {
                EdgePaths.Add(new List<Edge<Node>>(Path));
                return;
            }
            var Edges = Graph.AdjacentEdges(Source);
            foreach (Edge<Node> Edge in Edges)
            {
                if (Edge != PreviousEdge)
                {
                    Path.Add(Edge);
                    FindEdgePaths(Edge.Target, Edge, Target, Path);
                    Path.RemoveAt(Path.Count - 1);
                }
            }
        }*/

        public List<List<GraphEdge>> AllMinPaths(Node Source, Node Target)
        {
            MinPaths.Clear();
            NodePaths.Clear();
            Queue <List<Node>> Queue = new();

            List<Node> Path = new();
            Path.Add(Source);
            Queue.Enqueue(Path);
            while (Queue.Count != 0)
            {
                Path = Queue.Dequeue();
                Node Last = Path[^1];
                if (Last == Target)
                {
                    MinPaths.Add(PathNodeToEdge(Path));
                    NodePaths.Add(new List<Node> (Path));
                }
                var Edges = Graph.AdjacentEdges(Last);
                foreach(GraphEdge Edge in Edges)
                {
                    if (/*(Edge.Source == Last && !Path.Contains(Edge.Target)) ||
                        (Edge.Target == Last && !Path.Contains(Edge.Source))*/
                        !Path.Contains(Edge.GetOtherVertex(Last)))
                    {
                        List<Node> New = new(Path);
                        New.Add(/*Edge.Source == Last ? Edge.Target : Edge.Source*/Edge.GetOtherVertex(Last));
                        Queue.Enqueue(New);
                    }
                }
            }
            return MinPaths;
        }

        public List<GraphEdge> PathNodeToEdge(List<Node> Path)
        {
            List<GraphEdge> New = new();
            GraphEdge Edge;
            for (int i = 0; i < Path.Count - 1; i++)
            {
                Edge = Graph.Edges.First(edge => edge.UndirectedVertexEquality(Path[i], Path[i + 1])/*(edge.Source == Path[i] && edge.Target == Path[i + 1]) ||
                    (edge.Target == Path[i] && edge.Source == Path[i + 1])*/);
                New.Add(Edge);
            }
            return New;
        }

        /*public List<List<Edge<Node>>> AllMinimalCuts(Node Source, Node Target)
        {
            UndirectedGraph<Node, Edge<Node>> ClonedGraph = Graph.Clone();
            HashSet<Node> SeparateNodes = new();
            //return null;
        }*/
        // Karger's Algorithm
        public List<List<GraphEdge>> AllMinCuts(Node Source, Node Target)
        {
            MinCuts.Clear();
            //HashSet<Node> SourceSet = new();
            List<List<GraphEdge>> Cuts = new();
            Queue<List<Node>> Queue = new();
            //UndirectedGraph<Node, Edge<Node>> graph = Graph.Clone();

            //Node Current = Source;
            //var Edges = graph.AdjacentEdges(Source);
            //SourceSet.Add(Current);
            /*Cuts.Add(graph.AdjacentEdges(CurrentSource).ToList());
            while (graph.Vertices.Count() > 2)
            {
                //var Edges = AllEdges(graph, CurrentSource);
                //Edge<Node> Edge = Edges[Generator.Next(0, Edges.Count)];
                //graph.MergeVertex(Edge.Source == CurrentSource ? Edge.Target : Edge.Source, EdgeCreator);
                var SourceEdges = graph.AdjacentEdges(CurrentSource);

                Edge<Node> Edge;
                do
                {
                    int RandomInt = Generator.Next(0, SourceEdges.Count());
                    Edge = SourceEdges.ElementAt(RandomInt);
                } while (Edge.Source == Target || Edge.Target == Target);
                CurrentSource = MergeNodes(graph, CurrentSource, Edge.Source == Source ? Edge.Target : Edge.Source);
                Cuts.Add(graph.AdjacentEdges(CurrentSource).ToList());
            }*/
            List<Node> CurrentSet = new() { Source };
            List<GraphEdge> CurrentCut = new();
            //GraphEdge RandomEdge;
            Queue.Enqueue(CurrentSet);
            while (Queue.Count != 0)
            {
                CurrentSet = Queue.Dequeue();
                //Console.Write("\nCurrent Set: ");
                //CurrentSet.ForEach((Node) => Console.Write($"{Node.Label} |"));
                CurrentCut = new();
                foreach (Node Node in CurrentSet)
                {
                    CurrentCut.AddRange(Graph.AdjacentEdges(Node).Where(Edge => !(CurrentSet.Contains(Edge.Source) && CurrentSet.Contains(Edge.Target))));
                }
                Cuts.Add(CurrentCut);

                //Console.Write("\nCurrent Cut: ");
                //CurrentCut.ForEach((Edge) => Console.Write($"P[{Edge.Source.Label}-{Edge.Target.Label}] |"));
                if (Graph.Vertices.Count() - CurrentSet.Count >= 2)
                {
                    List<GraphEdge> CutWithoutTarget = CurrentCut.Where(Edge => Edge.Source != Target && Edge.Target != Target).ToList();
                    foreach (GraphEdge EveryEdge in CutWithoutTarget)
                    {
                        List<Node> New = new(CurrentSet);
                        if (CurrentSet.Contains(EveryEdge.Source))
                        {
                            if (!CurrentSet.Contains(EveryEdge.Target))
                            {
                                New.Add(EveryEdge.Target);
                            }
                        }
                        else
                        {
                            New.Add(EveryEdge.Source);
                        }
                        //New.Add(CurrentSet.Contains(EveryEdge.Source) ? EveryEdge.Target : EveryEdge.Source);
                        Queue.Enqueue(New);
                    }
                    //int RandomInt = Generator.Next(0, CutWithoutTarget.Count);
                    //RandomEdge = CutWithoutTarget.ElementAt(RandomInt);
                    //CurrentSet.Add(CurrentSet.Contains(RandomEdge.Source) ? RandomEdge.Target : RandomEdge.Source);
                    //CurrentCut = new();
                }
                /*foreach (Node Node in CurrentSet)
                {
                    CurrentCut.AddRange(Graph.AdjacentEdges(Node).Where(Edge => !(CurrentSet.Contains(Edge.Source) && CurrentSet.Contains(Edge.Target))));
                }
                Cuts.Add(CurrentCut);*/
            }
            //MinCuts.AddRange(Cuts);
            foreach (var Cut in Cuts)
            {
                MinCuts.AddUnique(Cut);
            }
            /*foreach (List<GraphEdge> Cut in Cuts)
            {
                if (Cuts.Any(OtherCut.All()))
            }*/
            /*foreach(List<GraphEdge> Cut in Cuts)
            {
                foreach (List<GraphEdge> OtherCut in Cuts)
                {
                    if (!Cut.SequenceEqual(OtherCut) || !(MinCuts.Contains(Cut) || MinCuts.Contains(OtherCut)))
                    {
                        MinCuts.Add(Cut);
                    }
                }
            }*/
            /*foreach (List<GraphEdge> Cut in Cuts)
            {
                //if (Cuts.Any(OtherCut => Cut.All(edge => OtherCut.Any(otherEdge => EqualEdges(edge, otherEdge)))))
                if (!Cuts.Any(OtherCut => (IsSubset(Cut, OtherCut) && Cut != OtherCut) || 
                !(MinCuts.Contains(Cut) && MinCuts.Contains(OtherCut))))
                {
                    MinCuts.Add(Cut);
                }
            }*/
            //MinCuts.AddRange(Cuts);
            /*List<List<Edge<Node>>> NewCuts = new();
            foreach (List<Edge<Node>> Cut in Cuts)
            {
                //if (Cuts.Any(OtherCut => Cut.All(edge => OtherCut.Any(otherEdge => EqualEdges(edge, otherEdge)))))
                if (!Cuts.Any(OtherCut => IsSubset(Cut, OtherCut)))
                {
                    NewCuts.Add(Cut);
                }
            }
            foreach (List<Edge<Node>> Cut in NewCuts)
            {
                List<Edge<Node>> NewCut = new();
                foreach (Edge<Node> Edge in Cut)
                {
                    NewCut.Add(Graph.Edges.First(edge => EqualEdges(Edge, edge)));
                }
                MinCuts.Add(NewCut);
            }*/
            return MinCuts;
        }
        /*private static bool IsSubset(List<GraphEdge> Set, List<GraphEdge> SubSet) =>
            SubSet.All(SubEdge => Set.Any(Edge => Edge.UndirectedVertexEquality(SubEdge.Source, SubEdge.Target)));*.

        /*private bool EqualEdges(GraphEdge Edge1, GraphEdge Edge2) =>
            (Edge1.Source == Edge2.Source && Edge1.Target == Edge2.Target) ||
            (Edge1.Source == Edge2.Target && Edge1.Target == Edge2.Source);*/
        /*private List<Edge<Node>> AllEdges(BidirectionalGraph<Node, Edge<Node>> graph, Node node)
        {
            var Result = graph.InEdges(node).ToList();
            Result.AddRange(graph.OutEdges(node));
            return Result;
        }*/
    }

    public static class ListExtensions
    {
        public static void AddUnique<T>(this List<List<T>> list, List<T> item)
        {
            if (list.All(innerList => !innerList.All(obj => item.Contains(obj, EqualityComparer<T>.Default))))
            {
                list.Add(item);
            }
        }
    }
}