using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using MyFirstWebApp.Classes.Processors;
using MyFirstWebApp.Database;
using MyFirstWebApp.Servises.Contracts;
using PuppeteerSharp;

namespace MyFirstWebApp.Servises
{
    public class TopoProccessorsServise : ITopoProccessorsServise
    {
        private readonly WebDatabaseContext _dbContext;

        private string _topoCentrasProcesorsBaseUrl = @"https://www.topocentras.lt/kompiuteriai-ir-plansetes/kompiuteriu-komponentai/procesoriai.html";
        private int _itemsInPage = 20;
        private string _topoCentrasLimit = @$"?limit=";
        private string _topoCentrasPage = @"&p=";

        public TopoProccessorsServise(WebDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<ProcessorListing> GetProcessorsFromDB()
        {
            var processors = _dbContext.Processors.ToList();
            var processorsListing = new List<ProcessorListing>();

            foreach(var processor in processors)
            {
                processorsListing.Add(new ProcessorListing(processor));
            }

            return processorsListing;
        }

        public async Task<List<ProcessorListing>> ScrapeTopoProcesors()
        {
            var processors = new List<ProcessorListing>();
            var pageCounter = 1;

            var basePage = await ScrapeJavaScriptPage($"{_topoCentrasProcesorsBaseUrl}{_topoCentrasLimit}{_itemsInPage}{_topoCentrasPage}{pageCounter}");

            var pageLimit = GetPageLimit(basePage);

            processors.AddRange(GetProcessorsFromPage(basePage));

            for (pageCounter = 2; pageCounter <= pageLimit; pageCounter++)
            {
                var nextPage = await ScrapeJavaScriptPage($"{_topoCentrasProcesorsBaseUrl}{_topoCentrasLimit}{_itemsInPage}{_topoCentrasPage}{pageCounter}");

                processors.AddRange(GetProcessorsFromPage(nextPage));
            }

            return processors;
        }

        public async Task<List<ProcessorListing>> ScrapeTopoProcesorsPage(int PageNumber)
        {
            var processors = new List<ProcessorListing>();
            var basePage = await ScrapeJavaScriptPage($"{_topoCentrasProcesorsBaseUrl}{_topoCentrasLimit}{_itemsInPage}{_topoCentrasPage}{PageNumber}");
            processors.AddRange(GetProcessorsFromPage(basePage));

            return processors;
        }

        public async Task<List<ProcessorListing>> ScrapeTopoProcesorsFirstPage()
        {
            var processors = new List<ProcessorListing>();
            var basePage = await ScrapeJavaScriptPage($"{_topoCentrasProcesorsBaseUrl}{_topoCentrasLimit}{_itemsInPage}{_topoCentrasPage}1");
            processors.AddRange(GetProcessorsFromPage(basePage));

            return processors;
        }

        private int GetPageLimit(string page)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(page);

            var elements = htmlDocument.QuerySelectorAll(".Count-pageCount-LOv > span");

            if (int.TryParse(elements[2].InnerHtml, out var pageLimit))
            {
                var pageCounter = 1;
                pageLimit -= _itemsInPage;

                while (pageLimit > 0)
                {
                    pageLimit -= _itemsInPage;
                    pageCounter++;
                }

                return pageCounter;
            }

            return 1;
        }

        private IEnumerable<ProcessorListing> GetProcessorsFromPage(string page)
        {
            var processors = new List<ProcessorListing>();

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(page);

            var elements = htmlDocument.QuerySelectorAll("article .ProductGridItem-productWrapper-2ip");

            foreach (var element in elements)
            {
                var price = element.QuerySelector(".Price-price-27p").InnerText;
                var name = element.QuerySelector(".ProductGridItem-productName-3ZD").InnerText;
                var pictureUrl = element.QuerySelector(".ProductGridItem-imageContainer-pMi").FirstChild.GetAttributeValue("src", string.Empty);
                var productLink = element.QuerySelector(".ProductGridItem-link-3xD")?.FirstChild?.GetAttributeValue<string>("href", "#") ?? "#";

                var processorListing = new ProcessorListing(name, pictureUrl, price, productLink);

                processors.Add(processorListing);
            }

            return processors;
        }

        private async Task<string> ScrapeJavaScriptPage(string url)
        {
            await new BrowserFetcher(Product.Chrome).DownloadAsync();
            using var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true,
                Product = Product.Chrome
            });

            using var page = await browser.NewPageAsync();

            await page.SetViewportAsync(new ViewPortOptions { Width = 1300, Height = 8000 });
            await page.GoToAsync(url, new NavigationOptions { WaitUntil = new WaitUntilNavigation[] { WaitUntilNavigation.Load } });

            //await page.EvaluateExpressionAsync(" window.scrollTo(0, 0); ");
            //await page.EvaluateExpressionAsync("" +
            //    "var x = 0;" +
            //    "var fullLength = window.scrollTo(0, window.document.body.scrollHeight); ");

            return await page.GetContentAsync();
        }
    }
}
