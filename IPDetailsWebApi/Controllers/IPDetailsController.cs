using IPDetailsLibrary;
using IPDetailsWebApi.Models.Cache;
using IPDetailsWebApi.Models.DB;
using System;
using System.Web.Http;
using Hangfire;
using IPDetailsWebApi.Models;
using System.Net;

namespace IPDetailsWebApi.Controllers
{
    public class IPDetailsController : ApiController
    {
        private ICacheDataStore _cacheStore;
        private IDatabaseDataStore _dbStore;

        public IPDetailsController(ICacheDataStore cacheStore, IDatabaseDataStore dbStore)
        {
            _cacheStore = cacheStore;
            _dbStore = dbStore;
        }

        public IHttpActionResult Get()
        {
            return Ok(_dbStore.GetAllIpDetails());
        }

        [Route("api/ipdetails/{ip}")]
        public IHttpActionResult Get(string ip)
        {
            //check if the ip is valid
            IPAddress ipAddress = null;
            if (!IPAddress.TryParse(ip, out ipAddress))
                return BadRequest("Invalid IP.");

            ip = ipAddress.ToString();

            if (IPIsPrivate(ip))
                return BadRequest("IP is private.");

            IPDetailsModel ipDetails;
            
            try
            {
                //get from cache 
                ipDetails = _cacheStore.Get(ip);
                if (ipDetails == null)
                {
                    //get from db
                    ipDetails = _dbStore.GetIpDetails(ip);
                    if (ipDetails == null)
                    {
                        //get from library
                        var factory = new IpInfoProviderFactory();
                        IIpInfoProvider provider = factory.Create();
                        ipDetails = CopyToModel(provider.GetDetails(ip), ip);
                        
                        //if ip found in library => store into cache and db
                        _dbStore.AddIpDetails(ip, ipDetails);
                        _cacheStore.Add(ip, ipDetails, AppSettingsManager.CacheExpirationTime);
                    }
                    else
                    {
                        //if ip found in db => store into cache
                        _cacheStore.Add(ip, ipDetails, AppSettingsManager.CacheExpirationTime);
                    }
                }

                return Ok(ipDetails);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        private bool IPIsPrivate(string ip)
        {
            System.Net.IPAddress address = null;
           
            var parts = ip.Split('.');

            var part2Int = Convert.ToInt32(parts[1]);
            if (parts[0].Equals("10") ||
                 (parts[0].Equals("172") && part2Int >= 16 && part2Int <= 31) ||
                 (parts[0].Equals("192") && parts[1].Equals("168")))
                   return true;
            
            return false;
        }

        private IPDetailsModel CopyToModel(IpDetails ipDetails, string ip)
        {
            var model = new IPDetailsModel();
            model.Ip = ip;
            model.City = ipDetails.City;
            model.Continent = ipDetails.Continent;
            model.Country = ipDetails.Country;
            model.Latitude = ipDetails.Latitude;
            model.Longitude = ipDetails.Longitude;

            return model;
        }

        public IHttpActionResult Post([FromBody]IPDetailsModel[] ipInfos)
        {
            try
            {
                //create new GUID for job
                var newId = Guid.NewGuid();

                //store job initial details into db
                if (_dbStore.AddJobDetails(new JobDetails()
                {
                    Id = newId,
                    StartedOn = DateTime.Now,
                    Status = JobStatusEnum.InProgress,
                    Total = ipInfos.Length
                }))
                {
                    //create new background job that updates ip details
                    BackgroundJob.Enqueue(() => JobManager.InsertIpDetailsJob(ipInfos, newId));
                }

                //return job id 
                return Ok(new { jobId = newId });
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}
