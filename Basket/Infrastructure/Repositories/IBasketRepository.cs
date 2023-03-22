using Basket.Models;

namespace Basket.Infrastructure.Repositories
{
    public interface IBasketRepository
    {
        Task<CustomerBasket> GetBasketAsync(Guid customerId);
        Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket);
        Task<bool> DeleteBasketAsync(Guid id);
    }
}
