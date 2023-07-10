using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace HKDB.Models;
public partial class Application
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ApplicationId { get; set; }
    public string? Model { get; set; }
    public string? Parameter { get; set; }
    public string ApplicationName { get; set; }

    public virtual int MemberId { get; set; }
    public virtual Member Member { get; set; }
    public virtual ICollection<Aifile> Aifile { get; set; }
}

