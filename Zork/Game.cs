using System;
using System.Linq;

namespace Zork
{
    public class Game
    {
        public World World { get; }

        public Player Player { get; }

        public Game(World world, string startingLocation)
        {
            World = world;
            Player = new Player(World, startingLocation);
        }

        private void Look()
        {
            Console.WriteLine(Player.CurrentRoom.Description);
            foreach (Item item in Player.CurrentRoom.Inventory)
            {
                Console.WriteLine(item.LookDescription);
            }
        }

        public void Run()
        {
            Room previousRoom = null;
            bool isRunning = true;

            

        while (isRunning)
            {
                Console.WriteLine($"{Player.CurrentRoom}");

                if (previousRoom != Player.CurrentRoom)
                {
                    Look();
                    previousRoom = Player.CurrentRoom;
                }

                Console.Write("\n> ");

                string inputString = Console.ReadLine().Trim();
                char seperator = ' ';
                string[] commandTokens = inputString.Split(seperator);

                string verb = null;
                string subject = null;

                if (commandTokens.Length == 0)
                {
                    continue;
                }
                else if (commandTokens.Length == 1)
                {
                    verb = commandTokens[0];
                }
                else
                {
                    verb = commandTokens[0];
                    subject = commandTokens[1];
                    
                }

                Commands command = ToCommand(verb);

                string outputString = null;
                switch (command)
                {
                    case Commands.Quit:
                        isRunning = false;
                        outputString = "Thank you for playing!";
                        break;

                    case Commands.Look:
                        Look();
                        break;

                    case Commands.North:
                    case Commands.South:
                    case Commands.East:
                    case Commands.West:
                        Directions direction = (Directions)command;
                        if (Player.Move(direction))
                        {
                            outputString = $"You moved {command}.";
                        }
                        else
                        {
                            outputString = "The way is shut!";
                        }
                        break;

                    case Commands.Take: //Take isn't complete
                        //foreach (Item item in Item)
                        //{
                        //    if (string.Compare(item.Name, subject, ignoreCase: true) == 0)
                        //    {
                        //        itemToTake = item;
                        //        break;
                        //    }
                        //}
                        if (subject == null)
                        {
                            outputString = "There is no such thing.";
                        }
                        else
                        {
                            outputString = "Taken";
                        }
                        break;


                    case Commands.Drop: //Drop isn't complete
                        if (subject == null)
                        {
                            outputString = "You don't have that item";
                        }
                        else
                        {
                            outputString = "Dropped";
                        }
                        break;

                    case Commands.Inventory:
                        if (Player.Inventory.Count == 0)
                        {
                            outputString = "You are empty-handed";
                        }
                        else
                        {
                            outputString = "You are carring\n ";
                            foreach (Item item in Player.Inventory)
                            {
                                outputString += $"{item.InventoryDescription}\n";
                            }
                        }
                        break;

                    default:
                        outputString = "Unknown command.";
                        break;
                }

                if (outputString != null)
                {
                    Console.WriteLine(outputString);
                }
                    
            }
        }
        private static Commands ToCommand(string commandString) => Enum.TryParse(commandString, true, out Commands result) ? result : Commands.Unknown;
    }
    
}


