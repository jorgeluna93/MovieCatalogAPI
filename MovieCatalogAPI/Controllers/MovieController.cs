using Microsoft.AspNetCore.Mvc;
using MovieCatalogAPI.Services.Interfaces;
using MovieCatalogAPI.Services.Implementations;

namespace MovieCatalogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet("catalog")]
        public async Task<IActionResult> GetMovieCatalog()
        {
            var result = await _movieService.GetMoviesCatalogWithMetadata();
            return Ok(result);
        }

        [HttpGet("catalog/{id}")]
        public async Task<IActionResult> GetMovieData(int id)
        {
            var result = await _movieService.GetMovieWithMetaData(id);
            return Ok(result);
        }
    }
}
