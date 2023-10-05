using AutoMapper;
using News_App_API.Models;

namespace News_App_API.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CreateMap<UserForRegistrationDto, UserAuthDto>()
                //.ForMember(u => u.na, opt => opt.MapFrom(x => x.Email));
        }
    }
}
