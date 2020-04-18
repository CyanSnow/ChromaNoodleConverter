using System;

namespace ChromaNoodleConverter
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            String[] inputData = args;
            new JSONParser(inputData);
            Console.WriteLine("Press ENTER to close");
            Console.ReadKey();
        }
    }
}