using MyFirstWebApp.Classes.Books;
using MyFirstWebApp.Servises.Contracts;
using Newtonsoft.Json;

namespace MyFirstWebApp.Servises
{
    public class BookServise : IBookServise
    {
        private string _authorsSearchBase = @"https://openlibrary.org/search/authors.json?q=";
        private string _authorsBooksBase = @"https://openlibrary.org/authors/";

        private readonly ILoggerServise _logger;

        public BookServise(ILoggerServise logger)
        {
            _logger = logger;
        }

        public async Task<List<Book>> SearchBooksByAuthor(string searchKeyWords)
        {
            var authorsKeys = await GetAuthorsKeys(searchKeyWords);
            var books = await GetBooks(authorsKeys);

            return books;
        }

        private async Task<List<Book>> GetBooks(Author[] authors)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            var books = new List<Book>();

            foreach (var author in authors)
            {
                try
                {
                    var result = await client.GetAsync($"{_authorsBooksBase}{author.key}/works.json");

                    result.EnsureSuccessStatusCode();

                    var bookPack = JsonConvert.DeserializeObject<OpenLibraryResponse>(await result.Content.ReadAsStringAsync()).entries.ToList();

                    foreach (var book in bookPack)
                    {
                        try
                        {
                            book.author = author;
                            book.CoverUrl = $"https://covers.openlibrary.org/b/id/{book.covers.FirstOrDefault()}-M.jpg";
                        }
                        catch{ }
                    }

                    books.AddRange(bookPack);

                }
                catch (Exception ex)
                {
                    await _logger.LogWarning($"{_authorsBooksBase}{author}/works.json");
                    await _logger.LogWarning($"error: {ex.Message} \nTrace: {ex.StackTrace}");
                }
            }

            return books;
        }

        private async Task<Author[]> GetAuthorsKeys(string searchKeyWords)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            try
            {
                var result = await client.GetAsync($"{_authorsSearchBase}{searchKeyWords}");

                result.EnsureSuccessStatusCode();

                return JsonConvert.DeserializeObject<OpenLibraryResponse>(await result.Content.ReadAsStringAsync()).docs.ToArray();
            }
            catch (Exception ex)
            {
                await _logger.LogWarning($"Cannot reach {_authorsSearchBase}{searchKeyWords}");
                await _logger.LogWarning($"error: {ex.Message} \nTrace: {ex.StackTrace}");

                return null;
            }
        }
    }
}
