using PlataformasWeb.Data;
using PlataformasWeb.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace PlataformasWeb.Services
{
    public class PlatformInfoRepository : IPlatformInfoRepository
    {
        private readonly PlatformInfoContext _context;

        public PlatformInfoRepository(PlatformInfoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Platform>> GetPlatformsAsync()
        {
            return await _context.Platforms.OrderBy(p => p.Title).ToListAsync();
        }

        public async Task<(IEnumerable<Platform>, PaginationMetadata)> GetPlatformsAsync(
            string? title, string? searchQuery, int pageNumber, int pageSize)
        {
            // collection to start from
            var collection = _context.Platforms as IQueryable<Platform>;

            if (!string.IsNullOrWhiteSpace(title))
            {
                title = title.Trim();
                collection = collection.Where(p => p.Title == title);
            }

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                collection = collection.Where(a => a.Title.Contains(searchQuery)
                    || (a.Description != null && a.Description.Contains(searchQuery)));
            }

            var totalItemCount = await collection.CountAsync();

            var paginationMetadata = new PaginationMetadata(
                totalItemCount, pageSize, pageNumber);

            var collectionToReturn = await collection.OrderBy(p => p.Title)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (collectionToReturn, paginationMetadata);
        }



        public async Task<Platform?> GetPlatformAsync(int platformId, bool includeMovies)
        {
            if (includeMovies)
            {
                return await _context.Platforms.Include(p => p.Movies)
                    .Where(p => p.Id == platformId).FirstOrDefaultAsync();
            }

            return await _context.Platforms
                  .Where(p => p.Id == platformId).FirstOrDefaultAsync();
        }

        public async Task<bool> PlatformExistsAsync(int platformId)
        {
            return await _context.Platforms.AnyAsync(p => p.Id == platformId);
        }

        public async Task<Movie?> GetMovieForPlatformAsync(
            int platformId, 
            int movieId)
        {
            return await _context.Movies
               .Where(m => m.PlatformId == platformId && m.Id == movieId)
               .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Movie>> GetMoviesForPlatformAsync(
            int platformId)
        {
            return await _context.Movies
                           .Where(m => m.PlatformId == platformId).ToListAsync();
        }

        public async Task AddMovieForPlatformAsync(int platformId, 
            Movie movie)
        {
            var platform = await GetPlatformAsync(platformId, false);
            if (platform != null)
            {
                platform.Movies.Add(movie);
            }
        }

        public void DeleteMovie(Movie movie)
        {
            _context.Movies.Remove(movie);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
