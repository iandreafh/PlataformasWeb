using PlataformasWeb.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace PlataformasWeb.Data
{
    public class PlatformInfoContext : DbContext
    {
        public DbSet<Platform> Platforms { get; set; } = null!;
        public DbSet<Movie> Movies { get; set; } = null!;

        public PlatformInfoContext(DbContextOptions<PlatformInfoContext> options) 
            : base(options) 
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Platform>()
                .HasData(
               new Platform("Netflix")
               {
                   Id = 1,
                   Description = "The reliable one, always creating new content."
               },
               new Platform("Amazon Prime Video")
               {
                   Id = 2,
                   Description = "The cheapest one, accesible for everyone."
               },
               new Platform("Disney+")
               {
                   Id = 3,
                   Description = "The youngest of them all."
               });

            modelBuilder.Entity<Movie>()
             .HasData(
               new Movie("Orgullo y Prejuicio")
               {
                   Id = 1,
                   PlatformId = 1,
                   Description = "Película romántica de época basada en una novela."
               },
               new Movie("Star Wars")
               {
                   Id = 2,
                   PlatformId = 1,
                   Description = "Película de ciencia ficción sobre la galaxia."
               },
                 new Movie("Niños grandes")
                 {
                     Id = 3,
                     PlatformId = 2,
                     Description = "Película de comedia americana sobre un grupo de amigos."
                 },
               new Movie("Canta")
               {
                   Id = 4,
                   PlatformId = 2,
                   Description = "Película musical de animación sobre un teatro de animales."
               },
               new Movie("Camp Rock")
               {
                   Id = 5,
                   PlatformId = 3,
                   Description = "Película musical sobre un grupo de adolescentes que van a un campamento de verano."
               },
               new Movie("Hermano Oso")
               {
                   Id = 6,
                   PlatformId = 3,
                   Description = "Película infantil de animación sobre el valor de la amistad y la familia."
               }
               );
            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite("connectionstring");
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
