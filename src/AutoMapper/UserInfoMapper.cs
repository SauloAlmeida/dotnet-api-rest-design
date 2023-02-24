using AutoMapper;
using src.DTO;
using src.Model;

namespace src.AutoMapper;

public class UserInfoMapper : Profile
{
    public UserInfoMapper()
    {
        CreateMap<UserInfoModel, UserInfoOutput>();
    }
}