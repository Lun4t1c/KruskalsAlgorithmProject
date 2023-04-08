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
            Console.WriteLine("Hello graph");            
        }
    }
}
