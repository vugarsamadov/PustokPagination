using System.ComponentModel.DataAnnotations;
using PustokProject.CoreModels;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PustokProject.ViewModels.Books
{
    public class VM_CreateBook
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public string ProductCode { get; set; }
        
        public string? IsAvailable { get; set; }

        public string? CoverImageUrl { get; set; }
        [Required]
        [Range(0, 10000)]
        public decimal Price { get; set; }
        [Range(0, 100)]
        public decimal? DiscountPercentage { get; set; }

        [Required]
        public IFormFile  ImageFileCover{ get; set; }

        [Required]
        public IFormFile ImageFileBack
        {
            get; set;
        }
        
        public SelectList? Authors { get; set; }

        public ICollection<int>? AuthorIds { get; set; }

    }
}
