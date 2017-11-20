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
    using System.IO;
    using System.Xml;
    using TrackService;

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

            string trackStatus = ConsultTrackStatus(rastreio.Codigo);
            rastreio.Status = trackStatus;
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

        private string parseXML(string xml)
        {
            string result = "";
            int initIndexDesc = xml.IndexOf("<descricao>") + "<descricao>".Length;
            int finalLenghtDesc = xml.IndexOf("</descricao>") - initIndexDesc;
            result += xml.Substring(initIndexDesc, finalLenghtDesc);
            if (xml.Contains("<destino>"))
            {
                int initIndexLocal = xml.IndexOf("<local>", xml.IndexOf("<local>") + 1) + "<local>".Length;
                int finalLenghtLocal = xml.IndexOf("</local>", xml.IndexOf("</local>") + 1) - initIndexLocal;
                result += "para " + xml.Substring(initIndexLocal, finalLenghtLocal);
            }
            return result;
        }

        private static HttpWebRequest CreateWebRequest()
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(@"http://webservice.correios.com.br/service/rastro");
            webRequest.Headers.Add("SOAPAction", "http://webservice.correios.com.br/service/rastro/Rastro");
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }

        private string ConsultTrackStatus(string trackCode)
        {
            HttpWebRequest request = CreateWebRequest();
            XmlDocument soapEnvelopeXml = new XmlDocument();
            soapEnvelopeXml.LoadXml(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/""
            xmlns:res=""http://resource.webservice.correios.com.br/"">
            <soapenv:Header/>
            <soapenv:Body>
            <res:buscaEventos>
            <usuario>ECT</usuario>
            <senha>SRO</senha>
            <tipo>L</tipo>
            <resultado>T</resultado>
            <lingua>101</lingua>
            <objetos>" + trackCode + @"</objetos>
            </res:buscaEventos>
            </soapenv:Body>
            </soapenv:Envelope>");

            using (Stream stream = request.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }

            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                {
                    string soapResult = rd.ReadToEnd();
                    return parseXML(soapResult);
                }
            }
        }
    }
}