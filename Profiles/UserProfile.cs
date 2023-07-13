using AutoMapper;
using e_catalog_backend.Dtos.User;
using e_catalog_backend.Models;

namespace e_catalog_backend.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, ReadUserDto>();
        CreateMap<CreateUserDto, User>();
        CreateMap<UpdateUserDto, User>();
    }
}