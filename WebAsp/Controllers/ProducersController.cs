using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAsp.Data;

namespace WebAsp.Controllers
{
    public class ProducersController : Controller
    {
        private readonly AppDbContext _context;

        public ProducersController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var allProducers = await _context.Producers.ToListAsync();
            return View();
        }
    }
}
