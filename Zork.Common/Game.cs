using System;
using System.Linq;
using Newtonsoft.Json;

namespace Zork.Common
{
    public class Game
    {
        public World World { get; }

        [JsonIgnore]
        public Player Player { get; }

        [JsonIgnore]
        public Enemy Enemy { get; }

        [JsonIgnore]
        public IInputService Input { get; private set; }

        [JsonIgnore]
        public IOutputService Output { get; private set; }

        [JsonIgnore]
        public bool IsRunning { get; private set; }

        public Game(World world, string startingLocation)
        {
            World = world;
            Player = new Player(World, startingLocation);
        }

        public void Run(IInputService input, IOutputService output)
        {
            Input = input ?? throw new ArgumentNullException(nameof(input));
            Output = output ?? throw new ArgumentNullException(nameof(output));

            IsRunning = true;
            Input.InputReceived += OnInputReceived;
            Player.Health = Player.maxHealth;
            Output.WriteLine("Welcome to Zork!");
            Look();
            Output.WriteLine($"\n{Player.CurrentRoom}");
        }

        public void OnInputReceived(object sender, string inputString)
        {
            char separator = ' ';
            string[] commandTokens = inputString.Split(separator);

            string verb;
            string subject = null;
            string with = null;
            string weapon = null;

            if (commandTokens.Length == 0)
            {
                return;
            }
            else if (commandTokens.Length == 1)
            {
                verb = commandTokens[0];
            }
            else if (commandTokens.Length == 2)
            {
                verb = commandTokens[0];
                subject = commandTokens[1];
            }
            else if (commandTokens.Length == 3)
            {
                Output.WriteLine("Invalid Command");
                return;
            }
            else
            {
                verb = commandTokens[0];
                subject = commandTokens[1];
                with = commandTokens[2];
                weapon = commandTokens[3];
            }

            Room previousRoom = Player.CurrentRoom;
            Commands command = ToCommand(verb);
            switch (command)
            {
                case Commands.Quit:
                    IsRunning = false;
                    Output.WriteLine("Thank you for playing!");
                    break;

                case Commands.Look:
                    Look();
                    Player.AddMoves();
                    break;

                case Commands.North:
                case Commands.South:
                case Commands.East:
                case Commands.West:
                    Directions direction = (Directions)command;
                    Output.WriteLine(Player.Move(direction) ? $"You moved {direction}." : "The way is shut!");
                    Player.AddMoves();
                    break;

                case Commands.Take:
                    if (string.IsNullOrEmpty(subject))
                    {
                        Output.WriteLine("This command requires a subject.");
                    }
                    else
                    {
                        Take(subject);
                        Player.AddMoves();
                    }
                    break;

                case Commands.Drop:
                    if (string.IsNullOrEmpty(subject))
                    {
                        Output.WriteLine("This command requires a subject.");
                    }
                    else
                    {
                        Drop(subject);
                        Player.AddMoves();
                    }
                    break;

                case Commands.Eat:
                    if (string.IsNullOrEmpty(subject))
                    {
                        Output.WriteLine("This command requires a subject.");
                    }
                    else
                    {
                        Eat(subject);
                        Player.AddMoves();
                    }
                    break;

                case Commands.Inventory:
                    if (Player.Inventory.Count() == 0)
                    {
                        Output.WriteLine("You are empty handed.");
                        Player.AddMoves();
                    }
                    else
                    {
                        Output.WriteLine("You are carrying:");
                        foreach (Item item in Player.Inventory)
                        {
                            Output.WriteLine(item.InventoryDescription);
                            Player.AddMoves();
                        }
                    }
                    break;

                case Commands.Reward:
                    Player.Score++;
                    Player.AddScore();
                    Output.WriteLine($"You now have " + Player.Score + " Points!");
                    break;

                case Commands.Attack:
                    if (Player.CurrentRoom.Enemies.Count() > 0)
                    {
                        if (string.IsNullOrEmpty(subject))
                        {
                            Output.WriteLine("What are you trying to attack?");
                            Player.AddMoves();
                            return;
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(weapon))
                            {
                                Output.WriteLine("What are you using to attack?");
                                Player.AddMoves();
                                return;
                            }
                            else
                            {
                                if (string.Compare(with, "With", ignoreCase: true) != 0)
                                {
                                    Output.WriteLine("Attacking how?");
                                    Player.AddMoves();
                                    return;
                                }
                                else
                                {
                                    Attack(weapon, subject);
                                    Player.AddMoves();
                                }
                            }
                        }
                    }
                    else
                    {
                        Output.WriteLine("There are no enemies here.");
                        Player.AddMoves();
                    }
                    break;

                default:
                    Output.WriteLine("Unknown command.");
                    break;
            }

            if (ReferenceEquals(previousRoom, Player.CurrentRoom) == false)
            {
                Look();
            }

            Output.WriteLine($"\n{Player.CurrentRoom}");
            Output.WriteLine($"You have moved " + Player.Moves + " times.");
        }
        
