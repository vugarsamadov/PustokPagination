using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokProject.Persistance;
using PustokProject.ViewModels.Books.Non_Admin;

namespace PustokProject.Controllers;

public class BookDetails : Controller
{
    private readonly ApplicationDbContext _context;

    public BookDetails(ApplicationDbContext context)
    {
        _context = context;
    }
    public async  Task<IActionResult> Index(int Id)
    {
        var book = await _context.Books.Include(b => b.Images).Include(b=>b.BookAuthors).ThenInclude(v=>v.Author).FirstOrDefaultAsync(b => b.Id == Id);

        var model = new VM_BookDetail();
        model.Description = book.Description;
        model.Price = book.Price;
        model.DiscountPercentage = book.DiscountPercentage;
        model.Authors = book.BookAuthors.Select(a => a.Author);
        model.BookImages = book.Images;
        model.IsAvailable = book.IsAvailable;
        model.ProductCode = book.ProductCode;
   
        return View(model);
    }
}