using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Models;

public class UserProfile
{
    [Key]
    public long Id { get; set; }

    [Required]
    public long UserId { get; set; }

    [Required]
    [StringLength(256)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(256)]
    public string LastName { get; set; }

    [StringLength(256)]
    public string MiddleName { get; set; }

    public string OrganizationName { get; set; }

    [Required]
    [Phone]
    [RegularExpression(@"^[0-9]+$", ErrorMessage = "PhoneNumber  Invalid")]

    public string PhoneNumber { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    public virtual ICollection<UserEmail> UserEmails { get; set; } = new List<UserEmail>();

    public virtual ICollection<UserPhoneNumber> UserPhoneNumbers { get; set; } = new List<UserPhoneNumber>();


}