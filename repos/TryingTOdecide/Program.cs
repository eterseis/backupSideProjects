using System.ComponentModel.Design.Serialization;

namespace TryingTOdecide
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] arr = { 16, 45, 192, 236, 246 };
            var rnd = new Random();

            var x = arr[rnd.Next(arr.Length - 1)];
            Console.WriteLine(x);
        }
    }
}
