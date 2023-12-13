using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokProject.CoreModels;
using PustokProject.Persistance;
using PustokProject.ViewModels.BookImages;

namespace PustokProject.Areas.Admin.Controllers;

[Area("Admin")]
public class BookImagesController : Controller
{
    private readonly ApplicationDbContext _dbContext;

    public BookImagesController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> Index(int Id)
    {
        var book = await _dbContext.Books.Include(b=>b.Images)
            .Where(b => b.Id == Id).FirstOrDefaultAsync();
        
        var model = new VM_BookImagesAddImage();
        model.BookId = Id;
        model.CoverImageUrl = book.CoverImageUrl;
        model.BackImageUrl = book.BackImageUrl;
        
        if (book.Images != null)
        {
            model.BookImages = book.Images.ToList();
        }
        return View(model);
    }

    [HttpPost("{bookId:int}")]
    public async Task<IActionResult> AddImage(int bookId,VM_BookImagesAddImage model)
    {
        var book = await _dbContext.Books.Where(b => !b.IsDeleted & b.Id == bookId).FirstOrDefaultAsync();
        if (book is null) return NotFound();
        var imageName = await model.ImageFile.SaveToRootWithUniqueNameAsync();
        BookImage bi = new();
        bi.BookId = model.BookId;
        bi.ImagePath = imageName;
        await _dbContext.BookImages.AddAsync(bi);
        await _dbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Index), new { Id = bookId});
    }

    [HttpGet]
    public async Task<IActionResult> RevokeDelete(int Id,int imageid)
    {
        var bookImage = await _dbContext.BookImages.FirstOrDefaultAsync(bi=>bi.Id == imageid);
        bookImage.RevokeDelete();
        await _dbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Index), new{ Id = Id});
    }
    
    [HttpGet]
    public async Task<IActionResult> DeleteBookImage(int Id,int imageid)
    {
        var bookImage = await _dbContext.BookImages.FirstOrDefaultAsync(bi=>bi.Id == imageid);
        bookImage.Delete();
        await _dbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Index), new{ Id = Id});
    }

    [HttpGet]
    public async Task<IActionResult> SetAsCover(int Id,string imageName)
    {
        var book = await _dbContext.Books
            .Include(b=>b.Images)
            .Where(b => b.Id == Id).FirstOrDefaultAsync();
        if (book != null && book.Images.Any(b=>b.ImagePath == imageName))
        {
            book.SetCoverImage(imageName);
        }
        await _dbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Index), new{ Id = Id});
    }
    
    [HttpGet]
    public async Task<IActionResult> SetAsBack(int Id,string imageName)
    {
        var book = await _dbContext.Books
            .Include(b=>b.Images)
            .Where(b => b.Id == Id).FirstOrDefaultAsync();
        if (book != null && book.Images.Any(b=>b.ImagePath == imageName))
        {
            book.SetBackImage(imageName);
        }
        await _dbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Index), new{ Id = Id});
    }
    
    
}