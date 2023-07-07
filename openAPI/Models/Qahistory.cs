using System;
using System.Collections.Generic;

namespace openAPI.Models;

public partial class Qahistory
{
    public int QahistoryId { get; set; }

    public string? QahistoryQ { get; set; }

    public string? QahistoryA { get; set; }

    public string? QahistoryVector { get; set; }

    public int ChatId { get; set; }

    public virtual Chat Chat { get; set; } = null!;
}
