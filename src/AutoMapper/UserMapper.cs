using AutoMapper;
using src.DTO;
using src.Model;

namespace src.AutoMapper;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<UserModel, UserOutput>();
    } 
}