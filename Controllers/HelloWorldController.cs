using Microsoft.AspNetCore.Mvc;

namespace DatabaseConnectionProvider.Controllers
{
    [Route("api")]
    public class HelloWorldController : Controller
    {
        [HttpGet]
        [Route("HelloWorld")]
        public string GetHelloWorld()
        {
            return "Hello World";
        }
    }
}
