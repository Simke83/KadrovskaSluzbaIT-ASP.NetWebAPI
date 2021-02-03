using KadrovskaSluzbaITKompanije.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KadrovskaSluzbaITKompanije.Repository.Interfaces
{
   public interface IOrgJedinicaRepository
    {
        IEnumerable<OrganizacionaJedinica> GetAll();

        OrganizacionaJedinica GetById(int id);

        IQueryable<OrganizacionaJedinica> GetTradicija();

        IQueryable<OrganizacionaJedinica> GetBrojnost();

        IQueryable<OrganizacionaJedinicaDTO> PostPlate(OrgJedinicaPretraga pretragaModel);
    }
}
