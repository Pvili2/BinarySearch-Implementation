using System;
namespace BinarySearch
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Numbers[] numbers = new Numbers[]
            {
                new Numbers(1),
                new Numbers(2),
                new Numbers(3),
                new Numbers(4)
            };

            //CompareTo return 0 if the two obj is equal, 1 if the first is greater, and -1 if the second is bigger
            Console.WriteLine(numbers[2].CompareTo(numbers[3]));

            Console.ReadLine();
        }
    }
}