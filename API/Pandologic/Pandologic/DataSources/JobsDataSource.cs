using Microsoft.Extensions.Configuration;
using Pandologic.Models;
using Services;
using System.Data;
using System.Linq;

namespace Pandologic.DataSources
{
    public class JobsDataSource : IDataSource
    {
        public ResultSet<JobsViewModel> Query<IDataModel>()
        {
            var jobsDataService = new JobsMappingDataService();

            var items = QueryItems(jobsDataService);

            return items;
        }

        public ResultSet<JobsViewModel> QueryItems(IJobsMappingDataService dataService)
        {
            var searchQuery = new JobsMappingQuery()
            {
                Date = null,
                NumberOfActiveJobs = null,
                NumberOfJobViews = null,
                NumberOfPredictedJobViews = null
            };


            var result = dataService.GetJobsQuery(searchQuery);
            var resultsResultSet =
                new ResultSet<JobsMapping>
                {
                    Items = result.Items,
                };

            var results = MapToResultSet(resultsResultSet);

            return results;
        }

        protected ResultSet<JobsViewModel> MapToResultSet(ResultSet<JobsMapping> contents)
        {
            var result = new ResultSet<JobsViewModel>()
            {
                Items = new JobsViewModel[] { }
            };

            if (contents != null && contents.Items != null)
            {
                result.Items = contents.Items.Select(content => MapToViewModel(content)).ToArray();
            }

            return result;
        }

        private JobsViewModel MapToViewModel(JobsMapping content)
        {
            var resultModel = new JobsViewModel();

            resultModel.Load(content);

            return resultModel;
        }
    }
}

