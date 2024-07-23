namespace EasyInventory.Models
{
    public class Inventory
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

    }
}
