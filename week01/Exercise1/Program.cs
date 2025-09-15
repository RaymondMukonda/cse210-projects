using System;

class Program
{
    static void Main(string[] args)
    {
        // Console.Write("What is your first name ? ");
        // string first = Console.ReadLine();

        // Console.Write("What is your last name ? ");
        // string last = Console.ReadLine();

        // Console.WriteLine($"Your name is {last}..., {first} {last}.");












        Console.Write("What was your grade score: ");
        string gradeScore = Console.ReadLine();

        int score = int.Parse(gradeScore);
        string grade = "";

        if (score > 90)
        {
            grade = "A";
        }
        else if (score > 80)
        {
            grade = "B";
        }
        else if (score > 70)
        {
            grade = "C";
        }
        else if (score > 60)
        {
            grade = "D";
        }
        else
        {
            grade = "f";
        }

        if (score >= 60)
        {
            Console.WriteLine($"Your grade is: {grade}");
            Console.WriteLine("Well done you passed");
        }
        else
        {
            Console.WriteLine($"Your grade is: {grade}");
            Console.WriteLine("Better luck next time");
        }

    }
}

