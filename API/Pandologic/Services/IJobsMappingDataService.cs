using Pandologic;
using System.ServiceModel;

namespace Services
{
    [ServiceContract]
    public interface IJobsMappingDataService
    {
        [OperationContract]
        public ResultSet<JobsMapping> GetJobsQuery(JobsMappingQuery item);
    }
}
