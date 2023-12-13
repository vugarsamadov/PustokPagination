using PustokProject.CoreModels;

namespace PustokProject.ViewModels.Books.Non_Admin;

public class VM_BookDetail
{
    public string BookName { get; set; }

    public string ProductCode { get; set; }
    public bool IsAvailable { get; set; }

    public decimal Price { get; set; }
    public decimal? DiscountPercentage { get; set; }
    public decimal? DiscountedPrice { get => !(DiscountPercentage is null or 0m) ? Price * (1 - DiscountPercentage / 100) : Price; }
    public string Description { get; set; }
    public IEnumerable<string> Tags { get; set; }
    public IEnumerable<Author> Authors { get; set; }
    public IEnumerable<BookImage> BookImages { get; set; }
}