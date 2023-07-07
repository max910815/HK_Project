using openAPI.Models;

namespace openAPI.ViewModels
{
    public class TurboViewModel
    {
        public required string Question { get; set; }
        public string Sim_Anser { get; set; }
        public Application Setting { get; set; }
        public float temperature { get; set; }
        public int ChatId { get; set; }
    }
}
