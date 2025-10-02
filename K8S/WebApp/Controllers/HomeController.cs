using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        

        public IActionResult Index(string apiResponse = null)
        {
            ViewBag.ApiResponse = apiResponse;
            return View();
        }

        [HttpPost]
        public IActionResult GoToWebApp()
        {
            return Redirect("https://admin-service.frontend.svc.cluster.local:30794/");
        }

        [HttpPost]
        public async Task<IActionResult> CallApi()
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetStringAsync("https://api-service.backend.svc.cluster.local:30723/api");

                return RedirectToAction("Index", new { apiResponse = response });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling API");
                return RedirectToAction("Index", new { apiResponse = "Error: " + ex.Message });
            }
        }
        [HttpPost]
        public async Task<IActionResult> CallApi2()
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetStringAsync("https://admin-service.frontend.svc.cluster.local:30794/api");

                return RedirectToAction("Index", new { apiResponse = response });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling API");
                return RedirectToAction("Index", new { apiResponse = "Error: " + ex.Message });
            }
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
