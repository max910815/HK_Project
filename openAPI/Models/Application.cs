namespace openAPI.Models;

public partial class Application
{
    public string ApplicationId { get; set; } = null!;

    public string Model { get; set; } = null!;

    public string Parameter { get; set; } = null!;

    public string? MemberId { get; set; }

    public virtual ICollection<Aifile> Aifiles { get; set; } = new List<Aifile>();

    public virtual Member? Member { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
