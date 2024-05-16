using QuikGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace University_Diploma
{
    public class GraphEdge: UndirectedEdge<Node>
    {
        [XmlAttribute("probability")]
        private double _probability;
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
