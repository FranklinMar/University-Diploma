using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace University_Diploma
{
    public class EdgeBar: TrackBar
    {
        public GraphEdge Edge { get; private set; }
        public Label Label { get; private set; }
        public EdgeBar(GraphEdge edge, Label label): base()
        {
            Edge = edge;
            Label = label;
            Maximum = 100;
            Minimum = 0;
            TickFrequency = 1;
            LargeChange = 5;
            SmallChange = 1;
            Value = (int)(Edge.Probability * 100);
            Label.Text = string.Format("{0:N2}", Edge.Probability).Replace(',', '.');
        }

        public void ChangeValue()
        {
            Edge.Probability = Value / 100.0;
            Label.Text = string.Format("{0:N2}", Edge.Probability).Replace(',', '.');
        }
    }
}
