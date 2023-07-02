namespace openAPI.Models;

public partial class Member
{
    public string MemberId { get; set; } = null!;

    public string MemberName { get; set; } = null!;

    public string MemberEmail { get; set; } = null!;

    public string MemberPassword { get; set; } = null!;

    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();
}
