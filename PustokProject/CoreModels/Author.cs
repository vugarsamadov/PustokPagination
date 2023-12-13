using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace PustokProject.CoreModels
{
    public class Author : BaseModel
    {
        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string Name { get; set; }
        
        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string Surname { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; }
    }
}
    