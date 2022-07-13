using MyFirstWebApp.Classes.API;

namespace MyFirstWebApp.Servises
{
    public interface IApiProcessorsServise
    {
        public Task<Result> AddSeller(SellersDto seller);
    }
}