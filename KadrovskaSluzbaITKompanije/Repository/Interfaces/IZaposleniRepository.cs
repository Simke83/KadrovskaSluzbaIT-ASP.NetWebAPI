using KadrovskaSluzbaITKompanije.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KadrovskaSluzbaITKompanije.Repository.Interfaces
{
   public interface IZaposleniRepository
    {
        IQueryable<Zaposleni> GetAll();

        Zaposleni GetById(int id);

        IQueryable<Zaposleni> GetZaposleniGodina(int rodjenje);

        void Add(Zaposleni zaposleni);

        void Update(Zaposleni zaposleni);

        void Delete(Zaposleni zaposleni);

        IQueryable<Zaposleni> PostPretraga(ZaposleniPretraga pretragaModel);
    }
}
