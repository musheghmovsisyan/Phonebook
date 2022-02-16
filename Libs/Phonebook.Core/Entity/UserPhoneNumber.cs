using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneBook.Core.Entity;

public class UserPhoneNumber
{
    [Key]
    public long Id { get; set; }

    [Required]
    public long UserProfileId { get; set; }

    [Required]
    public string PhoneNumber { get; set; }

  
    [ForeignKey(nameof(UserProfileId))]
    public UserProfile UserProfile { get; set; }
}