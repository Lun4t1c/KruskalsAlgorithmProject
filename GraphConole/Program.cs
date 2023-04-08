using System;
using System.IO;

using GraphLib;

namespace GraphConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Graph graph = Graph.GenerateSampleGraph();
            Graph graph2 = new Graph(graph);

            PrintGraph(graph);
            Console.WriteLine();
            PrintGraph(graph2);
        }

        public static void PrintGraph(Graph graph)
        {
            foreach (Edge edge in graph.Edges)
            {
                Console.WriteLine($"{edge.FromVertex.Label}--{edge.ToVertex.Label} ({edge.Weight})");
            }
        }
    }
}
