using AutoMapper;
using PlataformasWeb.Models;
using PlataformasWeb.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace PlataformasWeb.Controllers
{
    [ApiController]
    [Route("api/platforms")]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformInfoRepository _platformInfoRepository;
        private readonly IMapper _mapper;
        const int maxPlatformsPageSize = 20;

        public PlatformsController(IPlatformInfoRepository platformInfoRepository,
            IMapper mapper)
        {
            _platformInfoRepository = platformInfoRepository ?? 
                throw new ArgumentNullException(nameof(platformInfoRepository));
            _mapper = mapper ?? 
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlatformWithoutMoviesDto>>> GetPlatforms(
            string? title, string? searchQuery, int pageNumber = 1, int pageSize = 10)
        {
            if (pageSize > maxPlatformsPageSize)
            {
                pageSize = maxPlatformsPageSize;
            }

            var (platformEntities, paginationMetadata) = await _platformInfoRepository
                .GetPlatformsAsync(title, searchQuery, pageNumber, pageSize);

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(paginationMetadata));

            return Ok(_mapper.Map<IEnumerable<PlatformWithoutMoviesDto>>(platformEntities));       
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlatform(
            int id, bool includeMovies = false)
        {
            var platform = await _platformInfoRepository.GetPlatformAsync(id, includeMovies);
            if (platform == null)
            {
                return NotFound();
            }

            if (includeMovies)
            {
                return Ok(_mapper.Map<PlatformDto>(platform));
            }

            return Ok(_mapper.Map<PlatformWithoutMoviesDto>(platform));
        }
    }
}
