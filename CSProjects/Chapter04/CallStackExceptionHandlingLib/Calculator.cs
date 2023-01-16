namespace CallStackExceptionHandlingLib
{
    public class Calculator
    {
        public static void Gamma()
        {
            Console.WriteLine("In Gamma");
            Delta();
        }

        private static void Delta()
        {
            Console.WriteLine("In Delta");
            File.OpenText("bad file path");
        }
    }

}
