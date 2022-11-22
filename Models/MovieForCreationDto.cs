using System.ComponentModel.DataAnnotations;

namespace PlataformasWeb.Models
{
    public class MovieForCreationDto
    {
        [Required(ErrorMessage = "You should provide a title value.")]
        [MaxLength(50)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(300)]
        public string? Description { get; set; }
    }
}
