using Microsoft.Build.Evaluation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mime;

namespace PustokProject.CoreModels
{
    public class Book : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProductCode { get; set; }
        public bool IsAvailable { get; set; }
        public decimal Price { get; set; }
        [Column(TypeName= "decimal(4,2)")]
        public decimal? DiscountPercentage { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public List<BookImage> Images { get; set; } = new();


        public void SetCoverImage(string iamgeUrl)
        {
            CoverImageUrl = iamgeUrl;
        }

        public void SetBackImage(string iamgeUrl)
        {
            BackImageUrl = iamgeUrl;
        }

        public string CoverImageUrl { get; set; }
        public string BackImageUrl { get; set; }


        [NotMapped]
        public decimal? DiscountedPrice { get => !(DiscountPercentage is null or 0m) ? Price * (1 - DiscountPercentage / 100) : Price; }

        public ICollection<BookAuthor> BookAuthors { get; set; }

    }
}
