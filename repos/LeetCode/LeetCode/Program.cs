namespace LeetCode
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> list = new List<string>();
            var input = int.Parse(Console.ReadLine()!);
            for (var i = 0; i < input; i++)
            {
                list.Add(Console.ReadLine()!);
            }
            for (var l = 0; l < list.Count; l++)
            {
                if (list[l].Length > 10)
                    Console.WriteLine($"{list[l][0]}{list[l].Length - 2}{list[l][list[l].Length - 1]}");
                else
                {
                    Console.WriteLine(list[l]);
                }
            }
        }
    }
}
