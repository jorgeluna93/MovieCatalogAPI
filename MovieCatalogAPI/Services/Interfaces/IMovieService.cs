using MovieCatalogAPI.Model.DTOs.Movie;

namespace MovieCatalogAPI.Services.Interfaces
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieCatalogResponseDto>> GetMoviesCatalogWithMetadata();
        Task<MovieResponseDto> GetMovieWithMetaData(int id);
    }
}
