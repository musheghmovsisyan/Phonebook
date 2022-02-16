using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PhoneBook.Core.Contracts.Services;
using PhoneBook.Core.Entity;
using PhoneBook.Infrastructure.Data;

namespace PhoneBook.Infrastructure.Services;

public class UserProfileServices : IUserProfileServices
{
    private readonly PhoneBookDbContext _dbContext;
    private readonly ILogger<UserProfileServices> _logger;

    public UserProfileServices(PhoneBookDbContext dbContext, ILogger<UserProfileServices> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public Task<UserProfile?> GetAsync(long userId)
    {
        return _dbContext.UserProfiles
            .Include(_ => _!.UserEmails)
            .Include(_ => _!.UserPhoneNumbers)
            .FirstOrDefaultAsync(_ => _!.UserId == userId);
    }

    public bool Update(UserProfile userProfile)
    {
        try
        {
            _dbContext.Update(userProfile);
            _dbContext.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message, e);
        }

        return false;
    }

    public bool Delete(long id, long userId)
    {
        try
        {
            var userProfile = _dbContext.UserProfiles.FirstOrDefault(_ => _!.Id == id && _.UserId == userId);

            if (userProfile is null)
            {
                return false;
            }

            _dbContext.Remove(userProfile);
            _dbContext.SaveChanges();

            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message, e);
        }

        return false;
    }

    public bool DeleteUserPhoneNumbers(IList<UserPhoneNumber> userPhoneNumber)
    {
        try
        {
            _dbContext.RemoveRange(userPhoneNumber);
            _dbContext.SaveChanges();

            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message, e);
        }

        return false;
    }

    public bool DeleteUserEmails(IList<UserEmail> userEmail)
    {
        try
        {
            _dbContext.RemoveRange(userEmail);
            _dbContext.SaveChanges();

            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message, e);
        }

        return false;
    }
}