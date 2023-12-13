using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PustokProject.ViewModels.Books;

public class VM_UpdateBook
{
    [Microsoft.Build.Framework.Required]
    public string Name { get; set; }
    [Microsoft.Build.Framework.Required]
    public string Description { get; set; }
    [Microsoft.Build.Framework.Required]
    public int CategoryId { get; set; }
    
    [Microsoft.Build.Framework.Required]
    public string ProductCode { get; set; }
    [Microsoft.Build.Framework.Required]
    public bool? IsAvailable { get; set; }
    
    public string? CoverImageUrl { get; set; }
    public string? BackImageUrl { get; set; }

    [Microsoft.Build.Framework.Required]
    [Range(0, 10000,ErrorMessage = "Price value should be betwen 0 and 10000")]
    public decimal Price { get; set; }

    
    [Range(0, 100,ErrorMessage = "Percentage value should be between 0 and 100!")] 
    public decimal? DiscountPercentage { get; set; }
    public SelectList? Authors { get; set; }

    public ICollection<int>? AuthorIds { get; set; }
    //public IFormFile? ImageFile { get; set; }
}