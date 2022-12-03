namespace Zork.Common
{
    public class Item
    {
        public string Name { get; }

        public string LookDescription { get; }

        public string InventoryDescription { get; }

        public int Damage { get; }

        public Item(string name, string lookDescription, string inventoryDescription, int damage)
        {
            Name = name;
            LookDescription = lookDescription;
            InventoryDescription = inventoryDescription;
            Damage = damage;
        }

        public override string ToString() => Name;
    }
}