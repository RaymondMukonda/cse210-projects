using System;
using System.Drawing;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Console.Write("What is your first name ? ");
        // string first = Console.ReadLine();

        // Console.Write("What is your last name ? ");
        // string last = Console.ReadLine();

        // Console.WriteLine($"Your name is {last}..., {first} {last}.");


        // string userInput = "";

        // while (userInput != "end")
        // {
        //     Console.Write("Guess the magic number (1-10) or type 'end' to quit: ");
        //     userInput = Console.ReadLine().ToLower();

        //     if (userInput == "end")
        //     {
        //         Console.WriteLine("Thanks for playing!");
        //         break;
        //     }

        //     int input;
        //     bool isValid = int.TryParse(userInput, out input);

        //     if (!isValid || input < 1 || input > 10)
        //     {
        //         Console.WriteLine("Please enter a valid number between 1 and 10.");
        //         continue;
        //     }

        //     Random randomGenerator = new Random();
        //     int computerNumber = randomGenerator.Next(1, 11);

        //     if (input == computerNumber)
        //     {
        //         Console.WriteLine("You guessed it right!");
        //     }
        //     else if (input < computerNumber)
        //     {
        //         Console.WriteLine("Higher");
        //     }
        //     else
        //     {
        //         Console.WriteLine("Lower");
        //     }
        // }

        //    how to write lists 
        // List<string> words = new List<string>();

        // words.Add("phone");
        // words.Add("keyboard");
        // words.Add("mouse");

        // foreach (string word in words)
        // {
        //     Console.WriteLine(word);

        // }
        // Console.WriteLine(words.Count);

        // for (int i = 0; i < words.Count; i++)
        // {
        //     Console.WriteLine(words[i]);
        // }


    DisplayMessage();  

    string name = PromptUserName();            
    int userFav = PromptUserNumber();           
    int userSquared = PromptUserSquareNumber(userFav);  

    DisplayResult(name, userFav, userSquared);
    
    }

    static void DisplayMessage()
    {
        Console.WriteLine("Welcome to the program!");
    }

    static string PromptUserName()
    {
        Console.Write("Please enter your name: ");
        string name = Console.ReadLine();
        return name;
    }
    
    static int PromptUserNumber()
    {
        Console.Write("What is your fav number: ");
        int userFav = int.Parse(Console.ReadLine());
        return userFav;
    }

    static int PromptUserSquareNumber(int userInput)
    {
        int userSquared = userInput * userInput;
        return userSquared;
    }

    static void DisplayResult(string name, int userFav, int userSqaured)
    {
        Console.WriteLine($"hi {name} your favourite num is {userFav} and here is your squared number is {userSqaured}");
    }
    
    

}

