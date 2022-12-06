namespace Zork.Common
{
    public class Item
    {
        public string Name { get; }

        public string LookDescription { get; }

        public string InventoryDescription { get; }

        public int Damage { get; }

        public int Heal { get; }

        public Item(string name, string lookDescription, string inventoryDescription, int damage, int heal)
        {
            Name = name;
            LookDescription = lookDescription;
            InventoryDescription = inventoryDescription;
            Damage = damage;
            Heal = heal;
        }

        public override string ToString() => Name;
    }
}