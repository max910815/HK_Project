using System;
using System.Collections.Generic;

namespace openAPI.Models;

public partial class AiFile
{
    public int AifileId { get; set; }

    public string? AifileType { get; set; }

    public string? AifilePath { get; set; }

    public string? Language { get; set; }

    public int ApplicationId { get; set; }

    public virtual Application Application { get; set; } = null!;

    public virtual ICollection<Embedding> Embeddings { get; set; } = new List<Embedding>();
}
