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
    public class AfiliadosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public AfiliadosController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Afiliados
        public async Task<IActionResult> Index(string busqApellido, int? busqDni)
        {
            //filtros
            var appDBcontext = _context.afiliados.Select(a => a);

            if (!string.IsNullOrEmpty(busqApellido)) { appDBcontext = appDBcontext.Where(a => a.Apellidos.Contains(busqApellido)); }
            if (busqDni.HasValue) { appDBcontext = appDBcontext.Where(a => a.DNI.ToString().Contains(busqDni.ToString())); }

            AfiliadosViewModel model = new()
            {
                Afiliados1 = [.. appDBcontext],
                Dni = busqDni,
                Apellido1 = busqApellido
            };

            return View(model);
        }
        [Authorize]
        public async Task<IActionResult> Importar()
        {
            var archivos = HttpContext.Request.Form.Files;
            if (archivos != null && archivos.Count > 0)
            {
                var archivo = archivos[0];
                if (archivo.Length > 0)
                {
                    var pathDestino = Path.Combine(_env.WebRootPath, "importaciones");
                    var archivoDestino = Guid.NewGuid().ToString().Replace("-", "");
                    archivoDestino += Path.GetExtension(archivo.FileName);
                    var rutaDestino = Path.Combine(pathDestino, archivoDestino);

                    using (var filestream = new FileStream(rutaDestino, FileMode.Create))
                    {
                        archivo.CopyTo(filestream);
                    };

                    using (var file = new FileStream(rutaDestino, FileMode.Open))
                    {
                        List<string> renglones = new List<string>();
                        List<Afiliado> afiliadoArchivo = new List<Afiliado>();

                        StreamReader fileContent = new StreamReader(file, System.Text.Encoding.Default);  
                        do
                        {
                            renglones.Add(fileContent.ReadLine());
                        }
                        while (!fileContent.EndOfStream);

                        foreach (string renglon in renglones)
                        {
                            int salida;
                            string[] datos = renglon.Split(',', ';');
                            if (datos.Count() > 0)
                            {
                                Afiliado afiliado1 = new Afiliado();
                                afiliado1.Apellidos = datos[0];
                                afiliado1.Nombres = datos[1];
                                afiliado1.DNI = int.TryParse(datos[2], out salida) ? salida : 0;
                                if (DateTime.TryParse(datos[3], out DateTime fechaNacimiento))
                                {
                                    afiliado1.fechaNacimiento = fechaNacimiento;
                                }
                                else
                                {
                                    afiliado1.fechaNacimiento = fechaNacimiento.Date;
                                }
                                afiliadoArchivo.Add(afiliado1);
                            }
                        }
                        if (afiliadoArchivo.Count > 0)
                        {
                            _context.afiliados.AddRange(afiliadoArchivo);
                            _context.SaveChanges();
                        }

                        ViewBag.cantReng = afiliadoArchivo.Count + " de " + renglones.Count;
                    }
                }
            }
            var afiliadosList = await _context.afiliados.ToListAsync();
            AfiliadosViewModel model = new AfiliadosViewModel
            {
                Afiliados1 = afiliadosList,
                Dni = null,
                Apellido1 = null
            };
            return View("Index", model);
        }

        // GET: Afiliados/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var afiliado = await _context.afiliados
                .FirstOrDefaultAsync(m => m.Id == id);
            if (afiliado == null)
            {
                return NotFound();
            }

            return View(afiliado);
        }

        // GET: Afiliados/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Afiliados/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Apellidos,Nombres,DNI,fechaNacimiento,foto")] Afiliado afiliado)
        {
            if (ModelState.IsValid)
            {
                var archivos = HttpContext.Request.Form.Files;
                if (archivos != null && archivos.Count > 0)
                {
                    var archivoFoto = archivos[0];
                    var pathDestino = Path.Combine(_env.WebRootPath, "images");
                    if (archivoFoto.Length > 0)
                    {
                        //genera un nombre aleatorio para el archivo
                        var archivoDestino = Guid.NewGuid().ToString().Replace("-", "");
                        archivoDestino += Path.GetExtension(archivoFoto.FileName);

                        using (var filestream = new FileStream(Path.Combine(pathDestino, archivoDestino), FileMode.Create))
                        {
                            archivoFoto.CopyTo(filestream);
                            afiliado.foto = archivoDestino;
                        };
                    }
                }

                _context.Add(afiliado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(afiliado);
        }

        // GET: Afiliados/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var afiliado = await _context.afiliados.FindAsync(id);
            if (afiliado == null)
            {
                return NotFound();
            }
            return View(afiliado);
        }

        // POST: Afiliados/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Apellidos,Nombres,DNI,fechaNacimiento,foto")] Afiliado afiliado)
        {
            if (id != afiliado.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var archivos = HttpContext.Request.Form.Files;
                if (archivos != null && archivos.Count > 0)
                {
                    var archivoFoto = archivos[0];
                    var pathDestino = Path.Combine(_env.WebRootPath, "images");
                    if (archivoFoto.Length > 0)
                    {
                        //genera un nombre aleatorio para el archivo
                        var archivoDestino = Guid.NewGuid().ToString().Replace("-", "");
                        archivoDestino += Path.GetExtension(archivoFoto.FileName);

                        if (!string.IsNullOrEmpty(afiliado.foto))
                        {
                            string fotoAnterior = Path.Combine(pathDestino, afiliado.foto);
                            if(System.IO.File.Exists(fotoAnterior))
                                System.IO.File.Delete(fotoAnterior);
                        }

                        using (var filestream = new FileStream(Path.Combine(pathDestino, archivoDestino), FileMode.Create))
                        {
                            archivoFoto.CopyTo(filestream);
                            afiliado.foto = archivoDestino;
                        };
                    }
                }
                try
                {
                    _context.Update(afiliado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AfiliadoExists(afiliado.Id))
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
            return View(afiliado);
        }

        // GET: Afiliados/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var afiliado = await _context.afiliados
                .FirstOrDefaultAsync(m => m.Id == id);
            if (afiliado == null)
            {
                return NotFound();
            }

            return View(afiliado);
        }

        // POST: Afiliados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var afiliado = await _context.afiliados.FindAsync(id);
            if (afiliado != null)
            {
                _context.afiliados.Remove(afiliado);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AfiliadoExists(int id)
        {
            return _context.afiliados.Any(e => e.Id == id);
        }
    }
}
