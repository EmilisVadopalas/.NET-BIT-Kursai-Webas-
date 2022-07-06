using MyFirstWebApp.Classes.Books;

namespace MyFirstWebApp.Models
{
    public class BookModel 
    {
        public List<Book> Books { get; set; }

        public string SearchTerm { get; set; }

        public BookModel(List<Book> books, string searchTerm)
        {
            Books = books;
            SearchTerm = searchTerm;
        }
    }

}
