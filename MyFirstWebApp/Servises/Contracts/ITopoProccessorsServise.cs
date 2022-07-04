﻿using MyFirstWebApp.Classes.Processors;

namespace MyFirstWebApp.Servises.Contracts
{
    public interface ITopoProccessorsServise
    {
        public Task<List<ProcessorListing>> ScrapeTopoProcesors();

        public Task<List<ProcessorListing>> ScrapeTopoProcesorsPage(int PageNumber);

        public Task<List<ProcessorListing>> ScrapeTopoProcesorsFirstPage();
    }
}
