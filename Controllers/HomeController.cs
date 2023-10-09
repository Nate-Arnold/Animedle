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
            AniList aniList = new AniList(_configuration); //TODO Testing
            AniListResults topAnime = aniList.GetTopAnime(); //TODO Testing
            aniList.AniListClearDatabase(); //TODO Testing
            aniList.PopulateAnimedleDatabase(); //TODO Testing

            //Testing "GetBy" functions
            //AniListMedia media = aniList.AniListGetByRomaji("Koe No Katachi"); //TODO Testing
            //media = aniList.AniListGetByRomaji("Shingeki no Kyojin"); //TODO Testing 
            //media = aniList.AniListGetByRomaji("Kimetsu no Yaiba"); //TODO Testing 
            //aniList.AniListRemoveByID(21); //TODO Testing Should Remove One Piece from database

            return View(topAnime);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}