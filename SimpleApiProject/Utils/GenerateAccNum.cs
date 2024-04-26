namespace SimpleApiProject.Utils
{
    public class GenerateAccNum
    {
        public static string Gen()
         {
            Random random = new Random();

        // Generate a 6-digit random number
            string randomNumber = (random.Next(100000, 999999)).ToString();
            return randomNumber;
         }
    }
}
