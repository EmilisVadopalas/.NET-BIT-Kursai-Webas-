using MyFirstWebApp.Database.Entities;

namespace MyFirstWebApp.Classes.Processors
{
    public class ProcessorListing
    {
        public string Name { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public string ProductLink { get; set; }

        public ProcessorListing(string name, string pictureUrl, string price, string productLink)
        {
            Name = name;
            PictureUrl = pictureUrl;
            ProductLink = productLink;

            if (decimal.TryParse(price.Replace("€", "").Trim(), out decimal priceDecimal))
            {
                Price = priceDecimal / 100;
            }
            else
            {
                Price = -1;
            }
        }

        public ProcessorListing(Processor proc)
        {
            Name = proc.Name;
            PictureUrl = proc.PictureUrl;
            ProductLink = proc.ProductLink;
            Price = proc.Price;
        }
    }
}
