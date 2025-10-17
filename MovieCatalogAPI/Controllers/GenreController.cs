using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieCatalogAPI.Model.DTOs.Genre;
using MovieCatalogAPI.Services.Implementations;
using MovieCatalogAPI.Services.Interfaces;

namespace MovieCatalogAPI.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
        public class GenreController : ControllerBase
        {
            private readonly IGenreService _genreService;

            public GenreController(IGenreService genreService)
            {
                _genreService = genreService;
            }

            [HttpGet]
            public async Task<IActionResult> GetAllGenres()
            {
                var genres = await _genreService.GetAllGenresAsync();
                return Ok(genres);
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> GetGenreById(int id)
            {
                var genre = await _genreService.GetGenreByIdAsync(id);
                if (genre == null)
                {
                    return NotFound();
                }

                return Ok(genre);
            }

            [HttpPost]
            public async Task<IActionResult> CreateGenre([FromBody] GenreCreateDto dto)
            {
                await _genreService.AddGenreAsync(dto);
                return Ok("Genre created successfully.");
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateGenre(int id, [FromBody] GenreUpdateDto dto)
            {
                if (id != dto.Id)
                {
                    return BadRequest("Id mismatch");
                }

                await _genreService.UpdateGenreAsync(dto);
                return Ok("Genre updated successfully.");
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteGenre(int id)
            {
                await _genreService.DeleteGenreAsync(id);
                return Ok("Genre deleted successfully.");
            }
        }
    
}
