using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace HK_Product.Models
{
    public partial class Chat
    {
        public Chat()
        {
            Qahistories = new HashSet<Qahistory>();
        }
        [Key]
        public string ChatId { get; set; }
        public DateTime ChatTime { get; set; }
        public string ChatName { get; set; }
        public string UserId { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Qahistory> Qahistories { get; set; }
    }
}
