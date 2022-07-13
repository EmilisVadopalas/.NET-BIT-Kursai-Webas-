using MyFirstWebApp.Classes.API;
using MyFirstWebApp.Database;
using MyFirstWebApp.Database.Entities;
using MyFirstWebApp.Servises.Contracts;

namespace MyFirstWebApp.Servises
{
    public class ApiProcessorsServise : IApiProcessorsServise
    {
        private readonly WebDatabaseContext _webDatabaseContext;
        private readonly ILoggerServise _logger;

        public ApiProcessorsServise(ILoggerServise logger, WebDatabaseContext webDatabaseContext)
        {
            _webDatabaseContext = webDatabaseContext;
            _logger = logger;
        }

        public async Task<Result> AddSeller(SellersDto seller)
        {
            try
            {
                var sellersEntity = new ApiSeller
                {
                    Name = seller.Name,
                    Surname = seller.Surname,
                    Phone = seller.Phone,
                    Address = seller.Address,
                    Email = seller.Email
                };

                _webDatabaseContext.apiSellers.Add(sellersEntity);
                await _webDatabaseContext.SaveChangesAsync();

                seller.Id = sellersEntity.Id;

                return seller.Id != 0 
                    ? new Result()
                    : new Result("Failed to Add To Database");
            }
            catch (Exception ex)
            {
               await _logger.LogError(ex.Message);
               await _logger.LogError(ex.StackTrace);

               return new Result(ex.Message);
            }
        }


    }
}
