using System;

namespace Zork
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Zork!");        
            string inputString = Console.ReadLine().Trim().ToUpper();
            Commands command = ToCommand(inputString);

            if (command == Commands.Quit)
            {
                Console.WriteLine("Thank you for playing.");
            }
            else if  (command == Commands.Look)
            {
                Console.WriteLine("This is an open field west of a white house, with a boarded front door.\nA rubber mat saying 'Welcome to Zork!' lies by the door.");
            }
            else if (command == Commands.North)
            {
                Console.WriteLine("You went North.");
            }
            else if (command == Commands.South)
            {
                Console.WriteLine("You went South.");
            }
            else if (command == Commands.East)
            {
                Console.WriteLine("You went East.");
            }
            else if (command == Commands.West)
            {
                Console.WriteLine("You went West.");
            }
            else
            {
                Console.WriteLine($"Unrecognized command: {inputString}");
            }
        }

        static Commands ToCommand(string commandString)
        {
            return Enum.TryParse<Commands>(commandString, true, out Commands command) ? command : Commands.Unknown;
           
        }
    }
}
