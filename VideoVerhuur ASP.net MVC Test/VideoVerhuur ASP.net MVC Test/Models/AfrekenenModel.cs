using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideoVerhuur_ASP.net_MVC_Test.Models
{
    public class AfrekenenModel
    {
        public List<Film> films { get; set; }
        public Klant klant { get; set; }
    }
}