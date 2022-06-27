using Microsoft.AspNetCore.Mvc;
using MyFirstWebApp.Servises.Contracts;

namespace MyFirstWebApp.Controllers
{
    public class PaskaitosController : Controller
    {        
        private readonly IJokeServise _jokeServise;

        public PaskaitosController(IJokeServise jokeServise)
        {
            _jokeServise = jokeServise;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Jokes()
        {
            ViewBag.Joke = await _jokeServise.GetRandomJoke();
            return View();
        }

        public IActionResult Processors()
        {
            return View();
        }

        public IActionResult Books()
        {
            return View();
        }
    }
}
