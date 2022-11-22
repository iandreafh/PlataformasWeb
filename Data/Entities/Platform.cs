using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlataformasWeb.Data.Entities
{
    public class Platform
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [MaxLength(300)]
        public string? Description { get; set; }

        public ICollection<Movie> Movies { get; set; }
               = new List<Movie>();

        public Platform(string title)
        {
            Title = title;
        }
    }
}
