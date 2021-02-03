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
    public class JediniceController : ApiController
    {
        private IOrgJedinicaRepository _orgJedinicaRepository { get; set; }

        public JediniceController(IOrgJedinicaRepository organizacionaJedincaRepository)
        {
            this._orgJedinicaRepository = organizacionaJedincaRepository;
        }

        public IEnumerable<OrganizacionaJedinica> Get()
        {
            return _orgJedinicaRepository.GetAll();
        }

        public IHttpActionResult GetById(int id)
        {
            OrganizacionaJedinica jedinica = _orgJedinicaRepository.GetById(id);

            if (jedinica == null)
            {
                return NotFound();
            }

            return Ok(jedinica);
        }

        [HttpGet]
        [Route("api/tradicija")]
        public IQueryable<OrganizacionaJedinica> Tradicija()
        {
            return _orgJedinicaRepository.GetTradicija();
        }


        [HttpGet]
        [Route("api/brojnost")]
        public IQueryable<OrganizacionaJedinica> Brojnost()
        {
            return _orgJedinicaRepository.GetBrojnost();
        }

        [HttpPost]
        [Route("api/plate")]
        public IQueryable<OrganizacionaJedinicaDTO> Plate(OrgJedinicaPretraga pretragaModel)
        {
            return _orgJedinicaRepository.PostPlate(pretragaModel);
        }
    }
}
