﻿using System;
using System.Collections.Generic;

namespace Zork
{
    internal class Program
    {      
        private static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Zork!");

            Game game = new Game();
            game.Run();
            
        }
    }
}
