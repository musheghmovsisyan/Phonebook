using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneBook.Core.Entity;

public class UserEmail
{
    [Key]
    public long Id { get; set; }

    [Required]
    public long UserProfileId { get; set; }

    [Required]
    public string Email { get; set; }


    [ForeignKey(nameof(UserProfileId))]
    public UserProfile UserProfile { get; set; }

}