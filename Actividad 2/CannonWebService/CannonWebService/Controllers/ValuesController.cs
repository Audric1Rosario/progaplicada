using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CannonModel;

namespace CannonWebService.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        BDISCEntities dbContext = new BDISCEntities();
        public IEnumerable<string> Get()
        {
            return new string[] { "Nope", "Nope Nope" };
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
        public int Put(int id, [FromBody]CannonTable value)
        {
            dbContext.CannonTable.Add(value);
            return dbContext.SaveChanges();
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
