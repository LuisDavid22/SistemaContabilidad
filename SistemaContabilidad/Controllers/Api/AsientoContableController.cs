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
using SistemaContabilidad.Dto;
using SistemaContabilidad.Models;

namespace SistemaContabilidad.Controllers.Api
{
    public class AsientoContableController : ApiController
    {
        private ContabilidadEntities1 db = new ContabilidadEntities1();
        public enum Auxiliares
        {
            Contabilidad = 1,
            Nomina = 2,
            Facturacion = 3,
            Inventario = 4,
            CuentasXCobrar = 5,
            CuentasXPagar = 6,
            Compras = 7,
            ActivoFijos = 8,
            Cheques = 9
        }

        // GET: api/AsientoContable
        public IQueryable<dynamic> GetAsientoContable()
        {
           

            var asientoContable = db.AsientoContable.Include(a => a.AsientoCuenta).Select(asiento => new
            {
                idAsiento = asiento.idAsientoContable,
                asiento.Descripcion,
                Auxiliar = asiento.idAuxiliar,
                asiento.Fecha,
                asiento.Estado,
                Cuentas = asiento.AsientoCuenta.Select(cuenta => new
                {
                    id = cuenta.idCuentaContable,
                    cuenta = cuenta.CuentaContable.Descripcion,
                    tipo = cuenta.tipoMov,
                    monto = cuenta.Monto

                }).ToList()
            });

            return asientoContable;
        }
   
     
        // GET: api/AsientoContable/5
        [ResponseType(typeof(AsientoContable))]
        public IHttpActionResult GetAsientoContable(int id)
        {
            var asientoContable = db.AsientoContable.Include(a => a.AsientoCuenta).Select(asiento => new
            {
                idAsiento = asiento.idAsientoContable,
                asiento.Descripcion,
                Auxiliar = asiento.idAuxiliar,
                asiento.Fecha,
                asiento.Estado,
                Cuentas = asiento.AsientoCuenta.Select(cuenta => new
                {
                    id = cuenta.idCuentaContable,
                    cuenta = cuenta.CuentaContable.Descripcion,
                    tipo = cuenta.tipoMov,
                    monto = cuenta.Monto

                }).ToList()
            }).FirstOrDefault(a => a.idAsiento == id);

            if (asientoContable == null)
            {
                return NotFound();
            }

            return Ok(asientoContable);
        }

        // PUT: api/AsientoContable/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAsientoContable(int id, AsientoContableDto asientoContableDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != asientoContableDto.idAsiento)
            {
                return BadRequest();
            }

            if (asientoContableDto.Cuentas.Count == 0)
            {
                return BadRequest("Debe especificar las cuentas de este asiento");

            }

            if (asientoContableDto.Auxiliar == 0)
            {
                asientoContableDto.Auxiliar = (int)Auxiliares.Contabilidad;
            }


            //foreach (var cuenta in asientoContableDto.Cuentas)
            //{
            //    cuenta.id = asientoContableDto.idAsiento;
            //}

            AsientoContable asientoContable = new AsientoContable
            {
                idAsientoContable = asientoContableDto.idAsiento,
                Descripcion = asientoContableDto.Descripcion,
                idAuxiliar = asientoContableDto.Auxiliar,
                AsientoCuenta = asientoContableDto.Cuentas.Select(c => new AsientoCuenta
                {
                    idCuentaContable = c.id,
                    idAsientoContable = asientoContableDto.idAsiento,
                    Monto = c.monto,
                    tipoMov = c.tipo
                }).ToArray(),
                Fecha = DateTime.Now,
                Estado = "Registrado"

            };




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
        public IHttpActionResult PostAsientoContable(AsientoContableDto asientoContableDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Objeto invalido. Para mas información comuniquese con el administrador de la api. ");
            }

            if (asientoContableDto.Cuentas.Count == 0)
            {
                return BadRequest("Debe especificar las cuentas de este asiento");

            }

            if (asientoContableDto.Auxiliar == 0)
            {
                asientoContableDto.Auxiliar = (int)Auxiliares.Contabilidad;
            }

            foreach (var cuenta in asientoContableDto.Cuentas)
            {
                if (cuenta.tipo.ToLower() != "db" || cuenta.tipo.ToLower() != "cr" || cuenta.tipo == null)
                    return BadRequest("Cuentas invalidas. Para mas información comuniquese con el administrador de la api");
            }




            AsientoContable asientoContable = new AsientoContable
            {
                Descripcion = asientoContableDto.Descripcion,
                idAuxiliar = asientoContableDto.Auxiliar,
                AsientoCuenta = asientoContableDto.Cuentas.Select(c => new AsientoCuenta
                {
                    idCuentaContable = c.id,
                    Monto = c.monto,
                    tipoMov = c.tipo
                }).ToArray(),
                Fecha = DateTime.Now,
            Estado = "Registrado"

        };

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

            //return CreatedAtRoute("DefaultApi", new { id = asientoContable.idAsientoContable }, asientoContable);

            return Ok("Asiento registrado sastifactoriamente");
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