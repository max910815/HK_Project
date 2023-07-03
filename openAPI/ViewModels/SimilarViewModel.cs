namespace openAPI.ViewModels
{
    public class SimilarViewModel
    {
        public required string ChatId { get; set; }
        public required float temperature { get; set; }
        public required string Question { get; set; }
        public required string ApplicationId { get; set; }
        public required string DataId { get; set; }
    }
}
