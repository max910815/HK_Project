using System.ComponentModel.DataAnnotations;

namespace openAPI.Models {
    public partial class Chat
    {
        public Chat()
        {
            Qahistorie = new HashSet<Qahistory>();
        }
        [Key]
        public string ChatId { get; set; }
        public DateTime ChatTime { get; set; }
        public string ChatName { get; set; }
        public string UserId { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Qahistory> Qahistorie { get; set; }
    }
}

