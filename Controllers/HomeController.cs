using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using appcolegio.Models;
using appcolegio.Models.viewmodels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace appcolegio.Controllers
{
    public class HomeController : Controller
    {
        private readonly ColegioContext _DBcontext;

        public HomeController(ColegioContext context)
        {
            _DBcontext = context;
        }

        public IActionResult Index()
        {
            List<Notum> lista = _DBcontext.Nota
                .Include(e => e.oEstudiante)
                .Include(m => m.oMateria)
                .ToList();
            return View(lista);
        }
        [HttpGet]
        public IActionResult Estudiante_Detalle(int? id)
        {
            var oestudianteVM = new EstudianteVM
            {

                oNota = new Notum()
            };
            if (id.HasValue)
            {
                var nota = _DBcontext.Nota.Include(n => n.oEstudiante)
                    .Include(m => m.oMateria).FirstOrDefault(n => n.Id == id.Value);
                if (nota == null)
                {
                    return NotFound();
                }
                oestudianteVM.oNota = nota;

                //datos del estudiante 
                oestudianteVM.Nombre = nota.oEstudiante?.Nombre ?? "";
                oestudianteVM.Apellido = nota.oEstudiante?.Apellido ?? "";

                //datos materia
                oestudianteVM.NombreMateria = nota.oMateria?.NomMateria ?? "";

                //datos de la nota
                decimal n = nota.Nota ?? 0;

            }
            return View(oestudianteVM);

        }
        [HttpPost]
        public IActionResult Estudiante_Detalle(EstudianteVM oestudianteVM)
        {
            if (oestudianteVM.oNota.Id == 0)
            {
                _DBcontext.Nota.Add(oestudianteVM.oNota);
            }
            _DBcontext.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult Guardar(EstudianteVM oestudianteVM)
        {
            if (ModelState.IsValid)
            {
                if (oestudianteVM.oNota.Id == 0)
                {
                    _DBcontext.Nota.Add(oestudianteVM.oNota);
                }
                else
                {
                    _DBcontext.Nota.Update(oestudianteVM.oNota);
                }
                _DBcontext.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(oestudianteVM);

        }
        [HttpGet]
        public IActionResult Editar(int id)
        {
            var vm = new EstudianteVM();
            if (id == 0)
            {
                vm.oNota = new Notum();
            }
            else
            {
                var nota = _DBcontext.Set<Notum>()
                    .Include(n => n.oEstudiante)
                    .Include(m => m.oMateria)
                    .FirstOrDefault(n => n.Id == id);
                if (nota == null) return NotFound();
                vm.oNota = nota;
                vm.Nombre = nota.oEstudiante?.Nombre;
                vm.Apellido = nota.oEstudiante?.Apellido;
                vm.NombreMateria = nota.oMateria?.NomMateria;
                vm.Calificacion = nota.Nota;
            }

            return View("Estudiante_Detalle", vm);
        }
        [HttpPost]
        public IActionResult Editar(EstudianteVM oestudianteVM)
        {
            Console.WriteLine($"id:{oestudianteVM.oNota.Id},Nota recibida:{oestudianteVM.Calificacion}");

            var notaDb = _DBcontext.Nota
                .Include(n => n.oEstudiante)
                .Include(m => m.oMateria)
                .FirstOrDefault(n => n.Id == oestudianteVM.oNota.Id);

            if (notaDb == null) return NotFound();

            if (oestudianteVM.Calificacion.HasValue)
                notaDb.Nota = oestudianteVM.Calificacion.Value;

            _DBcontext.Update(notaDb);
            _DBcontext.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Eliminar(int Id)
        {
            var estudianteDB = _DBcontext.Estudiantes
            .Include(n => n.Nota)
            .FirstOrDefault(e => e.Id == Id);

            if (estudianteDB == null) return NotFound();

            _DBcontext.Estudiantes.Remove(estudianteDB);
            _DBcontext.SaveChanges();

            return RedirectToAction("Index");

        }
       
    }
}

