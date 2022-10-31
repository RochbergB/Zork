namespace Zork
{
    public class Item
    {
        public string Name { get; }

        public string Description { get; }

        public string InventoryDescription { get; }

        public Item(string name, string description, string inventoryDescription)
        {
            Name = name;
            Description = description;
            InventoryDescription = inventoryDescription;
        }

        //public string override
    }
}
