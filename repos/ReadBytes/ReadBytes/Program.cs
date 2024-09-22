namespace ReadBytes
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var path = "C:\\Users\\Administrator\\source\\repos\\ReadBytes\\ReadBytes\\Program.cs";
            var bytes = System.IO.File.ReadAllBytes(path);

            var lines = 1;
            foreach (var byt in bytes)
            {
                if (byt >= 0x20 && byt <= 0x7F || byt == 0x0a)
                {
                    Console.Write((char)byt);
                }
                if (byt == 0x00)
                {
                    Console.WriteLine();
                }
                if (byt == 0x0a)
                {
                    lines++;
                }
            }
            Console.WriteLine($"\n{lines}");
        }
    }
}