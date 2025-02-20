﻿using Lab05.Domain.Enums;

namespace Lab05.Domain.Models;

public class User
{        
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty; 
    public UserRole Role { get; set; }
}