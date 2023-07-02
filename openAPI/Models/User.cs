namespace openAPI.Models;

public partial class User
{
    public string UserId { get; set; } = null!;

    public string UserPassword { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string UserEmail { get; set; } = null!;

    public string? ApplicationId { get; set; }

    public virtual Application? Application { get; set; }

    public virtual ICollection<Chat> Chats { get; set; } = new List<Chat>();

}
