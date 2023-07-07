namespace openAPI.ViewModels
{
    public class SimilarViewModel
    {
        public required int ChatId { get; set; }
        public required float temperature { get; set; }
        public required string Question { get; set; }
        public required int ApplicationId { get; set; }
    }
}
