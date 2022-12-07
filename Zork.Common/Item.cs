namespace Zork.Common
{
    public class Item
    {
        public string Name { get; }

        public string LookDescription { get; }

        public string InventoryDescription { get; }

        public string EatDescription { get; }

        public int Damage { get; }

        public int Heal { get; }

        public int Points { get; }

        public Item(string name, string lookDescription, string inventoryDescription, string eatDescription, int damage, int heal, int points)
        {
            Name = name;
            LookDescription = lookDescription;
            InventoryDescription = inventoryDescription;
            EatDescription = eatDescription;
            Damage = damage;
            Heal = heal;
            Points = points;
        }
        public override string ToString() => Name;
    }
}