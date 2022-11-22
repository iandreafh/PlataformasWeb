using PlataformasWeb.Data.Entities;

namespace PlataformasWeb.Services
{
    public interface IPlatformInfoRepository
    {
        Task<IEnumerable<Platform>> GetPlatformsAsync();
        Task<(IEnumerable<Platform>, PaginationMetadata)> GetPlatformsAsync(
            string? title, string? searchQuery, int pageNumber, int pageSize);
        Task<Platform?> GetPlatformAsync(int platformId, bool includeMovies);
        Task<bool> PlatformExistsAsync(int platformId);
        Task<IEnumerable<Movie>> GetMoviesForPlatformAsync(int platformId);
        Task<Movie?> GetMovieForPlatformAsync(int platformId, 
            int movieId);
        Task AddMovieForPlatformAsync(int platformId, Movie movie);
        void DeleteMovie(Movie movie);
        Task<bool> SaveChangesAsync();
    }
}
