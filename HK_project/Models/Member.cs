using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace HK_Product.Models
{
    public partial class Member
    {
        public Member()
        {
            Applications = new HashSet<Application>();
        }

        [Key]
        public string MemberId { get; set; }
        public string MemberName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string MemberEmail { get; set; }
        [DataType(DataType.Password)]
        public string MemberPassword { get; set; }

        public virtual ICollection<Application> Applications { get; set; }
    }
}
