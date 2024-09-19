﻿using System.ComponentModel.DataAnnotations;

namespace WebTech.Domain.Entities.Database;

public class User : DatabaseEntity
{
    [Required]
    [MaxLength(48)]
    [MinLength(3)]
    public string UserName { get; set; }

    [Required]
    [MaxLength(48)]
    [MinLength(3)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(48)]
    [MinLength(3)]
    public string LastName { get; set; }

    [Required] public DateTime BirthDate { get; set; }

    [Required] [MaxLength(128)] public string Address { get; set; }

    [Required] public byte[] PasswordSalt { get; set; }

    [Required] public byte[] PasswordHash { get; set; }
}