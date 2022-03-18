using Microsoft.AspNetCore.Mvc;
using TokenConsumer.Dto;

namespace TokenConsumer.Controllers
{
    [ApiController]
    [Route("test")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public ActionResult<TestResponse> Get()
        {
            var result = new TestResponse
            {
                Name = HttpContext.User?.Identity?.Name
            };

            return Ok(result);
        }
    }
}