﻿using System;
using System.Xml.Serialization;
using QuikGraph;

namespace University_Diploma
{
    [Serializable]
    public class GraphEdge: UndirectedEdge<Node>
    {
        private double _probability;
        [XmlAttribute("probability")]
        public double Probability { 
            get {
                return _probability;
            }
            set {
                if (value < 0 || value > 1)
                {
                    throw new ArgumentException("Probability must be in range (0; 1)", nameof(Probability));
                }
                _probability = value;
            } 
        }

        public GraphEdge(Node source, Node target, double probability = 0.5) : base(source, target)
        {
            Probability = probability;
        }
        public virtual string ToLabel()
        {
            return $"{Probability}";
        }
    }
}
