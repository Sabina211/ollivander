using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API
{
    [Authorize]
    [ApiController]
    [Route("auth")]
    public class TestController : ControllerBase
    {
        /// <summary>
        /// проверить авторизацию (тестовый метод)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Все ок)");
        }
    }
}
