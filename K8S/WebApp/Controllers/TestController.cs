using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class TestController : Controller
    {
        [HttpGet("test")]
        public async Task<IActionResult> Check()
        {
            return Ok("Test endpoint is working");
        }
    }
}
