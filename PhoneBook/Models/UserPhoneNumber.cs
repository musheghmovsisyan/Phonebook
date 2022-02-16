using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Models;

public class UserPhoneNumber
{
    public long Id { get; set; }

    [Required] 
    [Phone]
    [StringLength(256)]
    [RegularExpression(@"^[0-9]+$", ErrorMessage = "PhoneNumber  Invalid")]
    public string PhoneNumber { get; set; }

    public bool DeletePhone { get; set; }
}