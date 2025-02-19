namespace Lab05.Domain;
using Lab05.Domain.Enums;


public class User
{        
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty; 
    public UserRole Role { get; set; }
}