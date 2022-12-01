﻿namespace Zork.Common
{
    public class Enemy
    {
        public string Name { get; }

        public string Description { get; }

        public Enemy(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public override string ToString() => Name;
    }
}
