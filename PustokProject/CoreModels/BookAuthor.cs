using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace PustokProject.CoreModels;


public class BookAuthor : BaseModel
{
    public int BookId { get; set; }
    public int AuthorId { get; set; }
    public Book Book { get; set; }
    public Author Author { get; set; }
}