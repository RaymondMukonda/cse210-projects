using System;
using System.Drawing;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

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


        // DisplayMessage();  

        // string name = PromptUserName();            
        // int userFav = PromptUserNumber();           
        // int userSquared = PromptUserSquareNumber(userFav);  

        // DisplayResult(name, userFav, userSquared);





        // Practise.DoSomething();

        // Person person = new Person();
        // person._givenName = "Raymond";
        // person._familyName = "Mukonda";

        // person.ShowEasternName();  
        // person.ShowWesternName();  

        // MathHelper.SayHi(); // No need to create a MathHelper object

        Account savings = new Account();

        savings.Deposit(100);
        savings.Deposit(50);
        savings.Deposit(25);
        savings.Deposit(255);
        savings.Deposit(28);

        Console.WriteLine(savings); // Shows current balance

        Console.WriteLine("Deposit History:");
        foreach (int deposit in savings.GetDepositHistory())
        {
            Console.WriteLine($"- {deposit}");
        } 

    }


    class Account
    {
        private int _balance = 50;
        private List<int> _depositHistory = new List<int>();

        public void Deposit(int amount)
        {
            _balance += amount;
            _depositHistory.Add(amount);
            Console.WriteLine($"Deposited: {amount}");
        }

        public override string ToString()
        {
            return $"Account Balance: {_balance}";
        }

        public List<int> GetDepositHistory()
        {
            return _depositHistory;
        }

    }

}

