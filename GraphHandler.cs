using QuikGraph;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuikGraph.Algorithms.Search;
using QuikGraph.Algorithms.Observers;

namespace University_Diploma
{
    public class GraphHandler
    {
        
        // UndirectedGraph
        // UndirectedDepthFirstSearchAlgorithm
        // UndirectedVertexPredecessorRecorderObserver
        public UndirectedGraph<Node, UndirectedEdge<Node>> Graph { get; private set; }
        public Dictionary<UndirectedEdge<Node>, double> Probabilities;
        public readonly List<List<Node>> NodePaths = new();
        private readonly List<List<UndirectedEdge<Node>>> MinPaths = new();
        private readonly List<List<UndirectedEdge<Node>>> MinCuts = new();
        //private UndirectedDepthFirstSearchAlgorithm<Node, UndirectedEdge<Node>> Algorithm;
        //private UndirectedVertexPredecessorRecorderObserver<Node, UndirectedEdge<Node>> Observer;
        private readonly Random Generator = new ();

        public GraphHandler(UndirectedGraph<Node, UndirectedEdge<Node>> graph)
        {
            Graph = graph;
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

        // Karger's Algorithm
        public List<List<UndirectedEdge<Node>>> AllMinPaths(Node Source, Node Target)
        {
            //NodePaths.Clear();
            MinPaths.Clear();
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
                    //NodePaths.Add(new List<Node> (Path));
                }
                var Edges = Graph.AdjacentEdges(Last);
                foreach(UndirectedEdge<Node> Edge in Edges)
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

        public List<UndirectedEdge<Node>> PathNodeToEdge(List<Node> Path)
        {
            List<UndirectedEdge<Node>> New = new();
            UndirectedEdge<Node> Edge;
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
        public List<List<UndirectedEdge<Node>>> AllMinCuts(Node Source, Node Target)
        {
            MinCuts.Clear();
            //HashSet<Node> SourceSet = new();
            List<List<UndirectedEdge<Node>>> Cuts = new();
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
            List<UndirectedEdge<Node>> CurrentCut = new();
            UndirectedEdge<Node> RandomEdge;
            while (Graph.Vertices.Count() - CurrentSet.Count >= 2)
            {
                foreach (Node Node in CurrentSet)
                {
                    CurrentCut.AddRange(Graph.AdjacentEdges(Node).Where(Edge => !(CurrentSet.Contains(Edge.Source) && CurrentSet.Contains(Edge.Target))));
                }
                Cuts.Add(CurrentCut);
                List<UndirectedEdge<Node>> CutWithoutTarget = CurrentCut.Where(Edge => Edge.Source != Target && Edge.Target != Target).ToList();
                int RandomInt = Generator.Next(0, CutWithoutTarget.Count);
                RandomEdge = CutWithoutTarget.ElementAt(RandomInt);
                CurrentSet.Add(CurrentSet.Contains(RandomEdge.Source) ? RandomEdge.Target : RandomEdge.Source);
                CurrentCut = new();
            }
            foreach (Node Node in CurrentSet)
            {
                CurrentCut.AddRange(Graph.AdjacentEdges(Node).Where(Edge => !(CurrentSet.Contains(Edge.Source) && CurrentSet.Contains(Edge.Target))));
            }
            Cuts.Add(CurrentCut);

            /*foreach (List<Edge<Node>> Cut in Cuts)
            {
                //if (Cuts.Any(OtherCut => Cut.All(edge => OtherCut.Any(otherEdge => EqualEdges(edge, otherEdge)))))
                if (!Cuts.Any(OtherCut => IsSubset(Cut, OtherCut) && Cut != OtherCut))
                {
                    MinCuts.Add(Cut);
                }
            }*/
            MinCuts.AddRange(Cuts);
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

        /*private *//*UndirectedGraph<Node, Edge<Node>>*//*Node MergeNodes(UndirectedGraph<Node, Edge<Node>> graph, Node Source, Node Merging)
        {
            List<Edge<Node>> SourceEdges = new();
            graph.AdjacentEdges(Source).ToList().ForEach(Edge => SourceEdges.Add(new (Source, Edge.Source == Source ? Edge.Target : Edge.Source)));
            List<Edge<Node>> MergingNodeEdges = new();
            graph.AdjacentEdges(Merging).ToList().ForEach(Edge => MergingNodeEdges.Add(new(Merging, Edge.Source == Merging ? Edge.Target : Edge.Source)));
            //graph.AdjacentEdges(Merging);
            Node NewNode = new(Merging.ID*//*Guid.NewGuid().ToString("N")*//*, Source.Label);
            //List<Node> AdjacentNodes = new();
            *//*for (Edge<Node> Edge in SourceEdges)
            {
                if (Edge.Source !=)
                //AdjacentNodes.Add()
            }*//*
            graph.RemoveVertex(Source);
            graph.RemoveVertex(Merging);
            graph.AddVertex(NewNode);
            foreach (Edge<Node> Edge in SourceEdges)
            {
                if (Edge.Source != Source && Edge.Target != Merging*//* && Edge.Source != Merging && Edge.Target != Source*//*)
                {
                    graph.AddEdge(new Edge<Node>(NewNode, Edge.Target*//*, Edge.Source == Source ? Edge.Target : Edge.Source*//*));
                }
            }
            foreach (Edge<Node> Edge in MergingNodeEdges)
            {
                if (Edge.Source != Merging && Edge.Target != Source*//* && Edge.Source != Merging && Edge.Target != Source*//*)
                {
                    graph.AddEdge(new Edge<Node>(NewNode, Edge.Target*//*, Edge.Source == Merging ? Edge.Target : Edge.Source*//*));
                }
            }
            return NewNode;
        }*/
        private static bool IsSubset(List<UndirectedEdge<Node>> Set, List<UndirectedEdge<Node>> SubSet) =>
            SubSet.All(SubEdge => Set.Any(Edge => Edge.UndirectedVertexEquality(SubEdge.Source, SubEdge.Target)));

        /*private bool EqualEdges(UndirectedEdge<Node> Edge1, UndirectedEdge<Node> Edge2) =>
            (Edge1.Source == Edge2.Source && Edge1.Target == Edge2.Target) ||
            (Edge1.Source == Edge2.Target && Edge1.Target == Edge2.Source);*/
        /*private List<Edge<Node>> AllEdges(BidirectionalGraph<Node, Edge<Node>> graph, Node node)
        {
            var Result = graph.InEdges(node).ToList();
            Result.AddRange(graph.OutEdges(node));
            return Result;
        }*/

        /*public List<List<Edge<Node>>> AllMinCuts(Node Source, Node Target)
        {
            MinCuts.Clear();
            FindMinCuts(Graph.Clone(), Source, Target, new List<Edge<Node>>());
            var Cuts = MinCuts.OrderBy(Cut => Cut.Count).ToArray();
            var OtherCuts = MinCuts.ToArray();
            MinCuts.Clear();
            foreach (List<Edge<Node>> Cut in Cuts)
            {
                //if (Cuts.Any(OtherCut => Cut.All(edge => OtherCut.Any(otherEdge => EqualEdges(edge, otherEdge)))))
                if (!Cuts.Any(OtherCut => IsSubset(Cut, OtherCut)))
                {
                    MinCuts.Add(Cut);
                }
            }
            *//*foreach (List<Edge<Node>> Cut in Cuts)
            {
                foreach (List<Edge<Node>> OtherCut in OtherCuts)
                {
                    // If a cut isn't the same, and contains all edges of a small cut
                    // Delete this cut form the list
                    if (!Cut.Equals(OtherCut) && Cut.All(Edge => OtherCut.Contains(Edge)))
                    {
                        MinCuts.Remove(OtherCut);
                    }
                }
            }*//*
            return MinCuts;
        }

        private void FindMinCuts(UndirectedGraph<Node, Edge<Node>> graph, Node Source, Node Target, List<Edge<Node>> Cut)
        {
            //graph.
            Algorithm = new(graph);
            Observer = new();
            Observer.Attach(Algorithm);
            Algorithm.Compute(Source);
            if (!Observer.TryGetPath(Target, out _))
            {
                MinCuts.Add(new List<Edge<Node>>(Cut));
                return;
            }
            var Edges = graph.Edges.ToArray();
            foreach (Edge<Node> edge in Edges)
            {
                if (!Cut.Contains(edge))
                {
                    Cut.Add(edge);
                    graph.RemoveEdge(edge);
                    FindMinCuts(graph, Source, Target, Cut);
                    graph.AddEdge(edge);
                }
            }
        }*/
    }
}
