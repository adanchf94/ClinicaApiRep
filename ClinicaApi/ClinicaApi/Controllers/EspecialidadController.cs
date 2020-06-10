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
    public class EspecialidadController : ApiController
    {

        private ClinicaDBEntitiesConn DbContext = new ClinicaDBEntitiesConn();

        [HttpGet]
        public IEnumerable<Especialidad> Get()
        {
            using (ClinicaDBEntitiesConn ClinicaEntities = new ClinicaDBEntitiesConn())
            {
                return ClinicaEntities.Especialidads.ToList();
            }
        }

        [HttpGet]
        public Especialidad Get(string id)
        {
            using (ClinicaDBEntitiesConn ClinicaEntities = new ClinicaDBEntitiesConn())
            {
                return ClinicaEntities.Especialidads.FirstOrDefault(e => e.IdEspecialidad == id);
            }
        }

    }
}
