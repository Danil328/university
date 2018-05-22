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
            var size = 100_000_000;

            #region Memory Consumption
            // int: 400Мб
            // decimal: 1600Мб
            // ptr: 400Мб
            // linked list<int>: 2000 Мб

            var bytes = GC.GetTotalMemory(false);
            var intArray = new int[size];
            intArray[0] = 0;
            var bytes2 = GC.GetTotalMemory(false);
            Console.WriteLine(String.Format("Size of int array: {0}", (bytes2 - bytes) / 1024 / 1024));

            bytes = GC.GetTotalMemory(false);
            var decimalArray = new decimal[size];
            decimalArray[0] = 0;
            bytes2 = GC.GetTotalMemory(false);
            Console.WriteLine(String.Format("Size of decimal array: {0}", (bytes2 - bytes) / 1024 / 1024));

            bytes = GC.GetTotalMemory(false);
            var linkedList = new LinkedList<int>(intArray);
            bytes2 = GC.GetTotalMemory(false);
            Console.WriteLine(String.Format("Size of linked list: {0}", (bytes2 - bytes) / 1024 / 1024 / 2));
            #endregion

            int number = 8;
            int factorial = Factorial(number);
            Console.WriteLine(String.Format("\nFactorial of {0} = {1}", number, factorial));

            #region Comparison of different sorts for Array
            Console.WriteLine("\nComparison of different sorts for Array\n=======================================");
            foreach (int testSize in new int[]{ 1000, 5000, 10000, 20000, 30000 })
            {
                Console.WriteLine(String.Format("  Array size: {0}", testSize));

                var randomArray = new int[testSize];
                var random = new Random();
                for (int i = 0; i < randomArray.Length; i++)
                    randomArray[i] = random.Next(0, 1000000);

                var stopwatch = new Stopwatch();

                stopwatch.Start();
                int[] result = SortByChoose((int[])randomArray.Clone());
                stopwatch.Stop();
                Console.WriteLine(String.Format("    Selection Sort \t\t {0}", stopwatch.Elapsed.TotalSeconds));

                stopwatch.Restart();
                result = SortByInsertion((int[])randomArray.Clone());
                stopwatch.Stop();
                Console.WriteLine(String.Format("    Insertion Sort \t\t {0}", stopwatch.Elapsed.TotalSeconds));

                stopwatch.Restart();
                result = SortByBinaryInsertion((int[])randomArray.Clone());
                stopwatch.Stop();
                Console.WriteLine(String.Format("    BinInsert Sort \t\t {0}", stopwatch.Elapsed.TotalSeconds));

                stopwatch.Restart();
                result = SortByBucket((int[])randomArray.Clone());
                stopwatch.Stop();
                Console.WriteLine(String.Format("    Bucket Sort \t\t {0}\n", stopwatch.Elapsed.TotalSeconds));
            }
            #endregion

            Console.WriteLine("\nComparison of sorts for Array and Linked List\n=======================================");

            foreach (int testSize in new int[] { 1000, 5000, 10000, 20000 })
            {
                Console.WriteLine(String.Format("  Array size: {0}", testSize));

                var randomArray2 = new int[testSize];
                var random2 = new Random();
                var simplyLinkedList = new SimplyLinkedList();
                var simplyLinkedList2 = new SimplyLinkedList();
                for (int i = 0; i < randomArray2.Length; i++)
                {
                    int randomNumber = random2.Next(0, 1000000);
                    randomArray2[i] = randomNumber;
                    simplyLinkedList.Push(random2.Next(randomNumber));
                    simplyLinkedList2.Push(random2.Next(randomNumber));
                }

                var stopwatch2 = new Stopwatch();

                stopwatch2.Start();
                int[] result2 = SortByInsertion((int[])randomArray2.Clone());
                stopwatch2.Stop();
                Console.WriteLine(String.Format("    Insertion Sort (Array) \t\t\t {0}", stopwatch2.Elapsed.TotalSeconds));

                stopwatch2.Restart();
                var result3 = SortByInsertionLinkedList(simplyLinkedList);
                stopwatch2.Stop();
                Console.WriteLine(String.Format("    Insertion Sort (List) \t\t\t {0}", stopwatch2.Elapsed.TotalSeconds));

                stopwatch2.Restart();
                result2 = SortByBucket((int[])randomArray2.Clone());
                stopwatch2.Stop();
                Console.WriteLine(String.Format("    Bucket Sort (Array) \t\t\t {0}", stopwatch2.Elapsed.TotalSeconds));

                stopwatch2.Restart();
                result3 = SortByBucketLinkedList(simplyLinkedList2);
                stopwatch2.Stop();
                Console.WriteLine(String.Format("    Bucket Sort (List) \t\t\t\t {0}\n", stopwatch2.Elapsed.TotalSeconds));
            }

            Console.ReadKey();
        }

        static int Factorial(int number)
        {
            if (number == 0)
                return 1;
            else if (number == 1)
                return number;
            return number * Factorial(number - 1);
        }

        static int[] SortByChoose(int[] array)
        {
            var sortArray = new int[array.Length];
            int iterator = array.Length;
            while (iterator > 0)
            {
                var max = int.MinValue;
                var maxIndex = int.MinValue;
                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i] > max)
                    {
                        max = array[i];
                        maxIndex = i;
                    }
                }
                sortArray[array.Length - iterator] = max;
                array[maxIndex] = -1;
                iterator--;
            }
            return sortArray;
        }

        static int[] SortByInsertion(int[] array)
        {
            for (int i = 1; i < array.Length; i++)
            {
                int hold = array[i];
                int position = 0;

                while (hold > array[position])
                    position += 1;

                if (position < i)
                {
                    for (int j = i; j > position; j--)
                        array[j] = array[j - 1];
                    array[position] = hold;
                }
            }
            return array;
        }

        static int[] SortByBinaryInsertion(int[] array)
        {
            for (int i = 1; i < array.Length; i++)
            {
                int hold = array[i];
                int position = Math.Abs(Array.BinarySearch(array, 0, i, hold) + 1);

                if (position < i)
                {
                    for (int j = i; j > position; j--)
                        array[j] = array[j - 1];
                    array[position] = hold;
                }
            }
            return array;
        }

        static int[] SortByBucket(int[] array)
        {
            int minValue = array[0];
            int maxValue = array[0];

            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] > maxValue)
                    maxValue = array[i];
                if (array[i] < minValue)
                    minValue = array[i];
            }

            var bucket = new List<int>[maxValue - minValue + 1];

            for (int i = 0; i < bucket.Length; i++)
                bucket[i] = new List<int>();

            for (int i = 0; i < array.Length; i++)
                bucket[array[i] - minValue].Add(array[i]);

            int k = 0;
            for (int i = 0; i < bucket.Length; i++)
            {
                if (bucket[i].Count > 0)
                {
                    for (int j = 0; j < bucket[i].Count; j++)
                    {
                        array[k] = bucket[i][j];
                        k++;
                    }
                }
            }

            return array;
        }

        static SimplyLinkedList SortByInsertionLinkedList(SimplyLinkedList linkedList)
        {
            Node iteratorNode = linkedList.Head.Next;
            int iteratorIndex = 1;
            var preIteratorNode = new Node();

            while (iteratorIndex < linkedList.Count)
            {
                Node holdNode = iteratorNode;
                Node positionNode = linkedList.Head;
                int positionIndex = 0;
                var prepositionNode = new Node();

                while (holdNode.Data > positionNode.Data)
                {
                    prepositionNode = positionNode;
                    positionNode = positionNode.Next;
                    positionIndex++;
                }

                if (positionIndex < iteratorIndex)
                {
                    if (positionIndex == 0 && iteratorIndex - positionIndex == 1)
                    {
                        positionNode.Next = holdNode.Next;
                        holdNode.Next = positionNode;
                        linkedList.Head = holdNode;
                        iteratorNode = positionNode;
                    }
                    else if (positionIndex == 0 && iteratorIndex - positionIndex > 1)
                    {
                        preIteratorNode.Next = holdNode.Next;
                        holdNode.Next = positionNode;
                        linkedList.Head = holdNode;
                        iteratorNode = preIteratorNode;
                    }
                    else if (positionIndex > 0 && iteratorIndex - positionIndex == 1)
                    {
                        positionNode.Next = holdNode.Next;
                        holdNode.Next = positionNode;
                        prepositionNode.Next = holdNode;
                        iteratorNode = positionNode;
                    }
                    else
                    {
                        preIteratorNode.Next = holdNode.Next;
                        prepositionNode.Next = holdNode;
                        holdNode.Next = positionNode;
                        iteratorNode = preIteratorNode;
                    }
                }

                preIteratorNode = iteratorNode;
                iteratorIndex++;
                iteratorNode = iteratorNode.Next;
            }
            return linkedList;
        }

        static SimplyLinkedList SortByBucketLinkedList(SimplyLinkedList linkedList)
        {
            long minValue = linkedList.Head.Data;
            long maxValue = linkedList.Head.Data;

            Node iteratorNode = linkedList.Head;
            for (int i = 1; i < linkedList.Count; i++)
            {
                iteratorNode = iteratorNode.Next;

                if (iteratorNode.Data > maxValue)
                    maxValue = iteratorNode.Data;
                if (iteratorNode.Data < minValue)
                    minValue = iteratorNode.Data;
            }

            var bucket = new List<long>[maxValue - minValue + 1];

            for (int i = 0; i < bucket.Length; i++)
                bucket[i] = new List<long>();

            iteratorNode = linkedList.Head;
            for (int i = 0; i < linkedList.Count; i++)
            {
                bucket[iteratorNode.Data - minValue].Add(iteratorNode.Data);
                iteratorNode = iteratorNode.Next;
            }

            Node currentNode = linkedList.Head;
            for (int i = 0; i < bucket.Length; i++)
            {
                if (bucket[i].Count > 0)
                {
                    for (int j = 0; j < bucket[i].Count; j++)
                    {
                        currentNode.Data = bucket[i][j];
                        currentNode = currentNode.Next;
                    }
                }
            }
            return linkedList;
        }
    }
}
