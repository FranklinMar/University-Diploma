﻿using Newtonsoft.Json.Linq;
using QuikGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace University_Diploma
{
    public delegate void Notify(/*JObject JSON*/);
    public class ProxyController
    {
        public UndirectedGraph<Node, GraphEdge> Graph { get; private set; } = new();
        //public Dictionary<GraphEdge, double> Probabilities { get; private set; } = new();
        public event Notify GraphChanged;

        public ProxyController(Notify graphChanged/*out UndirectedGraph<Node, Edge<Node>> graph*/)
        {
            GraphChanged += graphChanged;
            //graph = Graph;
        }

        public void DisplayError(string message)
        {
            MessageBox.Show(message, "Error");
        }

        public void RecieveGraph(string JSON)
        {
            //List<Dictionary<string,>>
            //List<string> TempIDs = new();
            JObject JSONObject = JObject.Parse(JSON);
            Graph.Clear();
            //Probabilities.Clear();
            JToken[] edges = JSONObject.GetValue("edges").ToArray();
            JToken[] nodes = JSONObject.GetValue("nodes").ToArray();
            foreach (JToken Node in nodes)
            {
                if (Node.Value<string>("label") != "")
                {
                    Graph.AddVertex(new Node(Node.Value<string>("id"), Node.Value<string>("label")/*, Node.Value<bool?>("pole") != null*/));
                }/* else
                    {
                        TempIDs.Add(new Node(Node.Value<string>("id"));
                    }*/
            }
            foreach (JToken Edge in edges)
            {
                try
                {
                    Node Source = Graph.Vertices.First(node => node.ID.Equals(Edge.Value<string>("source")));
                    Node Target = Graph.Vertices.First(node => node.ID.Equals(Edge.Value<string>("target")));
                    GraphEdge edge;
                    try
                    {
                        if (Edge.Value<string>("label") != "")
                        {
                            //MessageBox.Show($"'{Edge.Value<string>("label")}'");
                            edge = new(Source, Target, Edge.Value<double>("label"));
                            Graph.AddEdge(edge);
                        }
                    }
                    catch (ArgumentException)
                    {
                        //edge = new(Target, Source);
                    }
                    //Probabilities.Add(edge, Edge.Value<double>("label"));
                    //Graph.AddEdge(new Edge<Node>(Target, Source));
                }
                catch (InvalidOperationException)
                {
                    continue;
                }
            }
            GraphChanged?.Invoke(/*JSONObject*/);
            //Graph.Edges[0].
            //Graph.AddVertex()
            //MessageBox.Show(JSON.GetType().ToString());
            //MessageBox.Show(Graph.ToString());
        }
    }
}
