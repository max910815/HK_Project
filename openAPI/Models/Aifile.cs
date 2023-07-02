namespace openAPI.Models;

public partial class Aifile
{
    public string AifileId { get; set; } = null!;

    public string AifileType { get; set; } = null!;

    public string AifilePath { get; set; } = null!;

    public string? ApplicationId { get; set; }

    public virtual Application? Application { get; set; }

    public virtual ICollection<Embedding> Embeddings { get; set; } = new List<Embedding>();
}
