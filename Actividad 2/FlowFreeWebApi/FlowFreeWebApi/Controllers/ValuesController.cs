using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FlowModel;

namespace FlowFreeWebApi.Controllers
{
    public class ValuesController : ApiController
    {
        DBFLOWFREEEntities dbContext = new DBFLOWFREEEntities();
        // GET api/values
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
        public int Put(int id, [FromBody]FlowFreeTable newAction)
        {
            dbContext.FlowFreeTable.Add(newAction);
            return dbContext.SaveChanges();
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
