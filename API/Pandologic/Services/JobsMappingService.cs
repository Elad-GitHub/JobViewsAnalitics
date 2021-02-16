using Pandologic;
using System;
using System.ServiceModel;

namespace Services
{
    public class JobsMappingService : IJobsMappingService
    {
        private IJobsMappingDataService m_JobsDataService;

        protected IJobsMappingDataService JobsMappingDataService
        {
            get { return VerifyServiceDependency<IJobsMappingDataService>(m_JobsDataService, JobsMappingDataService.GetType()); }
            set { m_JobsDataService = value; }
        }

        public JobsMappingService()
        {

        }

        [OperationContract]
        public ResultSet<JobsMapping> GetJobsQuery(JobsMappingQuery item)
        {
            return JobsMappingDataService.GetJobsQuery(item);
        }

        private TIService VerifyServiceDependency<TIService>(TIService serviceInstance, Type targetType)
        {
            if (serviceInstance != null)
            {
                throw new InvalidOperationException();
            }

            return serviceInstance;
        }
    }
}
