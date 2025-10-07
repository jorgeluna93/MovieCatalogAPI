namespace MovieCatalogAPI.Model.DTOs.Movie
{
    public class MovieResponseDto
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public DateTime ReleaseDate { get; set; }
        public List<string> Genre { get; set; }
        public List<ActorsInfo> Actors { get; set; }
        public List<RatingInfo> Rating { get; set; }

    }
    public class ActorsInfo
    {
        public string ActorName { get; set;}
        public string PictureUrl { get; set;}

    }

    public class RatingInfo
    {
        public double AverageRating { get; set;}
        public List<IndividualRating> IndividualRating { get; set;}
    }
    public class IndividualRating
    {
        public string Comment { get; set;}
        public string UserName { get; set;}
    }

}
