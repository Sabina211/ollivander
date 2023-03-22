namespace Basket.Models
{
    public class CustomerBasket
    {
        public Guid? CustomerId { get; set; }
        public List<ProductQuantity> CustomerProducts { get; set; }
        public CustomerBasket(Guid id)
        {
            CustomerId = id;
        }
        public CustomerBasket()
        {
            //CustomerId = Guid.NewGuid();//нужно убрать после авторизации
        }
    }
}
