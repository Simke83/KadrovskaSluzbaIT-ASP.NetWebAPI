using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KadrovskaSluzbaITKompanije.Models
{
    public class OrganizacionaJedinica
    {
        public int Id { get; set; }
        [Required]
        [StringLength(40,ErrorMessage="Unesite vrednost do 40 karaktera!")]
        public string Ime { get; set; }
        [Range(2010,2020,ErrorMessage ="Godina osnivanja mora biti u opsegu 2010-2020 !")]
        public int GodinaOsnivanja { get; set; }
        
        public ICollection<Zaposleni> Zaposleni{ get; set; }

    }
}