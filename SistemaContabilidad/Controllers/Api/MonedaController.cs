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
using SistemaContabilidad;

namespace SistemaContabilidad.Controllers.Api
{
    public class MonedaController : ApiController
    {
        private ContabilidadEntities db = new ContabilidadEntities();

        // GET: api/Moneda
        public IQueryable<Moneda> GetMoneda()
        {
            return db.Moneda;
        }

        // GET: api/Moneda/5
        [ResponseType(typeof(Moneda))]
        public IHttpActionResult GetMoneda(string id)
        {
            Moneda moneda = db.Moneda.Find(id);
            if (moneda == null)
            {
                return NotFound();
            }

            return Ok(moneda);
        }

        // PUT: api/Moneda/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMoneda(string id, Moneda moneda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != moneda.idTipoMoneda)
            {
                return BadRequest();
            }

            db.Entry(moneda).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MonedaExists(id))
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

        // POST: api/Moneda
        [ResponseType(typeof(Moneda))]
        public IHttpActionResult PostMoneda(Moneda moneda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Moneda.Add(moneda);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (MonedaExists(moneda.idTipoMoneda))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = moneda.idTipoMoneda }, moneda);
        }

        // DELETE: api/Moneda/5
        [ResponseType(typeof(Moneda))]
        public IHttpActionResult DeleteMoneda(string id)
        {
            Moneda moneda = db.Moneda.Find(id);
            if (moneda == null)
            {
                return NotFound();
            }

            db.Moneda.Remove(moneda);
            db.SaveChanges();

            return Ok(moneda);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MonedaExists(string id)
        {
            return db.Moneda.Count(e => e.idTipoMoneda == id) > 0;
        }
    }
}