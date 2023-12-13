using PustokProject.CoreModels;
using System.ComponentModel.DataAnnotations;

namespace PustokProject.ViewModels.Blogs
{
    public class VM_BlogIndex
    {
        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string Title { get; set; }
        [Required]
        [MaxLength(120)]
        [MinLength(3)]
        public string Description { get; set; }
        public int AuthordId { get; set; }


        public ICollection<int> TagIds { get; set; }

    }
}
