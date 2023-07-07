using System;
using System.Collections.Generic;

namespace openAPI.Models;

public partial class Chat
{
    public int ChatId { get; set; }

    public DateTime ChatTime { get; set; }

    public string? ChatName { get; set; }

    public int UserId { get; set; }

    public virtual ICollection<Qahistory> Qahistories { get; set; } = new List<Qahistory>();

    public virtual User User { get; set; } = null!;
}
