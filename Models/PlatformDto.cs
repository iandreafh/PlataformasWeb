namespace PlataformasWeb.Models
{
    public class PlatformDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int NumberOfMovies
        {
            get
            {
                return Movies.Count;
            }
        }

        public ICollection<MovieDto> Movies { get; set; }
            = new List<MovieDto>();
    }
}
