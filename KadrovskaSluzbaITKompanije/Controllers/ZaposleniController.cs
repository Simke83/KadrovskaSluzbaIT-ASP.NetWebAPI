using KadrovskaSluzbaITKompanije.Models;
using KadrovskaSluzbaITKompanije.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KadrovskaSluzbaITKompanije.Controllers
{
    public class ZaposleniController : ApiController
    {
        IZaposleniRepository _zaposleniRepository { get; set; }
        public ZaposleniController(IZaposleniRepository rep)
        {
            _zaposleniRepository = rep;
        }

        public IQueryable<Zaposleni> Get()
        {
            return _zaposleniRepository.GetAll();
        }

        public IHttpActionResult GetById(int id)
        {
            Zaposleni zaposleni = _zaposleniRepository.GetById(id);

            if (zaposleni == null)
            {
                return NotFound();

            }

            return Ok(zaposleni);
        }

        public IQueryable<Zaposleni> GetRodjenje(int rodjenje)
        {
            return _zaposleniRepository.GetZaposleniGodina(rodjenje);
        }

        public IHttpActionResult PostZaposleni(Zaposleni zaposleni)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _zaposleniRepository.Add(zaposleni);
            return CreatedAtRoute("DefaultApi", new { id = zaposleni.Id }, zaposleni);
        }
        [Authorize]
        public IHttpActionResult DeleteZaposleni(int id)
        {
            Zaposleni zaposleni = _zaposleniRepository.GetById(id);

            if (zaposleni == null)
            {
                return NotFound();
            }
            _zaposleniRepository.Delete(zaposleni);

            return StatusCode(HttpStatusCode.NoContent);
        }

        public IHttpActionResult PutZaposleni(int id, Zaposleni zaposleni)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != zaposleni.Id)
            {
                return BadRequest();
            }
            try
            {
                _zaposleniRepository.Update(zaposleni);

            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(zaposleni);
        }
        [Authorize]
        [HttpPost]
        [Route("api/pretraga")]
        public IQueryable<Zaposleni> Pretraga(ZaposleniPretraga pretragaModel)
        {
            return _zaposleniRepository.PostPretraga(pretragaModel);
        }
    }
}
