using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace HK_project.Models
{
    public partial class Embedding
    {
        [Key]
        public string EmbeddingId { get; set; }
        public string? EmbeddingQuestion { get; set; }
        public string? EmbeddingAnswer { get; set; }
        public string Qa { get; set; }
        public string EmbeddingVectors { get; set; }
        public string AifileId { get; set; }
        public virtual Aifile Aifile { get; set; }
    }
}
