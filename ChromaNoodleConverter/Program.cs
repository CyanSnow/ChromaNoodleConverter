using System;
using System.IO;

namespace ChromaNoodleConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            String[] inputData = args;
            new JSONParser(inputData);

            Console.WriteLine("Press ENTER to stop");
            do
            {
                while (!Console.KeyAvailable)
                {
                    // Do something
                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Enter);
        }

    }
}
