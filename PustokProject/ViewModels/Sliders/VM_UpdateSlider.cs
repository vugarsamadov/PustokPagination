using PustokProject.Enums;
using System.ComponentModel.DataAnnotations;

namespace PustokProject.ViewModels.Sliders
{
    public class VM_UpdateSlider
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Title { get; set; }

        [Required]
        [MaxLength(40)]
        public string Description { get; set; }

        [Required]
        [MaxLength(40)]
        public string ButtonText { get; set; }

        [Required]
        public int TextPosition { get; set; }

        [Required]
        public string ThumpnailUrl { get; set; }
    }
}
