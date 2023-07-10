using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace HKDB.Models;
public partial class Aifile
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AifileId { get; set; }
    public string? AifileType { get; set; }
    public required string AifilePath { get; set; }
    public string? Language { get; set; }

    public virtual int ApplicationId { get; set; }
    public virtual Application Application { get; set; }
    public virtual ICollection<Embedding> Embeddings { get; set; }
}

