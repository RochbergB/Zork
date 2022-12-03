namespace Zork.Common
{
    public class Enemy
    {
        public string Name { get; }

        public string Description { get; }

        public int Health { get; set; }

        public int Damage { get; }

        public Enemy(string name, string description, int health, int damage)
        {
            Name = name;
            Description = description;
            Health = health;
            Damage = damage;
        }

        public override string ToString() => Name;
    }
}
