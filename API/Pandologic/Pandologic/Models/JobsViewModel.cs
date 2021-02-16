using Services;
using System;

namespace Pandologic.Models
{
    public class JobsViewModel
    {
        public DateTime Date { get; set; }
        public int NumberOfActiveJobs { get; set; }
        public int NumberOfJobViews { get; set; }
        public int NumberOfPredictedJobViews { get; set; }

        public virtual void Load(JobsMapping content)
        {
            Guard.ArgumentNotNull(content, "content");

            Date = content.Date;
            NumberOfActiveJobs = content.NumberOfActiveJobs;
            NumberOfJobViews = content.NumberOfJobViews;
            NumberOfPredictedJobViews = content.NumberOfPredictedJobViews;
        }
    }
}
