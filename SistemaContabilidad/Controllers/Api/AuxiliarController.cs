﻿using System;
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
    public class AuxiliarController : ApiController
    {
        private ContabilidadEntities1 db = new ContabilidadEntities1();

        // GET: api/Auxiliar
       
        public IQueryable<dynamic> GetAuxiliar()
        {
            return db.Auxiliar.Select(auxiliar => new {
                auxiliar.idAuxiliar,
                auxiliar.Descripcion,
                auxiliar.Activo

            });
        }

        // GET: api/Auxiliar/5
        [ResponseType(typeof(Auxiliar))]
        public IHttpActionResult GetAuxiliar(int id)
        {
            var Auxiliar = db.Auxiliar.Select(auxiliar => new
            {
                auxiliar.idAuxiliar,
                auxiliar.Descripcion,
                auxiliar.Activo

            }).FirstOrDefault(a => a.idAuxiliar == id);

            if (Auxiliar == null)
            {
                return NotFound();
            }

            return Ok(Auxiliar);
        }

        // PUT: api/Auxiliar/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAuxiliar(int id, Auxiliar auxiliar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != auxiliar.idAuxiliar)
            {
                return BadRequest();
            }

            db.Entry(auxiliar).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuxiliarExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            //return StatusCode(HttpStatusCode.NoContent);
            return Ok("Nitido");
        }

        // POST: api/Auxiliar
        [ResponseType(typeof(Auxiliar))]
        public IHttpActionResult PostAuxiliar(Auxiliar auxiliar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           int ultimoId = db.Auxiliar.OrderByDescending(a => a.idAuxiliar)
                            .Select(a => a.idAuxiliar)
                            .First();

            auxiliar.idAuxiliar = ultimoId + 1;

            db.Auxiliar.Add(auxiliar);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AuxiliarExists(auxiliar.idAuxiliar))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = auxiliar.idAuxiliar }, auxiliar);
        }

        // DELETE: api/Auxiliar/5
        [ResponseType(typeof(Auxiliar))]
        public IHttpActionResult DeleteAuxiliar(int id)
        {
            Auxiliar auxiliar = db.Auxiliar.Find(id);
            if (auxiliar == null)
            {
                return NotFound();
            }

            db.Auxiliar.Remove(auxiliar);
            db.SaveChanges();

            return Ok(auxiliar);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AuxiliarExists(int id)
        {
            return db.Auxiliar.Count(e => e.idAuxiliar == id) > 0;
        }
    }
}