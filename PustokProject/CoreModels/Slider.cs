using PustokProject.Enums;
using System.ComponentModel.DataAnnotations;

namespace PustokProject.CoreModels
{
    public class Slider : BaseModel
    {
        [Required]
        [MaxLength(30)]
        public string Title{ get; set; }
        
        [Required]
        [MaxLength(40)]
        public string Description{ get; set; }
        
        [Required]
        [MaxLength(40)]
        public string ButtonText{ get; set; }
        
        [Required]
        public HeroAreaTextPosition TextPosition { get; set; }
        
        [Required]
        public string ThumpnailUrl { get; set; }

    }
}
