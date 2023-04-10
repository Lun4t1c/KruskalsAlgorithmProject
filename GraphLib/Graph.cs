namespace GraphLib
{
    public class Graph
    {
        #region Properties
        public List<Edge> Edges { get; set; } = new List<Edge>();
        public List<Vertex> Vertices { get; set; } = new List<Vertex>();
        #endregion


        #region Constructor
        public Graph()
        {

        }

        public Graph(Graph graph)
        {
            Edges = new List<Edge>(graph.Edges);
            Vertices = new List<Vertex>(graph.Vertices);
        }
        #endregion


        #region Methods
        public int GetWeight()
        {
            int result = 0;

            foreach (Edge edge in Edges)
                result += edge.Weight;

            return result;
        }

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

        public void AddEdge(Edge edge)
        {
            Vertex fromVertex = _findOrCreateVertex(edge.FromVertex.Label);
            Vertex toVertex = _findOrCreateVertex(edge.ToVertex.Label);

            Edges.Add(new Edge(
                edge.Label,
                edge.Weight,
                fromVertex,
                toVertex
            ));
        }

        public Graph GetMSTKruskal()
        {
            List<Edge> edges = new List<Edge>(this.Edges);
            edges.Sort((e1, e2) => e1.Weight.CompareTo(e2.Weight));

            int[] parent = new int[Vertices.Count];
            for (int i = 0; i < Vertices.Count; i++)
            {
                parent[i] = i;
            }

            List<Edge> minimumSpanningTree = new List<Edge>();

            foreach (Edge edge in edges)
            {
                int root1 = FindRoot(edge.FromVertex.Id - 1, parent);
                int root2 = FindRoot(edge.ToVertex.Id - 1, parent);

                if (root1 != root2)
                {
                    minimumSpanningTree.Add(edge);
                    parent[root1] = root2;
                }
            }

            Graph graph = new Graph();
            foreach (Edge edge in minimumSpanningTree)
            {
                graph.AddEdge(edge);
            }
            return graph;
        }

        int FindRoot(int vertex, int[] parent)
        {
            while (parent[vertex] != vertex)
            {
                vertex = parent[vertex];
            }
            return vertex;
        }

        public void TransformIntoMSTKruskal()
        {
            Graph mst = this.GetMSTKruskal();
            this.Edges = mst.Edges;
        }

        public List<Graph> PerformKruskalAlgorithmGetStepByStep()
        {
            List<Graph> steps = new List<Graph>();

            throw new NotImplementedException();

            return steps;
        }

        private Vertex _addVertex(string label)
        {
            Vertex vertex = new Vertex(Vertices.Count + 1, label);
            Vertices.Add(vertex);
            return vertex;
        }

        private void _containsCycle()
        {
            throw new NotImplementedException();
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

            graph.AddEdge("a", 2, "a", "b");
            graph.AddEdge("b", 4, "a", "d");
            graph.AddEdge("c", 5, "a", "f");
            graph.AddEdge("d", 1, "b", "d");
            graph.AddEdge("e", 3, "b", "e");
            graph.AddEdge("f", 7, "b", "c");
            graph.AddEdge("g", 4, "b", "g");
            graph.AddEdge("h", 8, "b", "f");
            graph.AddEdge("i", 10, "c", "e");
            graph.AddEdge("j", 2, "d", "e");
            graph.AddEdge("k", 1, "f", "g");
            graph.AddEdge("l", 6, "g", "c");

            return graph;
        }
        #endregion
    }
}