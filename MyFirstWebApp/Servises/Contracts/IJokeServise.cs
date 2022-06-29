using MyFirstWebApp.Classes.Joke;

namespace MyFirstWebApp.Servises.Contracts
{
    public interface IJokeServise
    {
        public Task<DadJoke> GetRandomJoke();

        public Task<DadJoke> GetJokeById(string jokeId = "");

        public Task<DadJoke[]> GetNumberOfRandomJokes(int quantity);
    }
}
