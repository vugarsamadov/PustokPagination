using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokProject.Models;
using PustokProject.Persistance;
using System.Diagnostics;
using PustokProject.CoreModels;
using PustokProject.ViewModels.Books.Non_Admin;

namespace PustokProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public ApplicationDbContext dbContext { get; }

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext DbContext)
        {
            _logger = logger;
            dbContext = DbContext;
        }

        public async Task<IActionResult> Index()
        {
            var model = new VM_Home();
            model.Sliders = await dbContext.Sliders.Where(b=>!b.IsDeleted).ToListAsync();
            model.Books = await dbContext.Books.Where(b=>!b.IsDeleted).ToListAsync();


            var BooksAbove20Perc = await dbContext.Books.Where(b => !b.IsDeleted && b.DiscountPercentage > 20).Skip(0).Take(4).ToListAsync();
            var count = await dbContext.Books.Where(b => !b.IsDeleted && b.DiscountPercentage > 20).CountAsync();

            var hasNext = (count >= 4);

            model.PagedBookVM = new PagedBooksVm<IEnumerable<Book>>(hasNext, 0,8, BooksAbove20Perc);


           // model.BooksAbove20Perc = await dbContext.Books.Where(b=>!b.IsDeleted && b.DiscountPercentage > 20).ToListAsync();
            model.BooksChildren = await dbContext.Books
                .Include(b=>b.Category)
                .Where(b=>!b.IsDeleted && b.Category.Name == "Children")
                .ToListAsync();
            return View(model);
        }

        [HttpGet]

        public async Task<IActionResult> BookPagination(int skip=0,int take = 4)
        {

            var BooksAbove20Perc = await dbContext.Books.Where(b => !b.IsDeleted && b.DiscountPercentage > 20).Skip(skip).Take(take).ToListAsync();
            var count = await dbContext.Books.Where(b => !b.IsDeleted && b.DiscountPercentage > 20).CountAsync();

            var hasNext = (count >= skip + take);

            var model = new PagedBooksVm<IEnumerable<Book>>(hasNext,0,take+4,BooksAbove20Perc);


            return PartialView("_BooksPagedViewPartial", model);
        }


        public IActionResult Privacy()
        {
            throw new NotImplementedException("s");
        }

        
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }





    }
}