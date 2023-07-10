using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace HKDB.Models;
public partial class Embedding
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int EmbeddingId { get; set; }
    public string? EmbeddingQuestion { get; set; }
    public string? EmbeddingAnswer { get; set; }
    public required string Qa { get; set; }
    public required string EmbeddingVector { get; set; } //測試

    public virtual int AifileId { get; set; }
    public virtual Aifile Aifile { get; set; }
}

