using System;
using System.Collections.Generic;

namespace openAPI.Models;

public partial class Embedding
{
    public int EmbeddingId { get; set; }

    public string? EmbeddingQuestion { get; set; }

    public string? EmbeddingAnswer { get; set; }

    public string? Qa { get; set; }

    public string? EmbeddingVector { get; set; }

    public int AifileId { get; set; }

    public virtual AiFile Aifile { get; set; } = null!;
}
