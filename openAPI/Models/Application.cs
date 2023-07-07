using System;
using System.Collections.Generic;

namespace openAPI.Models;

public partial class Application
{
    public int ApplicationId { get; set; }

    public string? Model { get; set; }

    public string? Parameter { get; set; }

    public string? ApplicationName { get; set; }

    public int MemberId { get; set; }

    public virtual ICollection<AiFile> AiFiles { get; set; } = new List<AiFile>();

    public virtual Member Member { get; set; } = null!;
}
