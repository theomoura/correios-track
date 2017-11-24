using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CorreiosTrack.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Xml;
using System.IO;

namespace CorreiosTrack.Services
{
    public class RastreioService
    {
        private CorreiosTrackContext db = new CorreiosTrackContext();

        public IQueryable<Rastreio> GetRastreios()
        {
            return db.Rastreios;
        }

        public Rastreio GetRastreio(long id)
        {
            return db.Rastreios.Find(id);
        }

        public bool PutRastreio(long id, Rastreio rastreio)
        {
            db.Entry(rastreio).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RastreioExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
            return true;
        }

        public void PostRastreio(Rastreio rastreio)
        {
            db.Rastreios.Add(rastreio);
            db.SaveChanges();
        }

        public Rastreio DeleteRastreio(long id)
        {
            Rastreio rastreio = db.Rastreios.Find(id);
            if (rastreio == null)
            {
                return null;
            }

            db.Rastreios.Remove(rastreio);
            db.SaveChanges();

            return rastreio;
        }

        public List<Rastreio> GetSomething(long id)
        {
            //Example using LINQ for test purposes
            using (var context = new CorreiosTrackContext())
            {
                context.Rastreios.Load();
                var query = (from r in context.Rastreios.Local where r.Status.Contains("encaminhado") select r).ToList();
                return query;
            }
        }

        private bool RastreioExists(long id)
        {
            return db.Rastreios.Count(e => e.Id == id) > 0;
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
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

        public string ConsultTrackStatus(string trackCode)
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