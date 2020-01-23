using System;
using System.Diagnostics;

namespace RodCutting
{
    class Program
    {
        static void Main(string[] args)
        {
            // n=1..30 used to verify correctness
            //int[] prices1 = new int[] { 1 };
            //int[] prices2 = new int[] { 1, 5 };
            //int[] prices3 = new int[] { 1, 5, 8 };
            //int[] prices4 = new int[] { 1, 5, 8, 9 };
            //int[] prices5 = new int[] { 1, 5, 8, 9, 10 };
            //int[] prices6 = new int[] { 1, 5, 8, 9, 10, 17 };
            //int[] prices7 = new int[] { 1, 5, 8, 9, 10, 17, 17 };
            //int[] prices8 = new int[] { 1, 5, 8, 9, 10, 17, 17, 20 };
            //int[] prices9 = new int[] { 1, 5, 8, 9, 10, 17, 17, 20, 24 };
            //int[] prices10 = new int[] { 1, 5, 8, 9, 10, 17, 17, 20, 24, 30 };
            //int[] prices15 = new int[] { 1, 5, 8, 9, 10, 17, 17, 20, 24, 30, 38, 42, 50, 51, 52 };
            //int[] prices20 = new int[] { 1, 5, 8, 9, 10, 17, 17, 20, 24, 30, 38, 42, 50, 51, 52, 55, 57, 60, 65, 70 };
            //int[] prices25 = new int[] { 1, 5, 8, 9, 10, 17, 17, 20, 24, 30, 38, 42, 50, 51, 52, 55, 57, 60, 65, 70, 71, 74, 83, 85, 90 };
            //int[] prices26 = new int[] { 1, 5, 8, 9, 10, 17, 17, 20, 24, 30, 38, 42, 50, 51, 52, 55, 57, 60, 65, 70, 71, 74, 83, 85, 90, 91 };
            //int[] prices27 = new int[] { 1, 5, 8, 9, 10, 17, 17, 20, 24, 30, 38, 42, 50, 51, 52, 55, 57, 60, 65, 70, 71, 74, 83, 85, 90, 91, 92 };
            //int[] prices28 = new int[] { 1, 5, 8, 9, 10, 17, 17, 20, 24, 30, 38, 42, 50, 51, 52, 55, 57, 60, 65, 70, 71, 74, 83, 85, 90, 91, 92, 100 };
            //int[] prices29 = new int[] { 1, 5, 8, 9, 10, 17, 17, 20, 24, 30, 38, 42, 50, 51, 52, 55, 57, 60, 65, 70, 71, 74, 83, 85, 90, 91, 92, 100, 103 };
            //int[] prices30 = new int[] { 1, 5, 8, 9, 10, 17, 17, 20, 24, 30, 38, 42, 50, 51, 52, 55, 57, 60, 65, 70, 71, 74, 83, 85, 90, 91, 92, 100, 103, 105 };


            Console.WriteLine("Algorithm 1, CutRod");
            Console.WriteLine("-------------------------------------------------------------------------------");
            RunTest("CutRod", CreateSampleData(10), 10);
            RunTest("CutRod", CreateSampleData(15), 15);
            RunTest("CutRod", CreateSampleData(20), 20);
            RunTest("CutRod", CreateSampleData(25), 25);
            RunTest("CutRod", CreateSampleData(26), 26);
            RunTest("CutRod", CreateSampleData(27), 27);
            RunTest("CutRod", CreateSampleData(28), 28);
            RunTest("CutRod", CreateSampleData(29), 29);
            RunTest("CutRod", CreateSampleData(30), 30);

            Console.WriteLine("Algorithm 2, BottomUpCutRod");
            Console.WriteLine("-------------------------------------------------------------------------------");

            RunTest("BottomUpCutRod", CreateSampleData(1000), 1000);
            RunTest("BottomUpCutRod", CreateSampleData(2000), 2000);
            RunTest("BottomUpCutRod", CreateSampleData(3000), 3000);
            RunTest("BottomUpCutRod", CreateSampleData(4000), 4000);
            RunTest("BottomUpCutRod", CreateSampleData(5000), 5000);
            RunTest("BottomUpCutRod", CreateSampleData(10000), 10000);
            RunTest("BottomUpCutRod", CreateSampleData(15000), 15000);
            RunTest("BottomUpCutRod", CreateSampleData(20000), 20000);
            RunTest("BottomUpCutRod", CreateSampleData(25000), 25000);
            RunTest("BottomUpCutRod", CreateSampleData(30000), 30000);
            RunTest("BottomUpCutRod", CreateSampleData(35000), 35000);
            RunTest("BottomUpCutRod", CreateSampleData(40000), 40000);
            RunTest("BottomUpCutRod", CreateSampleData(45000), 45000);
            RunTest("BottomUpCutRod", CreateSampleData(50000), 50000);

            Console.WriteLine("Algorithm 3, MemoizedCutRod");
            Console.WriteLine("-------------------------------------------------------------------------------");

            RunTest("MemoizedCutRod", CreateSampleData(1000), 1000);
            RunTest("MemoizedCutRod", CreateSampleData(2000), 2000);
            RunTest("MemoizedCutRod", CreateSampleData(3000), 3000);
            RunTest("MemoizedCutRod", CreateSampleData(4000), 4000);
            RunTest("MemoizedCutRod", CreateSampleData(5000), 5000);

            Console.WriteLine();
            Console.WriteLine("Finished");
            Console.ReadLine();
        }

