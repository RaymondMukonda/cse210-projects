using System;
using System.Collections.Generic;


class Practise
{
    public static void DoSomething()
    {
        Console.WriteLine("hello from practise");
    }
}

    public class Person
    {
        // The C# convention is to start member variables with an underscore _
        public string _givenName = "";
        public string _familyName = "";

        // A special method, called a constructor that is invoked using the  
        // new keyword followed by the class name and parentheses.
        public Person()
        {
        }

        // A method that displays the person's full name as used in eastern 
        // countries or <family name, given name>.
        public void ShowEasternName()
        {
            Console.WriteLine($"{_familyName}, {_givenName}");
        }

        // A method that displays the person's full name as used in western 
        // countries or <given name family name>.
        public void ShowWesternName()
        {
            Console.WriteLine($"{_givenName} {_familyName}");
        }
    }


// this code below helps you call the function without needing to create a new object bec it has static 
//  if it didnt have staic youd have to call it like this
// (inside the main) MathHelper helper = new MathHelper();  
// helper.SayHi();
class MathHelper
{
    public static void SayHi()
    {
        Console.WriteLine("Hi from MathHelper!");
    }
}