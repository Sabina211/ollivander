using Basket.Models;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Domain
{
    public interface IBasketService
    {
        public Task<CustomerBasket> AddProductToBasketAsync(Guid customerId, ProductQuantity productQuantity);
        public Task<CustomerBasket> DeleteProductFromBasketAsync(Guid customerId, Guid productId);
        public Task<CustomerBasket> EditProductQuantityAsync(Guid customerId, ProductQuantity productQuantity);
        public Task<CustomerBasket> BuyProductsAsync(Guid customerId, BuyProductModel data);
    }
}
