using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ConectarDatos;
using System.Data.Entity;

namespace ClinicaApi.Controllers
{
    public class CitaController : ApiController
    {

        private ClinicaDBEntitiesConn DbContext = new ClinicaDBEntitiesConn();

        [HttpGet]
        public IEnumerable<Cita> Get()
        {
            using (ClinicaDBEntitiesConn ClinicaEntities = new ClinicaDBEntitiesConn())
            {
                return ClinicaEntities.Citas.ToList();
            }
        }

        [HttpGet]
        public Cita GetCitasByPaciente(int id)
        {
            using (ClinicaDBEntitiesConn ClinicaEntities = new ClinicaDBEntitiesConn())
            {
                return ClinicaEntities.Citas.FirstOrDefault(e => e.Identificacion == id);
            }
        }

        [HttpGet]
        public Cita GetCitasByEspecialidad(string id)
        {
            using (ClinicaDBEntitiesConn ClinicaEntities = new ClinicaDBEntitiesConn())
            {
                return ClinicaEntities.Citas.FirstOrDefault(e => e.IdEspecialidad == id);
            }
        }

        [HttpPost]
        public IHttpActionResult AgregarCita([FromBody]Cita obj)
        {
            if (ModelState.IsValid)
            {
                DbContext.Citas.Add(obj);
                DbContext.SaveChanges();
                return Ok(obj);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public IHttpActionResult ActualizarCita(int id, [FromBody]Cita obj)
        {
            if (ModelState.IsValid)
            {
                var Cita = DbContext.Citas.Count(c => c.IdCita == id) > 0;

                if (Cita)
                {
                    DbContext.Entry(obj).State = EntityState.Modified;
                    DbContext.SaveChanges();
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public IHttpActionResult EliminarCita(int id)
        {
            var Cita = DbContext.Citas.Find(id);

            if (Cita != null)
            {
                DbContext.Citas.Remove(Cita);
                DbContext.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

    }
}
