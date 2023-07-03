using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace HK_project.Models
{
    public partial class Aifile
    {
        public Aifile()
        {
            Embeddings = new HashSet<Embedding>();
        }
        [Key]
        public string AifileId { get; set; }
        public string AifileType { get; set; }
        public string AifilePath { get; set; }
        public string ApplicationId { get; set; }
        public string? Language { get; set; }
        public virtual Application Application { get; set; }
        public virtual ICollection<Embedding> Embeddings { get; set; }
    }
}
