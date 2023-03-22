namespace Basket.Models
{
    public class BuyProductModel
    {
        public string City { get; set; }
        public string Street { get; set; }
        public string Building { get; set; }
        public string FlatNumber { get; set; }
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public DateTime CardExpiration { get; set; }
        public List<ProductQuantity> Products { get; set; }
    }
}
