using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using AnimedleWeb.Models;
using AnimedleWeb.API;

namespace AnimedleWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Test()
        {
            AniList aniList = new AniList(_configuration);
            AniListResults topAnime = aniList.GetTopAnime();
            aniList.PopulateAnimedleDatabase();

            return View(topAnime);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}