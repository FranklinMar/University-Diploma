using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using QuikGraph;

namespace University_Diploma
{
    public class GraphHandler
    {
        public UndirectedGraph<Node, GraphEdge> Graph { get; private set; }
        public List<List<Node>> NodePaths;
        private List<List<GraphEdge>> MinPaths;
        private List<List<GraphEdge>> MinCuts;

        public GraphHandler(UndirectedGraph<Node, GraphEdge> graph)
        {
            Graph = graph;
        }
        // Modified Breadth-first Search
        public List<List<GraphEdge>> AllMinPaths(Node Source, Node Target)
        {
            MinPaths = new();
            NodePaths = new();
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
                    if (
                        !Path.Contains(Edge.GetOtherVertex(Last)))
                    {
                        List<Node> New = new(Path);
                        New.Add(Edge.GetOtherVertex(Last));
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
                Edge = Graph.Edges.First(edge => edge.UndirectedVertexEquality(Path[i], Path[i + 1]));
                New.Add(Edge);
            }
            return New;
        }
        // Modified (Stoer-Wagner) Karger's Algorithm
        public List<List<GraphEdge>> AllMinCuts(Node Source, Node Target)
        {
            MinCuts = new();
            Queue<List<Node>> Queue = new();
            
            List<Node> CurrentSet = new() { Source };
            List<GraphEdge> CurrentCut = new();
            Queue.Enqueue(CurrentSet);
            while (Queue.Count != 0)
            {
                CurrentSet = Queue.Dequeue();
                CurrentCut = new();
                foreach (Node Node in CurrentSet)
                {
                    CurrentCut.AddRange(Graph.AdjacentEdges(Node).Where(Edge => !(CurrentSet.Contains(Edge.Source) && CurrentSet.Contains(Edge.Target))));
                }
                MinCuts.AddUnique(CurrentCut);
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
                        Queue.Enqueue(New);
                    }
                }
            }
            return MinCuts;
        }
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