using AdminPanel.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http;

namespace AdminPanel.Controllers
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
        [HttpPost]
        public async Task<IActionResult> CallApi()
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetStringAsync("https://webapplication.dev:7273/test");

                return RedirectToAction("Index", new { apiResponse = response });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling API");
                return RedirectToAction("Index", new { apiResponse = "Error: " + ex.Message });
            }
        }
        public IActionResult Index(string apiResponse = null)
        {
            ViewBag.ApiResponse = apiResponse;
            return View();
        }
        [HttpPost]
        public IActionResult GoToWebApp()
        {
            return Redirect("https://webapplication.dev:7273/");
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
