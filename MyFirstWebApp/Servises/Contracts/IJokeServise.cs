namespace MyFirstWebApp.Servises.Contracts
{
    public interface IJokeServise
    {
        public Task<string> GetRandomJoke();

        public Task<string> GetJoke(string jokeId);
    }
}
