using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Zork.Common
{
    public class Enemy
    {
        public string Name { get; }

        public string Description { get; }

        public string AttackDescription { get; }

        public string DefeatDescription { get; }

        public int Health { get; set; }

        public int Damage { get; }

        public int Points { get; }

        //public IEnumerable<Item> EInventory => _eInventory;
        //public string[] EnemyInventoryNames { get; set; }

        public Enemy(string name, string description, string attackDescription, string defeatDescription, int health, int damage, int points)//, string[] enemyInventoryNames
        {
            Name = name;
            Description = description;
            AttackDescription = attackDescription;
            DefeatDescription = defeatDescription;
            Health = health;
            Damage = damage;
            Points = points;

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
