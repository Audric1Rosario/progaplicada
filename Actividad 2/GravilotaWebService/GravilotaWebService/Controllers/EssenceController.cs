using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GravilotaModel;
namespace GravilotaWebService.Controllers
{
    public class EssenceController : ApiController
    {
        BDISC210Entities dbContext = new BDISC210Entities();
        // GET api/<controller>
        public IEnumerable<EssenceScore> Get()
        {
            return dbContext.EssenceScore.OrderBy(essence => essence.BlueScore +
                essence.GreenScore + essence.OrangeScore + essence.PurpleScore + essence.RedScore +
                essence.YellowScore).Take(10).ToList(); ;
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public int Put([FromBody]EssenceScore newScore)
        {
            EssenceScore _newEntry = new EssenceScore();
            // Normalmente usado para filtrar
            _newEntry.Id = newScore.Id + 1;
            _newEntry.PlayerName = newScore.PlayerName;
            _newEntry.BlueScore = newScore.BlueScore;
            _newEntry.GreenScore = newScore.GreenScore;
            _newEntry.OrangeScore = newScore.OrangeScore;
            _newEntry.PurpleScore = newScore.PurpleScore;
            _newEntry.RedScore = newScore.RedScore;
            _newEntry.YellowScore = newScore.YellowScore;

            dbContext.EssenceScore.Add(_newEntry);
            return dbContext.SaveChanges();
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}