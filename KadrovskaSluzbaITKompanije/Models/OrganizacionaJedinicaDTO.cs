using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KadrovskaSluzbaITKompanije.Models
{
    public class OrganizacionaJedinicaDTO
    {
        public int Id { get; set; }


        public string Ime { get; set; }


        public int GodinaOsnivanja { get; set; }

        public ICollection<Zaposleni> Zaposleni { get; set; }

        public decimal ProsecnaPlata { get; set; }
    }
}