using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using VideoVerhuur_ASP.net_MVC_Test.Repositorys;
using VideoVerhuur_ASP.net_MVC_Test.Models;

namespace VideoVerhuur_ASP.net_MVC_Test.Controllers
{

    public class HomeController : Controller
    {

        private VideoVerhuurRepository _videoVerhuurRepository = null;
        

        public HomeController()
        {
            _videoVerhuurRepository = new VideoVerhuurRepository();
            
               
        }


        public ActionResult Index(string Betaling)
        {
            if(Betaling != null)
            {
                foreach(string nummer in Session)
                {
                    if (int.TryParse(nummer, out int id))
                    {

                        var klant = (VideoVerhuur_ASP.net_MVC_Test.Models.Klant)Session["klant"];
                        _videoVerhuurRepository.Verhuring(id, klant);
                    }
                }
                TempData["Betaling"] = Betaling;
            }

            Session.Clear();
            return View();
        }

        [HttpPost]
        public ActionResult Bevestiging(string naam, string postcode)
        {
            var klant = _videoVerhuurRepository.GetKlant(naam, postcode);
            if(klant != null)
            {
                Session["Klant"] = klant;
               
                TempData["LoginValid"] = "Welkom " + klant.Naam + " " + klant.Voornaam;
                return View(klant);
            }
            else
            {
                TempData["ErrorMessage"] = "Foutieve gegevens gelieve opnieuw in te loggen";
                return RedirectToAction("Index", "Home");
            }
           
        }

        public ActionResult VerhuurIndex()
        {
            var Genres = _videoVerhuurRepository.GetGenres();
            return View(Genres);
        }

        public ActionResult GenreDetail(int? id)
        {
            var FilmsOpGenre = _videoVerhuurRepository.GetFilmsOpGenre(id);
            return View(FilmsOpGenre);
        }

     
        public ActionResult Mandje(int? id)
        {
            var Film = _videoVerhuurRepository.GetFilm(id);
            if (Film != null)
            {
                Session[id.ToString()] = Film.BandNr;
            }
                decimal teBetalen = 0;
                List<Film> mandjeItems = new List<Film>();
                foreach(string nummer in Session)
                {
                    int BandNr;
                    if(int.TryParse(nummer, out BandNr))
                    {
                        Film film = _videoVerhuurRepository.GetFilm(BandNr);
                        if(film != null)
                        {
                            mandjeItems.Add(film);
                            teBetalen += film.Prijs;
                        }
                    }
                }
                ViewBag.teBetalen = teBetalen;
                return View(mandjeItems);      
        }

        public ActionResult Verwijderen(string id)
        {   
            var film = _videoVerhuurRepository.GetFilm(Convert.ToInt32(id));
            return View(film);
        }

        public ActionResult VerwijderenConfirm(string id)
        {
            if (id != null)
            {
                Session.Remove(id);
            }

            return RedirectToAction("Mandje", "Home");
        }


        public ActionResult Afrekenen()
        {
            decimal teBetalen = 0;
            var afrekenenModel = new AfrekenenModel();
            afrekenenModel.films = new List<Film>();
            foreach (string nummer in Session)
            {
                int BandNr;
                if (int.TryParse(nummer, out BandNr))
                {
                    Film film = _videoVerhuurRepository.GetFilm(BandNr);
                    if (film != null)
                    {
                        afrekenenModel.films.Add(film);
                        teBetalen += film.Prijs;
                    }
                }
            }

            var klant = (VideoVerhuur_ASP.net_MVC_Test.Models.Klant)Session["klant"];
            afrekenenModel.klant = _videoVerhuurRepository.GetKlantById(klant.KlantNr);
            ViewBag.teBetalen = teBetalen;
            return View(afrekenenModel);
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}