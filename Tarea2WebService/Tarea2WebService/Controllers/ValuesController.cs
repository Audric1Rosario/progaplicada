using ArcherModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Tarea2WebService.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        BDISC210Entities dbContext = new BDISC210Entities();
        public IEnumerable<string> Get()
        {
            return new string[] { "Nope", "Nope nope" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "Nope";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        //public int Put(int id, [FromBody]FlowFreeTable newAction)
        // PUT api/values/5
        public int Put(int id, [FromBody]Archery value)
        {
            dbContext.Archery.Add(value);
            return dbContext.SaveChanges();
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
