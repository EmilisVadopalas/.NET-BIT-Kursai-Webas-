
using MyFirstWebApp.Classes.Books;

namespace MyFirstWebApp.Servises
{
    public interface IBookServise
    {
        public Task<List<Book>> SearchBooksByAuthor(string searchKeyWords);
    }
}