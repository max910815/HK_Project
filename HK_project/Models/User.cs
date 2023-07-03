using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace HK_project.Models
{
    public partial class User
    {

        public User()
        {
            Chat = new HashSet<Chat>();
        }

        [Key]
        public string UserId { get; set; }
        public string UserName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string UserEmail { get; set; }
        [DataType(DataType.Password)]
        public string UserPassword { get; set; }
        public string ApplicationId { get; set; }
        public Application Application { get; set; }
        public virtual ICollection<Chat> Chat { get; set; }

    }
}
