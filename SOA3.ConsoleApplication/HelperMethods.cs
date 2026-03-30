namespace SOA3.ConsoleApplication
{
    public class HelperMethods
    {
        public static string RequireInput(string message)
        {
            Console.WriteLine($"[please enter] {message}");
            Console.Write("[input] ");
            return Console.ReadLine();
        }

        public static void RenderNewMenu(string? pageName = "PageNameHolder")
        {
            Console.Clear();
            Console.WriteLine($"================ Avans Devops Systeem ({pageName}) ================");
        }

        public static void RenderSeperator() => Console.WriteLine("==================================================================");

        public static void ExitProgram()
        {
            Console.WriteLine("Are you sure y/n");
            var input = Console.ReadLine();
            switch (input)
            {
                case "y" or "Y":
                    Console.WriteLine("Exiting");
                    Environment.Exit(0);
                    break;
                case "n" or "N":
                    Console.WriteLine("Not exiting");
                    break;
                default:
                    Console.WriteLine("incorrect input");
                    ExitProgram();
                    break;
            }
        }
    }
}
