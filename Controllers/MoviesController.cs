using AutoMapper;
using PlataformasWeb.Models;
using PlataformasWeb.Services;
using PlataformasWeb.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace PlataformasWeb.Controllers
{
    [Route("api/platforms/{platformId}/movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ILogger<MoviesController> _logger;
        private readonly IMailService _mailService;
        private readonly IPlatformInfoRepository _platformInfoRepository;
        private readonly IMapper _mapper;

        public MoviesController(ILogger<MoviesController> logger,
            IMailService mailService,
            IPlatformInfoRepository platformInfoRepository,
            IMapper mapper)
        {
            _logger = logger ?? 
                throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? 
                throw new ArgumentNullException(nameof(mailService));
            _platformInfoRepository = platformInfoRepository ?? 
                throw new ArgumentNullException(nameof(platformInfoRepository));
            _mapper = mapper ?? 
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies(
            int platformId)
        {
            if (!await _platformInfoRepository.PlatformExistsAsync(platformId))
            {
                _logger.LogInformation(
                    $"Platform with id {platformId} wasn't found when accessing movies.");
                return NotFound();
            }

            var moviesForPlatform = await _platformInfoRepository
                .GetMoviesForPlatformAsync(platformId);

            return Ok(_mapper.Map<IEnumerable<MovieDto>>(moviesForPlatform));
        }

        [HttpGet("{movieid}", Name = "GetMovie")]
        public async Task<ActionResult<MovieDto>> GetMovie(
            int platformId, int movieId)
        {
            if (!await _platformInfoRepository.PlatformExistsAsync(platformId))
            {
                return NotFound();
            }

            var movie = await _platformInfoRepository
                .GetMovieForPlatformAsync(platformId, movieId);

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<MovieDto>(movie));
        }

        [HttpPost]
        public async Task<ActionResult<MovieDto>> CreateMovie(
           int platformId,
           MovieForCreationDto movie)
        {
            if (!await _platformInfoRepository.PlatformExistsAsync(platformId))
            {
                return NotFound();
            }

            var finalMovie = _mapper.Map<Movie>(movie);

            await _platformInfoRepository.AddMovieForPlatformAsync(
                platformId, finalMovie);

            await _platformInfoRepository.SaveChangesAsync();

            var createdMovieToReturn = 
                _mapper.Map<Models.MovieDto>(finalMovie);

            return CreatedAtRoute("GetMovie",
                 new
                 {
                     platformId = platformId,
                     movieId = createdMovieToReturn.Id
                 },
                 createdMovieToReturn);
        }

        [HttpPut("{movieid}")]
        public async Task<ActionResult> UpdateMovie(int platformId, int movieId,
            MovieForUpdateDto movie)
        {
            if (!await _platformInfoRepository.PlatformExistsAsync(platformId))
            {
                return NotFound();
            }

            var movieEntity = await _platformInfoRepository
                .GetMovieForPlatformAsync(platformId, movieId);
            if (movieEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(movie, movieEntity);

            await _platformInfoRepository.SaveChangesAsync();

            return NoContent();
        }


        [HttpPatch("{movieid}")]
        public async Task<ActionResult> PartiallyUpdateMovie(
            int platformId, int movieId,
            JsonPatchDocument<MovieForUpdateDto> patchDocument)
        {
            if (!await _platformInfoRepository.PlatformExistsAsync(platformId))
            {
                return NotFound();
            }

            var movieEntity = await _platformInfoRepository
                .GetMovieForPlatformAsync(platformId, movieId);
            if (movieEntity == null)
            {
                return NotFound();
            }

            var movieToPatch = _mapper.Map<MovieForUpdateDto>(
                movieEntity);

            patchDocument.ApplyTo(movieToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(movieToPatch))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(movieToPatch, movieEntity);
            await _platformInfoRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{movieId}")]
        public async Task<ActionResult> DeleteMovie(
            int platformId, int movieId)
        {
            if (!await _platformInfoRepository.PlatformExistsAsync(platformId))
            {
                return NotFound();
            }

            var movieEntity = await _platformInfoRepository
                .GetMovieForPlatformAsync(platformId, movieId);
            if (movieEntity == null)
            {
                return NotFound();
            }

            _platformInfoRepository.DeleteMovie(movieEntity);
            await _platformInfoRepository.SaveChangesAsync();

            _mailService.SendMessage(
                "",
                "Movie deleted.",
                $"Movie {movieEntity.Title} with id {movieEntity.Id} was deleted.");
         
            return NoContent();
        }

    }
}
