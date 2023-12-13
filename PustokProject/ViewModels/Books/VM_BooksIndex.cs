using Microsoft.AspNetCore.Mvc.Rendering;
using PustokProject.CoreModels;

namespace PustokProject.ViewModels.Books
{
    public class VM_BooksIndex
    {
        public ICollection<Book> Books { get; set; }
      

    }
}
