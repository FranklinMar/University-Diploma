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
        public UndirectedGraph<Node, Edge<Node>> Graph { get; private set; }
        public readonly List<List<Node>> NodePaths = new();
        private readonly List<List<Edge<Node>>> MinPaths = new();
        private readonly List<List<Edge<Node>>> MinCuts = new();
        private UndirectedDepthFirstSearchAlgorithm<Node, Edge<Node>> Algorithm;
        private UndirectedVertexPredecessorRecorderObserver<Node, Edge<Node>> Observer;

        public GraphHandler(UndirectedGraph<Node, Edge<Node>> graph)
        {
            Graph = graph;
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
        public List<List<Edge<Node>>> AllMinPaths(Node Source, Node Target)
        {
            NodePaths.Clear();
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
                    NodePaths.Add(new List<Node> (Path));
                }
                var Edges = Graph.AdjacentEdges(Last);
                foreach(Edge<Node> Edge in Edges)
                {
                    if (!Path.Contains(Edge.Target))
                    {
                        List<Node> New = new(Path);
                        New.Add(Edge.Target);
                        Queue.Enqueue(New);
                    }
                }
            }
            return MinPaths;
        }

        public List<Edge<Node>> PathNodeToEdge(List<Node> Path)
        {
            List<Edge<Node>> New = new();
            Edge<Node> Edge;
            for (int i = 0; i < Path.Count - 1; i++)
            {
                Edge = Graph.Edges.First(edge => edge.Source == Path[i] && edge.Target == Path[i + 1]);
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

        public List<List<Edge<Node>>> AllMinCuts(Node Source, Node Target)
        {
            MinCuts.Clear();
            FindMinCuts(Graph.Clone(), Source, Target, new List<Edge<Node>>());
            var Cuts = MinCuts.OrderBy(Cut => Cut.Count).ToArray();
            var OtherCuts = MinCuts.ToArray();
            foreach (List<Edge<Node>> Cut in Cuts)
            {
                foreach(List<Edge<Node>> OtherCut in OtherCuts)
                {
                    // If a cut isn't the same, and contains all edges of a small cut
                    // Delete this cut form the list
                    if (!Cut.Equals(OtherCut) && Cut.All(Edge => OtherCut.Contains(Edge)))
                    {
                        MinCuts.Remove(OtherCut);
                    }
                }
            }
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
                MinCuts.Add(new List<Edge<Node>> (Cut));
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
        }
    }
}
