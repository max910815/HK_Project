using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace HKDB.Models;
public partial class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserId { get; set; }
    public string? UserName { get; set; }
    [DataType(DataType.EmailAddress)]
    public string UserEmail { get; set; }
    [DataType(DataType.Password)]
    public string? UserPassword { get; set; }

    public virtual ICollection<Chat> Chat { get; set; }

}

