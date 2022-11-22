namespace PlataformasWeb.Models
{
    public class PlatformWithoutMoviesDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
