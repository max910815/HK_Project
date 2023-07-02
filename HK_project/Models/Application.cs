using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace HK_Product.Models
{
    public partial class Application
    {
        public Application()
        {
            Aifiles = new HashSet<Aifile>();
        }
        [Key]
        public string ApplicationId { get; set; }
        public string? Model { get; set; }
        public string? Parameter { get; set; }
        public string UserId { get; set; }

        public virtual Member Member { get; set; }
        public virtual ICollection<Aifile> Aifiles { get; set; }
        public virtual User User { get; set; }
    }
}
