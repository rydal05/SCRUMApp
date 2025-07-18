using Microsoft.EntityFrameworkCore.Metadata;

namespace SCRUMApp.Models
{
    public class Suggestion
    {
        public int Id { get; set; }
        public string mechanic { get; set; }
        public string explanation { get; set; }

        public Suggestion()
        {
            
        }
    }
}
