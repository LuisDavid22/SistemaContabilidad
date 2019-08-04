using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SistemaContabilidad.Controllers.Api
{
    public class MonedaController : ApiController
    {
        private ContabilidadEntities1 db = new ContabilidadEntities1();

        // GET: api/Moneda
        public async Task<IQueryable<Moneda>> GetMoneda()
        {
            var Monedas =  db.Moneda;

            try
            {
                foreach (var moneda in Monedas)
                {
                    moneda.UltimaTasaCambiaria = await getTasaMonedaFromApi(moneda.idTipoMoneda);
           
                }
                db.SaveChanges();

            }
            catch (System.Exception)
            {

                throw;
            }

            return Monedas;
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
        public async  Task<IHttpActionResult> PutMoneda(string id, Moneda moneda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != moneda.idTipoMoneda)
            {
                return BadRequest();
            }

            moneda.UltimaTasaCambiaria = await getTasaMonedaFromApi(moneda.idTipoMoneda);

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

            //return StatusCode(HttpStatusCode.NoContent);
            return Ok("Nitido");
        }

        // POST: api/Moneda
        [ResponseType(typeof(Moneda))]
        public async Task<IHttpActionResult> PostMoneda(Moneda moneda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            moneda.UltimaTasaCambiaria = await getTasaMonedaFromApi(moneda.idTipoMoneda);

            db.Moneda.Add(moneda);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (MonedaExists(moneda.idTipoMoneda))
                {
                    return BadRequest("Esta moneda ya existe");
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

        private async Task<double> getTasaMonedaFromApi(string IdMoneda)
        {
            double Tasa = 0;

            if (IdMoneda.ToLower() == "dop") return ++Tasa;
         
            try
            {
               
                var httpClient = new HttpClient();
                var json = await httpClient.GetStringAsync("https://sistemaweb20190730113624.azurewebsites.net/api/monedas/mostrar/" + IdMoneda);

                if (json != null)
                {
                    MonedaEsquema Moneda = JsonConvert.DeserializeObject<MonedaEsquema>(json);

                    Tasa = Moneda.tasa_cambiaria;
                }


                return Tasa;
            }
            catch (System.Exception)
            {

                return Tasa;
            }
           
        }

        class MonedaEsquema
        {
            public string moneda_id { get; set; }
            public double tasa_cambiaria { get; set; }

        }
    }
}