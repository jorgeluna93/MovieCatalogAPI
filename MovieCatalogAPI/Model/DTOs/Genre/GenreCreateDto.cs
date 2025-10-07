using System.ComponentModel.DataAnnotations;

namespace MovieCatalogAPI.Model.DTOs.Genre
{
    public class GenreCreateDto
    {
        [Required]
        public string Name { get; set; }
    }
}
