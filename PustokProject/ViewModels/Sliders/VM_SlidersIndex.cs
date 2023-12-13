using System.ComponentModel.DataAnnotations;
using PustokProject.CoreModels;

namespace PustokProject.ViewModels.Sliders
{
    public class VM_SlidersIndex
    {
        [Required]
        public string PageTitle { get; set; }
        public IEnumerable<Slider> Sliders { get; set; }
    }
}
