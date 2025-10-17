using Microsoft.EntityFrameworkCore;
using MovieCatalogAPI.DAL;
using MovieCatalogAPI.Model.DTOs.Movie;
using MovieCatalogAPI.Services.Interfaces;
namespace MovieCatalogAPI.Services.Implementations
{
    public class MovieService:IMovieService
    {
        private readonly AppDbContext _context;

        public MovieService(AppDbContext context) { 
            _context = context;
        }

        /// <summary>
        /// Returns a list of movies with basic information
        /// </summary>
        /// <returns>List of Movies</returns>
        public async Task<IEnumerable<MovieCatalogResponseDto>> GetMoviesCatalogWithMetadata()
        {
            var movies = await _context.Movies
                .Include(m => m.Ratings)
                .Include(m => m.MovieGenres)
                .ThenInclude(mg => mg.Genre)
                .ToListAsync();

            var movieCatalog = movies.Select(movie => {
                var averageRating = movie.Ratings.Any() ? movie.Ratings.Average(r => r.Score) : 0;
                var genreNames = movie.MovieGenres.Select(mg => mg.Genre.Name).Distinct().ToList();

                return new MovieCatalogResponseDto
                {
                    MovieId = movie.Id,
                    Title = movie.Title,
                    Description = movie.Description,
                    PictureUrl = movie.PictureUrl,
                    releaseDate= movie.ReleaseDate,
                    AverageRating = Math.Round(averageRating,2),
                    Genres = genreNames
                };
            });

            return movieCatalog;
        }
        
        /// <summary>
        /// Returns a movie with all its meta data
        /// </summary>
        /// <param name="movieId">Id of the movie</param>
        /// <returns></returns>
        public async Task<MovieResponseDto> GetMovieWithMetaData(int movieId)
        {
            var movie = await _context.Movies
                .Include(m => m.MovieGenres).ThenInclude(mg => mg.Genre)
                .Include(m => m.MovieActors).ThenInclude(ma => ma.Actor)
                .Include(m => m.Ratings).ThenInclude(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == movieId); ;

            if (movie == null){
                return null;
            }

            var genres = movie.MovieGenres.Select(mg => mg.Genre.Name).Distinct().ToList();
            
            var actors = movie.MovieActors.Select(ma => new ActorsInfo {
                ActorName= ma.Actor?.FullName ?? "Unknown",
                PictureUrl = ma.Actor?.PictureUrl ?? string.Empty
            }).ToList();

            var averageRating = movie.Ratings.Any() ? movie.Ratings.Average(r => r.Score) : 0;
            
            var individualRatings = movie.Ratings.Select(r => new IndividualRating {
                Score = r.Score,
                Comment = r.Comment,
                UserName = r.User?.Username ?? string.Empty
            }).ToList();

            var ratingInfo = new RatingInfo
            {
                AverageRating = Math.Round(averageRating, 2),
                IndividualRating = individualRatings
            };

            return new MovieResponseDto
            {
                MovieId = movie.Id,
                Title = movie.Title,
                Description = movie.Description,
                PictureUrl = movie.PictureUrl,
                ReleaseDate = movie.ReleaseDate,
                Genres = genres,
                Actors = actors,
                Rating = new List<RatingInfo> { ratingInfo }
            };
        }

        
    }
}
