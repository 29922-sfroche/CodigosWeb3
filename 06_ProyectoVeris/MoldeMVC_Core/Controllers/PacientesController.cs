using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoldeMVC_Core.Data;
using MoldeMVC_Core.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.IO;
using System.Linq;

namespace MoldeMVC_Core.Controllers
{
    public class PacientesController : Controller
    {
        private readonly VerisMongoContext _context;
        private readonly IWebHostEnvironment _env;

        public PacientesController(VerisMongoContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        private async Task<string> GuardarFotoAsync(IFormFile? fotoFile, string subcarpeta, string? fotoActual = null)
        {
            if (fotoFile == null || fotoFile.Length == 0)
            {
                return fotoActual ?? string.Empty;
            }

            var extensionesPermitidas = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var extension = Path.GetExtension(fotoFile.FileName).ToLowerInvariant();
            if (!extensionesPermitidas.Contains(extension))
            {
                throw new InvalidOperationException("Solo se permiten imágenes JPG, PNG o GIF.");
            }

            if (fotoFile.Length > 2 * 1024 * 1024)
            {
                throw new InvalidOperationException("La imagen no debe superar 2 MB.");
            }

            var nombreArchivo = $"{Guid.NewGuid()}{extension}";
            var carpeta = Path.Combine(_env.WebRootPath, "imaganes", subcarpeta);
            Directory.CreateDirectory(carpeta);

            var rutaCompleta = Path.Combine(carpeta, nombreArchivo);
            using (var stream = new FileStream(rutaCompleta, FileMode.Create))
            {
                await fotoFile.CopyToAsync(stream);
            }

            if (!string.IsNullOrEmpty(fotoActual))
            {
                var rutaAnterior = Path.Combine(carpeta, fotoActual);
                if (System.IO.File.Exists(rutaAnterior))
                {
                    System.IO.File.Delete(rutaAnterior);
                }
            }

            return nombreArchivo;
        }

        private bool ValidarCedulaEcuatoriana(int cedula)
        {
            string c = cedula.ToString().PadLeft(10, '0');
            if (c.Length != 10) return false;
            if (!int.TryParse(c[..2], out int provincia)) return false;
            if (provincia < 1 || provincia > 24) return false;
            if (int.Parse(c[2].ToString()) >= 6) return false;

            int[] coeficientes = { 2, 1, 2, 1, 2, 1, 2, 1, 2 };
            int suma = 0;
            for (int i = 0; i < 9; i++)
            {
                int digito = int.Parse(c[i].ToString()) * coeficientes[i];
                if (digito >= 10) digito -= 9;
                suma += digito;
            }

            int verificador = (10 - (suma % 10)) % 10;
            return verificador == int.Parse(c[9].ToString());
        }

        // GET: Pacientes
        public async Task<IActionResult> Index()
        {
            var pacientes = await _context.Pacientes
                .Find(Builders<Pacientes>.Filter.Empty)
                .SortBy(p => p.Nombre)
                .ToListAsync();

            return View(pacientes);
        }

        // GET: Pacientes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id) || !ObjectId.TryParse(id, out _))
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes
                .Find(p => p.Id == id)
                .FirstOrDefaultAsync();

            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        // GET: Pacientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pacientes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Pacientes pacientes)
        {
            pacientes.Id = ObjectId.GenerateNewId().ToString();

            if (!ValidarCedulaEcuatoriana(pacientes.Cedula))
            {
                ModelState.AddModelError(nameof(Pacientes.Cedula), "La cédula ecuatoriana ingresada no es válida.");
            }

            if (!ModelState.IsValid)
            {
                return View(pacientes);
            }

            try
            {
                var existeCedula = await _context.Pacientes
                    .Find(p => p.Cedula == pacientes.Cedula)
                    .AnyAsync();

                if (existeCedula)
                {
                    ModelState.AddModelError(nameof(Pacientes.Cedula), "Ya existe un paciente registrado con esta cédula.");
                    return View(pacientes);
                }

                pacientes.Foto = await GuardarFotoAsync(pacientes.FotoFile, "pacientes");
                await _context.Pacientes.InsertOneAsync(pacientes);

                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(nameof(Pacientes.FotoFile), ex.Message);
            }
            catch (MongoWriteException ex)
            {
                ModelState.AddModelError(string.Empty, "Error al guardar en MongoDB: " + ex.Message);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error inesperado: " + ex.Message);
            }

            return View(pacientes);
        }

        // GET: Pacientes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id) || !ObjectId.TryParse(id, out _))
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes
                .Find(p => p.Id == id)
                .FirstOrDefaultAsync();

            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        // POST: Pacientes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Pacientes pacientes)
        {
            if (string.IsNullOrEmpty(id) || !ObjectId.TryParse(id, out _))
            {
                return NotFound();
            }

            if (id != pacientes.Id)
            {
                return NotFound();
            }

        var pacienteActual = await _context.Pacientes
            .Find(p => p.Id == id)
            .FirstOrDefaultAsync();

        if (pacienteActual == null)
        {
            return NotFound();
        }

        pacientes.Foto = pacienteActual.Foto;

            if (!ValidarCedulaEcuatoriana(pacientes.Cedula))
            {
                ModelState.AddModelError(nameof(Pacientes.Cedula), "La cédula ecuatoriana ingresada no es válida.");
            }

            if (!ModelState.IsValid)
            {
                return View(pacientes);
            }

            try
            {
                var cedulaUsadaPorOtro = await _context.Pacientes
                    .Find(p => p.Cedula == pacientes.Cedula && p.Id != pacientes.Id)
                    .AnyAsync();

                if (cedulaUsadaPorOtro)
                {
                    ModelState.AddModelError(nameof(Pacientes.Cedula), "Ya existe otro paciente registrado con esta cédula.");
                    return View(pacientes);
                }

                pacientes.Foto = await GuardarFotoAsync(pacientes.FotoFile, "pacientes", pacienteActual.Foto);

                var resultado = await _context.Pacientes
                    .ReplaceOneAsync(p => p.Id == id, pacientes);

                if (resultado.MatchedCount == 0)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(nameof(Pacientes.FotoFile), ex.Message);
                pacientes.Foto = pacienteActual.Foto;
            }
            catch (MongoWriteException ex)
            {
                ModelState.AddModelError(string.Empty, "Error al actualizar en MongoDB: " + ex.Message);
                pacientes.Foto = pacienteActual.Foto;
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error inesperado: " + ex.Message);
                pacientes.Foto = pacienteActual.Foto;
            }

            return View(pacientes);
        }

        // GET: Pacientes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id) || !ObjectId.TryParse(id, out _))
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes
                .Find(p => p.Id == id)
                .FirstOrDefaultAsync();

            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        // POST: Pacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (string.IsNullOrEmpty(id) || !ObjectId.TryParse(id, out _))
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes
                .Find(p => p.Id == id)
                .FirstOrDefaultAsync();

            if (paciente == null)
            {
                return NotFound();
            }

            var resultado = await _context.Pacientes
                .DeleteOneAsync(p => p.Id == id);

            if (resultado.DeletedCount == 0)
            {
                return NotFound();
            }

            if (!string.IsNullOrWhiteSpace(paciente.Foto))
            {
                var rutaFoto = Path.Combine(_env.WebRootPath, "imaganes", "pacientes", paciente.Foto);
                if (System.IO.File.Exists(rutaFoto))
                {
                    System.IO.File.Delete(rutaFoto);
                }
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
