using PustokProject.CoreModels;

namespace PustokProject.ViewModels.Books.Non_Admin;

public class VM_Home
{
    public IEnumerable<Slider> Sliders { get; set; }
    public IEnumerable<Book> Books { get; set; }
    public IEnumerable<Book> BooksAbove20Perc { get; set; }
    public PagedBooksVm<IEnumerable<Book>> PagedBookVM { get; set; }
    public IEnumerable<Book> BooksChildren { get; set; }
    
}