using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoginAPIwithEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        [HttpGet]
        public string Greeting()
        {
            return "Helloo";
        }
    }
}
