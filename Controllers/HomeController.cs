using Microsoft.AspNetCore.Mvc;
using spendsmart.Models;
using System.Diagnostics;

namespace spendsmart.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly SpendSmartDbContext _context;

        public HomeController(ILogger<HomeController> logger, SpendSmartDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Expences() 
        {
            var allExpences = _context.Expences.ToList();

            var totalExpences = allExpences.Sum(x => x.Value);
            ViewBag.Expences = totalExpences;

            return View(allExpences);
        }

        public IActionResult CreateEditExpence(int? id)
        { 
            if(id != null)
            {
                var expenceInDb = _context.Expences.SingleOrDefault(expence => expence.Id == id);
                return View(expenceInDb);
            }
            return View();
        }

        public IActionResult DeleteExpence(int id)
        {
            var expenceInDb = _context.Expences.SingleOrDefault(expence => expence.Id == id);
            _context.Expences.Remove(expenceInDb);
            _context.SaveChanges();
            return RedirectToAction("Expences");
        }
        public IActionResult CreateEditExpenceForm(Expence model)
        {
            if(model.Id==0)
            {
                _context.Expences.Add(model);
            }
            else
            {
                _context.Expences.Update(model);
            }
            _context.SaveChanges();
            
            return RedirectToAction("Expences");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
