using Basket.Infrastructure.Repositories;
using Basket.Models;
using Common.Exceptions;

namespace Basket.Domain
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _repository;

        public BasketService(IBasketRepository repository)
        {
            _repository = repository;
        }

        public async Task<CustomerBasket> AddProductToBasketAsync( Guid customerId, ProductQuantity productQuantity)
        {
            var currentData = await _repository.GetBasketAsync(customerId);
            if (currentData == null) 
                return await _repository.UpdateBasketAsync(new CustomerBasket() { CustomerId = customerId, CustomerProducts = new List<ProductQuantity>() { new ProductQuantity { Quantity = productQuantity.Quantity, ProductId = productQuantity.ProductId } } });
            currentData.CustomerProducts.Add(productQuantity);
            return await _repository.UpdateBasketAsync(currentData);

        }

        public async Task<CustomerBasket> DeleteProductFromBasketAsync(Guid customerId, Guid productId)
        {
            var currentData = await _repository.GetBasketAsync(customerId);
            if (currentData == null) throw new NotFoundException("Нет товаров в корзине с таким Id");
            var product = currentData.CustomerProducts.FirstOrDefault(x => x.ProductId == productId);
            if (product == null) throw new NotFoundException("Нет такого товара в коризине");
            currentData.CustomerProducts.Remove(product);
            await _repository.UpdateBasketAsync(currentData);
            var result = await _repository.GetBasketAsync(customerId);
            return result;
        }

        public async Task<CustomerBasket> EditProductQuantityAsync(Guid customerId, ProductQuantity productQuantity)
        {
            var currentData = await _repository.GetBasketAsync(customerId);
            if (currentData == null) throw new NotFoundException("Нет товаров в корзине с таким Id");
            var product = currentData.CustomerProducts.FirstOrDefault(x => x.ProductId == productQuantity.ProductId);
            if (product == null) throw new NotFoundException("Нет такого товара в коризине");
            product.Quantity= productQuantity.Quantity;
            await _repository.UpdateBasketAsync(currentData);
            return await _repository.GetBasketAsync(customerId);
        }

        public async Task<CustomerBasket> BuyProductsAsync(Guid customerId, BuyProductModel data)
        {
            //нужно как-то эмулирвоать покупку, если ок, то удалять купленные товары из корзины
            var currentData = await _repository.GetBasketAsync(customerId);
            if (currentData == null) throw new NotFoundException("Нет товаров в корзине с таким Id");
            foreach (var item in data.Products)
            {
                var product = currentData.CustomerProducts.FirstOrDefault(x => x.ProductId == item.ProductId);
                if(product!=null) DeleteProductFromBasketAsync(customerId, product.ProductId);
            }
            await _repository.UpdateBasketAsync(currentData);
            return await _repository.GetBasketAsync(customerId);
        }
    }
}
