using MyFirstWebApp.Classes.Processors;

namespace MyFirstWebApp.Servises.Contracts
{
    public interface ITopoProccessorsServise
    {
        public List<ProcessorListing> GetProcessorsFromDB();

        public Task<List<ProcessorListing>> ScrapeTopoProcesors();

        public Task<List<ProcessorListing>> ScrapeTopoProcesorsPage(int PageNumber);

        public Task<List<ProcessorListing>> ScrapeTopoProcesorsFirstPage();
    }
}
