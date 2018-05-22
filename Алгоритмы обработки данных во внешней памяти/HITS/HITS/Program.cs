using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HITS
{
    class Program
    {
        static void Main(string[] args)
        {
            var graph = new Graph();
            var hits = new HITS(graph, 10, true);
            hits.Run();

            Console.ReadKey();
        }

        public class HITS
        {
            public Graph Graph { get; set; }

            public int Iterations { get; set; }


            public bool Verbose { get; set; }

            public HITS (Graph graph, int iterations,  bool verbose)
            {
                Graph = graph;
                Iterations = iterations;
                Verbose = verbose;
            }

            public void Run()
            {
                int graphCount = Graph.graphNodes.Count;
                var adjacencyMatrix = new double[graphCount, graphCount];

                foreach (Node graphNode in Graph.graphNodes)
                {
                    foreach(Node outputNode in graphNode.OutputNodes)
                    {
                        int xСoordinate = Graph.graphNodes.IndexOf(graphNode);
                        int yCoordinate = Graph.graphNodes.IndexOf(outputNode);
                        adjacencyMatrix[xСoordinate, yCoordinate] = 1;
                    }
                }

                double[,] adjacencyMatrixTranspose = TransposeMatrix(adjacencyMatrix);
                
                var hubWeightVector = new double[graphCount, 1];
                var authorityWeightVector = new double[graphCount, 1];
                for (int i = 0; i < graphCount; i++)
                {
                    hubWeightVector[i, 0] = 1 / Math.Sqrt(graphCount);
                    authorityWeightVector[i, 0] = 1 / Math.Sqrt(graphCount);
                }

                int iterator = 0;
                while (iterator < Iterations)
                {
                    hubWeightVector = MultiplyMatrix(adjacencyMatrix, authorityWeightVector);
                    authorityWeightVector = MultiplyMatrix(adjacencyMatrixTranspose, hubWeightVector);

                    double hubSum = 0;
                    double authoritySum = 0;
                    for (int i = 0; i < graphCount; i++)
                    {
                        hubSum += Math.Pow(hubWeightVector[i, 0], 2);
                        authoritySum += Math.Pow(authorityWeightVector[i, 0], 2);
                    }

                    for (int i = 0; i < graphCount; i++)
                    {
                        hubWeightVector[i, 0] = hubWeightVector[i, 0] / Math.Sqrt(hubSum);
                        authorityWeightVector[i, 0] = authorityWeightVector[i, 0] / Math.Sqrt(authoritySum); 
                    }


                    if (Verbose)
                    {
                        var hubWeightVector1D = new double[hubWeightVector.GetLength(0)];
                        var authorityWeightVector1D = new double[authorityWeightVector.GetLength(0)];

                        for (int i = 0; i < graphCount; i++)
                        {
                            hubWeightVector1D[i] = hubWeightVector[i, 0];
                            authorityWeightVector1D[i] = authorityWeightVector[i, 0];
                        }

                        Console.WriteLine(String.Format("Iteration {2}:\t Hubs: ({0})\t | Auth: ({1})", String.Join(" ", hubWeightVector1D.Select(x => Math.Round(x, 3).ToString()).ToArray()),
                                                                                                        String.Join(" ", authorityWeightVector1D.Select(x => Math.Round(x, 3).ToString()).ToArray()),
                                                                                                        iterator + 1));
                    }

                    iterator++;
                }


            }

            private double[,] TransposeMatrix(double[,] matrix)
            {
                var resultMatrix = new double[matrix.GetLength(0), matrix.GetLength(1)];

                for (int i = 0; i < matrix.GetLength(0); i++)
                    for (int j = 0; j < matrix.GetLength(1); j++)
                        resultMatrix[i, j] = matrix[j, i];

                return resultMatrix;
            }
            private double[,] MultiplyMatrix(double[,] matrixA, double[,] matrixB)
            {
                double temp = 0;
                var resultMatrix = new double[matrixA.GetLength(0), matrixB.GetLength(1)];
                if (matrixA.GetLength(1) != matrixB.GetLength(0))
                {
                    Console.WriteLine("Dimensional error");
                }
                else
                {
                    for (int i = 0; i < matrixA.GetLength(0); i++)
                    {
                        for (int j = 0; j < matrixB.GetLength(1); j++)
                        {
                            temp = 0;
                            for (int k = 0; k < matrixA.GetLength(1); k++)
                                temp += matrixA[i, k] * matrixB[k, j];

                            resultMatrix[i, j] = temp;
                        }
                    }
                }
                return resultMatrix;
            }
        }

        public class Node
        {
            public List<Node> InputNodes { get; set; }
            public List<Node> OutputNodes { get; set; }
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
                var N1 = new Node("Yahoo");
                var N2 = new Node("Amazon");
                var N3 = new Node("Microsoft");

                N1.OutputNodes.Add(N1);
                N1.OutputNodes.Add(N2);
                N1.OutputNodes.Add(N3);
                N1.InputNodes.Add(N2);

                N2.OutputNodes.Add(N1);
                N2.OutputNodes.Add(N3);
                N2.InputNodes.Add(N1);
                N2.InputNodes.Add(N3);

                N3.OutputNodes.Add(N2);
                N3.InputNodes.Add(N1);
                N3.InputNodes.Add(N2);

                graphNodes = new List<Node> { N1, N2, N3 };
            }
        }
    }
}
