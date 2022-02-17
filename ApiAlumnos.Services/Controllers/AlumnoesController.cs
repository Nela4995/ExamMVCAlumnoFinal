using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiAlumnos.Services.Datos;

namespace ApiAlumnos.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnoesController : ControllerBase
    {
        private readonly AlumnosContext _context;

        public AlumnoesController(AlumnosContext context)
        {
            _context = context;
        }

        [Route("ListarAlumnos")]
        // GET: api/Alumnoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Alumno>>> GetAlumnos()
        {
            return await _context.Alumnos.ToListAsync();
        }

        // GET: api/Alumnoes/5
        [Route("BuscarAlumno/{id:int}")]
        [HttpGet]
        public async Task<ActionResult<Alumno>> GetAlumno(int id)
        {
            var alumno = await _context.Alumnos.FindAsync(id);

            if (alumno == null)
            {
                return NotFound();
            }

            return alumno;
        }

        [Route ("EditarAlumnos")]
        [HttpPost]
        public async Task<String> EditAlumno(Alumno alumno) {

            try
            {
                _context.Update(alumno);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlumnoExists(alumno.IdAlumno))
                {
                    return ("error");
                }
                else
                {
                    throw;
                }
            }
            return ("ok");

        }


        // PUT: api/Alumnoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlumno(int id, Alumno alumno)
        {
            if (id != alumno.IdAlumno)
            {
                return BadRequest();
            }

            _context.Entry(alumno).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlumnoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Alumnoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("RegistrarAlumnos")]
        [HttpPost]
        public async Task<string> PostAlumno(Alumno alumno)
        {
            _context.Alumnos.Add(alumno);
            await _context.SaveChangesAsync();

            return "ok";
        }

        // DELETE: api/Alumnoes/5
        [Route("EliminarAlumno/{id:int}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAlumno(int id)
        {
            var alumno = await _context.Alumnos.FindAsync(id);
            if (alumno == null)
            {
                return NotFound();
            }

            _context.Alumnos.Remove(alumno);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlumnoExists(int id)
        {
            return _context.Alumnos.Any(e => e.IdAlumno == id);
        }
    }
}
