using Catalog.Models;
using Catalog.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly ICatalogRepository _catalogRepository;

        public CatalogController(ICatalogRepository catalogRepository)
        {
            _catalogRepository = catalogRepository;
        }

        /// <summary>
        /// добавить палочку
        /// </summary>
        /// <param name="wand"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> AddWand(Wand wand)
        {
            _catalogRepository.AddWand(wand);
            return Ok();
        }

        [HttpGet]
        public async Task<List<Wand>> GetWands()
        {
            var wands = _catalogRepository.GetWands();
            return wands;
        }
    }
}
