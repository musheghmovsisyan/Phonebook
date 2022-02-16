using Microsoft.EntityFrameworkCore;
using PhoneBook.Core.Entity;

namespace PhoneBook.Infrastructure.Data;

public class PhoneBookDbContext : DbContext
{
    public PhoneBookDbContext(DbContextOptions<PhoneBookDbContext> options)
        : base(options)
    {
    }

    public DbSet<UserEmail> UserEmails { get; set; } = null!;

    public DbSet<UserPhoneNumber> UserPhoneNumbers { get; set; } = null!;

    public DbSet<UserProfile> UserProfiles { get; set; } = null!;
}