        private static int[] CreateSampleData(int arraySize)
        {
            int[] prices = new int[arraySize];

            Random rnd = new Random();

            int lastPrice = 0;

            for (int i = 0; i < arraySize; i++)
            {
                int r = rnd.Next(1, 30);

                prices[i] = lastPrice + r;
                lastPrice = prices[i];
            }

            return prices;
        }

        private static void RunTest(string algorithm, int[] prices, int n)
        {
            int maxRevenue = 0;
            double elapsedSeconds = 0;
            double elapsedSecondsAverage = 0;

            Stopwatch stopwatch = new Stopwatch();

            Console.WriteLine("n = " + n);

            for (int test = 1; test <= 5; test++)
            {
                stopwatch.Reset();
                stopwatch.Start();

                switch (algorithm)
                {
                    case "CutRod":
                        maxRevenue = CutRod(prices, n);
                        break;
                    case "BottomUpCutRod":
                        maxRevenue = BottomUpCutRod(prices, n);
                        break;
                    case "MemoizedCutRod":
                        maxRevenue = MemoizedCutRod(prices, n);
                        break;
                }


                stopwatch.Stop();

                elapsedSeconds = stopwatch.Elapsed.TotalSeconds;

                if (test >= 2)
                {
                    elapsedSecondsAverage += elapsedSeconds;
                }

                // Use this to print each individual test result
                //Console.WriteLine("Test Number: " + test + " Max Revenue: " + maxRevenue + " Elapsed Seconds: " + elapsedSeconds);
            }

            // Use this to printthe average of the last 4 test results
            Console.WriteLine("Average Elapsed Seconds of Tests 2 to 5: " + elapsedSecondsAverage / 4);

            Console.WriteLine();
        }

        /// <summary>
        /// Cut Rod -- Recursive Top-Down Implementation
        /// C# uses 0 based arrays so I had to adjust the algorithm to 
        /// take this into account.
        /// </summary>
        /// <param name="prices">An array of prices</param>
        /// <param name="n">Length of the Rod</param>
        /// <returns>Maximum Revenue Possible. 0 if no revenue is possible.</returns>
        public static int CutRod(int[] prices, int n)
        {
            if (n == 0)
            {
                return 0;
            }

            int q = int.MinValue;

            for (int i = 1; i <= n; i++)
            {
                q = Math.Max(q, prices[i - 1] + CutRod(prices, n - i));
            }

            return q;
        }


        /// <summary>
        /// Memoized Cut Rod -- Top-Down Memoized Implementation
        /// C# uses 0 based arrays so I had to adjust the algorithm to 
        /// take this into account.
        /// </summary>
        /// <param name="prices">An array of prices</param>
        /// <param name="n">Length of the Rod</param>
        /// <returns>Maximum Revenue Possible. 0 if no revenue is possible.</returns>
        public static int MemoizedCutRod(int[] prices, int n)
        {
            int[] revenues = new int[n];

            for (int i = 1; i <= n; i++)
            {
                revenues[i - 1] = int.MinValue;
            }

            return MemoizedCutRodAux(prices, n, revenues);
        }

        public static int MemoizedCutRodAux(int[] prices, int n, int[] revenues)
        {
            if (n == 0)
            {
                return 0;
            }

            if (revenues[n - 1] >= 0)
            {
                return revenues[n - 1];
            }

            int q = int.MinValue;

            for (int i = 1; i <= n; i++)
            {
                q = Math.Max(q, prices[i - 1] + MemoizedCutRodAux(prices, n - i, revenues));
            }

            revenues[n - 1] = q;

            return q;
        }


        /// <summary>
        /// Bottom Up Cut Rod -- 
        /// C# uses 0 based arrays so I had to adjust the algorithm to 
        /// take this into account.
        /// </summary>
        /// <param name="prices">An array of prices</param>
        /// <param name="n">Length of the Rod</param>
        /// <returns>Maximum Revenue Possible. 0 if no revenue is possible.</returns>
        static int BottomUpCutRod(int[] prices, int n)
        {
            int[] revenues = new int[n + 1];
            revenues[0] = 0;

            for (int i = 1; i <= n; i++)
            {
                int q = int.MinValue;

                for (int j = 0; j < i; j++)
                {
                    q = Math.Max(q, prices[j] + revenues[i - j - 1]);
                }

                revenues[i] = q;
            }

            return revenues[n];
        }
    }
}