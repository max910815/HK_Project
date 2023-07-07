using System;
using System.Collections.Generic;

namespace openAPI.Models;

public partial class Member
{
    public int MemberId { get; set; }

    public string? MemberName { get; set; }

    public string? MemberEmail { get; set; }

    public string? MemberPassword { get; set; }

    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();
}
