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
        public Enemy(string name, string description, string attackDescription, string defeatDescription, int health, int damage, int points)
        {
            Name = name;
            Description = description;
            AttackDescription = attackDescription;
            DefeatDescription = defeatDescription;
            Health = health;
            Damage = damage;
            Points = points;
        }
        public override string ToString() => Name;

    }
}
