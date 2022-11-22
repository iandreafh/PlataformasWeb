using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlataformasWeb.Data.Entities
{
    public class Movie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [MaxLength(300)]
        public string Description { get; set; }


        [ForeignKey("PlatformId")]
        public Platform? Platform { get; set; }
        public int PlatformId { get; set; }

        public Movie(string title)
        {
            Title = title;
        }
    }
}
