using Microsoft.AspNetCore.Mvc;
using MyFirstWebApp.Models;
using MyFirstWebApp.Servises.Contracts;

namespace MyFirstWebApp.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class PaskaitosController : Controller
    {
        private readonly IJokeServise _jokeServise;
        private readonly ITopoProccessorsServise _topoProccessorsServise;

        public PaskaitosController(
            IJokeServise jokeServise,
            ITopoProccessorsServise topoProccessorsServise)
        {
            _jokeServise = jokeServise;
            _topoProccessorsServise = topoProccessorsServise;
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

        [Route("/Paskaitos/Processors")]
        public async Task<IActionResult> Processors()
        {
            var porcessors = await _topoProccessorsServise.ScrapeTopoProcesorsFirstPage();
            return View(new ProccessorsModel(porcessors));
        }

        [Route("/Paskaitos/ProcessorsDb")]
        public IActionResult Processors(object ok)
        {
            var porcessors = _topoProccessorsServise.GetProcessorsFromDB();
            return View(new ProccessorsModel(porcessors));
        }

        [Route("/Paskaitos/Processors/Page/{page}")]
        public async Task<IActionResult> Processors(int page)
        {
            return View(new ProccessorsModel(await _topoProccessorsServise.ScrapeTopoProcesorsPage(page)));
        }

        public IActionResult Books()
        {
            return View();
        }
    }
}

