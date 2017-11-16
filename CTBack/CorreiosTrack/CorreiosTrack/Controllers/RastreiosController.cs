using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CorreiosTrack.Models;

namespace CorreiosTrack.Controllers
{
    public class RastreiosController : ApiController
    {
        private CorreiosTrackContext db = new CorreiosTrackContext();

        // GET: api/Rastreios
        public IQueryable<Rastreio> GetRastreios()
        {
            return db.Rastreios;
        }

        // GET: api/Rastreios/5
        [ResponseType(typeof(Rastreio))]
        public IHttpActionResult GetRastreio(long id)
        {
            Rastreio rastreio = db.Rastreios.Find(id);
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

            db.Entry(rastreio).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RastreioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
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

            db.Rastreios.Add(rastreio);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = rastreio.Id }, rastreio);
        }

        // DELETE: api/Rastreios/5
        [ResponseType(typeof(Rastreio))]
        public IHttpActionResult DeleteRastreio(long id)
        {
            Rastreio rastreio = db.Rastreios.Find(id);
            if (rastreio == null)
            {
                return NotFound();
            }

            db.Rastreios.Remove(rastreio);
            db.SaveChanges();

            return Ok(rastreio);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RastreioExists(long id)
        {
            return db.Rastreios.Count(e => e.Id == id) > 0;
        }
    }
}