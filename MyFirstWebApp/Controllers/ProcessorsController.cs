using Microsoft.AspNetCore.Mvc;
using MyFirstWebApp.Classes.API;
using MyFirstWebApp.Servises;

namespace MyFirstWebApp.Controllers
{
    [ApiController]
    public class ProcessorsController : ControllerBase
    {      
       
        private readonly IApiProcessorsServise _processorsApiServise;

        public ProcessorsController(IApiProcessorsServise processorsApiServise)
        {
            _processorsApiServise = processorsApiServise;
        }

        /// <summary>
        ///  Sukuriamas pirkejas
        /// </summary>
        /// <returns></returns>
        [HttpPost("Processors/CreateSeller")]
        public async Task<Responce<SellersDto>> CreateSeller([FromBody] SellersDto seller)
        {
            var result = await _processorsApiServise.AddSeller(seller);

            if (result.success)
            {
                return Responce<SellersDto>.Success(seller);
            }

            return Responce<SellersDto>.Failure(seller, 504, result.error);
        }

        /// <summary>
        /// Prideti preke specifiniui pirkejui
        /// </summary>
        /// <param name="sellerId"></param>
        /// <returns></returns>
        [HttpPost("Processors/{sellerId}/Add")]
        public async Task AddSellersProcessor(int sellerId, [FromBody] ProcessorDto processor)
        {

        }

        /// <summary>
        /// istrinti specifine preke
        /// </summary>
        /// <param name="sellerId"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        [HttpDelete("Processors/{sellerId}/{itemId}")]
        public async Task RemoveSellersItem(int sellerId, int itemId)
        {

        }

        /// <summary>
        /// Atnaujinti specifine preke
        /// </summary>
        /// <param name="sellerId"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        [HttpPut("Processors/{sellerId}/{itemId}")]
        public async Task UpdateSellersItem(int sellerId, int itemId, [FromBody] ProcessorDto processor)
        {

        }

        /// <summary>
        /// Gauti info apie specifine preke
        /// </summary>
        /// <param name="sellerId"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        [HttpGet("Processors/{sellerId}/{itemId}")]
        public async Task GetSellersItem(int sellerId, int itemId)
        {

        }

        /// <summary>
        /// Gauti info apie pardaveja
        /// </summary>
        /// <param name="sellerId"></param>
        /// <returns></returns>
        [HttpGet("Processors/{sellerId}")]
        public async Task GetSeller(int sellerId)
        {

        }

        /// <summary>
        /// Istrinti pardaveja
        /// </summary>
        /// <param name="sellerId"></param>
        /// <returns></returns>
        [HttpDelete("Processors/{sellerId}")]
        public async Task DeleteSeller(int sellerId)
        {

        }

        /// <summary>
        /// atnaujinti specifinio pirkejo info
        /// </summary>
        /// <param name="sellerId"></param>
        /// <returns></returns>
        [HttpPut("Processors/{sellerId}")]
        public async Task UpdateSeller(int sellerId, [FromBody] ProcessorDto processor)
        {

        }

        /// <summary>
        /// Pirkejo parduodamas prekes
        /// </summary>
        /// <param name="sellerId"></param>
        /// <returns></returns>
        [HttpGet("Processors/{sellerId}/Products")]
        public async Task GetAllSellersProducts(int sellerId)
        {

        }

        /// <summary>
        /// Gauti visu pardaveju sarasa
        /// </summary>
        /// <returns></returns>
        [HttpGet("Processors/Sellers")]
        public async Task GetAllSellers()
        {

        } 
    }    
}
