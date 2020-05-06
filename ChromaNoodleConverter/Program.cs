using System;

namespace ChromaNoodleConverter
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            String[] inputData = args;
            Console.WriteLine("Hello! This shouldn't take too long. If you've waited longer than 30 seconds and nothing showed up,");
            Console.WriteLine("restart this or buy a better computer.");
            Console.WriteLine();
            Console.WriteLine();
            new JSONParser(inputData);
            Console.WriteLine("Press ENTER to close");
            Console.ReadKey();
        }
    }
}
