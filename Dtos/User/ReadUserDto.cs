using e_catalog_backend.Models;

namespace e_catalog_backend.Dtos.User;

public class ReadUserDto
{
    public Guid UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string? PhotoUrl { get; set; }
    public Role Role { get; set; }
}