using Pandologic;
using System.ServiceModel;

namespace Services
{
    [ServiceContract]
    public interface IJobsMappingService
    {
        [OperationContract]
        public ResultSet<JobsMapping> GetJobsQuery(JobsMappingQuery item);
    }
}
