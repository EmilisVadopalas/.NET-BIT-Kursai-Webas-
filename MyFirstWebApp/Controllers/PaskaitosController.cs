using Microsoft.AspNetCore.Mvc;
using MyFirstWebApp.Models;
using MyFirstWebApp.Servises.Contracts;

namespace MyFirstWebApp.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
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

        [Route("/Paskaitos/Jokes/Id/{id}")]
        public async Task<IActionResult> Jokes(string id)
        {
            var model = new JokeModel(await _jokeServise.GetJokeById(id));
            model.Description = $"This is where we will show jokes by id of {id} from API";

            return View(model);
        }


        [Route("/Paskaitos/Jokes/Quantity/{quantity}")]
        public async Task<IActionResult> Jokes(int quantity)
        {
            var model = new JokeModel(await _jokeServise.GetNumberOfRandomJokes(quantity));
            model.Quantity = quantity;
            model.Description = $"This is where we will show {quantity} random jokes from API";

            return View(model);
        }

        public async Task<IActionResult> Jokes()
        {
            return View(new JokeModel(await _jokeServise.GetRandomJoke()));
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

