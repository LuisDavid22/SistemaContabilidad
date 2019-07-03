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
    public class CuentaContableController : ApiController
    {
        private ContabilidadEntities db = new ContabilidadEntities();

   
        // GET: api/CuentaContable
        public IQueryable<dynamic> GetCuentaContable()
        {
            var cuentaContable = db.CuentaContable.Select(cuenta => new
            {
                id = cuenta.idCuentaContable,
                descripcion = cuenta.Descripcion,
                tipoCuenta =  new
                {
                    id = cuenta.TipoCuenta.idTipoCuenta,
                    descripcion = cuenta.TipoCuenta.Descripcion,
                    origen = cuenta.TipoCuenta.Origen,
                    estado = cuenta.TipoCuenta.Estado
                },
                transacciones = cuenta.PermiteTrans,
                nivel = cuenta.Nivel,
                cuentaMayor = cuenta.CuentaContable2 == null? null: new
                {
                    id = (int?)cuenta.CuentaContable2.idCuentaContable,
                    descripcion = cuenta.CuentaContable2.Descripcion
                },
                balance = cuenta.Balance,
                estado = cuenta.Estado
            });

       

            return cuentaContable;

            //cuentaMayor = cuenta.CuentaContable2 == null ? new { id = 0, descripcion = "" } : new
            //{
            //    id = cuenta.CuentaContable2.idCuentaContable,
            //    descripcion = cuenta.CuentaContable2.Descripcion
            //}

            //tipoCuenta = new
            //{
            //    id = cuenta.TipoCuenta.idTipoCuenta,
            //    descripcion = cuenta.TipoCuenta.Descripcion,
            //    origen = cuenta.TipoCuenta.Origen,
            //    estado = cuenta.TipoCuenta.Estado
            //}
        }

        // GET: api/CuentaContable/5
        //[ResponseType(typeof(CuentaContable))]
        public IHttpActionResult GetCuentaContable(int id)
        {
            var cuentaContable = db.CuentaContable.Select(cuenta => new
            {
                id = cuenta.idCuentaContable,
                descripcion = cuenta.Descripcion,
                tipoCuenta = new
                {
                    id = cuenta.TipoCuenta.idTipoCuenta,
                    descripcion = cuenta.TipoCuenta.Descripcion,
                    origen = cuenta.TipoCuenta.Origen,
                    estado = cuenta.TipoCuenta.Estado
                },
                transacciones = cuenta.PermiteTrans,
                nivel = cuenta.Nivel,
                cuentaMayor = cuenta.CuentaContable2 ,
                balance = cuenta.Balance,
                estado = cuenta.Estado
            }).FirstOrDefault(c => c.id == id);
            if (cuentaContable == null)
            {
                return NotFound();
            }

            return Ok(cuentaContable);
        }

        // PUT: api/CuentaContable/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCuentaContable(int id, CuentaContable cuentaContable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cuentaContable.idCuentaContable)
            {
                return BadRequest();
            }

            db.Entry(cuentaContable).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CuentaContableExists(id))
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

        // POST: api/CuentaContable
        [ResponseType(typeof(CuentaContable))]
        public IHttpActionResult PostCuentaContable(CuentaContable cuentaContable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CuentaContable.Add(cuentaContable);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CuentaContableExists(cuentaContable.idCuentaContable))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = cuentaContable.idCuentaContable }, cuentaContable);
        }

        // DELETE: api/CuentaContable/5
        [ResponseType(typeof(CuentaContable))]
        public IHttpActionResult DeleteCuentaContable(int id)
        {
            CuentaContable cuentaContable = db.CuentaContable.Find(id);
            if (cuentaContable == null)
            {
                return NotFound();
            }

            db.CuentaContable.Remove(cuentaContable);
            db.SaveChanges();

            return Ok(cuentaContable);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CuentaContableExists(int id)
        {
            return db.CuentaContable.Count(e => e.idCuentaContable == id) > 0;
        }
    }
}