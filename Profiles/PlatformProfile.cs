using AutoMapper;

namespace PlataformasWeb.Profiles
{
    public class PlatformProfile : Profile
    {
        public PlatformProfile()
        {
            CreateMap<Data.Entities.Platform, Models.PlatformWithoutMoviesDto>();
            CreateMap<Data.Entities.Platform, Models.PlatformDto>();
        }
    }
}
