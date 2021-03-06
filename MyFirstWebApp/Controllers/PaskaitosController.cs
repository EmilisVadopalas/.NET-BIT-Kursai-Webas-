using Microsoft.AspNetCore.Mvc;
using MyFirstWebApp.Classes.Processors;
using MyFirstWebApp.Models;
using MyFirstWebApp.Servises;
using MyFirstWebApp.Servises.Contracts;

namespace MyFirstWebApp.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class PaskaitosController : Controller
    {
        private readonly IJokeServise _jokeServise;
        private readonly ITopoProccessorsServise _topoProccessorsServise;
        private readonly IBookServise _bookServise;
        private readonly ILoggerServise _loggerServise;

        public PaskaitosController(
            IJokeServise jokeServise,
            ITopoProccessorsServise topoProccessorsServise,
            IBookServise bookServise,
            ILoggerServise loggerServise)
        {
            _jokeServise = jokeServise;
            _topoProccessorsServise = topoProccessorsServise;
            _bookServise = bookServise;
            _loggerServise = loggerServise;
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
            //await _loggerServise.LogError("testas /Paskaitos/Joikoes");

            return View(new JokeModel(await _jokeServise.GetRandomJoke()));
        }

        [Route("/Paskaitos/Processors")]
        public async Task<IActionResult> Processors()
        {
            //await _loggerServise.LogError("testas /Paskaitos/Processors");

            List<ProcessorListing> processors;

            try
            {
                processors = await _topoProccessorsServise.ScrapeTopoProcesorsFirstPage();
                //throw new NotImplementedException();
            }
            catch (Exception ex)
            {
               // await _loggerServise.LogError(ex.Message);
               // await _loggerServise.LogError(ex.Source);
               // await _loggerServise.LogError(ex.StackTrace);

                return View("Views/Shared/Error.cshtml", new ErrorViewModel(ex.Message, ex.StackTrace));
            }
            
            return View(new ProccessorsModel(processors));
        }

        [Route("/Paskaitos/ProcessorsDb")]
        public IActionResult Processors(object _)
        {
            var porcessors = _topoProccessorsServise.GetProcessorsFromDB();
            return View(new ProccessorsModel(porcessors));
        }

        [Route("/Paskaitos/Processors/Page/{page}")]
        public async Task<IActionResult> Processors(int page)
        {
            return View(new ProccessorsModel(await _topoProccessorsServise.ScrapeTopoProcesorsPage(page)));
        }

        [Route("/Paskaitos/Books/{author}")]
        public async Task<IActionResult> Books(string author)
        {
            return View(new BookModel(await _bookServise.SearchBooksByAuthor(author), author));
        }
    }
}

