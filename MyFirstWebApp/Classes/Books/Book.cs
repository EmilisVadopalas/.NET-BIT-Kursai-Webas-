namespace MyFirstWebApp.Classes.Books
{
    public class Book
    {
        public string title { get; set; }
        public int[] covers { get; set; }
        public string key { get; set; }
        public Excerpt[] excerpts { get; set; }

        public Author author { get; set; }
        public string CoverUrl { get; set; }
    }


}
