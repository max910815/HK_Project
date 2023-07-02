using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace HK_Product.Models
{
    public partial class User
    {
        public User()
        {
            Chats = new HashSet<Chat>();
        }
        [Key]
        public string UserId { get; set; }
        public string UserName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string UserEmail { get; set; }
        [DataType(DataType.Password)]
        public string UserPassword { get; set; }
 
        public virtual ICollection<Application> Applications { get; set; }
        public virtual ICollection<Chat> Chats { get; set; }

    }
}
