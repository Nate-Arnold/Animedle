using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using AnimedleWeb.Models;
using AnimedleWeb.API;

namespace AnimedleWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Test()
        {
            AniList aniList = new AniList();
            AniListResults topAnime = aniList.GetTopAnime();

            return View(topAnime);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}