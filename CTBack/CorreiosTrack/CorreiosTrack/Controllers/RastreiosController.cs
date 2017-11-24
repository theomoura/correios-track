using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using CorreiosTrack.Models;

namespace CorreiosTrack.Controllers
{
    using CorreiosTrack.Services;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;

    [RoutePrefix("api/Rastreios")]
    public class RastreiosController : ApiController
    {

        private CorreiosTrackContext db = new CorreiosTrackContext();

        private readonly RastreioService _service;

        public RastreiosController()
        {
            _service = new RastreioService();
        }

        // GET: api/Rastreios
        public IQueryable<Rastreio> GetRastreios()
        {
            return _service.GetRastreios();
        }

        //Example of custom routing for test purposes
        [Route("test/{id}")]
        [HttpGet]
        public IHttpActionResult GetSomething(long id)
        {
            return Ok(_service.GetSomething(2));
        }

        // GET: api/Rastreios/5
        [ResponseType(typeof(Rastreio))]
        public IHttpActionResult GetRastreio(long id)
        {
            Rastreio rastreio = _service.GetRastreio(id);
            if (rastreio == null)
            {
                return NotFound();
            }

            return Ok(rastreio);
        }

        // PUT: api/Rastreios/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRastreio(long id, Rastreio rastreio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rastreio.Id)
            {
                return BadRequest();
            }

            string trackStatus = _service.ConsultTrackStatus(rastreio.Codigo);
            rastreio.Status = trackStatus;

            bool result = _service.PutRastreio(id, rastreio);
            if (!result)
            {
                return NotFound();
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Rastreios
        [ResponseType(typeof(Rastreio))]
        public IHttpActionResult PostRastreio(Rastreio rastreio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string trackStatus = _service.ConsultTrackStatus(rastreio.Codigo);
            rastreio.Status = trackStatus;

            _service.PostRastreio(rastreio);

            return CreatedAtRoute("DefaultApi", new { id = rastreio.Id }, rastreio);
        }

        // DELETE: api/Rastreios/5
        [ResponseType(typeof(Rastreio))]
        public IHttpActionResult DeleteRastreio(long id)
        {
            Rastreio rastreio = _service.DeleteRastreio(id);
            if (rastreio == null)
            {
                return NotFound();
            }
            return Ok(rastreio);
        }


       
    }
}