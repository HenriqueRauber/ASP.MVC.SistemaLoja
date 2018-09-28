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
using SistemaLoja.Models;

namespace SistemaLoja.Controllers
{
    public class OrdensAPIController : ApiController
    {
        private SistemaLojaContext db = new SistemaLojaContext();

        // GET: api/OrdensAPI
        public IHttpActionResult GetOrdensAPIs()
        {
            var ordens = db.Ordem.ToList();
            var ordensAPI = new List<OrdensAPI>();

            foreach (var ordem in ordens)
            {
                var ordemAPI = new OrdensAPI() {
                    customizar = ordem.Customizar,
                    OrdemData = ordem.OrdemData,
                    Detalhes = ordem.OrdensDetalhes,
                    OrdensAPIId = ordem.OrdemId,
                    OrdemStatus = ordem.OrdemStatus
                };
                ordensAPI.Add(ordemAPI);
            }

            return Ok(ordensAPI);
        }

        // GET: api/OrdensAPI/5
        [ResponseType(typeof(OrdensAPI))]
        public IHttpActionResult GetOrdensAPI(int id)
        {
            OrdensAPI ordensAPI = db.OrdensAPIs.Find(id);
            if (ordensAPI == null)
            {
                return NotFound();
            }

            return Ok(ordensAPI);
        }

        // PUT: api/OrdensAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrdensAPI(int id, OrdensAPI ordensAPI)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ordensAPI.OrdensAPIId)
            {
                return BadRequest();
            }

            db.Entry(ordensAPI).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdensAPIExists(id))
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

        // POST: api/OrdensAPI
        [ResponseType(typeof(OrdensAPI))]
        public IHttpActionResult PostOrdensAPI(OrdensAPI ordensAPI)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.OrdensAPIs.Add(ordensAPI);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = ordensAPI.OrdensAPIId }, ordensAPI);
        }

        // DELETE: api/OrdensAPI/5
        [ResponseType(typeof(OrdensAPI))]
        public IHttpActionResult DeleteOrdensAPI(int id)
        {
            OrdensAPI ordensAPI = db.OrdensAPIs.Find(id);
            if (ordensAPI == null)
            {
                return NotFound();
            }

            db.OrdensAPIs.Remove(ordensAPI);
            db.SaveChanges();

            return Ok(ordensAPI);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrdensAPIExists(int id)
        {
            return db.OrdensAPIs.Count(e => e.OrdensAPIId == id) > 0;
        }
    }
}