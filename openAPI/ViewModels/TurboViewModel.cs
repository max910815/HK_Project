
namespace openAPI.ViewModels
{
    public class TurboViewModel
    {
        public required string Question { get; set; }
        public string Sim_Anser { get; set; }
        public string Model { get; set; }
        public string Parameter { get; set; }
        public float temperature { get; set; }
        public int ChatId { get; set; }
    }
}
