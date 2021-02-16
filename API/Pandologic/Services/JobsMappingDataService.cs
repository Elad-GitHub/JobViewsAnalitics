using Pandologic;
using System.ServiceModel;

namespace Services
{
    [ServiceContract]
    public class JobsMappingDataService : DatabaseResource, IJobsMappingDataService
    {
        protected const string DefaultDatabaseName = "Guide";

        protected override string GetDatabaseName()
        {
            return DefaultDatabaseName;
        }

        public JobsMappingDataService() 
        {
            ConnectionStringProvider = new ConfigurationBasedDatabaseConnectionStringProvider();
            DatabaseConnectionFactory = new AdoNetDatabaseConnectionFactory();
        }

        [OperationContract]
        public ResultSet<JobsMapping> GetJobsQuery(JobsMappingQuery item)
        {
            var jobsMapper = new DbReaderMapper<JobsMapping>();

            var parameters = new
            {
                Date = item.Date,
                NumberOfActiveJobs = item.NumberOfActiveJobs,
                NumberOfJobViews = item.NumberOfJobViews,
                NumberOfPredictedJobViews = item.NumberOfPredictedJobViews
            };

            ReadStoredProcedure("GetJobsMapping", parameters, jobsMapper);
            
            var results =
                new ResultSet<JobsMapping>
                {
                    Items = jobsMapper.Results.ToArray()
                };
            

            return results;
        }
    }
}

