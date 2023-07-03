using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace HK_project.Models
{
    public partial class Application
    {
        public Application()
        {
            User = new HashSet<User>();
            Aifile = new HashSet<Aifile>();
        }
     

        [Key]
        public string ApplicationId { get; set; }
        public string? Model { get; set; }
        public string? Parameter { get; set; }
        public string MemberId { get; set; }
        public string ApplicationName { get; set; }

        public virtual Member Member { get; set; }
        public virtual ICollection<Aifile> Aifile { get; set; }
        public virtual ICollection<User> User { get; set; }
    }
}
