using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebAppNetCore1.Models;

namespace WebAppNetCore1.Controllers
{
    public class Estudiantes : Controller
    {
        public IActionResult Index()
        {
            List<Alumno> alumnos = new List<Alumno>();

            HttpClient clienteHttp = new HttpClient();
            clienteHttp.BaseAddress = new Uri("https://localhost:44306/api/");
            var request = clienteHttp.GetAsync("Alumnoes/ListarAlumnos").Result;

            if (request.IsSuccessStatusCode)
            {
                var resultString = request.Content.ReadAsStringAsync().Result;
                alumnos = JsonConvert.DeserializeObject<List<Alumno>>(resultString);
            }
            return View(alumnos);
        }

        public IActionResult Editar(int? id)
        {
            Alumno alumno = new Alumno();

            HttpClient clienteHttp = new HttpClient();
            clienteHttp.BaseAddress = new Uri("https://localhost:44306/api/");
            var request = clienteHttp.GetAsync("Alumnoes/BuscarAlumno/" +id).Result;

            if (request.IsSuccessStatusCode)
            {
                var resultString = request.Content.ReadAsStringAsync().Result;
                alumno = JsonConvert.DeserializeObject<Alumno>(resultString);
            }
            return View(alumno);
        }
        public IActionResult CrearEstudiante()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAlumno(int id, [Bind("IdAlumno,Nombres,Apellidos,Edad")] Alumno alumno)
        {
            if (ModelState.IsValid)
            {
                Request peticion = new Request();
                string respuesta = peticion.Send("https://localhost:44306/api/" + "Alumnoes/EditarAlumnos", alumno);
                //add el metodo para guardar el archivo
                // _context.Add(alumno);
                // await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(alumno);

        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("IdAlumno,Nombres,Apellidos,Edad")] Alumno alumno)
        {
            if (ModelState.IsValid)
            {
                Request peticion = new Request();
                string respuesta = peticion.Send("https://localhost:44306/api/" + "Alumnoes/RegistrarAlumnos", alumno);
                //add el metodo para guardar el archivo
                // _context.Add(alumno);
                // await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(alumno);
        }

        public IActionResult Eliminar(int id)
        {
            Alumno alumno = new Alumno();

            HttpClient clienteHttp = new HttpClient();
            clienteHttp.BaseAddress = new Uri("https://localhost:44306/api/");
            var request = clienteHttp.DeleteAsync("Alumnoes/EliminarAlumno/" + id).Result;

            if (request.IsSuccessStatusCode)
            {
                var resultString = request.Content.ReadAsStringAsync().Result;
                alumno = JsonConvert.DeserializeObject<Alumno>(resultString);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
