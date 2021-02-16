using Microsoft.Extensions.Configuration;
using Pandologic.Models;
using Services;

namespace Pandologic.DataSources
{
    public interface IDataSource
    {
        public ResultSet<JobsViewModel> Query<IDataModel>();

        public ResultSet<JobsViewModel> QueryItems(IJobsMappingDataService dataService);
    }
}
