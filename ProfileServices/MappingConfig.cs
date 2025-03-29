using AutoMapper;
using ProfileServices.Models.Dto;
using ProfileServices.Models.Entity;

namespace ProfileServices
{
    public class MappingConfig : AutoMapper.Profile
    {
        public MappingConfig()
        {
            CreateMap<ApplicationUser, ApplicationUserDto>().ReverseMap();
            CreateMap<CandidateProfile, CandidateProfileDto>().ReverseMap(); 
        }
    }
}
