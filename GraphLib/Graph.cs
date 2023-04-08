namespace GraphLib
{
    public class Graph
    {
        #region Properties
        public int _numberOfVertices { get; set; }

        public List<Edge> Edges { get; set; } = new List<Edge>();
        public List<Vertex> Vertices { get; set; } = new List<Vertex>();
        #endregion


        #region Constructor
        public Graph()
        {
            _numberOfVertices = 0;
        }

        public Graph(Graph graph)
        {
            _numberOfVertices = graph._numberOfVertices;
            Edges = new List<Edge>(graph.Edges);
            Vertices = new List<Vertex>(graph.Vertices);
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

        public List<Graph> performKruskalAlgorithm()
        {
            List<Graph> steps = new List<Graph>();

            return steps;
        }

        private Vertex _addVertex(string label)
        {
            Vertex vertex = new Vertex(++_numberOfVertices, label);
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