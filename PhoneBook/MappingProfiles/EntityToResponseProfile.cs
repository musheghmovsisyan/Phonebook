using AutoMapper;
using PhoneBook.Core.Entity;

namespace PhoneBook.MappingProfiles;

public class EntityToResponseProfile : Profile
{
    public EntityToResponseProfile()
    {
        CreateMap<UserEmail, Models.UserEmail>();
        CreateMap<UserPhoneNumber, Models.UserPhoneNumber>();
        CreateMap<UserProfile, Models.UserProfile>();
    }
}