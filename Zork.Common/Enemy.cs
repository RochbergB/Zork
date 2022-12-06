using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Zork.Common
{
    public class Enemy
    {
        public string Name { get; }

        public string Description { get; }

        public int Health { get; set; }

        public int Damage { get; }

        //public IEnumerable<Item> EInventory => _eInventory;
        //public string[] EnemyInventoryNames { get; set; }

        public Enemy(string name, string description, int health, int damage)//, string[] enemyInventoryNames
        {
            Name = name;
            Description = description;
            Health = health;
            Damage = damage;

            //EnemyInventoryNames = enemyInventoryNames ?? new string[0];
            //_eInventory = new List<Item>();
        }

        //public void RemoveEnemyItemFromInventory(Item enemyItemToRemove)
        //{
        //    if (_eInventory.Remove(enemyItemToRemove) == false)
        //    {
        //        throw new Exception("Could not remove item from inventory.");
        //    }
        //}

        public override string ToString() => Name;
        //private readonly List<Item> _eInventory;

    }
}
