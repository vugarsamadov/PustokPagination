//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using PustokProject.Persistance;
//using PustokProject.ViewModels.Blogs;

//namespace PustokProject.Areas.Admin.Controllers
//{
//    [Area("Admin")]
//    public class BlogsController : Controller
//    {

//        public BlogsController(ApplicationDbContext  context)
//        {
//            Context = context;
//        }

//        public ApplicationDbContext Context { get; }


//        public async Task<IActionResult> Index()
//        {
//            var vm = new VM_BlogIndex();
//            vm.Blogs = await Context.Blogs.ToListAsync();

//            return View(vm);
//        }
        
//    }
//}
