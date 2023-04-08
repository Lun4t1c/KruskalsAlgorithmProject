namespace GraphLib
{
    public class Graph
    {
        #region Properties
        private static int _numberOfEdges = 0;

        public List<Edge> Edges { get; set; } = new List<Edge>();
        public List<Vertex> Vertices { get; set; } = new List<Vertex>();
        #endregion


        #region Constructor
        public Graph()
        {

        }
        #endregion


        #region Methods
        public void AddEdge(string label, int weight, string fromLabel, string toLabel)
        {
            Vertex fromVertex = _findOrCreateVertex(fromLabel);
            Vertex toVertex = _findOrCreateVertex(toLabel);

            Edges.Add(new Edge(
                label,
                weight,
                fromVertex,
                toVertex
            ));
        }

        private Vertex _addVertex(string label)
        {
            Vertex vertex = new Vertex(++_numberOfEdges, label);
            Vertices.Add(vertex);
            return vertex;
        }

        private Vertex _findOrCreateVertex(string label)
        {
            Vertex? result = Vertices.FirstOrDefault(v => v.Label == label);

            if (result == null)
            {
                Console.WriteLine($"Vertex '{label}' does not exist, creating...");
                return _addVertex(label);
            }

            return result;
        }
        #endregion


        #region Static methods
        public static Graph GenerateSampleGraph()
        {
            Graph graph = new Graph();

            return graph;
        }
        #endregion
    }
}