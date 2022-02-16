using System.Collections.Generic;
using System.Threading.Tasks;
using PhoneBook.Core.Entity;

namespace PhoneBook.Core.Contracts.Services;

public interface IUserProfileServices
{
    Task<UserProfile?> GetAsync(long userId);

    bool Update(UserProfile userProfile);

    bool Delete(long id, long userId);

    bool DeleteUserPhoneNumbers(IList<UserPhoneNumber> userEmail);
    bool DeleteUserEmails(IList<UserEmail> userEmail);
}