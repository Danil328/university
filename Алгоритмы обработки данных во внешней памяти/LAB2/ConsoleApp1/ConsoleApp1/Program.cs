using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(String.Format("Size of pointer: {0}", IntPtr.Size));
            Console.WriteLine(String.Format("Is 64bit process: {0}", Environment.Is64BitProcess));
            Console.WriteLine(String.Format("OS version: {0} \n", Environment.OSVersion));
            

            var SIZE_OF_STRUCTURES = 1_000_000;
            var EXPERIMENTS_COUNT = 10;
            var random = new Random();
            var stopwatches = Tuple.Create(new Stopwatch(), new Stopwatch());

            #region Array tests
            var array = new long[SIZE_OF_STRUCTURES];

            for (var i = 0; i < array.Length; i++)
                array[i] = i + 1;

            var arrayResults = new List<(long linear, long binary)>();
            for (var i = 0; i < EXPERIMENTS_COUNT; i++)
            {
                var numToFind = random.Next(1, array.Length - 1);

                stopwatches.Item1.Start();
                var linearResult = LinearSearch(array, numToFind);
                stopwatches.Item1.Stop();

                stopwatches.Item2.Start();
                var binaryResult = BinarySearch(array, numToFind);
                stopwatches.Item2.Stop();

                arrayResults.Add(createTuple(linearResult, binaryResult));
            }
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(String.Format("Linear steps to find number for array size of {0}, Average time elapsed in seconds: {1}", 
                                            SIZE_OF_STRUCTURES,
                                            stopwatches.Item1.Elapsed.TotalSeconds));
            Console.ForegroundColor = ConsoleColor.White;
            arrayResults.Select(x => x.linear).ToList().ForEach(x => Console.Write($"{x} "));

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(String.Format("\nBinary steps to find number for array size of {0}, Average time elapsed in seconds: {1}",
                                            SIZE_OF_STRUCTURES,
                                            stopwatches.Item2.Elapsed.TotalSeconds));
            Console.ForegroundColor = ConsoleColor.White;
            arrayResults.Select(x => x.binary).ToList().ForEach(x => Console.Write($"{x} "));
            #endregion

            #region LinkedList tests
            stopwatches.Item1.Reset();
            stopwatches.Item2.Reset();
            var simplyLinkedList = new SimplyLinkedList();

            for (var i = 0; i < SIZE_OF_STRUCTURES; i++)
                simplyLinkedList.Push(i + 1);

            var linkedListResults = new List<(long linear, long binary)>();
            for (var i = 0; i < EXPERIMENTS_COUNT; i++)
            {
                var numToFind = random.Next(1, simplyLinkedList.Count - 1);

                stopwatches.Item1.Start();
                var linearResult = LinearSearchForLinkedList(simplyLinkedList, numToFind);
                stopwatches.Item1.Stop();

                stopwatches.Item2.Start();
                var binaryResult = BinarySearchForLinkedList(simplyLinkedList, numToFind);
                stopwatches.Item2.Stop();

                linkedListResults.Add(createTuple(linearResult, binaryResult));
            }
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(String.Format("\n\nLinear steps to find number for linked list size of {0}, Average time elapsed in seconds: {1}",
                                            SIZE_OF_STRUCTURES,
                                            stopwatches.Item1.Elapsed.TotalSeconds));
            Console.ForegroundColor = ConsoleColor.White;
            linkedListResults.Select(x => x.linear).ToList().ForEach(x => Console.Write($"{x} "));

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(String.Format("\nBinary steps to find number for linked list size of {0}, Average time elapsed in seconds: {1}",
                                            SIZE_OF_STRUCTURES,
                                            stopwatches.Item2.Elapsed.TotalSeconds));
            Console.ForegroundColor = ConsoleColor.White;
            linkedListResults.Select(x => x.binary).ToList().ForEach(x => Console.Write($"{x} "));
            #endregion

            Console.ReadKey();    
        }

        static (long linear, long binary) createTuple(long lin, long bin) => (lin, bin);
        static long LinearSearchForLinkedList(SimplyLinkedList simplyLinkedList, long numToFind)
        {
            var currentNode = simplyLinkedList.Head;
            var count = 0;

            while(currentNode != null)
            {
                count++;
                if (currentNode.Data == numToFind)
                    return count;
                currentNode = currentNode.Next;
            }
            return -1;
        }
        static long BinarySearchForLinkedList(SimplyLinkedList simplyLinkedList, long numToFind)
        {
            //Binary search is possible on the linked list if the list is ordered and you know the count of elements in list
            var start = 0;
            var startNode = simplyLinkedList.Head;
            var end = simplyLinkedList.Count - 1;
            var count = 0;
     
            while (start <= end)
            {
                count++;
                var middleNodeNumber = (start + end) / 2;
                var middleNode = simplyLinkedList.FindNodeFromNode(startNode, middleNodeNumber - start);

                if (middleNode.Data == numToFind)
                    return count;
                else
                {
                    if (numToFind < middleNode.Data)
                    {
                        startNode = middleNode.Next;
                        start = middleNodeNumber + 1;
                    }
                    else
                        end = middleNodeNumber - 1;
                }
            }
            return -1;
        }
        static long LinearSearch(long[] array, long numToFind)
        {
            for (var i = 0; i < array.Length; i++)
                if (numToFind == array[i])
                {
                    return i;
                }
            return -1;
        }
        static long BinarySearch(long[] array, long numToFind)
        {
            var start = 0;
            var end = array.Length;
            var count = 0;
            while (true)
            {
                var middle = (start + end) / 2;
                if (numToFind > middle)
                    start = middle;

                else if (numToFind < middle)
                    end = middle;

                else
                    return count;

                count++;
            }
        }
    }
}