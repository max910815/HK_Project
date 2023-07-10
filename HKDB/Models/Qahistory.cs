using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace HKDB.Models;
public partial class Qahistory
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int QahistoryId { get; set; }
    public string QahistoryQ { get; set; }
    public string QahistoryA { get; set; }
    public string QahistoryVector { get; set; }

    public int ChatId { get; set; }
    public virtual Chat Chat { get; set; }
}

