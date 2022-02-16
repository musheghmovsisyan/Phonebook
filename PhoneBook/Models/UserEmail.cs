using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Models;

public class UserEmail
{
    public long Id { get; set; }

    [Required]
    [StringLength(256)]
    [EmailAddress]
    public string Email { get; set; }

    public bool DeleteEmail { get; set; }
}