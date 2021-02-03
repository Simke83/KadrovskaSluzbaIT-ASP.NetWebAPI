using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KadrovskaSluzbaITKompanije.Models
{
    public class Zaposleni
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50,ErrorMessage="Ime i prezime ne sme da prevazilazi 50 karaktera!")]
        public string ImeIPrezime { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "Rola ne sme da prevazilazi 30 karaktera!")]
        public string Rola { get; set; }
        [Required]
        [Range(1955,2000,ErrorMessage ="Godina rodjenja mora biti u opsegu 1955-2000")]
        public int GodinaRodjenja { get; set; }
        [Required]
        [Range(2000,2020, ErrorMessage = "Godina zaposlenja mora biti u opsegu 2000-2020")]
        public int GodinaZaposlenja { get; set; }
        [Required]
        [Range(600.00,5000.00,ErrorMessage ="Plata mora biti u opsegu 600-5000")]
        public decimal Plata { get; set; }
        public OrganizacionaJedinica OrganizacionaJedinica{ get; set; }
        public int OrganizacionaJedinicaId { get; set; }
    }
}