using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLib
{
    public class Edge
    {
        #region Properties
        public string Label { get; }
        public int Weight { get; }
        public Vertex FromVertex { get; }
        public Vertex ToVertex { get; }
        #endregion


        #region Constructor
        public Edge(string label, int weight, Vertex from, Vertex to)
        {
            Label = label;
            Weight = weight;
            FromVertex = from;
            ToVertex = to;
        }
        #endregion


        #region Methods

        #endregion
    }
}
