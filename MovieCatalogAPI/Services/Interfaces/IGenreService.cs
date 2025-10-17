
using MovieCatalogAPI.Model.DTOs.Genre;

namespace MovieCatalogAPI.Services.Interfaces
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreResponseDto>> GetAllGenresAsync();
        Task<GenreResponseDto> GetGenreByIdAsync(int id);
        Task AddGenreAsync(GenreCreateDto dto);
        Task UpdateGenreAsync(GenreUpdateDto dto);
        Task DeleteGenreAsync(int id);



    }
}
