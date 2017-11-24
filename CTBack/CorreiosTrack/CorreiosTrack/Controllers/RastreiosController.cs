using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using CorreiosTrack.Models;

namespace CorreiosTrack.Controllers
{
    using CorreiosTrack.Services;

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
            return _service.ConsultAllRastreios();
        }

        //Example of custom routing for test purposes
        [Route("test/{id}")]
        [HttpGet]
        public IHttpActionResult GetSomething(long id)
        {
            return Ok(_service.ConsultByStatusTest(2));
        }

        // GET: api/Rastreios/5
        [ResponseType(typeof(Rastreio))]
        public IHttpActionResult GetRastreio(long id)
        {
            Rastreio rastreio = _service.ConsultRastreio(id);
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

            bool result = _service.EditRastreio(id, rastreio);
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
            _service.CreateRastreio(rastreio);

            return CreatedAtRoute("DefaultApi", new { id = rastreio.Id }, rastreio);
        }

        // DELETE: api/Rastreios/5
        [ResponseType(typeof(Rastreio))]
        public IHttpActionResult DeleteRastreio(long id)
        {
            Rastreio rastreio = _service.RemoveRastreio(id);
            if (rastreio == null)
            {
                return NotFound();
            }
            return Ok(rastreio);
        }


       
    }
}