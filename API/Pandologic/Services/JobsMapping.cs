using System;
using System.Runtime.Serialization;

namespace Services
{
    [DataContract]
    public class JobsMapping
    {
        public DateTime Date { get; set; }
        public int NumberOfActiveJobs { get; set; }
        public int NumberOfJobViews { get; set; }
        public int NumberOfPredictedJobViews { get; set; }
    }
}
