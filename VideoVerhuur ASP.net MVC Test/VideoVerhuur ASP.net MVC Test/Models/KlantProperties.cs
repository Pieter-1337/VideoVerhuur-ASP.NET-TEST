using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace VideoVerhuur_ASP.net_MVC_Test.Models
{
    public class KlantProperties
    {
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime DatumLid { get; set; }

     
    }
}