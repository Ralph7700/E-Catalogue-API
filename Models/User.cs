using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_catalog_backend.Models;

[Table("Users")]
public class User
{

    [Key]
    [Column("user_id")]
    public Guid UserId { get; set; } = Guid.NewGuid();
    
    [Column("first_name")]
    [Required]
    public string FirstName { get; set; }
    
    [Column("last_name")]
    [Required]
    public string LastName { get; set; }
    
    [Column("email")]
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Column("password")]
    [Required]
    public string Password { get; set; }
    
    [Column("phone_number")]
    [Required]
    [Phone]
    public string PhoneNumber { get; set; }
    
    [Column("photo_url")]
    public string? PhotoUrl { get; set; }
    
    [Column("role")]
    [DefaultValue(Models.Role.Salesman)]
    public Role Role { get; set; }
    
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    [Column("is_deleted")]
    public bool IsDeleted { get; set; } = false;
    
}

public enum Role
{
    Manager,
    Salesman
}