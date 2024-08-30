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

namespace tp_lab_4.Controllers
{
    public class TicketDetallesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TicketDetallesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TicketDetalles
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TicketDetalles.Include(t => t.Estados).Include(t => t.Tickets);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TicketDetalles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketDetalle = await _context.TicketDetalles
                .Include(t => t.Estados)
                .Include(t => t.Tickets)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticketDetalle == null)
            {
                return NotFound();
            }

            return View(ticketDetalle);
        }

        // GET: TicketDetalles/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["EstadosId"] = new SelectList(_context.estados, "Id", "Descripcion");
            ViewData["TicketId"] = new SelectList(_context.tickets, "Id", "Observacion");
            return View();
        }

        // POST: TicketDetalles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,TicketId,DescripcionPedido,EstadosId,fechaEstado")] TicketDetalle ticketDetalle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ticketDetalle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EstadosId"] = new SelectList(_context.estados, "Id", "Descripcion", ticketDetalle.EstadosId);
            ViewData["TicketId"] = new SelectList(_context.tickets, "Id", "Observacion", ticketDetalle.TicketId);
            return View(ticketDetalle);
        }

        // GET: TicketDetalles/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketDetalle = await _context.TicketDetalles.FindAsync(id);
            if (ticketDetalle == null)
            {
                return NotFound();
            }
            ViewData["EstadosId"] = new SelectList(_context.estados, "Id", "Descripcion", ticketDetalle.EstadosId);
            ViewData["TicketId"] = new SelectList(_context.tickets, "Id", "Observacion", ticketDetalle.TicketId);
            return View(ticketDetalle);
        }

        // POST: TicketDetalles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TicketId,DescripcionPedido,EstadosId,fechaEstado")] TicketDetalle ticketDetalle)
        {
            if (id != ticketDetalle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticketDetalle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketDetalleExists(ticketDetalle.Id))
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
            ViewData["EstadosId"] = new SelectList(_context.estados, "Id", "Descripcion", ticketDetalle.EstadosId);
            ViewData["TicketId"] = new SelectList(_context.tickets, "Id", "Observacion", ticketDetalle.TicketId);
            return View(ticketDetalle);
        }

        // GET: TicketDetalles/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketDetalle = await _context.TicketDetalles
                .Include(t => t.Estados)
                .Include(t => t.Tickets)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticketDetalle == null)
            {
                return NotFound();
            }

            return View(ticketDetalle);
        }

        // POST: TicketDetalles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticketDetalle = await _context.TicketDetalles.FindAsync(id);
            if (ticketDetalle != null)
            {
                _context.TicketDetalles.Remove(ticketDetalle);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketDetalleExists(int id)
        {
            return _context.TicketDetalles.Any(e => e.Id == id);
        }
    }
}
