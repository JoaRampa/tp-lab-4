using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using tp_lab_4.Data;
using tp_lab_4.Models;
using tp_lab_4.ViewsModels;

namespace tp_lab_4.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index(int page=1)
        {
            var pageSize = 3; // Número de elementos por página

            var appDBcontext = _context.TicketDetalles
                .Include(t => t.Tickets)
                .ThenInclude(h => h.Afiliados)
                .Include(a => a.Estados).Where(td => td.Estados.Descripcion == "Pendiente")
                .OrderBy(ts => ts.Tickets.FechaSolicitud)
                .AsQueryable();
            var totalTickets = await appDBcontext.CountAsync();
            var tickets = await appDBcontext.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            

            var model = new HomeViewModel
            {
                TicketDt1 = tickets,
                TotalPages = (int)Math.Ceiling(totalTickets / (double)pageSize),
                CurrentPage = page
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
