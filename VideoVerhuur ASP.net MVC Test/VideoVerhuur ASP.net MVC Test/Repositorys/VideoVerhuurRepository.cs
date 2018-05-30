using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideoVerhuur_ASP.net_MVC_Test.Models;

namespace VideoVerhuur_ASP.net_MVC_Test.Repositorys
{
    public class VideoVerhuurRepository
    {
        private VideoVerhuurEntities db = new VideoVerhuurEntities();

        public List<Klant> GetKlanten()
        {
            return db.Klants.OrderBy(k => k.Naam).ToList();
        }

        public bool BestaatKlant(string naam, string postcode)
        {
            naam = naam.ToUpper();
            int postcodeInt = Convert.ToInt32(postcode);
            var Klant = (from klant in db.Klants where klant.Naam == naam && klant.PostCode == postcodeInt select klant).FirstOrDefault();
            return Klant != null;
        }

        public Klant GetKlant(string naam, string postcode)
        {
            naam = naam.ToUpper();
            int postcodeInt = Convert.ToInt32(postcode);
            var Klant = (from klant in db.Klants where klant.Naam == naam && klant.PostCode == postcodeInt select klant).FirstOrDefault();
            if(Klant != null)
            {
                return Klant;
            }
            else
            {
                
                return null;
            }
        }


        public Klant GetKlantById(int? id)
        {
            var Klant = (from klant in db.Klants where klant.KlantNr == id select klant).FirstOrDefault();
            return Klant;
        }

        public List<Genre> GetGenres()
        {
            return db.Genres.OrderBy(g => g.GenreSoort).ToList();
            
        }

        public List<Film> GetFilmsOpGenre(int? id)
        {
            var FilmLijst = (from film in db.Films where film.GenreNr == id select film).ToList();
            return FilmLijst.OrderBy(f => f.Titel).ToList();
        }

        public Film GetFilm(int? id)
        {
            return db.Films.Find(id);
        }

        public void Verhuring(int? id, Klant klant)
        {
            var film = GetFilm(id);

            db.Verhuurs.Add(new Verhuur
            {
                BandNr = film.BandNr,
                KlantNr = klant.KlantNr,
                VerhuurDatum = new DateTime(System.DateTime.Now.Ticks),   
            });

            film.InVoorraad--;
            film.UitVoorraad++;
            

            db.SaveChanges();
        }
    }
}