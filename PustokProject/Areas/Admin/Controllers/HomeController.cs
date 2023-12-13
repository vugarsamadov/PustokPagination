using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokProject.CoreModels;
using PustokProject.Enums;
using PustokProject.Persistance;
using PustokProject.ViewModels;
using PustokProject.ViewModels.Sliders;
using System.Text.Json.Nodes;

namespace PustokProject.Areas.Home.Controllers
{

    [Area("Admin")]
    public class HomeController : Controller
    {

        public ApplicationDbContext _context { get; }

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index()
        {
            var vm = new VM_SlidersIndex();
            vm.PageTitle = "Admin index";
            var sliders = await _context.Sliders.ToListAsync();
            vm.Sliders = sliders;
            return View(vm);            
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(VM_CreateSlider createModel)
        {
            if (ModelState.IsValid)
            {
                var slider = new Slider();
                slider.Title = createModel.Title;
                slider.Description = createModel.Description;
                slider.ThumpnailUrl = createModel.ThumpnailUrl;
                slider.ButtonText = createModel.ButtonText;
                slider.TextPosition = (HeroAreaTextPosition) createModel.TextPosition;
                await _context.Sliders.AddAsync(slider);
                await _context.SaveChangesAsync();
            }
            return View(createModel);
        }
        
        
        [HttpGet]
        public async Task<IActionResult> UpdateSlider(int id)
        {
            var vm = new VM_UpdateSlider();
            var slider = await _context.Sliders.FindAsync(id);
            vm.Id = slider.Id;
            vm.Title = slider.Title;
            vm.Description = slider.Description;
            vm.ButtonText = slider.ButtonText;
            vm.TextPosition = (int)slider.TextPosition;
            vm.ThumpnailUrl = slider.ThumpnailUrl;

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateSlider(VM_UpdateSlider model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var slider = await _context.Sliders.FindAsync(model.Id);
            slider.Description = model.Description;
            slider.Title = model.Title;
            slider.ButtonText = model.ButtonText;
            slider.ThumpnailUrl = model.ThumpnailUrl;
            slider.TextPosition = (HeroAreaTextPosition)model.TextPosition;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index)); 
        }
    
        public async Task<IActionResult> DeleteSlider(int id)
        {
            var slider = await _context.Sliders.FindAsync(id);
            if (slider != null)
            {
                slider.Delete();
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> RevokeDelete(int id)
        {
            var slider = await _context.Sliders.FindAsync(id);
            if (slider != null)
            {
                slider.RevokeDelete();
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        
        
        
    }
}
