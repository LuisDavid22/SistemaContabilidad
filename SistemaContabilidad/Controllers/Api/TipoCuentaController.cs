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
    public class TipoCuentaController : ApiController
    {
        private ContabilidadEntities db = new ContabilidadEntities();

        // GET: api/TipoCuenta
        public IQueryable<dynamic> GetTipoCuenta()
        {
            var TipoCuenta = db.TipoCuenta.Select(tipo => new
 
                {
                tipo.idTipoCuenta,
                tipo.Descripcion,
                 tipo.Origen,
                 tipo.Estado
                }
            );
            return TipoCuenta;
        }

        // GET: api/TipoCuenta/5
        [ResponseType(typeof(TipoCuenta))]
        public IHttpActionResult GetTipoCuenta(int id)
        {
            var TipoCuenta = db.TipoCuenta.Select(tipo => new

                {
                  tipo.idTipoCuenta,
                   tipo.Descripcion,
                    tipo.Origen,
                   tipo.Estado
                }).FirstOrDefault(t => t.idTipoCuenta == id);

            if (TipoCuenta == null)
            {
                return NotFound();
            }

            return Ok(TipoCuenta);
        }

        // PUT: api/TipoCuenta/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTipoCuenta(int id, TipoCuenta tipoCuenta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tipoCuenta.idTipoCuenta)
            {
                return BadRequest();
            }

            db.Entry(tipoCuenta).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoCuentaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            //return StatusCode(HttpStatusCode.Created);
            return Ok("Nitido");
        }

        // POST: api/TipoCuenta
        [ResponseType(typeof(TipoCuenta))]
        public IHttpActionResult PostTipoCuenta(TipoCuenta tipoCuenta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

        
        int ultimoId = db.TipoCuenta.OrderByDescending(t => t.idTipoCuenta)
                        .Select(t => t.idTipoCuenta)
                        .First();

        tipoCuenta.idTipoCuenta = ultimoId + 1;

            db.TipoCuenta.Add(tipoCuenta);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (TipoCuentaExists(tipoCuenta.idTipoCuenta))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tipoCuenta.idTipoCuenta }, tipoCuenta);
        }

        // DELETE: api/TipoCuenta/5
        [ResponseType(typeof(TipoCuenta))]
        public IHttpActionResult DeleteTipoCuenta(int id)
        {
            TipoCuenta tipoCuenta = db.TipoCuenta.Find(id);
            if (tipoCuenta == null)
            {
                return NotFound();
            }

            db.TipoCuenta.Remove(tipoCuenta);
            db.SaveChanges();

            return Ok(tipoCuenta);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TipoCuentaExists(int id)
        {
            return db.TipoCuenta.Count(e => e.idTipoCuenta == id) > 0;
        }
    }
}