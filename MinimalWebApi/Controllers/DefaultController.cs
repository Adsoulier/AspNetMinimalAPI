using Microsoft.AspNetCore.Mvc;

namespace MinimalWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DefaultController : ControllerBase
    {
        [HttpGet(Name = "AutomaticGet")]
        public string GetBaseString()
        {
            return "Hello, World! This is the default controller response.";
        }
    }
}
