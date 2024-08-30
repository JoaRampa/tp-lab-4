using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using tp_lab_4.Data;
using tp_lab_4.Models;
using tp_lab_4.ViewsModels;
namespace tp_lab_4.Controllers
{
    public class TicketsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TicketsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tickets
        public async Task<IActionResult> Index(string busqObs, int? busqAfiliado, int page = 1)
        {
            var pageSize = 3; // Número de elementos por página

            var appDBcontext = _context.tickets.Include(t => t.Afiliados).AsQueryable();

            if (!string.IsNullOrEmpty(busqObs))
            {
                appDBcontext = appDBcontext.Where(a => a.Observacion.Contains(busqObs));
            }
            if (busqAfiliado.HasValue)
            {
                appDBcontext = appDBcontext.Where(a => a.AfiliadoId == busqAfiliado.Value);
            }

            var totalTickets = await appDBcontext.CountAsync();
            var tickets = await appDBcontext
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var model = new TicketViewModel
            {
                Tickets1 = tickets,
                AfiliadoId = busqAfiliado,
                Observacion1 = busqObs,
                TotalPages = (int)Math.Ceiling(totalTickets / (double)pageSize),
                CurrentPage = page
            };

            ViewData["AfiliadoId"] = new SelectList(_context.afiliados, "Id", "DNI", busqAfiliado);

            return View(model);
        }

        // GET: Tickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.tickets
                .Include(t => t.Afiliados)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // GET: Tickets/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["AfiliadoId"] = new SelectList(_context.afiliados, "Id", "DNI");
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,AfiliadoId,FechaSolicitud,Observacion")] Ticket modelo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(modelo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AfiliadoId"] = new SelectList(_context.afiliados, "Id", "DNI", modelo.AfiliadoId);
            return View(modelo);
        }

        // GET: Tickets/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            ViewData["AfiliadoId"] = new SelectList(_context.afiliados, "Id", "Id", ticket.AfiliadoId);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AfiliadoId,FechaSolicitud,Observacion")] Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AfiliadoId"] = new SelectList(_context.afiliados, "Id", "Id", ticket.AfiliadoId);
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.tickets
                .Include(t => t.Afiliados)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _context.tickets.FindAsync(id);
            if (ticket != null)
            {
                _context.tickets.Remove(ticket);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(int id)
        {
            return _context.tickets.Any(e => e.Id == id);
        }
    }
}
