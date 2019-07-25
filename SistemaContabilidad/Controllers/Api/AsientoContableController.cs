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
    public class AsientoContableController : ApiController
    {
        private ContabilidadEntities1 db = new ContabilidadEntities1();

        // GET: api/AsientoContable
        public IQueryable<dynamic> GetAsientoContable()
        {
            var AsientoContable = db.AsientoContable.Select(asiento => new
            {
               idAsiento = asiento.idAsientoContable,
                asiento.Descripcion,
                Auxiliar = asiento.idAuxiliar,
                asiento.Fecha,
                asiento.Estado,
                Cuentas = db.AsientoCuenta.Include(c => c.CuentaContable).Select(cuenta => new
                { id = cuenta.idCuentaContable,
                  cuenta = cuenta.CuentaContable.Descripcion,
                  tipo = cuenta.tipoMov,
                  monto = cuenta.Monto
                
                }),
            });

            return AsientoContable;
        }

        // GET: api/AsientoContable/5
        [ResponseType(typeof(AsientoContable))]
        public IHttpActionResult GetAsientoContable(int id)
        {
            AsientoContable asientoContable = db.AsientoContable.Find(id);
            if (asientoContable == null)
            {
                return NotFound();
            }

            return Ok(asientoContable);
        }

        // PUT: api/AsientoContable/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAsientoContable(int id, AsientoContable asientoContable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != asientoContable.idAsientoContable)
            {
                return BadRequest();
            }

            db.Entry(asientoContable).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AsientoContableExists(id))
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

        // POST: api/AsientoContable
        [ResponseType(typeof(AsientoContable))]
        public IHttpActionResult PostAsientoContable(AsientoContable asientoContable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AsientoContable.Add(asientoContable);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AsientoContableExists(asientoContable.idAsientoContable))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = asientoContable.idAsientoContable }, asientoContable);
        }

        // DELETE: api/AsientoContable/5
        [ResponseType(typeof(AsientoContable))]
        public IHttpActionResult DeleteAsientoContable(int id)
        {
            AsientoContable asientoContable = db.AsientoContable.Find(id);
            if (asientoContable == null)
            {
                return NotFound();
            }

            db.AsientoContable.Remove(asientoContable);
            db.SaveChanges();

            return Ok(asientoContable);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AsientoContableExists(int id)
        {
            return db.AsientoContable.Count(e => e.idAsientoContable == id) > 0;
        }
    }
}