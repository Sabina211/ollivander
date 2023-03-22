namespace Catalog.Models
{
    public class Wand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Quantity { get; set; }
        public string Wood { get; set; }
        public string Core { get; set; }
        public int Length { get; set; }

        public Wand(string name, int price, int quantity, string wood, string core, int length)
        {
            Name = name;
            Price = price;
            CreatedDate = DateTime.Now;
            Quantity = quantity;
            Wood = wood;
            Core = core;
            Length = length;
        }
    }
}
