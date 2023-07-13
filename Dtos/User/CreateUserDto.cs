using e_catalog_backend.Models;

namespace e_catalog_backend.Dtos.User;

public class CreateUserDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public IFormFile? Image { get; set; }
}