using System;
using System.Collections.Generic;

namespace openAPI.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? UserName { get; set; }

    public string? UserEmail { get; set; }

    public string? UserPassword { get; set; }

    public virtual ICollection<Chat> Chats { get; set; } = new List<Chat>();
}
