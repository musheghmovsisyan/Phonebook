using AutoMapper;
using PhoneBook.Models;

namespace PhoneBook.MappingProfiles;

public class RequestToEntityProfile : Profile
{
    public RequestToEntityProfile()
    {
        CreateMap<UserEmail, Core.Entity.UserEmail>();
        CreateMap<UserPhoneNumber, Core.Entity.UserPhoneNumber>();
        CreateMap<UserProfile, Core.Entity.UserProfile>();
    }
}