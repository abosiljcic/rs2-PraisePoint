namespace Posts.Domain.Entities;

// TODO: These fields need to change probably
public class UserInfo
{
    public Guid UserId { get; set; }
    public Guid CompanyId { get; set; }
    public string ImageUrl { get; set; }
}