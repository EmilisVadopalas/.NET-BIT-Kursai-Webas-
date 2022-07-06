using MyFirstWebApp.Classes.Processors;

namespace MyFirstWebApp.Models
{
    public class ProccessorsModel
    {
        public int Page { get; set; } = 1;
        public List<ProcessorListing> Processors { get; set; }

        public ProccessorsModel(List<ProcessorListing> processors)
        {
            Processors = processors;
        }

        public ProccessorsModel(List<ProcessorListing> processors, int page)
        {
            Processors = processors;
            Page = page;
        }
    }
}
