// See https://aka.ms/new-console-template for more information
using System.Transactions;
namespace Stage0
{
     partial class Program
    {
        static void Main(string[] args)
        {
            Welcome0468();
            Welcome0838();
            Console.ReadKey();
        }
        static partial void Welcome0468();
        private static void Welcome0838()
        {
            string name;
            Console.WriteLine("Enter your name:");
            name = Console.ReadLine();
            Console.WriteLine(name + " welcome to my first console application");
        }
    }
}
