using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PustokProject.CoreModels;
using PustokProject.Persistance;
using PustokProject.ViewModels;
using PustokProject.ViewModels.Categories;

namespace PustokProject.Areas.Admin.Controllers;

[Area("Admin")]
public class CategoriesController : Controller
{
    private readonly ApplicationDbContext _context;

    public CategoriesController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<IActionResult> Index()
    {
        var pagenumber = 1;
        var take = 3;

        var count = await _context.Categories
                .CountAsync();

        var categories = await _context.Categories
            .Include(c=>c.Parent)
            .Skip((pagenumber - 1) * take)
                .Take(take)
                .ToListAsync();

        var page = new VM_PaginatedEntityTable<Category>();
        page.Items = categories;
        page.PageCount = (int)Math.Ceiling((decimal)count / take);
        page.HasPrev = pagenumber > 1;
        page.HasNext = pagenumber < page.PageCount;
        page.CurrentPage = pagenumber;

        var routevaldic = new RouteValueDictionary();
        routevaldic["pagenumber"] = pagenumber + 1;
        routevaldic["take"] = take;

        page.NextPage = Url.Action(nameof(PaginatedCategories), routevaldic);
        routevaldic["pagenumber"] = pagenumber - 1;
        page.PreviousPage = Url.Action(nameof(PaginatedCategories), routevaldic);

        return View(page);
    }

    public async Task<IActionResult> PaginatedCategories(int pagenumber,int take)
    {
        var count = await _context.Categories
                .CountAsync();

        var categories = await _context.Categories
            .Include(c => c.Parent)
            .Skip((pagenumber - 1) * take)
                .Take(take)
                .ToListAsync();

        var page = new VM_PaginatedEntityTable<Category>();
        page.Items = categories;
        page.PageCount = (int)Math.Ceiling((decimal)count / take);
        page.HasPrev = pagenumber > 1;
        page.HasNext = pagenumber < page.PageCount;
        page.CurrentPage = pagenumber;

        var routevaldic = new RouteValueDictionary();
        routevaldic["pagenumber"] = pagenumber + 1;
        routevaldic["take"] = take;

        page.NextPage = Url.Action(nameof(PaginatedCategories), routevaldic);
        routevaldic["pagenumber"] = pagenumber - 1;
        page.PreviousPage = Url.Action(nameof(PaginatedCategories), routevaldic);

        return PartialView("_PaginatedColumnsCategories",page);
    }


    public async Task<IActionResult> CreateCategory()
    {
        var categories = await _context.Categories
            .Include(c=>c.Parent)
            .ToListAsync();
        ViewBag.Categories = new SelectList(categories,"Id","Name","SelectCategory");
        
        var model = new VM_CreateCategory();
        return View(model);
    }
    [HttpPost]
    public async Task<IActionResult> CreateCategory(VM_CreateCategory model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        var category = new Category();
        category.Name = model.Name;
        category.ParentId = model.ParentId;
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();  
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> UpdateCategory(int id)
    {
        
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        var model = new VM_UpdateCategory();
        if (category == null)
        {
            ModelState.AddModelError("Category","Category Not found!");
            return View(model);
        }
        model.Name = category.Name;
        model.ParentId = category.ParentId;
        
        var categories = await _context.Categories
            .Include(c=>c.Parent)
            .Where(c=>c.Id != id)
            .ToListAsync();
        ViewBag.Categories = new SelectList(categories,"Id","Name","SelectCategory");

        return View(model);
    }
    [HttpPost]
    public async Task<IActionResult> UpdateCategory(int id,VM_UpdateCategory model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        if (category == null)
        {
            ModelState.AddModelError("Category","Category Not found!");
            return View(model);
        }
        category.Name = model.Name;
        category.ParentId = model.ParentId;
        await _context.SaveChangesAsync();
        var categories = await _context.Categories
            .Include(c=>c.Parent)
            .Where(c=>c.Id != id)
            .ToListAsync();
        
        ViewBag.Categories = new SelectList(categories,"Id","Name","SelectCategory");
    
        return View(model);
    }
    
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        if (category != null)
        {
            
        await _context.Categories.Where(c => c.ParentId == id).ForEachAsync(c => c.Delete());
        category.Delete();
        await _context.SaveChangesAsync();

        }
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> RevokeDelete(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category != null)
        {
            category.RevokeDelete();
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

}