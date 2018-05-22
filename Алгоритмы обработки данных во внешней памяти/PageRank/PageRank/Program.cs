using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageRank
{
    class Program
    {
        static void Main(string[] args)
        {
            var graph = new Graph();
            var pageRank = new PageRank(graph, 20, 0.85, true);
            pageRank.Run();


            Console.ReadKey();
        }
    }

    public class Node
    {
        public List<Node> InputNodes { get; set; }
        public List<Node> OutputNodes { get; set; }
        public double Weight { get; set; }
        public double UpdatedWeight { get; set; }
        public string Name { get; set; }

        public Node(string name)
        {
            Name = name;
            InputNodes = new List<Node>();
            OutputNodes = new List<Node>();
        }
    }

    public class Graph
    {
        public List<Node> graphNodes { get; set; }

        public Graph()
        {
            var A = new Node("A");
            var B = new Node("B");
            var C = new Node("C");
            var D = new Node("D");

            A.OutputNodes.Add(B);
            A.OutputNodes.Add(C);
            A.InputNodes.Add(C);

            B.OutputNodes.Add(D);
            B.InputNodes.Add(A);
            B.InputNodes.Add(C);

            C.OutputNodes.Add(A);
            C.OutputNodes.Add(B);
            C.OutputNodes.Add(D);
            C.InputNodes.Add(A);
            C.InputNodes.Add(D);

            D.OutputNodes.Add(C);
            D.InputNodes.Add(C);
            D.InputNodes.Add(B);

            //var E = new Node("E");
            //E.InputNodes.Add(A);
            //E.InputNodes.Add(B);
            //E.OutputNodes.Add(A);

            graphNodes = new List<Node> { A, B, C, D, /*E*/ };
        }
    }

    public class PageRank
    {
        public int Iterations { get; set; }
        public double DampingFactor { get; set; }
        public Graph Graph { get; set; }
        public bool Verbose { get; set; }

        public PageRank(Graph graph, int iterations, double dampingFactor, bool verbose)
        {
            Iterations = iterations;
            DampingFactor = dampingFactor;
            Verbose = verbose;
            Graph = graph;
            foreach (var graphNode in Graph.graphNodes)
            {
                graphNode.Weight = 1.0 / Graph.graphNodes.Count;
            }
        }

        public void Run()
        {
            for (int i=0; i<Iterations; i++)
            {
                foreach (var graphNode in Graph.graphNodes)
                {
                    UpdatePageRank(graphNode);
                }

                foreach (var graphNode in Graph.graphNodes)
                {
                    graphNode.Weight = graphNode.UpdatedWeight;
                    graphNode.UpdatedWeight = 0;
                }

                if (Verbose)
                {
                    Console.WriteLine(String.Format("Iteration {0}: {1}",
                                                    i + 1,
                                                    String.Join(", ", Graph.graphNodes.Select(x => Math.Round(x.Weight, 2)).ToArray())));
                }
            }

            var sortedNodesByPR = Graph.graphNodes.OrderByDescending(x => x.Weight).Select(x => x.Name).ToList();
            Console.WriteLine("\nResult: ");
            foreach (var nodeName in sortedNodesByPR)
            {
                Console.WriteLine(nodeName);
            }
        }

        private void UpdatePageRank(Node graphNode)
        {
            foreach (var inputGraphNode in graphNode.InputNodes)
            {
                graphNode.UpdatedWeight += inputGraphNode.Weight / inputGraphNode.OutputNodes.Count;
            }
            graphNode.UpdatedWeight = (1 - DampingFactor) + DampingFactor * graphNode.UpdatedWeight;
        }
    }
}
