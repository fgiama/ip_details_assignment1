using IPDetailsLibrary;
using IPDetailsWebApi.Models.Cache;
using IPDetailsWebApi.Models.DB;
using System;
using System.Web.Http;
using Hangfire;
using IPDetailsWebApi.Models;

namespace IPDetailsWebApi.Controllers
{
    public class JobDetailsController : ApiController
    {
        private IDatabaseDataStore _dbStore;

        public JobDetailsController(IDatabaseDataStore dbStore)
        {
            _dbStore = dbStore;
        }
     
        public IHttpActionResult Get(string id)
        {
            try
            {
                var job = _dbStore.GetJobDetails(id);
                if (job == null)
                    return NotFound();

                return Ok(job);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }



    }
}
