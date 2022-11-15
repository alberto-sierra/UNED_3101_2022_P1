using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using _3101_proyecto1.FrontEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;

namespace Frontend.Controllers
{
    public class CitaController : Controller
    {
        private readonly string apiUrl = "http://localhost:34521";

        public CitaController()
        {
        }

        // GET: Cita
        public ActionResult Index()
        {
            return View();
        }

        // POST: Cita/List
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult List(string identificacion)
        {
            try
            {
                HttpClient client = new HttpClient();
                var response = client.GetAsync(apiUrl + "/Cita/GetAllByDoc/" + identificacion).Result;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseString = response.Content.ReadAsStringAsync().Result;
                    var citumViewModel = JsonConvert.DeserializeObject<List<CitumViewModel>>(responseString);
                    if (citumViewModel != null)
                    {
                        ViewBag.IdPaciente = citumViewModel[0].IdPaciente;
                        if (citumViewModel.Count == 1 && citumViewModel[0].Id == 0)
                        {
                            return View(nameof(List), Array.Empty<CitumViewModel>());
                        }
                        return View(citumViewModel);
                    }
                }

                throw new Exception();
            }
            catch
            {
                ViewBag.mensaje = "Error de comunicación con el API.";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Cita/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Cita/Create/1
        [HttpGet]
        public IActionResult Create([Bind("Id")]int? id)
        {
            //try
            //{
                HttpClient client = new HttpClient();
                var response = client.GetAsync(apiUrl + "/Cita").Result;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseString = response.Content.ReadAsStringAsync().Result;
                    var citumViewModelList = JsonConvert.DeserializeObject<List<CitumViewModel>>(responseString);
                    if (citumViewModelList == null || citumViewModelList.Count == 0)
                    {
                        ViewBag.mensaje = "No hay disponibilidad para nuevas citas.";
                        return RedirectToAction(nameof(Index));
                    }

                    var listaItems = citumViewModelList
                        .Select(x => new SelectListItem
                        {
                            Value = x.HoraInicio.ToString(),
                            Text = x.HoraInicio.ToString(@"hh\:mm")
                        })
                        .DistinctBy(x => x.Value)
                        .ToList();

                    

                    var citumViewModel = new CitumViewModel
                    {
                        IdPaciente = (long)id,
                        ListaItems = listaItems
                    };

                    return View(citumViewModel);
                }

                throw new Exception();
            //}
            //catch
            //{
            //    ViewBag.mensaje = "Error de comunicación con el API.";
            //    return RedirectToAction(nameof(Index));
            //}
        }

        // POST: Cita/Create2
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create2([Bind("IdPaciente, HoraInicio")] CitumViewModel citumViewModel)
        {
            try
            {
                HttpClient client = new HttpClient();
                var response = client.GetAsync(apiUrl + "/Cita").Result;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseString = response.Content.ReadAsStringAsync().Result;
                    var citumViewModelList = JsonConvert.DeserializeObject<List<CitumViewModel>>(responseString);
                    if (citumViewModelList == null || citumViewModelList.Count == 0)
                    {
                        ViewBag.mensaje = "No hay disponibilidad para nuevas citas.";
                        return RedirectToAction(nameof(Index));
                    }

                    var listaItems = citumViewModelList
                        .Where(x => x.HoraInicio == citumViewModel.HoraInicio)
                        .Select(x => new SelectListItem
                    {
                        Value = x.IdEspecialidad.ToString(),
                        Text = $"{x.Especialidad}"
                    })
                        .DistinctBy(x => x.Value)
                        .ToList();

                    citumViewModel.ListaItems = listaItems;
                    return View(citumViewModel);
                }

                throw new Exception();
            }
            catch
            {
                ViewBag.mensaje = "Error de comunicación con el API.";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Cita/Create3
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create3([Bind("IdPaciente, HoraInicio, IdEspecialidad")] CitumViewModel citumViewModel)
        {
            try
            {
                HttpClient client = new HttpClient();
                var response = client.GetAsync(apiUrl + "/Cita").Result;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseString = response.Content.ReadAsStringAsync().Result;
                    var citumViewModelList = JsonConvert.DeserializeObject<List<CitumViewModel>>(responseString);
                    if (citumViewModelList == null || citumViewModelList.Count == 0)
                    {
                        ViewBag.mensaje = "No hay disponibilidad para nuevas citas.";
                        return RedirectToAction(nameof(Index));
                    }

                    var listaItems = citumViewModelList
                        .Where(x => x.HoraInicio == citumViewModel.HoraInicio
                        && x.IdEspecialidad == citumViewModel.IdEspecialidad)
                        .Select(x => new SelectListItem
                        {
                            Value = x.IdEspecialista.ToString(),
                            Text = x.NombreEspecialista
                        })
                        .DistinctBy(x => x.Value)
                        .ToList();

                    citumViewModel.ListaItems = listaItems;
                    return View(citumViewModel);
                }

                throw new Exception();
            }
            catch
            {
                ViewBag.mensaje = "Error de comunicación con el API.";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Cita/Create4
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create4([Bind("IdPaciente, IdReserva")] CitumViewModel citumViewModel)
        {
            try
            {
                HttpClient client = new HttpClient();
                var response = client.PostAsJsonAsync<CitumViewModel>(apiUrl + "/Cita",citumViewModel).Result;
                if (response.StatusCode == HttpStatusCode.Created)
                {
                    var responseString = response.Content.ReadAsStringAsync().Result;
                    var citumViewModelList = JsonConvert.DeserializeObject<CitumViewModel>(responseString);
                    if (citumViewModelList == null)
                    {
                        ViewBag.mensaje = "Cita registrada con éxito.";
                        return RedirectToAction(nameof(Index));
                    }

                    return View(citumViewModel);
                }

                throw new Exception();
            }
            catch
            {
                ViewBag.mensaje = "Error de comunicación con el API.";
                return RedirectToAction(nameof(Index));
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