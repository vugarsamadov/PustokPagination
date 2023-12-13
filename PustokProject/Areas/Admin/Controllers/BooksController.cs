using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PustokProject.CoreModels;
using PustokProject.Persistance;
using PustokProject.ViewModels;
using PustokProject.ViewModels.Books;

namespace PustokProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BooksController : Controller
    {
        public BooksController(ApplicationDbContext context)
        {
            Context = context;
        }

        public ApplicationDbContext Context { get; }

        public async Task<IActionResult> Index()
        {
            var pagenumber = 1;
            var take = 3;

            var count = await Context.Books
                .CountAsync();

            var items = await Context.Books
                .Include(b => b.Category)
                .Include(b => b.BookAuthors)
                .ThenInclude(a => a.Author)
                .Skip((pagenumber-1) * take)
                .Take(take)
                .ToListAsync();

            var page = new VM_PaginatedEntityTable<Book>();
            page.Items = items;
            page.PageCount = (int)Math.Ceiling((decimal)count / take);
            page.HasPrev = pagenumber > 1;
            page.HasNext = pagenumber < page.PageCount;
            page.CurrentPage = pagenumber;

            var routevaldic = new RouteValueDictionary();
            routevaldic["pagenumber"] = pagenumber + 1;
            routevaldic["take"] = take;

            page.NextPage = Url.Action(nameof(PaginatedBooks), routevaldic);
            routevaldic["pagenumber"] = pagenumber - 1;
            page.PreviousPage = Url.Action(nameof(PaginatedBooks), routevaldic);

            return View(page);
        }

        public async Task<IActionResult> PaginatedBooks(int pagenumber,int take)
        {
            var count = await Context.Books
                .CountAsync();

            var items = await Context.Books
                .Include(b => b.Category)
                .Include(b => b.BookAuthors)
                .ThenInclude(a => a.Author)
                .Skip((pagenumber-1)*take)
                .Take(take)
                .ToListAsync();

            var page = new VM_PaginatedEntityTable<Book>();
            page.Items = items;
            page.PageCount = (int)Math.Ceiling((decimal)count / take);
            page.HasPrev = pagenumber > 1;
            page.HasNext = pagenumber < page.PageCount;
            page.CurrentPage = pagenumber;

            var routevaldic = new RouteValueDictionary();
            routevaldic["pagenumber"] = pagenumber + 1;
            routevaldic["take"] = take;

            page.NextPage = Url.Action(nameof(PaginatedBooks), routevaldic);
            routevaldic["pagenumber"] = pagenumber - 1;
            page.PreviousPage = Url.Action(nameof(PaginatedBooks), routevaldic);

            return PartialView("_PaginatedColumnsBooks",page);
        }



        public async Task<IActionResult> CreateBook()
        {
            var brands= await Context.Brands.ToListAsync();
            var categories= await Context.Categories.Where(c =>c.ParentId != null).ToListAsync();
            ViewBag.Brands = new SelectList(brands,"Id","Name","SelectBrand");
            ViewBag.Categories = new SelectList(categories,"Id","Name","SelectCategory");
            
            var model = new VM_CreateBook();
            model.Authors = new SelectList(await Context.Authors.ToListAsync(),"Id","Name");

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook(VM_CreateBook model)
        {
            var brands= await Context.Brands.ToListAsync();
            ViewBag.Brands = new SelectList(brands,"Id","Name","SelectBrand");
            var categories= await Context.Categories.Where(c =>c.ParentId != null).ToListAsync();
            ViewBag.Categories = new SelectList(categories,"Id","Name","SelectCategory");

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (!await Context.Authors.AllAsync(c => model.AuthorIds.Contains(c.Id)))
            {
                ModelState.AddModelError("AuthorIds","Author ids are invalid!");
                model.Authors = new SelectList(await Context.Authors.ToListAsync(),"Id","Name");
                return View(model);
            }
            
            var book = new Book();
            book.Name = model.Name;
            book.Description = model.Description;
            book.Price = model.Price;
            
            book.CategoryId = model.CategoryId;
            book.DiscountPercentage = model.DiscountPercentage;
            book.IsAvailable = model.IsAvailable == "true";
            book.ProductCode = model.ProductCode;

            book.BookAuthors = model.AuthorIds.Select(id => new BookAuthor()
            {
                AuthorId = id
            }).ToList();

            var imageNameCover = await model.ImageFileCover.SaveToRootWithUniqueNameAsync();
            var imageNameBack = await model.ImageFileBack.SaveToRootWithUniqueNameAsync();
            
            var imageCover = new BookImage();
            imageCover.ImagePath = imageNameCover;

            var imageBack = new BookImage();
            imageBack.ImagePath = imageNameBack;
            
            book.CoverImageUrl = imageNameCover;
            book.BackImageUrl = imageNameBack;
            
            
            book.Images.Add(imageCover);
            book.Images.Add(imageBack);
            
            await Context.Books.AddAsync(book);
            await Context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> UpdateBook(int id)
        {
            
            var brands= await Context.Brands.ToListAsync();
            ViewBag.Brands = new SelectList(brands,"Id","Name","SelectBrand");
            var categories= await Context.Categories.Where(c =>c.ParentId != null).ToListAsync();
            ViewBag.Categories = new SelectList(categories,"Id","Name","SelectCategory");
            
            var model = new VM_UpdateBook();

            var book = await Context.Books.FirstOrDefaultAsync(b => b.Id == id);
            model.Authors = new SelectList(await Context.Authors.ToListAsync(),"Id","Name");

            if (book == null)
            {
                ModelState.AddModelError("Book","Book Not found!");
                return View(model);
            }
            
            model.Name = book.Name;
            model.Description = book.Description;
            model.Price = book.Price;
            model.CategoryId = book.CategoryId;
            model.DiscountPercentage = book.DiscountPercentage;
            model.IsAvailable = book.IsAvailable;
            model.ProductCode = book.ProductCode;

            model.CoverImageUrl = book.CoverImageUrl;
            model.BackImageUrl = book.BackImageUrl;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateBook(int id,VM_UpdateBook model)
        {
            var brands= await Context.Brands.ToListAsync();
            ViewBag.Brands = new SelectList(brands,"Id","Name","SelectBrand");
            var categories= await Context.Categories.Where(c =>c.ParentId != null).ToListAsync();
            ViewBag.Categories = new SelectList(categories,"Id","Name","SelectCategory");

            
            if (!ModelState.IsValid)
            {
                model.Authors = new SelectList(await Context.Authors.ToListAsync(),"Id","Name");
                return View(model);
            }
            var book = await Context.Books.Include(b=>b.BookAuthors).FirstOrDefaultAsync(b => b.Id == id);
            if (book == null)
            {
                ModelState.AddModelError("Book","Book Not found!");
                return View(model);
            }
                            book.Name = model.Name;
                            book.Description = model.Description;
                            book.Price = model.Price;
                            book.CategoryId = model.CategoryId;
                            book.DiscountPercentage = model.DiscountPercentage;
                            book.IsAvailable = model.IsAvailable ?? false;
                            book.ProductCode = model.ProductCode;
                            if (model.AuthorIds != null)
                                book.BookAuthors = model.AuthorIds.Select(id => new BookAuthor()
                                {
                                    AuthorId = id
                                }).ToList();
                            // if (model.ImageFile != null)
            // {
            //     var imageName = await model.ImageFile.SaveToRootWithUniqueNameAsync();
            //     
            //     BookImage bi = new();
            //     bi.BookId = id;
            //     bi.ImagePath = imageName;
            //     //bi.Activate();
            //     await Context.BookImages.AddAsync(bi);
            //
            //     await Context.SaveChangesAsync();
            //     //book.CoverImageUrl = imageName;
            // }
            await Context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await Context.Books.FirstOrDefaultAsync(b => b.Id == id);
            if (book != null)
            {
                book.Delete();
                await Context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> RevokeDelete(int id)
        {
            var book = await Context.Books.FindAsync(id);
            if (book != null)
            {
                book.RevokeDelete();
                await Context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
