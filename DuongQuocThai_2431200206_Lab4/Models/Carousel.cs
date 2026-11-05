using System.ComponentModel.DataAnnotations;

namespace DuongQuocThai_2431200206_Lab4.Models
{
    public class Carousel
    {
        [Key]
        public int CarouselId { get; set; }
        [Required(ErrorMessage = "Please enter ImageUrl")]
        public string ImageUrl { get; set; }
        [Required(ErrorMessage = "Please enter Title")]
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? LinkUrl { get; set; }
        [Required(ErrorMessage = "Please enter Order")]
        public int Order { get; set; }
        [Required(ErrorMessage = "Please enter if carousel if active")]
        public bool IsActive { get; set; }
        [Required(ErrorMessage = "Please enter CreatedDate")]
        public DateTime CreatedDate { get; set; }
        [Required(ErrorMessage = "Please enter UpdatedDate")]
        public DateTime UpdatedDate { get; set; }

    }
}