        private void Look()
        {
            Output.WriteLine(Player.CurrentRoom.Description);
            foreach (Item item in Player.CurrentRoom.Inventory)
            {
                Output.WriteLine(item.LookDescription);
            }
            foreach (Enemy enemy in Player.CurrentRoom.Enemies)
            {
                Output.WriteLine(enemy.Description);
            }
        }

        private void Take(string itemName)
        {
            Item itemToTake = Player.CurrentRoom.Inventory.FirstOrDefault(item => string.Compare(item.Name, itemName, ignoreCase: true) == 0);
            if (itemToTake == null)
            {
                Output.WriteLine("You can't see any such thing.");                
            }
            else
            {
                Player.AddItemToInventory(itemToTake);
                Player.CurrentRoom.RemoveItemFromInventory(itemToTake);
                Output.WriteLine("Taken.");
            }
        }

        private void Eat(string itemName)
        {
            Item itemToEat = Player.Inventory.FirstOrDefault(item => string.Compare(item.Name, itemName, ignoreCase: true) == 0);
            if (itemToEat == null)
            {
                Output.WriteLine("You can't see any such thing.");
            }
            else
            {
                if (itemToEat.Heal <= 0)
                {
                    Output.WriteLine("You can't eat this item.");
                }
                else
                {
                    if (Player.Health >= 15)
                    {
                        Output.WriteLine("You are full health.");
                    }
                    else
                    {
                        Player.Health += itemToEat.Heal;
                        Player.Score += itemToEat.Points;
                        if (Player.Health > Player.maxHealth)
                        {
                            Player.Health = Player.maxHealth;
                            Output.WriteLine($"{itemToEat.EatDescription} You are rewarded with {itemToEat.Points} Points! You now have {Player.Score} Points and the max {Player.Health} Health.");
                        }
                        else
                        {
                            Output.WriteLine($"{itemToEat.EatDescription} You are rewarded with {itemToEat.Points} Points! You now have {Player.Score} Points and {Player.Health} Health.");
                        }
                        Player.ChangeHealth();
                        Player.AddScore();
                        Player.RemoveItemFromInventory(itemToEat);
                    }
                }
            }
        }

        private void Attack(string itemName, string enemyName)
        {
            Item itemToAttack = Player.Inventory.FirstOrDefault(item => string.Compare(item.Name, itemName, ignoreCase: true) == 0);
            Enemy enemyToAttack = Player.CurrentRoom.Enemies.FirstOrDefault(enemy => string.Compare(enemy.Name, enemyName, ignoreCase: true) == 0);

            if (itemToAttack == null)
            {
                Output.WriteLine("You can't see any such thing.");
            }
            else
            {

                if (itemToAttack.Damage == 0)
                {
                    Output.WriteLine("This item can't damage enemies.");
                }
                else
                {

                    if (enemyToAttack == null)
                    {
                        
                        Output.WriteLine("There is no such enemy.");
                    }
                    else
                    {
                        enemyToAttack.Health -= itemToAttack.Damage;
                        Output.WriteLine($"You attacked the {enemyToAttack.Name} with a {itemToAttack.Name}.");

                        if (enemyToAttack.Health > 0)
                        {
                            Player.Health -= enemyToAttack.Damage;
                            Player.ChangeHealth();
                            Output.WriteLine($"{enemyToAttack.AttackDescription} You now have {Player.Health} Health.");

                            //if (enemyToAttack.EInventory.Count() > 0)
                            //{
                            //    foreach (Item item in enemyToAttack.EInventory)
                            //    {
                            //        Item enemyItemToDrop = enemyToAttack.EInventory.First();
                            //        enemyToAttack.RemoveEnemyItemFromInventory(enemyItemToDrop);
                            //        Player.CurrentRoom.AddItemToInventory(enemyItemToDrop);
                            //    }

                            //}
                            //Add enemy's dropped enemy to current room (Remove item from Enemy's inventory, Add it to current room's inventory"
                        }
                        else
                        {
                            Player.CurrentRoom.RemoveEnemyFromRoom(enemyToAttack);
                            Player.Score += enemyToAttack.Points;
                            Output.WriteLine($"{enemyToAttack.DefeatDescription} You earned {enemyToAttack.Points} Points! You now have {Player.Score} Points.");
                            Player.AddScore();
                        }
                    }
                }
            }
        }

        private void Drop(string itemName)
        {
            Item itemToDrop = Player.Inventory.FirstOrDefault(item => string.Compare(item.Name, itemName, ignoreCase: true) == 0);
            if (itemToDrop == null)
            {
                Output.WriteLine("You can't see any such thing.");                
            }
            else
            {
                Player.CurrentRoom.AddItemToInventory(itemToDrop);
                Player.RemoveItemFromInventory(itemToDrop);
                Output.WriteLine("Dropped.");
            }
        }

        

        private static Commands ToCommand(string commandString) => Enum.TryParse(commandString, true, out Commands result) ? result : Commands.Unknown;
    }
}