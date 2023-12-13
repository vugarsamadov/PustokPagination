using Microsoft.Build.Framework;
using PustokProject.CoreModels;

namespace PustokProject.ViewModels.BookImages;

public class VM_BookImagesAddImage
{
    public List<BookImage> BookImages { get; set; } = new();
    
    [Required]
    public IFormFile ImageFile { get; set; }
    
    public int BookId { get; set; }

    public string CoverImageUrl { get; set; }
    public string BackImageUrl { get; set; }
}