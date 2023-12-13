using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.EntityFrameworkCore;
using PustokProject.CoreModels;
using PustokProject.Persistance;
using PustokProject.ViewModels;
using PustokProject.ViewModels.AuthorVMS;

namespace PustokProject.Areas.Admin.Controllers;

[Area("Admin")]
public class BookAuthorsController : Controller
{
    private readonly ApplicationDbContext _context;

    public BookAuthorsController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<IActionResult> Index()
    {

        var pagenumber = 1;
        var take = 3;

        var count = await _context.Authors
            .CountAsync();

        var authors = await _context.Authors
            .Skip((pagenumber - 1) * take)
            .Take(take)
            .ToListAsync();

        if (authors == null) return BadRequest();
        
        var model = new VM_BookAuthors();
        
        var page = new VM_PaginatedEntityTable<Author>();
        page.Items = authors;
        page.PageCount = (int)Math.Ceiling((decimal)count / take);
        page.HasPrev = pagenumber > 1;
        page.HasNext = pagenumber < page.PageCount;
        page.CurrentPage = pagenumber;

        var routevaldic = new RouteValueDictionary();
        routevaldic["pagenumber"] = pagenumber + 1;
        routevaldic["take"] = take;

        page.NextPage = Url.Action(nameof(PaginatedAuthors), routevaldic);
        routevaldic["pagenumber"] = pagenumber - 1;
        page.PreviousPage = Url.Action(nameof(PaginatedAuthors), routevaldic);
        model.PaginatedModel = page;
        return View(model);
    }

    public async Task<IActionResult> PaginatedAuthors(int pagenumber, int take)
    {
        var count = await _context.Authors
                .CountAsync();

        var authors = await _context.Authors
            .Skip((pagenumber - 1) * take)
            .Take(take)
            .ToListAsync();

        if (authors == null) return BadRequest();

        var model = new VM_BookAuthors();

        var page = new VM_PaginatedEntityTable<Author>();
        page.Items = authors;
        page.PageCount = (int)Math.Ceiling((decimal)count / take);
        page.HasPrev = pagenumber > 1;
        page.HasNext = pagenumber < page.PageCount;
        page.CurrentPage = pagenumber;

        var routevaldic = new RouteValueDictionary();
        routevaldic["pagenumber"] = pagenumber + 1;
        routevaldic["take"] = take;

        page.NextPage = Url.Action(nameof(PaginatedAuthors), routevaldic);
        routevaldic["pagenumber"] = pagenumber - 1;
        page.PreviousPage = Url.Action(nameof(PaginatedAuthors), routevaldic);
        model.PaginatedModel = page;

        return PartialView("_PaginatedColumnsBookAuthors",page);
    }


    [HttpPost]
    public async Task<IActionResult> CreateAuthor(int id,VM_BookAuthors model)
    {
        await _context.Authors.AddAsync(new Author(){Name = model.Name,Surname = model.Surname});
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index), new { id = id});
    }
    
        // public async Task<IActionResult> AddAuthor(int id,int authorId)
        // {
        //     var book = await _context.Books.Include(b => b.BookAuthors).FirstOrDefaultAsync(b => b.Id == id);
        //     if (book == null) return BadRequest();
        //     book.BookAuthors.Add(new BookAuthor(){Id = id});
        //     await _context.SaveChangesAsync();
        //     return RedirectToAction(nameof(Index), new { id = id});
        // }
    public async Task<IActionResult> DeleteAuthor(int id,int authorId)
    {
        var author = await _context.Authors
            .FirstOrDefaultAsync(a=>a.Id == authorId);
        if (author != null) author.Delete();
        
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index), new { id = id});
        }
    
    public async Task<IActionResult> RevokeDeleteAuthor(int id,int authorId)
            {
                var author = await _context.Authors
                    .FirstOrDefaultAsync(a=>a.Id == authorId);
                if (author != null) author.RevokeDelete();
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { id = id});
            }
}