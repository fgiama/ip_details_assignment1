using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPDetailsWebApi.Models.DB
{
    public class JobDetails
    {
        public Guid Id { get; set; }
        public DateTime StartedOn { get; set; }
        public DateTime? FinishedOn { get; set; }
        //items updated
        public int Progress { get; set; }
        //total items
        public int Total { get; set; }
        public JobStatusEnum Status { get; set; }
    }
}