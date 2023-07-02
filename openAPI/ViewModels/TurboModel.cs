using openAPI.Models;

namespace openAPI.ViewModels
{
    public class TurboModel
    {
        public required string DataId { get; set; }
        public required string Question { get; set; }
        public required string Sim_Anser { get; set; }
        public required Application Setting { get; set; }
        public required float temperature { get; set; }
        public required string ChatId { get; set; }
    }
}
