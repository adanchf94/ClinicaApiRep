using ConectarDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;

namespace ClinicaApi.Controllers
{
    public class PacienteController : ApiController
    {
        private ClinicaDBEntitiesConn DbContext = new ClinicaDBEntitiesConn();

        [HttpGet]
        public IEnumerable<Paciente> Get()
        {
            using (ClinicaDBEntitiesConn ClinicaEntities = new ClinicaDBEntitiesConn())
            {
                return ClinicaEntities.Pacientes.ToList();
            }
        }

        [HttpGet]
        public Paciente Get(int id)
        {
            using (ClinicaDBEntitiesConn ClinicaEntities = new ClinicaDBEntitiesConn())
            {
                return ClinicaEntities.Pacientes.FirstOrDefault(e => e.Identificacion == id);
            }
        }

        [HttpPost]
        public IHttpActionResult AgregarPaciente([FromBody]Paciente obj)
        {
            if (ModelState.IsValid)
            {
                DbContext.Pacientes.Add(obj);
                DbContext.SaveChanges();
                return Ok(obj);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public IHttpActionResult ActualizarPaciente(int id, [FromBody]Paciente obj)
        {
            if (ModelState.IsValid)
            {
                var Paciente = DbContext.Pacientes.Count(c => c.Identificacion == id) > 0;

                if (Paciente)
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
        public IHttpActionResult EliminarPaciente(int id)
        {
            var Paciente = DbContext.Pacientes.Find(id);

            if (Paciente != null)
            {
                DbContext.Pacientes.Remove(Paciente);
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
