namespace _09DemoSum
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;

    internal class Program
    {
        private static void Main(string[] args)
        {
            var arraySize = 50000000; // 50 000 000
            var array = BuildAnArray(arraySize);

            Task task1 = new Task(() =>
            {
                var stopwatch = Stopwatch.StartNew();
                var arrayProcessor = new ArrayProcessor(array, 0, arraySize);
                arrayProcessor.CalculateSum();

                stopwatch.Stop();

                var totalSum = arrayProcessor.Sum;
                Console.WriteLine($"Elapsed time: {stopwatch.Elapsed.TotalMilliseconds} ms");
                Console.WriteLine($"Sum: {totalSum}");
            });
            
            task1.Start();
            Console.ReadLine();
        }

        public static int[] BuildAnArray(int size)
        {
            var array = new int[size];

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = i;
            }

            return array;
        }
    }
}
