using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLib
{
    public class Vertex
    {
        #region Properties
        public int Id { get; }
        public string Label { get; }
        #endregion


        #region Contructor
        public Vertex(int id, string label)
        {
            Id = id;
            Label = label;
        }
        #endregion


        #region Methods

        #endregion
    }
}
