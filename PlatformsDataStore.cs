using PlataformasWeb.Models;

namespace PlataformasWeb
{
    public class PlatformsDataStore
    {
        public List<PlatformDto> Platforms { get; set; }
       // public static PlatformsDataStore Current { get; } = new PlatformsDataStore();

        public PlatformsDataStore()
        {
            // init dummy data
            Platforms = new List<PlatformDto>()
            {
                new PlatformDto()
                {
                     Id = 1,
                     Title = "Netflix",
                     Description = "The reliable one, always creating new content.",
                     Movies = new List<MovieDto>()
                     {
                         new MovieDto() {
                             Id = 1,
                             Title = "Orgullo y Prejuicio",
                             Description = "Película romántica de época basada en una novela." },
                          new MovieDto() {
                             Id = 2,
                             Title = "Star Wars",
                             Description = "Película de ciencia ficción sobre la galaxia." },
                     }
                },
                new PlatformDto()
                {
                    Id = 2,
                    Title = "Amazon Prime Video",
                    Description = "The cheapest one, accesible for everyone.",
                    Movies = new List<MovieDto>()
                     {
                         new MovieDto() {
                             Id = 3,
                             Title = "Niños grandes",
                             Description = "Película de comedia americana sobre un grupo de amigos." },
                          new MovieDto() {
                             Id = 4,
                             Title = "Canta",
                             Description = "Película musical de animación sobre un teatro de animales." },
                     }
                },
                new PlatformDto()
                {
                    Id= 3,
                    Title = "Disney+",
                    Description = "The youngest of them all.",
                    Movies = new List<MovieDto>()
                     {
                         new MovieDto() {
                             Id = 5,
                             Title = "Camp Rock",
                             Description = "Película musical sobre un grupo de adolescentes que van a un campamento de verano." },
                          new MovieDto() {
                             Id = 6,
                             Title = "Hermano Oso",
                             Description = "Película infantil de animación sobre el valor de la amistad y la familia." },
                     }
                }
            };

        }

    }
}
