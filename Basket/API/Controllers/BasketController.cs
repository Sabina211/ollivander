using Basket.Domain;
using Basket.Infrastructure.Repositories;
using Basket.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;
        private readonly IBasketService _service;

        public BasketController(IBasketRepository repository, IBasketService service)
        {
            _repository = repository;
            _service = service;
        }

        /// <summary>
        /// получить содержимое корзины по id клиента
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(CustomerBasket), (int) HttpStatusCode.OK)]
        public async Task<ActionResult<CustomerBasket>> GetBasketByIdAsync(Guid id)
        {
            var basket = await _repository.GetBasketAsync(id);
            return Ok(basket ?? new CustomerBasket(id));
        }

        /// <summary>
        /// обновить всю корзину
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(CustomerBasket), (int) HttpStatusCode.OK)]
        public async Task<ActionResult<CustomerBasket>> UpdateBasketAsync([FromBody] CustomerBasket value)
        {
            return Ok(await _repository.UpdateBasketAsync(value));
        }

        /// <summary>
        /// удалить корзину клиента
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(typeof(CustomerBasket), (int) HttpStatusCode.OK)]
        public async Task<ActionResult<CustomerBasket>> DeleteBasketAsync(Guid id)
        {
            return Ok(await _repository.DeleteBasketAsync(id));
        }

        /// <summary>
        /// добавить товар в корзину
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("product")]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        public async Task<ActionResult> AddProductToBasketAsync([FromQuery] Guid customerId, [FromBody] ProductQuantity product)
        {
            return Ok(await _service.AddProductToBasketAsync(customerId, product));
        }


        /// <summary>
        /// удалить товар из корзины
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("product/{id:guid}")]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteProductFromBasketAsync([FromQuery] Guid customerId, Guid id)
        {
            return Ok(await _service.DeleteProductFromBasketAsync(customerId, id));
        }

        /// <summary>
        /// изменить количество товара
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="productQuantity"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("product/{customerId:guid}")]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        public async Task<ActionResult> EditProductQuantityAsync(Guid customerId, [FromBody] ProductQuantity productQuantity)
        {
            return Ok(await _service.EditProductQuantityAsync(customerId, productQuantity));
        }

        /// <summary>
        /// купить товары
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("{customerId:guid}/buy")]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        public async Task<ActionResult> BuyProductsAsync(Guid customerId, [FromBody] BuyProductModel data)
        {
            return Ok(await _service.BuyProductsAsync(customerId, data));
        }
    }
}
