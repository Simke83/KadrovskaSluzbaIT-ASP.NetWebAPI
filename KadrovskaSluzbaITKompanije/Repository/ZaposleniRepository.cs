using KadrovskaSluzbaITKompanije.Models;
using KadrovskaSluzbaITKompanije.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data;

namespace KadrovskaSluzbaITKompanije.Repository
{
    public class ZaposleniRepository:IZaposleniRepository
    {
       private ApplicationDbContext db = new ApplicationDbContext();

        public void Add(Zaposleni zaposleni)
        {
            db.Zaposleni.Add(zaposleni);
            db.SaveChanges();
        }

        public void Delete(Zaposleni zaposleni)
        {
            db.Zaposleni.Remove(zaposleni);
            db.SaveChanges();
        }

        public IQueryable<Zaposleni> GetAll()
        {
            return db.Zaposleni.Include(x => x.OrganizacionaJedinica).OrderBy(x => x.GodinaZaposlenja);
        }

        public Zaposleni GetById(int id)
        {
            Zaposleni zaposleni = db.Zaposleni.Include(x => x.OrganizacionaJedinica).FirstOrDefault(x => x.Id == id);

            if (zaposleni == null)
            {
                return null;
            }

            return zaposleni;
        }

        public IQueryable<Zaposleni> GetZaposleniGodina(int rodjenje)
        {
            return db.Zaposleni.Include(x => x.OrganizacionaJedinica).Where(x => x.GodinaRodjenja > rodjenje).OrderBy(x => x.GodinaRodjenja);
        }

        public IQueryable<Zaposleni> PostPretraga(ZaposleniPretraga pretragaModel)
        {
            return db.Zaposleni.Include(x => x.OrganizacionaJedinica).Where(x => x.Plata >= pretragaModel.Najmanje && x.Plata <= pretragaModel.Najvise).OrderByDescending(x => x.Plata).AsQueryable();
        }

        public void Update(Zaposleni zaposleni)
        {
            db.Entry(zaposleni).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (DBConcurrencyException)
            {

                throw;
            }
        }
    }
}