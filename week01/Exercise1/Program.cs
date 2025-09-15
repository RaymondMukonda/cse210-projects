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







    string userInput = "";

    while (userInput != "end")
{
    Console.Write("Guess the magic number (1-10) or type 'end' to quit: ");
    userInput = Console.ReadLine().ToLower();

    if (userInput == "end")
    {
        Console.WriteLine("Thanks for playing!");
        break;
    }

    int input;
    bool isValid = int.TryParse(userInput, out input);

    if (!isValid || input < 1 || input > 10)
    {
        Console.WriteLine("Please enter a valid number between 1 and 10.");
        continue;
    }

    Random randomGenerator = new Random();
    int computerNumber = randomGenerator.Next(1, 11);

    if (input == computerNumber)
    {
        Console.WriteLine("You guessed it right!");
    }
    else if (input < computerNumber)
    {
        Console.WriteLine("Higher");
    }
    else
    {
        Console.WriteLine("Lower");
    }
}



        




    }
}

