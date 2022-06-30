using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFirstWebApp.Servises.Contracts;
using System.Text.Json;

namespace MyFirstWebApp.Controllers
{
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly IJokeServise _jokeServise;

        public ApiController(IJokeServise jokeServise)
        {
            _jokeServise = jokeServise;
        }

        [HttpGet("API/Jokes/Id/{id}")]
        public async Task<string> Jokes(string id)
        {
            return JsonSerializer.Serialize(await _jokeServise.GetJokeById(id));
        }

        [HttpGet("API/Jokes/Quantity/{quantity}")]
        public async Task<string> Jokes(int quantity)
        {
            return JsonSerializer.Serialize(await _jokeServise.GetNumberOfRandomJokes(quantity));
        }

        [HttpGet("API/Jokes")]
        public async Task<string> Jokes()
        {
            return JsonSerializer.Serialize(await _jokeServise.GetRandomJoke());
        }        
    }
}
