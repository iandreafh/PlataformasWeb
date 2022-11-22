using AutoMapper;

namespace PlataformasWeb.Profiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<Data.Entities.Movie, Models.MovieDto>();
            CreateMap<Models.MovieForCreationDto, Data.Entities.Movie>();
            CreateMap<Models.MovieForUpdateDto, Data.Entities.Movie>();
            CreateMap<Data.Entities.Movie, Models.MovieForUpdateDto>();
        }
    }
}
