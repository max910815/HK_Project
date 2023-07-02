namespace openAPI.Models;

public partial class Embedding
{
    public string EmbeddingId { get; set; } = null!;

    public string EmbeddingQuestion { get; set; } = null!;

    public string EmbeddingAnswer { get; set; } = null!;

    public string Qa { get; set; } = null!;

    public string EmbeddingVectors { get; set; } = null!;

    public string? AifileId { get; set; }

    public virtual Aifile? Aifile { get; set; }
}
