using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;

namespace Frontend.Controllers
{
    public class CitaController : Controller
    {
        private readonly string apiUrl = "http://localhost:34521";

        public CitaController(string apiUrl)
        {
            this.apiUrl = apiUrl;
        }

        // GET: Cita
        public ActionResult Index()
        {
            return View();
        }

        // POST: Cita/Start
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Start(string identificacion)
        {
            var data = new { identificacion };
            try
            {
                HttpClient client = new HttpClient();
                var response = client.PostAsJsonAsync(apiUrl + "/Paciente/Validate", data).Result;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseString = response.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject(responseString);
                    if (result !=null && result == "noerror") // Paciente existe
                    {
                        return RedirectToAction(nameof(List));
                    }
                    else // Paciente no existe
                    {
                        return RedirectToAction(nameof(Create),"Paciente");
                    }
                }

                ViewBag.mensaje = "Error de comunicación con el API.";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.mensaje = "Error de comunicación con el API.";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Cita
        public ActionResult List()
        {

            return View();
        }

        // GET: Cita/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Cita/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cita/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Cita/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Cita/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Cita/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Cita/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}