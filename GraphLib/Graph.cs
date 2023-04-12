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
            AddEdge(
                edge.Label,
                edge.Weight,
                edge.FromVertex.Label,
                edge.ToVertex.Label
            );
        }

        public Graph GetMSTKruskal()
        {
            List<Edge> edges = new List<Edge>(this.Edges);
            edges.Sort((e1, e2) => e1.Weight.CompareTo(e2.Weight));

            int[] parent = new int[Vertices.Count];
            for (int i = 0; i < Vertices.Count; i++)
                parent[i] = i;

            List<Edge> minimumSpanningTree = new List<Edge>();

            foreach (Edge edge in edges)
            {
                // Find absolute parents of both vertices
                int root1 = FindRoot(edge.FromVertex.Id - 1, parent);
                int root2 = FindRoot(edge.ToVertex.Id - 1, parent);

                // If parents are different vertices then it's guaranteed
                // that adding this edge will not form cycle in graph
                if (root1 != root2)
                {
                    minimumSpanningTree.Add(edge);
                    parent[root1] = root2;
                }
            }

            Graph graph = new Graph();
            foreach (Edge edge in minimumSpanningTree)
                graph.AddEdge(edge);

            return graph;
        }

        public List<Graph> GetMSTKruskalStepByStep()
        {
            List<Graph> steps = new List<Graph>();
            Graph tempGraph = new Graph();
            tempGraph.Vertices = new List<Vertex>(this.Vertices);
            steps.Add(new Graph(tempGraph));

            List<Edge> edges = new List<Edge>(this.Edges);
            edges.Sort((e1, e2) => e1.Weight.CompareTo(e2.Weight));

            int[] parent = new int[Vertices.Count];
            for (int i = 0; i < Vertices.Count; i++)
                parent[i] = i;

            List<Edge> minimumSpanningTreeEdges = new List<Edge>();

            foreach (Edge edge in edges)
            {
                // Find absolute parents of both vertices
                int root1 = FindRoot(edge.FromVertex.Id - 1, parent);
                int root2 = FindRoot(edge.ToVertex.Id - 1, parent);

                // If parents are different vertices then it's guaranteed
                // that adding this edge will not form cycle in graph
                if (root1 != root2)
                {
                    minimumSpanningTreeEdges.Add(edge);
                    tempGraph.AddEdge(edge);
                    steps.Add(new Graph(tempGraph));
                    parent[root1] = root2;
                }
            }

            Graph graph = new Graph();
            foreach (Edge edge in minimumSpanningTreeEdges)
                graph.AddEdge(edge);

            return steps;
        }

        int FindRoot(int vertex, int[] parent)
        {
            while (parent[vertex] != vertex)
                vertex = parent[vertex];

            return vertex;
        }

        public void TransformIntoMSTKruskal()
        {
            Graph mst = this.GetMSTKruskal();
            this.Edges = mst.Edges;
        }

        private Vertex _addVertex(string label)
        {
            Vertex vertex = new Vertex(Vertices.Count + 1, label);
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

        public static Graph GenerateSampleGraph2()
        {
            Graph graph = new Graph();

            graph.AddEdge("-", 2, "a", "b");
            graph.AddEdge("-", 3, "a", "m");
            graph.AddEdge("-", 1, "b", "k");
            graph.AddEdge("-", 6, "b", "j");
            graph.AddEdge("-", 3, "b", "c");
            graph.AddEdge("-", 1, "c", "f");
            graph.AddEdge("-", 7, "d", "e");
            graph.AddEdge("-", 3, "e", "f");
            graph.AddEdge("-", 2, "f", "h");
            graph.AddEdge("-", 4, "h", "g");
            graph.AddEdge("-", 5, "h", "i");
            graph.AddEdge("-", 1, "i", "g");
            graph.AddEdge("-", 1, "i", "j");
            graph.AddEdge("-", 2, "i", "k");
            graph.AddEdge("-", 4, "k", "j");
            graph.AddEdge("-", 4, "k", "l");
            graph.AddEdge("-", 3, "l", "m");
            graph.AddEdge("-", 7, "m", "k");

            return graph;
        }
        #endregion
    }
}