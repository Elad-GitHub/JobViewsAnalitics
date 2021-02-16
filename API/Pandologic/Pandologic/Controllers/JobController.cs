using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Pandologic.DataSources;
using Pandologic.Models;
using Services;

namespace Pandologic.Controllers
{
    public class JobController : ControllerBase
    {
        [Route ("Job/")]
        [EnableCors("HireIntelligenceAppPolicy")]
        [HttpGet]
        public ResultSet<JobsViewModel> Get()
        {
            var jobsDataSource = new JobsDataSource();

            var jobDailyAnalitics = jobsDataSource.Query<JobsViewModel>();

            return jobDailyAnalitics;
        }
    }
}
