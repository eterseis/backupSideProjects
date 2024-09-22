namespace CrackMe
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Guid guid = Guid.NewGuid();
            string password = guid.ToString();
            Console.Write("Enter password: ");
            var input = Console.ReadLine();

            if (input!.Equals(password))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nWelcome to our application");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nIncorrect password");
                Console.ResetColor();
            }
            Console.ReadKey(true);
        }
    }
}
