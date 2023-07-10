using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace HKDB.Models
{
    public partial class Member
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MemberId { get; set; }
        public string MemberName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string MemberEmail { get; set; }
        [DataType(DataType.Password)]
        public string MemberPassword { get; set; }

        public virtual ICollection<Application> Application { get; set; }
    }
}
