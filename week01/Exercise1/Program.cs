using System;
using System.Drawing;

class Program
{
    static void Main(string[] args)
    {
        // Console.Write("What is your first name ? ");
        // string first = Console.ReadLine();

        // Console.Write("What is your last name ? ");
        // string last = Console.ReadLine();

        // Console.WriteLine($"Your name is {last}..., {first} {last}.");







        Console.Write("Would you like to keep playing? ");
        string response = Console.ReadLine();

        while (response == "yes")
        {
            Console.Write("What is the magic number(1-10): ");
            string userInput = Console.ReadLine();
            int input = int.Parse(userInput);

            Random randomGenerator = new Random();
            int computerNumber = randomGenerator.Next(1, 11);

            if (input == computerNumber)
            {
                Console.WriteLine("You guessed it right!");
            }
            else if (input < computerNumber)
            {
                Console.WriteLine("Lower");
            }
            else
            {
                Console.WriteLine("Higer");
            }
        }



        




    }
}

