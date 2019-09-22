using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IPDetailsLibrary;

namespace IPDetailsWebApi.Models.DB
{
    public class SqlDataStore : IDatabaseDataStore
    {
        private readonly IPDetailsDBEntities _context;
        public SqlDataStore()
        {
            _context = new IPDetailsDBEntities();
        }

        public void AddIpDetails(string ip, IPDetailsModel item)
        {
            var dbItem = new IPDetail();
            dbItem.Ip = ip;
            dbItem.City = item.City;
            dbItem.Country = item.Country;
            dbItem.Continent = item.Continent;
            dbItem.Latitude = item.Latitude.ToString();
            dbItem.Longitude = item.Longitude.ToString();

            _context.IPDetails.Add(dbItem);

            _context.SaveChanges();
        }

        public IPDetailsModel GetIpDetails(string ip)
        {
            var dbItem = _context.IPDetails.FirstOrDefault(n => n.Ip == ip);
            IPDetailsModel ipDetails = null;

            if (dbItem != null)
            {
                ipDetails = new IPDetailsModel();
                ipDetails.Ip = ip;
                ipDetails.City = dbItem.City;
                ipDetails.Country = dbItem.Country;
                ipDetails.Continent = dbItem.Continent;
                ipDetails.Latitude = Convert.ToDouble(dbItem.Latitude);
                ipDetails.Longitude = Convert.ToDouble(dbItem.Longitude);
            }

            return ipDetails;
        }

        public bool AddJobDetails(JobDetails job)
        {
            var dbItem = new IPJob();
            dbItem.Id = job.Id.ToString();
            dbItem.StartedOn = job.StartedOn;
            dbItem.Status = (int)JobStatusEnum.InProgress;
            dbItem.Total = job.Total;

            _context.IPJobs.Add(dbItem);
            return _context.SaveChanges() != 0;
        }

        public JobDetails GetJobDetails(string id)
        {
            var dbItem = _context.IPJobs.FirstOrDefault(n=>n.Id == id);
            _context.Entry(dbItem).Reload();

            JobDetails jobDetails = null;

            if (dbItem != null)
            {
                jobDetails = new JobDetails();
                jobDetails.Id = Guid.Parse(id);
                jobDetails.StartedOn = dbItem.StartedOn;
                jobDetails.FinishedOn = dbItem.FinishedOn;
                jobDetails.Status = (JobStatusEnum)dbItem.Status;
                jobDetails.Progress = dbItem.Progress;
                jobDetails.Total = dbItem.Total;
            }

            return jobDetails;
        }

        public IEnumerable<IPDetailsModel> GetAllIpDetails()
        {
            return _context.IPDetails.Select(n => new IPDetailsModel() { Ip = n.Ip, City = n.City, Continent = n.Continent, Country = n.Country });
        }
    }
}