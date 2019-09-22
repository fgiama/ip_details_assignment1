using IPDetailsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;

namespace IPDetailsWebApi.Models.DB
{
    public static class JobManager
    {
        private static ObjectCache _cache;

        static JobManager()
        {
            _cache = MemoryCache.Default;
        }

        public static void InsertIpDetailsJob(IEnumerable<IPDetailsModel> ipInfos, Guid newId)
        {
            using (var context = new IPDetailsDBEntities())
            {
                //store items in a queue
                var queue = new Queue<IPDetailsModel>(ipInfos);

                var list = new Dictionary<string, IPDetailsModel>();
                var counter = AppSettingsManager.BucketsToStore; //items to update in batch
                while (queue.Count > 0 && counter-- >= 0)
                {
                    
                    var first = queue.Dequeue();
                    list.Add(first.Ip, first);


                    if (counter == 0 || queue.Count == 0) //if batch is ready or queue is empty  
                    {
                        try
                        {
                            UpdateIpDetailList(list, context); //update batch

                            if (queue.Count == 0) //if end of queue;
                                FinalizeJobDetails(JobStatusEnum.Completed, newId, context); //finilize job to complete status
                            else
                                UpdateJobDetails(list.Count, newId, context); //update progress
                            UpdateListInCache(list); //update these items in cache
                            list.Clear(); //clear batch
                            counter = AppSettingsManager.BucketsToStore; //start counter over
                        }
                        catch (Exception ex)
                        {
                            FinalizeJobDetails(JobStatusEnum.Faulted, newId, context); //on error finilize job to faulted status
                        }

                    }
                }
            }
        }

        private static void UpdateIpDetailList(IDictionary<string, IPDetailsModel> items, IPDetailsDBEntities context)
        {

            foreach (KeyValuePair<string, IPDetailsModel> item in items)
            {
                IPDetail dbItem = context.IPDetails.FirstOrDefault(n => n.Ip == item.Key);

                dbItem.Ip = item.Key;
                dbItem.City = item.Value.City;
                dbItem.Country = item.Value.City;
                dbItem.Latitude = item.Value.Latitude.ToString();
                dbItem.Longitude = item.Value.Longitude.ToString();
            }

            context.SaveChanges();

        }

        private static void UpdateJobDetails(int itemsInserted, Guid jobId, IPDetailsDBEntities context)
        {
            var jobItem = context.IPJobs.FirstOrDefault(n => n.Id == jobId.ToString());
            jobItem.Progress += itemsInserted;

            context.SaveChanges();
        }

        private static void FinalizeJobDetails(JobStatusEnum status, Guid jobId, IPDetailsDBEntities context)
        {
            var jobItem = context.IPJobs.FirstOrDefault(n => n.Id == jobId.ToString());
            jobItem.FinishedOn = DateTime.Now;
            jobItem.Status = (int)status;

            if (status == JobStatusEnum.Completed)
                jobItem.Progress = jobItem.Total; //all items are updated

            context.SaveChanges();
        }

        public static void UpdateListInCache(IDictionary<string, IPDetailsModel> items)
        {
            foreach (KeyValuePair<string, IPDetailsModel> item in items)
            {
                if (_cache.Contains(item.Key))
                {
                    _cache[item.Key] = item.Value;
                }
            }
        }
    }
}